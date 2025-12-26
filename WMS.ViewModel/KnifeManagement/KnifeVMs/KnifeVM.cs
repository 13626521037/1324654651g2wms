using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.KnifeManagement;
using WMS.Model;
using Elsa;
using NPOI.SS.Formula.Functions;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs;
using WMS.ViewModel.KnifeManagement.KnifeCheckInVMs;
using Microsoft.EntityFrameworkCore.Storage;
using DotLiquid.Util;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.Util.U9Para.Knife;
using WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs;
using WMS.ViewModel.KnifeManagement.KnifeTransferInVMs;
using System.Linq.Expressions;
using System.Net.Quic;
using Elsa.Models;
using NPOI.POIFS.FileSystem;
using NetTopologySuite.Index.HPRtree;
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using static NPOI.HSSF.Util.HSSFColor;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeVM : BaseCRUDVM<Knife>
    {

        public List<string> KnifeManagementKnifeFTempSelected { get; set; }

        public List<ComboSelectListItem> PrintModules { get; set; }

        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }



        public KnifeVM()
        {
            SetInclude(x => x.CurrentCheckOutBy);
            SetInclude(x => x.HandledBy);
            SetInclude(x => x.WhLocation);
            SetInclude(x => x.ItemMaster);
        }

        protected override void InitVM()
        {

        }

        public override DuplicatedInfo<Knife> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.SerialNumber));
            return rv;

        }

        public void DoAdd(string operationU9SourceLineID, string miscShipmentDocNo)
        {
            try
            {
                DoAdd();
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//仓管员
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                var currentTime = DateTime.Now;
                //  新增刀具时 操作记录-建档
                DC.Set<KnifeOperation>().Add(new KnifeOperation
                {
                    Knife = Entity,
                    KnifeId = Entity.ID,
                    DocNo = miscShipmentDocNo,
                    OperationType = KnifeOperationTypeEnum.Created,
                    OperationTime = currentTime,
                    OperationById = handledBy_operator.ID,
                    HandledBy = handledBy,
                    HandledById = handledBy.ID.ToString(),
                    HandledByName = handledBy.Name,
                    TotalUsedDays = 0,
                    UsedDays = 0,//Entity.TotalUsedDays,
                    RemainingDays = Entity.RemainingUsedDays,
                    CurrentLife = Entity.CurrentLife,
                    WhLocation = Entity.WhLocation,
                    WhLocationId = Entity.WhLocationId,
                    GrindNum = 0,
                    U9SourceLineID = operationU9SourceLineID,

                    CreateTime = currentTime,
                    CreateBy = LoginUserInfo.Name,
                });
                DC.SaveChanges();
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", $"新建刀具时 刀具操作记录建档失败: {ex.Message}");
                return;
            }

        }
        public override void DoAdd()
        {
            base.DoAdd(); // 执行原有的刀具新增操作
        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {

            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }

        /// <summary>
        /// 组合替换初始化/校验
        /// </summary>
        public void KnifeReplaceInitByInfo(KnifeReplaceInputInfo info, KnifeCheckOutVM vm_checkOut, KnifeCheckInVM vm_checkIn, IDbContextTransaction tran, ReturnResult<List<BlueToothPrintDataLineReturn>> rr)
        {
            try
            {
                //校验info
                //1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //2.info非空检验
                if (string.IsNullOrEmpty(info.KnifeReplaceByID) || string.IsNullOrEmpty(info.CombineKnifeNo)
                    || info.Lines is null || info.Lines.Count == 0
                    )
                {
                    MSD.AddModelError("", "输入参数不足 请检查");
                    return;
                }
                //3.info有效性校验
                //3.1组合刀号
                var knifeCombine = DC.Set<KnifeCombine>()
                    .Include(x => x.KnifeCombineLine_KnifeCombine)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.WhLocation)
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved && x.CombineKnifeNo == info.CombineKnifeNo)
                    .FirstOrDefault();
                if (knifeCombine is null)
                {
                    MSD.AddModelError("", "未检测到有效的组合刀号 检查是否已关闭");
                    return;
                }
                //如果是领用状态下的刀  替换人和领用人要相同  任意一把刀就可以了
                if (knifeCombine.KnifeCombineLine_KnifeCombine[0].Knife.Status==KnifeStatusEnum.CheckOut 
                    &&knifeCombine.KnifeCombineLine_KnifeCombine[0].Knife.CurrentCheckOutById !=Guid.Parse(info.KnifeReplaceByID))
                {
                    MSD.AddModelError("", "替换人不是领用人 无法替换");
                    return;
                }
                //替换人和组合单的领用人保持一致
                if (knifeCombine.CheckOutById!= Guid.Parse(info.KnifeReplaceByID))
                {
                    MSD.AddModelError("", "替换人不是组合单的领用人 无法替换");
                    return;
                }
                //3.2 替换人有效性校验 
                var knifeReplaceBy = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .FirstOrDefault(x => x.ID.ToString() == info.KnifeReplaceByID);//替换人
                if (knifeReplaceBy is null)
                {
                    MSD.AddModelError("", "无效的替换人");
                    return;
                }
                //3.3 组合刀具替换行  行信息有效性校验:老刀 新刀+库位 其实能获取信息就说明可以了..不对 状态还是要检验的 获取信息和点击的时间差久一点就会出问题
                foreach(var line in info.Lines)
                {
                    var oldknife = DC.Set<Knife>()
                        .Include(x=>x.ItemMaster)
                        .Include(x=>x.WhLocation).ThenInclude(x=>x.WhArea).ThenInclude(x=>x.WareHouse)
                        .Where(x => x.ID == Guid.Parse(line.OldKnifeId))
                        .FirstOrDefault();//被替换的老刀
                    
                    if (oldknife.WhLocation.WhArea.WareHouse.ID != whid)
                    {
                        MSD.AddModelError("", $"刀具{oldknife.SerialNumber}的不属于当前存储地点 不可用");
                        return;
                    }
                    if (oldknife.WhLocation.Locked == true)
                    {
                            MSD.AddModelError("", $"刀具{oldknife.SerialNumber}的库位{oldknife.WhLocation.Code}已锁定 不可用");
                            return;
                    }
                    if (oldknife.WhLocation.IsEffective == EffectiveEnum.Ineffective || oldknife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"刀具{oldknife.SerialNumber}所在库位{oldknife.WhLocation.Code}无效");
                        return ;
                    }
                    if (oldknife.WhLocation.Code.Contains("A9999999"))//不良退回库位
                    {
                        MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                        return ;
                    }
                    if (oldknife.Status != KnifeStatusEnum.InStock&& oldknife.Status != KnifeStatusEnum.CheckOut)//
                    {
                        MSD.AddModelError("", $"组合替换的老刀只能是在库/领用");
                        return ;
                    }
                    if (line.NewKnifeInfo.IsNew)//新刀 料品信息在库存信息上
                    {
                        if (string.IsNullOrEmpty(line.NewKnifeInfo.bar))
                        {
                            MSD.AddModelError("", $"用于替换的新刀输入参数不足 不可用");
                            return;
                        }
                        BaseInventory inventory = DC.Set<BaseInventory>()
                            .Include(x => x.ItemMaster)
                            .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                            .Where(x => x.IsAbandoned==false&&x.Qty>0)//未失效 数量大于0
                            .Where(x => x.SerialNumber == line.NewKnifeInfo.SerialNumber)
                            .FirstOrDefault();
                        if (inventory == null)
                        {
                            MSD.AddModelError("", $"库存信息中未找到此条码{line.NewKnifeInfo.SerialNumber} 请检查");
                            return;
                        }
                        if (inventory.FrozenStatus != FrozenStatusEnum.Normal)
                        {
                            MSD.AddModelError("", $"{inventory.SerialNumber}此条码已被冻结 不可用");
                            return;
                        }
                        //寿命表  是否进台账  不通过主分类 通过刀具寿命表 但是寿命表一会还有用 所以在外面查询 外面校验是否进台账
                        /*if (!inventory.ItemMaster.ItemCategory.Code.StartsWith("17"))
                        {
                            MSD.AddModelError("", $"{inventory.SerialNumber}此条码的物料并非刀具 不可用");
                            return;
                        }*/
                        
                        if (inventory.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", "此条码所在存储地点与当前登录存储地点不同 不可用");
                            return;
                        }
                        if (inventory.WhLocation.IsEffective == EffectiveEnum.Ineffective || inventory.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库存{inventory.SerialNumber}所在库位{inventory.WhLocation.Code}无效");
                            return ;
                        }
                        if (inventory.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"条码{inventory.SerialNumber}的库位{inventory.WhLocation.Code}已锁定 不可用");
                            return;
                        }
                        if (inventory.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                            return;
                        }

                        /* if (oldknife.ItemMaster.Code != newknife.ItemMaster.Code)
                         {
                             MSD.AddModelError("", $"替换{oldknife.KnifeNo}刀具的新刀料号不一致 请检查");
                             return;
                         }*/
                    }
                    else if (!line.NewKnifeInfo.IsNew)//已经在库存里了  料品信息在刀具里
                    {
                        Knife newknife = DC.Set<Knife>().Include(x => x.ItemMaster).Include(x => x.WhLocation).FirstOrDefault(x => x.ID.ToString() == line.NewKnifeInfo.KnifeId);
                        if(newknife is null)
                        {
                            MSD.AddModelError("", $"替换{newknife.SerialNumber}刀具的刀具不存在 请检查");
                            return;
                        }
                        
                        if (newknife.WhLocation.WhArea.WareHouse.ID != whid)
                        {
                            MSD.AddModelError("", $"刀具{newknife.SerialNumber}的不属于当前存储地点 不可用");
                            return;
                        }
                        if (newknife.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"刀具{newknife.SerialNumber}的库位{newknife.WhLocation.Code}已锁定 不可用");
                            return;
                        }
                        if (newknife.WhLocation.IsEffective == EffectiveEnum.Ineffective || newknife.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具{newknife.SerialNumber}所在库位{newknife.WhLocation.Code}无效");
                            return;
                        }
                        if (newknife.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                            return;
                        }
                        if (newknife.Status != KnifeStatusEnum.InStock)
                        {
                            MSD.AddModelError("", $"组合替换里新刀台账刀状态只能是在库");
                            return;
                        }
                    }
                    var whlocation = DC.Set<BaseWhLocation>()
                        .FirstOrDefault(x => x.ID.ToString() == line.WhLocationId&&x.WhArea.WareHouseId==whid);//存储地点的库位
                    if (whlocation is null)
                    {
                        MSD.AddModelError("", $"替换{oldknife.SerialNumber}刀具的归还库位不存在 请检查");
                        return;
                    }
                    if (whlocation.Locked == true)
                    {
                        MSD.AddModelError("", $"库位{whlocation.Code}已锁定 不可用");
                        return;
                    }
                    if (whlocation.IsEffective == EffectiveEnum.Ineffective || whlocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"归还库位{whlocation.Code}无效");
                        return;
                    }
                    if (whlocation.WhArea.WareHouseId != whid)
                    {
                        MSD.AddModelError("", $"此归还库位{whlocation.Code}所在存储地点与当前登录存储地点不同 不可用");
                        return;
                    }

                }
                //3.4 组合刀具有效性校验 准备数据   
                var currentTime = DateTime.Now;//当前时间
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//仓管员
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm

                var combineKnifeIds = knifeCombine.KnifeCombineLine_KnifeCombine.Select(line => (Guid)line.KnifeId).ToList();//组合单的所有行的刀id
                var combineKnifes = DC.Set<Knife>().Where(x => combineKnifeIds.Contains(x.ID)).ToList();//组合单的所有行的刀
                var oldKnifeIds = info.Lines.Select(x => Guid.Parse(x.OldKnifeId)).ToList();//被替换的老刀id
                var oldKnifes = DC.Set<Knife>()
                    .Where(x => oldKnifeIds.Contains(x.ID)).ToList();//被替换的老刀
                var unChangeOldKnifeIds = combineKnifeIds.Where(x => !oldKnifeIds.Contains(x)).ToList();//不变的老刀
                var unChangeOldKnifes = DC.Set<Knife>()
                    .Include(x=>x.WhLocation)
                    .Where(x => unChangeOldKnifeIds.Contains(x.ID))
                    .ToList();//不变的老刀

                var newKnifeIds_instock = info.Lines.Where(x=>x.NewKnifeInfo.IsNew==false).Select(x => x.NewKnifeInfo.KnifeId).ToList();//用于替换的 台账内的刀id
                var newKnifes_instock = DC.Set<Knife>().Where(x => newKnifeIds_instock.Contains(x.ID.ToString().ToUpper())).ToList();//用于替换的 台账内的刀 
                var newKnifeIds_serial = info.Lines.Where(x => x.NewKnifeInfo.IsNew == true).Select(x => x.NewKnifeInfo.SerialNumber).ToList();//用于替换的 台账外的刀系列号
                var newKnifes_Inventory = DC.Set<BaseInventory>()
                    .Include(x=>x.ItemMaster).ThenInclude(x=>x.ItemCategory)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Where(x => newKnifeIds_serial.Contains(x.SerialNumber)).ToList();//用于替换的 台账外的库存记录 
                var newKnifes_Inventory_MiscShip = new List<Knife>();//库存信息杂发完之后生成的刀 可能为空

                if (oldKnifeIds.Any(x => !combineKnifeIds.Contains(x)))
                {
                    MSD.AddModelError("", "被替换的刀列表存在刀具不属于组合刀");
                    return;
                }
                if (oldKnifes.Select(x => x.CurrentCheckOutById).Distinct().ToList().Count != 1)
                {
                    MSD.AddModelError("", "被替换的刀列表的原领用人不止一个 请检查");
                    return;
                }
                /*if (oldKnifes.Any(x => x.Status != KnifeStatusEnum.CheckOut))
                {
                    MSD.AddModelError("", "被替换的刀列表存在刀具状态不为领用 请检查");
                    return;
                }*/
                if (newKnifes_instock.Any(x => x.Status != KnifeStatusEnum.InStock))
                {
                    MSD.AddModelError("", "替换的刀列表存在刀具状态不为在库 请检查");
                    return;
                }
                if (newKnifes_Inventory.Any(x=>x.Qty != 1)  )
                {
                    MSD.AddModelError("", $"替换的刀列表的存在条码数量不为1 无法替换");
                    return;
                }
                //3.5 替换新刀的料号寿命表进台账的校验 低价值不能替换
                var newKnifes_Inventory_ItemCodes = newKnifes_Inventory.Select(x => x.ItemMaster.Code).ToList();
                //U9访问寿命接口  判断是否进台账
                //var newKnifes_Inventory_ItemLives_U9return = GetKnifesLivesSV.GetKnifesLives_1(newKnifes_Inventory_ItemCodes);
                var newKnifes_Inventory_ItemLives_U9return = Wtm.CreateVM<KnifeVM>().GetKnifesLives_2(newKnifes_Inventory_ItemCodes);
                if(newKnifes_Inventory_ItemCodes.Count!=0)//&& newKnifes_Inventory_ItemLives_U9return.Data.Count!= newKnifes_Inventory_ItemCodes.Count
                {
                    if (newKnifes_Inventory_ItemLives_U9return.Success != true)
                    {
                        MSD.AddModelError("", newKnifes_Inventory_ItemLives_U9return.Message);
                        return;
                    }
                    else if (newKnifes_Inventory_ItemLives_U9return.Success == true)
                    {
                        foreach (var temp in newKnifes_Inventory_ItemLives_U9return.Data)
                        {
                            if (temp.IsActive == false)
                            {
                                MSD.AddModelError("", $"{temp.ItemMaster}在U9刀具寿命管理里未生效 无法用于台账的刀具替换");
                                return;
                            }
                            if (temp.LedgerIncluded == false)
                            {
                                MSD.AddModelError("", $"{temp.ItemMaster}在U9刀具寿命管理里不进台账 无法用于台账的刀具替换");
                                return;
                            }
                        }
                    }  
                }
                //此序列号未在刀具里使用过 唯一校验
                if (newKnifes_Inventory != null && newKnifes_Inventory.Count != 0)
                {
                    var sameKnife = DC.Set<Knife>()
                        .Where(x => newKnifeIds_serial.Contains(x.SerialNumber))
                        .ToList();
                    if (sameKnife.Count != 0)
                    {
                        MSD.AddModelError("", $"{sameKnife[0].SerialNumber}此序列号已使用 不可再次使用");
                        return;
                    }
                }


                //校验通过 进行修改
                //关闭原有的组合单

                var ledgerKnifeCombineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine)
                        .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)//组合单是已核准状态
                        .Where(x => combineKnifeIds.Contains((Guid)x.KnifeId))//刀id和组合行的刀id匹配 
                        .ToList();
                var ledgerKnifeCombines = ledgerKnifeCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                if (ledgerKnifeCombines.Count == 0)
                {
                    MSD.AddModelError("", "未找到有效的组合单" );
                    return;

                }
                foreach (var ledgerKnifeCombine in ledgerKnifeCombines)
                {
                    ledgerKnifeCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                    ledgerKnifeCombine.CloseTime = currentTime;
                }
                //生成新的组合单=原来不换的刀+新的台账刀+新的库存杂发后的刀
                //打印数据只放组合单的刀号 详细什么刀是新的在杂发那里判定 组合判定不了的
                //----得先杂发 杂发后生成刀具 刀具操作记录 然后有了刀举id才能生成新的组合单   组合单放后面
                
                //库存信息不为0  准备杂发-杂发行id-生成刀-找采购行id-生成建档信息-放到刀里去
                if(newKnifes_Inventory!=null&& newKnifes_Inventory.Count != 0)
                {
                    //U9生成杂发单
                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//新刀领用固定组织
                    U9Return<MiscShipmentData> u9Return = (U9Return<MiscShipmentData>)apiHelper.CreateAndApprovedMiscShipment(handledBy_operator, newKnifes_Inventory, knifeReplaceBy,false);//高价值
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", "U9创建并审核杂发单失败:" + u9Return.Msg);
                        return;
                    }
                    //将刀具实际新增 对应的刀具操作记录
                    foreach (var line in newKnifes_Inventory)
                    {
                        //获取U9杂发行id
                        string U9MiscShipLineId = u9Return.Entity.Lines.Where(x => x.KnifeNo == line.SerialNumber).Select(x => x.ID).FirstOrDefault();
                        //获取关联的采购收货行id 需要条码是由采购收货行或者库存拆分操作生成 其他的比如说调入 杂收 盘盈等重新打的条码 连不上的就是null 没有重新打的不影响
                        var knifeCheckOutVM = Wtm.CreateVM<KnifeCheckOutVM>();
                        string U9RcvLineId = knifeCheckOutVM.GetRcvLineIdBySerialNumber(line.SerialNumber);
                        if (!MSD.IsValid)//||string.IsNullOrEmpty (U9RcvLineId)
                        {
                            MSD.AddModelError("", "新刀获取原采购行失败:" + MSD.GetFirstError());
                            return;
                        }
                        //获取寿命信息
                        var inledgerItemLive = newKnifes_Inventory_ItemLives_U9return.Data.Where(x => x.ItemMaster == line.ItemMaster.Code).FirstOrDefault();
                        //新建刀具
                        var knifeVM = Wtm.CreateVM<KnifeVM>();
                        knifeVM.Entity = new Knife()
                        {
                            CreatedDate = currentTime,
                            SerialNumber = line.SerialNumber,
                            BarCode = $"{(int)line.ItemSourceType}@{line.ItemMaster.Code}@{line.Qty.TrimZero()}@{line.SerialNumber}",
                            Status = KnifeStatusEnum.InStock,
                            HandledBy = handledBy,
                            HandledById = handledBy.ID.ToString(),
                            HandledByName = handledBy.Name,
                            LastOperationDate = currentTime,
                            WhLocation = line.WhLocation,
                            WhLocationId = line.WhLocation.ID,
                            GrindCount = 0,
                            InitialLife = inledgerItemLive.CurrentLife,//初始寿命 
                            CurrentLife = inledgerItemLive.CurrentLife,
                            TotalUsedDays = 0,
                            RemainingUsedDays = inledgerItemLive.CurrentLife,
                            ItemMaster = line.ItemMaster,
                            ItemMasterId = line.ItemMaster.ID,
                            MiscShipLineID = U9MiscShipLineId,//杂发行id
                            InStockStatus = KnifeInStockStatusEnum.InStock,
                            ActualItemCode = line.ItemMaster.Code,

                        };
                        knifeVM.DoAdd(U9RcvLineId, u9Return.Entity.DocNo);//采购行id(可能为空)杂发单单号 在刀具操作记录里
                        newKnifes_Inventory_MiscShip.Add(knifeVM.Entity);//刀具信息记下来 待会组合用
                        //打印信息 记录杂发  说明是新刀
                        rr.Entity.Add(new BlueToothPrintDataLineReturn
                        {
                            IsKnife = true,
                            IsNew = true,
                            SerialNumber = line.SerialNumber,
                            bar = $"{(int)line.ItemSourceType}@{line.ItemMaster.Code}@{line.Qty.TrimZero()}@{line.SerialNumber}",
                            ItemCode = line.ItemMaster.Code,
                            ItemName = line.ItemMaster.Name,
                            Specs = line.ItemMaster.SPECS,
                            Qty = "1",
                            CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                            NotG = "",
                        });
                        //库存信息变化 库存流水
                        this.CreateInvLog(OperationTypeEnum.InventoryOtherShipCreate, u9Return.Entity.DocNo, line.ID, null, -line.Qty, null);//流水记录  类型其他出库  出完
                        line.Qty = 0;

                    }
                    
                }

                //如果原来的刀具是在库状态 都是台账刀就有组合单            有库存就是组合单+     杂发(没有领用单 只是建档)
                //如果原来的刀具是领用状态 都是台账刀就是组合单+归还+领用  有库存就是组合单+归还+杂发+领用 

                //不一定会有归还和领用 在库的还是在库结束 领用的应该有归还 有领用
                if (oldKnifes[0].Status == KnifeStatusEnum.CheckOut)
                {
                    //归还部分
                    var KnifeCheckInDocNo = DocNoVm.GetSequence("KnifeCheckInRule", tran);//生成归还单单号
                    var KnifeCheckOutDocNo = DocNoVm.GetSequence("KnifeCheckOutRule", tran);//生成领用单单号
                    vm_checkIn.Entity.DocType = KnifeCheckInTypeEnum.NormalCheckIn;
                    vm_checkIn.Entity.DocNo = KnifeCheckInDocNo;
                    vm_checkIn.Entity.CheckInById = oldKnifes[0].CurrentCheckOutById;//任意老刀的当前领用人就是归还人
                    vm_checkIn.Entity.HandledById = handledBy.ID.ToString();
                    vm_checkIn.Entity.HandledBy = handledBy;
                    vm_checkIn.Entity.Status = KnifeOrderStatusEnum.Open;
                    vm_checkIn.Entity.CombineKnifeNo = info.CombineKnifeNo;
                    vm_checkIn.Entity.WareHouseId = whid;
                    vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn = new List<KnifeCheckInLine>();
                    foreach (var line in info.Lines)//归还库位 只能取line上的信息了
                    {
                        var knife = combineKnifes.Where(x => x.ID == Guid.Parse(line.OldKnifeId)).FirstOrDefault();
                        vm_checkIn.Entity.KnifeCheckInLine_KnifeCheckIn.Add(new KnifeCheckInLine
                        {
                            KnifeId = Guid.Parse(line.OldKnifeId),
                            ToWhLocationId = Guid.Parse(line.WhLocationId),
                            FromWhLocationId = knife.WhLocationId,
                        });
                    }

                    //领用部分 新刀  台账刀或者库存杂发完之后的刀   
                    vm_checkOut.Entity.DocType = KnifeCheckOutTypeEnum.NormalCheckOut;
                    vm_checkOut.Entity.DocNo = KnifeCheckOutDocNo;
                    vm_checkOut.Entity.CheckOutById = knifeReplaceBy.ID;//领用人就是替换人
                    vm_checkOut.Entity.HandledById = handledBy.ID.ToString();
                    vm_checkOut.Entity.HandledBy = handledBy;
                    vm_checkOut.Entity.Status = KnifeOrderStatusEnum.Open;
                    vm_checkOut.Entity.WareHouseId = whid;
                    vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut = new List<KnifeCheckOutLine>();
                    foreach (var newKnife_instock in newKnifes_instock)
                    {
                        vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut.Add(new KnifeCheckOutLine
                        {
                            KnifeId = newKnife_instock.ID,
                            FromWhLocationId = newKnife_instock.WhLocationId,

                        });
                    }
                    foreach (var knife in newKnifes_Inventory_MiscShip)
                    {
                        vm_checkOut.Entity.KnifeCheckOutLine_KnifeCheckOut.Add(new KnifeCheckOutLine
                        {
                            KnifeId = knife.ID,
                            FromWhLocationId = knife.WhLocationId,
                            IsNewLine = "1",
                        });
                    }

                }


               

                #region 新的组合单的生成 不换的老刀+新刀(台账+库存杂发完之后的刀)
                //生成组合刀号  台账刀+库存刀
                var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                var combineKnifeNo = baseSequenceDefineVM.SetProperty("ItemCategory", "ZHDH").GetSequence("KnifeNoRule", tran);//组合刀号就前缀不同 组合刀号的首拼音
                var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                knifeCombineVM.Entity = new KnifeCombine()
                {
                    DocNo = baseSequenceDefineVM.GetSequence("KnifeCombineDocNoRule", tran),//组合单号
                    HandledBy = handledBy,
                    HandledById = handledBy.ID.ToString(),
                    Status = KnifeOrderStatusEnum.Approved,//直接已审核
                    ApprovedTime = currentTime,//currentTime.ToString("yyyyy-MM-dd HH:mm:ss"),
                    CombineKnifeNo = combineKnifeNo,
                    CheckOutBy = knifeReplaceBy,
                    CheckOutById = knifeReplaceBy.ID,
                    WareHouseId = whid,
                    KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                };
                foreach (var k in unChangeOldKnifes)
                {
                    knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine()
                    {
                        KnifeId = k.ID,
                        FromWhLocationId = k.WhLocationId,
                    });
                }
                foreach (var k in newKnifes_instock)
                {
                    knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine()
                    {
                        KnifeId = k.ID,
                        FromWhLocationId = k.WhLocationId,
                    });
                }
                foreach (var k in newKnifes_Inventory_MiscShip)
                {
                    knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine()
                    {
                        KnifeId = k.ID,
                        FromWhLocationId = k.WhLocationId,
                    });
                }
                knifeCombineVM.DoAdd();//直接生成已审核的组合单 一张 刀号 ZHDH
                //生成组合单之后要打印
                rr.Entity.Add(new BlueToothPrintDataLineReturn
                {
                    IsKnife = false,
                    IsNew = true,
                    SerialNumber = "",
                    bar = $"{knifeCombineVM.Entity.CombineKnifeNo}",
                    ItemCode ="",
                    ItemName ="",
                    Specs =  "",
                    Qty = $"{knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Count}",
                    CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                    NotG = "not",
                });


                #endregion

                DC.SaveChanges();
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "初始化错误:" + e.Message);
                return;
            }
        }
        /// <summary>
        /// 刀具移库初始化/校验
        /// </summary>
        public void KnifeMoveInitByInfo(KnifeMoveInputInfo info, KnifeTransferOutVM vm_transferOut, KnifeTransferInVM vm_transferIn, IDbContextTransaction tran)
        {
            try
            {
                //1.仓管员当前登录状态
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

                //校验info
                //2.info非空检验
                if (info.Lines is null || info.Lines.Count == 0)
                {
                    MSD.AddModelError("", "输入参数不足 请检查");
                    return;
                }
                //3.info有效性校验
                //3.1 库位  移库是同存储地点移动 所以库位也是系统存储地点的
                var whLocationIds = info.Lines.Select(x => x.WhLocationId).ToList();
                var whLocations = DC.Set<BaseWhLocation>()
                    .Include(x => x.WhArea)
                        .ThenInclude(x => x.WareHouse)
                    .Where(x => whLocationIds.Contains(x.ID.ToString()))
                    .ToList();
                if (whLocations is null )
                {
                    MSD.AddModelError("", "未检测到有效库位");
                    return;
                }
                foreach (var whLocation in whLocations)
                {
                    if (whLocation is null || whLocation.IsEffective != EffectiveEnum.Effective || whLocation.WhArea.WareHouseId != whid)
                    {
                        MSD.AddModelError("", "检测到无效库位 不属于当前登录存储地点或者已失效");
                        return;
                    }
                    if (whLocation.Locked == true)
                    {
                        MSD.AddModelError("", $"库位{whLocation.Code}已锁定 不可用");
                        return;
                    }
                    if (whLocation.IsEffective == EffectiveEnum.Ineffective || whLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"目标库位{whLocation.Code}无效");
                        return ;
                    }
                }
                
                //3.2 刀具 库位都是当前的存储地点  状态都是在库
                var knifeIds = info.Lines.Select(k => k.KnifeId.ToUpper()).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x=>x.WhLocation)
                    .Where(k => k.WhLocation.WhArea.WareHouseId == whid && k.Status == KnifeStatusEnum.InStock && knifeIds.Contains(k.ID.ToString().ToUpper()))
                    .ToList();
                if(knifes is null || knifes.Count != info.Lines.Count)
                {
                    MSD.AddModelError("", "检测到无效刀具 不属于当前登录存储地点或者不在库 请检查");
                    return;
                }
                //遍历刀具  刀具移库不能移动到自己当前所在的库位     刀具所在库位不可被盘点锁定
                foreach(var line in info.Lines)
                {
                    var tempKnife = knifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                    var tempWhLocation = whLocations.FirstOrDefault(x => x.ID == Guid.Parse(line.WhLocationId));
                    if (tempKnife.WhLocationId == tempWhLocation.ID)
                    {
                        MSD.AddModelError("", $"{tempKnife.SerialNumber}不可以移动到自己当前所在的库位");
                        return;
                    }
                    if (tempKnife.WhLocation.Locked == true)
                    {
                        MSD.AddModelError("", $"{tempKnife.SerialNumber}所在的库位{tempKnife.WhLocation.Code}已锁定 不可用");
                        return;
                    }
                    if (tempKnife.WhLocation.IsEffective == EffectiveEnum.Ineffective || tempKnife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"刀具{tempKnife.SerialNumber}所在库位{tempKnife.WhLocation.Code}无效");
                        return ;
                    }
                }


                //校验通过 进行修改 给两个vm赋值
                //先调出
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//仓管员
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                var currentTime = DateTime.Now;//当前时间
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeTransferOutDocNo = DocNoVm.GetSequence("KnifeTransferOutRule", tran);//生成调出单单号
                var KnifeTransferInDocNo = DocNoVm.GetSequence("KnifeTransferInRule", tran);//生成调入单单号
                vm_transferOut.Entity.DocNo = KnifeTransferOutDocNo;
                vm_transferOut.Entity.Status = KnifeOrderStatusEnum.Open;
                vm_transferOut.Entity.HandledById = handledBy.ID.ToString();
                vm_transferOut.Entity.ToWhId =whid;
                vm_transferOut.Entity.FromWHId =whid;
                vm_transferOut.Entity.WareHouseId = whid;
                vm_transferOut.Entity.KnifeTransferOutLine_KnifeTransferOut = new List<KnifeTransferOutLine>();
                foreach (var knife in knifes)
                {
                    vm_transferOut.Entity.KnifeTransferOutLine_KnifeTransferOut.Add(new KnifeTransferOutLine
                    {
                        KnifeId =knife.ID,
                        Status=KnifeOrderStatusEnum.Open,
                        FromWhLocationId = knife.WhLocationId,
                    });
                }
                //再调入
                vm_transferIn.Entity.DocNo = KnifeTransferInDocNo;
                vm_transferIn.Entity.Status = KnifeOrderStatusEnum.Open;
                vm_transferIn.Entity.HandledById = handledBy.ID.ToString();
                vm_transferIn.Entity.TransferOutDocNo = KnifeTransferOutDocNo;
                vm_transferIn.Entity.FromWHId = whid;
                vm_transferIn.Entity.ToWHId = whid;
                vm_transferIn.Entity.WareHouseId = whid;
                vm_transferIn.Entity.KnifeTransferInLine_KnifeTransferIn = new List<KnifeTransferInLine>();
                foreach (var line in info.Lines)
                {
                    var knife = knifes.FirstOrDefault(x => x.ID== Guid.Parse(line.KnifeId));
                    vm_transferIn.Entity.KnifeTransferInLine_KnifeTransferIn.Add(new KnifeTransferInLine
                    {
                        KnifeId = Guid.Parse(line.KnifeId),
                        FromWhLocationId = knife.WhLocationId,
                        ToWhLocationId= Guid.Parse(line.WhLocationId),
                    });
                }
                #region 关闭涉及的组合单
                var toCloseCombineLines = DC.Set<KnifeCombineLine>()
                    .Include(x => x.KnifeCombine)
                    .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)//组合单是已核准状态
                    .Where(x => knifes.Contains(x.Knife))//刀id和组合行的刀id匹配 
                    .ToList();
                if (toCloseCombineLines != null) //涉及的组合单不一定有 有的话关闭 
                {
                    var toCloseCombines = toCloseCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                    foreach (var toCloseCombine in toCloseCombines)
                    {
                        toCloseCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                        toCloseCombine.CloseTime = DateTime.Now;
                    }
                }
                #endregion
                DC.SaveChanges();
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:" + e.Message);
                return;
            }
        }


        /// <summary>
        /// 初始化打印模板选项
        /// </summary>
        public void InitPrintModules()
        {
            // 获取打印模板（从打印服务器）
            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            string printBusinessName = Wtm.ConfigInfo.AppSettings["KnifeBarCodePrintBusinessName"];
            PrintApiHelper apiHelper = new PrintApiHelper(printServer);
            ReturnResult<List<GetPrintModuleResult>> rr = apiHelper.GetPrintModule(printBusinessName);
            if (rr.Success)
            {
                if (rr.Entity == null)
                {
                    MSD.AddModelError("", "未找到打印模板");
                }
                else
                {
                    PrintModules = new List<ComboSelectListItem>();
                    foreach (var item in rr.Entity)
                    {
                        PrintModules.Add(new ComboSelectListItem { Text = item.ModuleName, Value = item.ID });
                    }
                    if (PrintModules.Count > 0)
                    {
                        SelectedPrintModule = PrintModules[0].Value;
                    }
                }
            }
            else
            {
                MSD.AddModelError("", rr.Msg);
            }
        }

        /// <summary>
        /// 生成打印数据
        /// </summary>
        public string CreatePrintData()
        {
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "参数错误");
                return "";
            }
            Entity = DC.Set<Knife>().Include(x=>x.ItemMaster).FirstOrDefault(x=>x.ID==Entity.ID);
            if (Entity == null)
            {
                MSD.AddModelError("", "数据不存在，请刷新后重试");
                return "";
            }
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.ITCode;
            data.Records = new List<CreatePrintDataLinePara>();

            if (Entity.Status == KnifeStatusEnum.Scrapped)
            {
                MSD.AddModelError("",$"{Entity.SerialNumber}刀已报废 不可打印");
                return "";    
            }
            if (Entity.Status == KnifeStatusEnum.DefectiveReturned)
            {
                MSD.AddModelError("", $"{Entity.SerialNumber}刀已不良退回 不可打印");
                return "";
            }
            BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.ID == Entity.ItemMasterId).AsNoTracking().FirstOrDefault();
            // 获取条码记录
            string BarCode = Entity.BarCode;// $"5@{Entity.ItemMaster.Code}@1@{Entity.SerialNumber}";

            CreatePrintDataLinePara line = new CreatePrintDataLinePara();
            line.Fields = [
                new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = BarCode },
                new CreatePrintDataSubLinePara { FieldName = "刀具料号", FieldValue = item.Code },
                new CreatePrintDataSubLinePara { FieldName = "刀具规格", FieldValue = item.SPECS },
                
            ];
            data.Records.Add(line);

            string printServer = Wtm.ConfigInfo.AppSettings["PrintServer"];
            PrintApiHelper apiHelper = new PrintApiHelper(printServer);
            ReturnResult rr = apiHelper.CreatePrintData(data);
            if (rr.Success)
            {
                if (string.IsNullOrEmpty(rr.Msg))
                {
                    MSD.AddModelError("", "生成打印数据失败");
                }
                return rr.Msg;
            }
            else
            {
                MSD.AddModelError("", rr.Msg);
                return "";
            }
        }

        /// <summary>
        /// 获取刀具信息 三种:台账刀 库存信息  组合刀号(ZHDH开头或者已失效的且数量大于1的条码)
        /// </summary>
        /// <param name="bar">条码</param>
        /// <param name="type">当前使用场合</param>
        /// <returns></returns>
        public List<KnifeItemMasterReturn> GeBarInfo(string bar, int type)//0领用  1归还 2报废 3替换 4调出 5调入 6移库 7修磨申请 8修磨出库 9修磨入库
        {
            List<KnifeItemMasterReturn> result = new List<KnifeItemMasterReturn>();
            
            try
            {

                #region 校验登录存储地点
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return null;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                BaseWareHouse whid_WareHouse = DC.Set<BaseWareHouse>().FirstOrDefault(x => x.ID == whid);
                #endregion
                #region 校验输入参数非空
                if (string.IsNullOrEmpty(bar))
                {
                    MSD.AddModelError("", "条码不可为空");
                    return null;
                }
                bar = bar.Trim();

                #endregion
                #region 判断条码类型
                int bartype = 0;//条码类型 1台账刀 2库存信息  3组合刀号
                Knife knife = null;
                BaseInventory inventory = null;
                KnifeCombine knifeCombine = null;
                knifeCombine = DC.Set<KnifeCombine>()
                    .Include(x=>x.KnifeCombineLine_KnifeCombine).ThenInclude(x=>x.Knife).ThenInclude(x=>x.CurrentCheckOutBy).ThenInclude(x => x.Department).ThenInclude(x => x.Organization)
                    .Include(x => x.KnifeCombineLine_KnifeCombine).ThenInclude(x => x.Knife).ThenInclude(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                    .Include(x => x.KnifeCombineLine_KnifeCombine).ThenInclude(x => x.Knife).ThenInclude(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x=>x.WareHouse)
                    .Include(x=>x.CheckOutBy).ThenInclude(x=>x.Department).ThenInclude(x=>x.Organization)
                    .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                    .Where(x => x.CombineKnifeNo == bar)
                    .FirstOrDefault();
                if (knifeCombine != null)
                {
                    bartype = 3;
                }
                else
                {
                    knife = DC.Set<Knife>()
                        .Include(x => x.CurrentCheckOutBy).ThenInclude(x => x.Department).ThenInclude(x => x.Organization)
                        .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                        .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                        .Where(x => x.Status!= KnifeStatusEnum.DefectiveReturned)//不良退回的条码在刀具和库存信息中都存在 在台账刀这边排除掉
                        .Where(x => x.BarCode == bar)
                        .FirstOrDefault();
                    if (knife != null)
                    {
                        bartype = 1;
                    }
                    else
                    {
                        inventory=DC.Set<BaseInventory>()
                            .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                            .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                            .Where(x => x.IsAbandoned == false && x.Qty>0 )//非作废 数量大于零  条码相等 
                            .Where(x=> (((int)x.ItemSourceType).ToString() + "@" + x.ItemMaster.Code + "@" + ((int)x.Qty).ToString() + "@" + x.SerialNumber)==bar)
                            .FirstOrDefault();
                        if (inventory != null)
                        {
                            bartype = 2;
                        }
                    }
                }
                if (bartype == 0)
                {
                    MSD.AddModelError("", "无效条码");
                    return null;
                }
                #endregion
                #region 根据类型构造返回值
                switch (bartype){
                    case 1://台账刀
                        #region 校验
                        if (type != 5 && knife.WhLocation.WhArea.WareHouse.ID != whid)//调入的时候允许不同存储地点
                        {
                            MSD.AddModelError("", $"刀具所在存储地点{knife.WhLocation.WhArea.WareHouse.Code}与当前登录存储地点{whid_WareHouse.Code}不同");
                            return null;
                        }
                        if (knife.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"刀具所在库位{knife.WhLocation.Code}已锁定");
                            return null;
                        }
                        if (knife.WhLocation.IsEffective == EffectiveEnum.Ineffective||knife.WhLocation.AreaType!= WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具所在库位{knife.WhLocation.Code}无效");
                            return null;
                        }
                        if (knife.WhLocation.Code .Contains( "A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                            return null;
                        }
                        if (knife.Status==KnifeStatusEnum.DefectiveReturned)//
                        {
                            MSD.AddModelError("", $"此台账刀已不良退回 不可获取刀具信息");
                            return null;
                        }
                        #endregion
                        #region 赋值
                        var knife_KnifeItemMasterReturn = new KnifeItemMasterReturn()
                        {
                            IsNew=false,
                            ItemID=knife.ItemMaster.ID.ToString(),
                            ItemName=knife.ItemMaster.Name,
                            ItemCode = knife.ItemMaster.Code,
                            SPECS = knife.ItemMaster.SPECS,
                            Qty = 1,
                            KnifeID = knife.ID.ToString(),
                            SerialNumber = knife.SerialNumber,
                            bar = knife.BarCode,
                            CurrentWhLocationId = knife.WhLocationId.ToString(),
                            CurrentWhLocationCode= knife.WhLocation.Code,
                            CurrentWhLocationName=knife.WhLocation.Name,
                            GrindKnifeNO = knife.GrindKnifeNO,
                            /*CheckOutByName =
                            CheckOutByID
                            DeptName*///领用状态再加这三个
                        };
                        //如果它在组合中存在  把组合刀号的@末尾的字符串给上
                        var tempKnifeCombineLine =DC.Set<KnifeCombineLine>()
                            .Include(x=>x.KnifeCombine)
                            .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)
                            .Where(x => x.KnifeId == knife.ID)
                            .FirstOrDefault();
                        if (tempKnifeCombineLine != null)
                        {
                            knife_KnifeItemMasterReturn.CombineKnifeNo = tempKnifeCombineLine.KnifeCombine.CombineKnifeNo.Split('@').Last();
                        }
                        #endregion
                        #region 额外操作:当前界面和刀具的状态的校验与赋值
                        if (type == 0)//领用时
                        {
                            if (knife.Status != KnifeStatusEnum.InStock)
                            {
                                MSD.AddModelError("", "此刀不在库 不可领用");
                                return null;
                            }
                        }
                        if (type == 1)//归还时
                        {
                            if (knife.Status != KnifeStatusEnum.CheckOut)
                            {
                                MSD.AddModelError("", "此刀未被领用 不可归还");
                                return null;
                            }
                            if (knife.Status == KnifeStatusEnum.CheckOut)//归还时 把刀的领用人部门信息给上  三个字段 
                            {
                                knife_KnifeItemMasterReturn.DeptName = knife.CurrentCheckOutBy.Department.Name;
                                knife_KnifeItemMasterReturn.CheckOutByID = knife.CurrentCheckOutBy.ID.ToString();
                                knife_KnifeItemMasterReturn.CheckOutByName = knife.CurrentCheckOutBy.Name;
                            }
                        }
                        if (type == 2)//报废时
                        {
                            if (knife.Status != KnifeStatusEnum.InStock)
                            {
                                MSD.AddModelError("", "此刀不在库 不可报废");
                                return null;
                            }
                        }

                        if (type == 3)//组合替换时 扫描老刀 可能是被换的也可能是换的 状态可能在库或者领用 不能对状态做限制
                        {
                            if (knife.Status != KnifeStatusEnum.InStock && knife.Status != KnifeStatusEnum.CheckOut)
                            {
                                MSD.AddModelError("", "此刀未被领用也不在库 不可用于替换");
                                return null;
                            }
                            /*var tempKnifeCombineLine = DC.Set<KnifeCombineLine>()
                                .Include(x => x.KnifeCombine)
                                .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)
                                .Where(x => x.Knife.BarCode == bar)
                                .FirstOrDefault();
                            if (tempKnifeCombineLine == null)
                            {
                                MSD.AddModelError("", "生效的组合单不存在此条码 请检查");//台账刀可能是替换也可能是被替换   不能用这个来判定
                                return null;
                            }
                            knife_KnifeItemMasterReturn.CombineKnifeNo = tempKnifeCombineLine.KnifeCombine.DocNo;
                            */
                        }
                        if (type == 4)//调出时
                        {
                            if (knife.Status != KnifeStatusEnum.InStock)
                            {
                                MSD.AddModelError("", "此刀不在库 不可调出");
                                return null;
                            }
                        }
                        if (type == 5)//调入时
                        {
                            if (knife.Status != KnifeStatusEnum.Transferring)
                            {
                                MSD.AddModelError("", "此刀未调出 不可调入");
                                return null;
                            }
                        }
                        if (type == 6)//移库时
                        {
                            if (knife.Status != KnifeStatusEnum.InStock)
                            {
                                MSD.AddModelError("", "此刀不在库 不可移库");
                                return null;
                            }
                        }
                        if (type == 7)//修磨申请
                        {
                            if (knife.Status != KnifeStatusEnum.InStock)
                            {
                                MSD.AddModelError("", "此刀不在库 不可修磨申请");
                                return null;
                            }
                        }
                        if (type == 8)//修磨出库
                        {
                            if (knife.Status != KnifeStatusEnum.GrindRequested)
                            {
                                MSD.AddModelError("", "此刀未修磨申请 不可修磨出库");
                                return null;
                            }
                        }
                        if (type == 9)//修磨入库
                        {
                            if (knife.Status != KnifeStatusEnum.GrindingOut)
                            {
                                MSD.AddModelError("", "此刀未修磨出库 不可修磨入库");
                                return null;
                            }
                        }
                        #endregion
                        #region 通过 成功返回
                        result.Add(knife_KnifeItemMasterReturn);
                        return result;
                        #endregion

                    case 2://库存信息
                        #region 校验
                        //冻结
                        //库位锁定
                        //非当前登录存储地点
                        //非不良退回库位

                        //非作废和数量大于0的要求 在搜索时已经做了 这里就不加了
                        if (inventory.FrozenStatus == FrozenStatusEnum.Freezed)
                        {
                            MSD.AddModelError("", $"此库存信息已被冻结");
                            return null;
                        }
                        if (inventory.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"此库存信息所在库位{inventory.WhLocation.Code}已锁定");
                            return null;
                        }
                        if (inventory.WhLocation.IsEffective == EffectiveEnum.Ineffective || inventory.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库存信息所在库位{inventory.WhLocation.Code}无效");
                            return null;
                        }
                        if (inventory.WhLocation.WhArea.WareHouse.ID != whid)
                        {
                            MSD.AddModelError("", $"此库存信息所在存储地点{inventory.WhLocation.WhArea.WareHouse.Code}与当前登录存储地点{whid_WareHouse.Code}不同");
                            return null;
                        }
                        if (inventory.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此库存信息所在库位为不良退回库位 不可获取刀具信息");
                            return null;
                        }
               
                        #endregion
                        #region 赋值
                        var inventory_KnifeItemMasterReturn = new KnifeItemMasterReturn()
                        {
                            IsNew = true,
                            ItemID = inventory.ItemMaster.ID.ToString(),
                            ItemName = inventory.ItemMaster.Name,
                            ItemCode = inventory.ItemMaster.Code,
                            SPECS = inventory.ItemMaster.SPECS,
                            Qty = inventory.Qty,
                            SerialNumber = inventory.SerialNumber,
                            bar = ((int)inventory.ItemSourceType).ToString() + "@" + inventory.ItemMaster.Code + "@" + ((int)inventory.Qty).ToString() + "@" + inventory.SerialNumber,
                            CurrentWhLocationId = inventory.WhLocationId.ToString(),
                            CurrentWhLocationCode = inventory.WhLocation.Code,
                            CurrentWhLocationName = inventory.WhLocation.Name,
                        };
                        #endregion
                        #region 额外操作
                        if (type == 0)//领用时
                        {
                            //
                        }
                        if (type == 1)//归还时
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 2)//报废时
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }

                        if (type == 3)//组合替换时 库存信息只可用于 替换的新刀 数量必为1
                        {
                            if (inventory.Qty!=1)
                            {
                                MSD.AddModelError("", "在组合替换时库存信息只可为1");
                                return null;
                            }
                        }
                        if (type == 4)//调出时
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 5)//调入时
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 6)//移库时
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 7)//修磨申请
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 8)//修磨出库
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        if (type == 9)//修磨入库
                        {
                            MSD.AddModelError("", "库存信息不可用于此处");
                            return null;
                        }
                        #endregion
                        #region 通过 成功返回
                        result.Add(inventory_KnifeItemMasterReturn);
                        return result;
                        #endregion
                    case 3://组合刀号
                        #region 校验
                        foreach(var line in knifeCombine.KnifeCombineLine_KnifeCombine)
                        {
                            var tempKnife = line.Knife;
                            if (tempKnife.WhLocation.WhArea.WareHouse.ID != whid)
                            {
                                MSD.AddModelError("", $"刀具所在存储地点{tempKnife.WhLocation.WhArea.WareHouse.Code}与当前登录存储地点{whid_WareHouse.Code}不同");
                                return null;
                            }
                            if (tempKnife.WhLocation.Locked == true)
                            {
                                MSD.AddModelError("", $"刀具所在库位{tempKnife.WhLocation.Code}已锁定");
                                return null;
                            }
                            if (tempKnife.WhLocation.IsEffective == EffectiveEnum.Ineffective || tempKnife.WhLocation.AreaType != WhLocationEnum.Normal)
                            {
                                MSD.AddModelError("", $"刀具所在库位{tempKnife.WhLocation.Code}无效");
                                return null;
                            }
                            if (tempKnife.WhLocation.Code.Contains("A9999999"))//不良退回库位
                            {
                                MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                                return null;
                            }
                            if (tempKnife.Status == KnifeStatusEnum.DefectiveReturned)//
                            {
                                MSD.AddModelError("", $"此台账刀已不良退回 不可获取刀具信息");
                                return null;
                            }
                        }
                        #endregion
                        #region 赋值
                        var combineKnifes_KnifeItemMasterReturn = new List<KnifeItemMasterReturn>();
                        foreach (var line in knifeCombine.KnifeCombineLine_KnifeCombine)
                        {
                            var combineKnife_KnifeItemMasterReturn = new KnifeItemMasterReturn();
                            var tempKnife = line.Knife;
                            combineKnife_KnifeItemMasterReturn =new KnifeItemMasterReturn()
                            {
                                IsNew = false,
                                ItemID = tempKnife.ItemMaster.ID.ToString(),
                                ItemName = tempKnife.ItemMaster.Name,
                                ItemCode = tempKnife.ItemMaster.Code,
                                SPECS = tempKnife.ItemMaster.SPECS,
                                Qty = 1,
                                KnifeID = tempKnife.ID.ToString(),
                                SerialNumber = tempKnife.SerialNumber,
                                bar = tempKnife.BarCode,
                                CurrentWhLocationId = tempKnife.WhLocationId.ToString(),
                                CurrentWhLocationCode = tempKnife.WhLocation.Code,
                                CurrentWhLocationName = tempKnife.WhLocation.Name,
                                CombineKnifeNo = line.KnifeCombine.CombineKnifeNo.Split('@').Last(),
                                CombineKnifeNo_bar = line.KnifeCombine.CombineKnifeNo,

                                DeptName = knifeCombine?.CheckOutBy?.Department?.Name,
                                CheckOutByID = knifeCombine?.CheckOutBy?.ID.ToString(),
                                CheckOutByName = knifeCombine?.CheckOutBy?.Name,
                                GrindKnifeNO = tempKnife.GrindKnifeNO,

                            };
                            #region 转到这里的额外操作
                            if (type == 0)//领用时
                            {
                                if (tempKnife.Status != KnifeStatusEnum.InStock)
                                {
                                    MSD.AddModelError("", $"此组合刀号存在刀具不在库 不可领用");
                                    return null;
                                }
                            }
                            if (type == 1)//归还时
                            {
                                if (tempKnife.Status != KnifeStatusEnum.CheckOut)
                                {
                                    MSD.AddModelError("", "此组合刀号存在刀具未被领用 不可归还");
                                    return null;
                                }
                                //不需要归还时才有 可以用组合单上的领用人字段
                                /*if (tempKnife.Status == KnifeStatusEnum.CheckOut)//归还时 把刀的领用人部门信息给上  三个字段 
                                {
                                    combineKnife_KnifeItemMasterReturn.DeptName = tempKnife.CurrentCheckOutBy.Department.Name;
                                    combineKnife_KnifeItemMasterReturn.CheckOutByID = tempKnife.CurrentCheckOutBy.ID.ToString();
                                    combineKnife_KnifeItemMasterReturn.CheckOutByName = tempKnife.CurrentCheckOutBy.Name;
                                }*/
                            }
                            if (type == 2)//报废时
                            {
                                /*if (tempKnife.Status != KnifeStatusEnum.InStock)
                                {
                                    MSD.AddModelError("", "此组合刀号存在刀具不在库 不可报废");
                                    return null;
                                }*/
                                MSD.AddModelError("", "组合刀号不可用于报废");
                                return null;
                            }

                            if (type == 3)//组合替换时 扫描老刀 扫描新刀 扫组合不能一对一 但是需要先扫描组合刀号 所以放开限制
                            {
                                /*MSD.AddModelError("", "组合刀号不可用于组合替换");
                                return null;*/
                            }
                            if (type == 4)//调出时
                            {
                                MSD.AddModelError("", "组合刀号不可用于调出");
                                return null;
                                /*if (tempKnife.Status != KnifeStatusEnum.InStock)
                                {
                                    MSD.AddModelError("", "此组合刀号存在刀具不在库 不可调出");
                                    return null;
                                }*/
                            }
                            if (type == 5)//调入时
                            {
                                MSD.AddModelError("", "组合刀号不可用于调入");
                                return null;
                                /*if (tempKnife.Status != KnifeStatusEnum.Transferring)
                                {
                                    MSD.AddModelError("", "此组合刀号存在刀具未调出 不可调入");
                                    return null;
                                }*/
                            }
                            if (type == 6)//移库时
                            {
                                MSD.AddModelError("", "组合刀号不可用于移库");//可以用于移库?存疑
                                return null;
                                /*if (tempKnife.Status != KnifeStatusEnum.InStock)
                                {
                                    MSD.AddModelError("", "此刀不在库 不可移库");
                                    return null;
                                }*/
                            }
                            if (type == 7)//修磨申请
                            {
                                var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
                                if (currentVersion == null)
                                {
                                    MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                                    return null;
                                }
                                if (currentVersion.Value == "0")//原来的
                                {
                                    MSD.AddModelError("", "组合刀号不可用于修磨申请");
                                    return null;
                                }
                            }
                            if (type == 8)//修磨出库
                            {
                                var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
                                if (currentVersion == null)
                                {
                                    MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                                    return null;
                                }
                                if (currentVersion.Value == "0")//原来的
                                {
                                    MSD.AddModelError("", "组合刀号不可用于修磨出库");//
                                    return null;
                                }
                            }
                            if (type == 9)//修磨入库
                            {
                                var currentVersion = DC.Set<BaseSysPara>().FirstOrDefault(x => x.Code == "KnifeGrindCodeVersion");
                                if (currentVersion == null)
                                {
                                    MSD.AddModelError("", "未检测到刀具修磨代码版本 请联系管理员");
                                    return null;
                                }
                                if (currentVersion.Value == "0")//原来的
                                {
                                    MSD.AddModelError("", "组合刀号不可用于修磨入库");//
                                    return null;
                                }
                            }
                            #endregion
                            combineKnifes_KnifeItemMasterReturn.Add(combineKnife_KnifeItemMasterReturn);
                        }
                        #endregion
                        #region 额外操作
                        //额外操作需要遍历  转到赋值里一起进行
                        #endregion
                        #region 通过 成功返回
                        result.AddRange(combineKnifes_KnifeItemMasterReturn);
                        return result;
                    #endregion
                    default:
                        MSD.AddModelError("", "未知错误");
                        return null;
                }
                #endregion
                /*string[] bar_parts = bar.Trim().Split("@");
                if (bar_parts.Length!=4)
                {
                    MSD.AddModelError("", "条码格式错误");
                    return null;
                }*///也不一定了  可能是组合刀号
                

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:"+e.Message);
                return null;
            }

        }

        /// <summary>
        /// 查询库存/刀具
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<GetKnifeAndInventoryReturn> GetKnifeAndInventory(GetKnifeAndInventoryInputInfo info)
        {
            List<GetKnifeAndInventoryReturn> rr = new List<GetKnifeAndInventoryReturn>();
            try
            {
                var knifes =  DC.Set<Knife>()
                    .Include(x=>x.ItemMaster).ThenInclude(x=>x.Organization)
                    .Include(x=>x.WhLocation)
                    .Where(x => x.Status == KnifeStatusEnum.InStock)
                    .WhereIf(!string.IsNullOrEmpty(info.ItemCode), x => x.ItemMaster.Code == info.ItemCode)
                    .WhereIf(!string.IsNullOrEmpty(info.WhLocationCode), x => x.WhLocation.Code == info.WhLocationCode)
                    .WhereIf(!string.IsNullOrEmpty(info.Seiban), x => false)
                    .WhereIf(!string.IsNullOrEmpty(info.Bar), x => x.BarCode == info.Bar)
                    .ToList();
                foreach(var k in knifes)
                {
                    rr.Add( new GetKnifeAndInventoryReturn()
                    {
                        IsKnife =true,
                        ItemID = k.ItemMaster.ID.ToString(),
                        ItemCode = k.ItemMaster.Code,
                        ItemName = k.ItemMaster.Name,
                        ItemSpecs = k.ItemMaster.SPECS,
                        ItemOrgID = k.ItemMaster.Organization.ID.ToString(),
                        ItemOrgCode = k.ItemMaster.Organization.Code,
                        ItemOrgName = k.ItemMaster.Organization.Name,
                        Qty = "1",
                        WhLocationID = k.WhLocation.ID.ToString(),
                        WhLocationCode = k.WhLocation.Code ,
                        WhLocationName = k.WhLocation.Name,
                        Seiban = "",
                        Bar =k.BarCode,
                        BatchNumber="",
                    });
                }
                var inventorys = DC.Set<BaseInventory>()
                    .Include(x => x.ItemMaster).ThenInclude(x => x.Organization)
                    .Include(x => x.WhLocation)
                    .Where(x => x.Qty!=0&&x.IsAbandoned == false)//不作废且数量不为0
                    .WhereIf(!string.IsNullOrEmpty(info.ItemCode), x => x.ItemMaster.Code == info.ItemCode)
                    .WhereIf(!string.IsNullOrEmpty(info.WhLocationCode), x => x.WhLocation.Code == info.WhLocationCode)
                    .WhereIf(!string.IsNullOrEmpty(info.Seiban), x => x.Seiban == info.Seiban)
                    .WhereIf(!string.IsNullOrEmpty(info.Bar), x => ((int)x.ItemSourceType).ToString()+"@"+x.ItemMaster.Code+ "@" + ((int)x.Qty).ToString()+ "@" +x.SerialNumber == info.Bar)
                    .ToList();
                foreach (var i in inventorys)
                {
                    rr.Add(new GetKnifeAndInventoryReturn()
                    {
                        IsKnife = false,
                        ItemID = i.ItemMaster.ID.ToString(),
                        ItemCode = i.ItemMaster.Code,
                        ItemName = i.ItemMaster.Name,
                        ItemSpecs = i.ItemMaster.SPECS,
                        ItemOrgID = i.ItemMaster.Organization.ID.ToString(),
                        ItemOrgCode = i.ItemMaster.Organization.Code,
                        ItemOrgName = i.ItemMaster.Organization.Name,
                        Qty = i.Qty.ToString(),
                        WhLocationID = i.WhLocation.ID.ToString(),
                        WhLocationCode = i.WhLocation.Code,
                        WhLocationName = i.WhLocation.Name,
                        Seiban = i.Seiban,
                        Bar = $"{(int)i.ItemSourceType}@{i.ItemMaster.Code}@{(int)i.Qty}@{i.SerialNumber}",
                        BatchNumber = i.BatchNumber,
                    });
                }
                return rr;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:" + e.Message);
                return null;
            }
        }
        /// <summary>
        /// 修改刀具状态
        /// </summary>
        public void DoEditKnifeStatus()
        {
            try
            {
                var beforeKnife = DC.Set<Knife>()
                    .Include(x=>x.WhLocation)
                    .Where(x => x.ID == Entity.ID).FirstOrDefault();
                if (beforeKnife == null)
                {
                    MSD.AddModelError("", $"无效刀具id");
                    return;
                }
                var beforeKnifeStatus = beforeKnife.Status;

                var afterKnifeStatus = Entity.Status;
                if(afterKnifeStatus== beforeKnifeStatus)
                {
                    MSD.AddModelError("", $"未检测到修改内容");
                    return;
                }
                beforeKnife.Status = afterKnifeStatus;
                var knife = beforeKnife;
                var currentTime = DateTime.Now;
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//仓管员
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                if (knife.WhLocation.Locked == true)
                {
                    MSD.AddModelError("", $"刀具所在库位已锁定");
                    return ;
                }

                DC.Set<KnifeOperation>().Add(new KnifeOperation
                {
                    KnifeId = knife.ID,
                    DocNo = "强制修改",
                    OperationType = KnifeOperationTypeEnum.ForcedModify,
                    OperationTime = currentTime,
                    OperationBy = handledBy_operator,//操作人=归还人
                    OperationById = handledBy_operator.ID,//操作人=归还人
                    WhLocation = knife.WhLocation,
                    WhLocationId = knife.WhLocationId,
                    HandledBy = handledBy,
                    HandledById = handledBy.ID.ToString(),
                    UsedDays = 0,
                    RemainingDays = knife.RemainingUsedDays,
                    TotalUsedDays = knife.TotalUsedDays,
                    CurrentLife = knife.CurrentLife,
                    GrindNum = knife.GrindCount,
                    BeforeStatus = beforeKnifeStatus,
                    AfterStatus = afterKnifeStatus,
                    CreateBy = LoginUserInfo.Name,
                    CreateTime = currentTime,
                });

                DC.SaveChanges();
            }
            catch (Exception e)
            {
                MSD.AddModelError("", $" "+e.Message);
                return;
            }
        }

        /// <summary>
        /// 
        /// 获取刀具寿命信息配置
        /// </summary>
        /// <param name="itemCodes">料号list</param>
        /// <returns></returns>
        public U9KnifeLivesReturn GetKnifesLives_2(List<string> itemCodes)
        {
            var rr = new U9KnifeLivesReturn();
            try
            {

                
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode,"0410", LoginUserInfo.Name);
                var result = apiHelper.GetKnifeInfo(itemCodes);
                if(result == null)
                {
                    rr.Success = false;
                    rr.Message = $"访问U9刀具寿命表失败 未获取到信息";
                    rr.Data = null;
                    return rr;
                }
                rr.Success = result.Success;
                rr.Message = result.Msg;
                if (!rr.Success)
                {
                    if (result.Entity == null || (result.Entity != null && (result.Entity.MatchDatas.Count == 0)))
                    {
                        rr.Message = $"料号[{string.Join(",", itemCodes)}]在U9刀具寿命表未配置 无法获取信息";
                    }
                    else if (result.Entity != null && result.Entity.MatchDatas.Count != 0)
                    {
                        var missingItems = itemCodes
                            .Except(result.Entity.MatchDatas.Select(x => x.ItemCode))
                            .ToList();
                        rr.Message = $"料号[{string.Join(",", missingItems)}]在U9刀具寿命表未配置 无法获取信息";
                    }

                    rr.Data = null;
                    return rr;
                }
                rr.Data = new List<U9KnifeLivesReturn_Line>();
                foreach (var t in result.Entity.MatchDatas)
                {
                    rr.Data.Add(new U9KnifeLivesReturn_Line()
                    {
                        ItemMaster = t.ItemCode,
                        CurrentLife = t.CurrentLife,
                        FirstDepreciationLife = t.FirstDepreciationLife,
                        SecondDepreciationLife = t.SecondDepreciationLife,
                        ThirdDepreciationLife = t.ThirdDepreciationLife,
                        FourthDepreciationLife = t.FourthDepreciationLife,
                        FifthDepreciationLife = t.FifthDepreciationLife,
                        IsAutoSync = t.IsAutoSync,
                        IsActive = t.IsAutoSync,
                        LedgerIncluded = t.LedgerIncluded,
                        Remark = t.Remark,
                    });
                }
     
                
            }
            catch (Exception e)
            {
                rr.Success = false;
                rr.Message = "转换实体类型时失败。错误信息：" + e.Message;
            }
           
            return rr;

            
        }

        /// <summary>
        /// 获取刀具寿命信息配置_期初的数据 导入的 经办人不登录 放参数里
        /// </summary>
        /// <param name="itemCodes">料品code的list</param>
        /// <param name="handledName">无用参数 填个""吧 作区分用</param>
        /// <returns></returns>
        public U9KnifeLivesReturn GetKnifesLives_2(List<string> itemCodes,string handledName)
        {
            var rr = new U9KnifeLivesReturn();
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", "G2WMS");
            var result = apiHelper.GetKnifeInfo(itemCodes);
            if(result == null)
            {
                MSD.AddModelError("", "访问U9失败");
                return null;
            }
            rr.Success = result.Success;
            rr.Message = result.Msg;
            rr.Data = new List<U9KnifeLivesReturn_Line>();
            foreach (var t in result.Entity.MatchDatas)
            {
                rr.Data.Add(new U9KnifeLivesReturn_Line()
                {
                    ItemMaster = t.ItemCode,
                    CurrentLife = t.CurrentLife,
                    FirstDepreciationLife = t.FirstDepreciationLife,
                    SecondDepreciationLife = t.SecondDepreciationLife,
                    ThirdDepreciationLife = t.ThirdDepreciationLife,
                    FourthDepreciationLife = t.FourthDepreciationLife,
                    FifthDepreciationLife = t.FifthDepreciationLife,
                    IsAutoSync = t.IsAutoSync,
                    IsActive = t.IsAutoSync,
                    LedgerIncluded = t.LedgerIncluded,
                    Remark = t.Remark,
                });
            }
            try
            {
                if (!rr.Success)
                {
                    if (rr.Data == null || (rr.Data != null && rr.Data.Count == 0))
                    {
                        rr.Message = $"料号[{string.Join(",", itemCodes)}]在U9刀具寿命表未配置 无法获取信息";
                    }
                    else if (rr.Data != null && rr.Data.Count != 0)
                    {
                        var missingItems = itemCodes
                            .Except(rr.Data.Select(x => x.ItemMaster))
                            .ToList();
                        rr.Message = $"料号[{string.Join(",", missingItems)}]在U9刀具寿命表未配置 无法获取信息";
                    }
                }
                if (rr.Success && rr.Data == null || rr.Success && rr.Data != null && rr.Data.Count == 0)
                {
                    rr.Success = false;
                    rr.Message = $"未查询到刀具寿命";
                }
            }
            catch (Exception e)
            {
                rr.Success = false;
                rr.Message = "转换实体类型时失败。错误信息：" + e.Message;
            }


            return rr;
        }


        /// <summary>
        /// 库存调整_刀具部分  盘盈与盘亏
        /// </summary>
        /// <param name="knifeInfos_surplus">盘盈刀具信息 (盘盈刀id的list+库位)的list</param>
        /// <param name="knifeids_loss">盘亏刀id的list</param>
        /// <param name="docNo">盘点单号</param>
        public void DoInventoryAdjust_Knifes(List<(List<Guid> knifeids_surplus, Guid whLocationId)> knifeInfos_surplus, List<Guid> knifeids_loss,string docNo)
        {
            try
            {
                var currentTime = DateTime.Now;
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                if(handledBy == null)
                {
                    MSD.AddModelError("", "未检测到登录人");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                                   .Include(x => x.Department).ThenInclude(x => x.Organization)
                                   .FirstOrDefault(x => x.Code== LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//操作人人_业务员类型
                if (handledBy_operator == null)
                {
                    MSD.AddModelError("", "未检测到操作人 请在U9配置当前登录人的在刀具中心组织下的业务员后同步到G2 ");
                    return;
                }
                #region 盘亏
                var knifes_loss = DC.Set<Knife>()
                    .Include(x => x.WhLocation)
                    .Where(x => knifeids_loss.Contains(x.ID))
                    .ToList();
                if (knifes_loss.Count > 0)//盘亏只有一种 在库变成盘亏
                {
                    foreach (var k in knifes_loss)
                    {
                        var usedDays = k.RemainingUsedDays;
                        //刀变状态
                        k.Status = KnifeStatusEnum.inventoryLoss;
                        k.RemainingUsedDays = 0;
                        k.HandledByName = handledBy.Name;
                        k.HandledBy = handledBy;
                        k.HandledById = handledBy.ID.ToString();
                        k.LastOperationDate = currentTime;

                        k.UpdateBy = handledBy.Name;
                        k.UpdateTime = currentTime;
                        //操作记录
                        DC.Set<KnifeOperation>().Add(new KnifeOperation()
                        {
                            Knife = k,
                            KnifeId = k.ID,
                            DocNo = docNo,
                            OperationType = KnifeOperationTypeEnum.inventoryLoss,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = handledBy,
                            HandledById = handledBy.ID.ToString(),
                            HandledByName = handledBy.Name,
                            WhLocation = k.WhLocation,
                            WhLocationId = k.WhLocationId,

                            UsedDays = usedDays,
                            TotalUsedDays = k.TotalUsedDays,
                            RemainingDays =0,
                            CurrentLife = k.CurrentLife,
                            GrindNum =k.GrindCount,

                            BeforeStatus = KnifeStatusEnum.InStock,
                            AfterStatus = KnifeStatusEnum.inventoryLoss,

                            CreateTime = currentTime,
                            CreateBy = handledBy.Name,
                        });
                    }


                }
                #endregion
                #region 盘盈
                if (knifeInfos_surplus!=null&&knifeInfos_surplus.Count > 0)
                {
                    #region  G2系统内的操作
                    foreach (var(knifeids_surplus, whLocationId) in knifeInfos_surplus)
                    {
                        var knifes_surplus = DC.Set<Knife>()
                            .Include(x => x.WhLocation)
                            .Where(x => knifeids_surplus.Contains(x.ID))
                            .ToList();
                        var whLocation = DC.Set<BaseWhLocation>()
                            .Where(x => x.ID == whLocationId)
                            .FirstOrDefault();
                        if(whLocation == null)
                        {
                            MSD.AddModelError("", "未检测到有效的库位");
                            return;
                        }
                        if (knifes_surplus.Count > 0)
                        {
                            foreach (var k in knifes_surplus)//变盘盈 不同状态不同的操作 
                            {
                                switch (k.Status)
                                {
                                    
                                    case KnifeStatusEnum.inventoryLoss:
                                        #region 盘亏的刀允许盘盈 
                                        //刀变状态
                                        k.Status = KnifeStatusEnum.InStock;
                                        k.HandledByName = handledBy.Name;
                                        k.HandledBy = handledBy;
                                        k.HandledById = handledBy.ID.ToString();
                                        k.LastOperationDate = currentTime;
                                        k.UpdateBy = handledBy.Name;
                                        k.UpdateTime = currentTime;
                                        //操作记录
                                        DC.Set<KnifeOperation>().Add(new KnifeOperation()
                                        {
                                            Knife = k,
                                            KnifeId = k.ID,
                                            DocNo = docNo,
                                            OperationType = KnifeOperationTypeEnum.inventorySurplus,
                                            OperationTime = currentTime,
                                            OperationBy = handledBy_operator,
                                            OperationById = handledBy_operator.ID,
                                            HandledBy = handledBy,
                                            HandledById = handledBy.ID.ToString(),
                                            HandledByName = handledBy.Name,
                                            WhLocation = k.WhLocation,
                                            WhLocationId = k.WhLocationId,


                                            UsedDays = 0,
                                            TotalUsedDays = k.TotalUsedDays,
                                            RemainingDays = k.RemainingUsedDays,
                                            CurrentLife = k.CurrentLife,
                                            GrindNum = k.GrindCount,

                                            BeforeStatus = KnifeStatusEnum.inventoryLoss,
                                            AfterStatus = KnifeStatusEnum.InStock,

                                            CreateTime = currentTime,
                                            CreateBy = handledBy.Name,
                                        });
                                        #endregion
                                        break;
                                    //在库的刀报错
                                    case KnifeStatusEnum.InStock:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为在库 不允许盘盈");
                                        return;
                                    //领用的刀报错
                                    case KnifeStatusEnum.CheckOut:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为领用 不允许盘盈");
                                        return;

                                        /*#region 领用的刀变盘盈 相当于归还 写完在注释掉
                                        //计算领用日期使用天数包含当天
                                        DateTime checkOutDate = (DateTime)k.LastOperationDate;
                                        var usedDays = (currentTime.Date - checkOutDate.Date).Days + 1; 
                                        //修改 刀具
                                        k.Status = KnifeStatusEnum.InStock;
                                        k.CurrentCheckOutBy = null;
                                        k.CurrentCheckOutById = null;
                                        k.HandledBy = handledBy;
                                        k.HandledById = handledBy.ID.ToString();
                                        k.HandledByName = handledBy.Name;
                                        k.WhLocation = whLocation;
                                        k.WhLocationId = whLocation.ID;
                                        k.LastOperationDate = currentTime;
                                        k.TotalUsedDays += usedDays;//累计使用天数
                                        k.RemainingUsedDays = Math.Max(0, (int)(k.RemainingUsedDays - usedDays));//剩余使用天数
                                        k.UpdateBy = handledBy.Name;
                                        k.UpdateTime = currentTime;
                                        //新增 操作记录-盘盈
                                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                                        {
                                            KnifeId = k.ID,
                                            DocNo = docNo,
                                            OperationType = KnifeOperationTypeEnum.inventorySurplus,
                                            OperationTime = currentTime,
                                            HandledBy = handledBy,
                                            HandledById = handledBy.ID.ToString(),
                                            HandledByName = handledBy.Name,
                                            OperationBy = handledBy_operator,//操作人=归还人
                                            OperationById = handledBy_operator.ID,//操作人=归还人
                                            WhLocation = whLocation,
                                            WhLocationId = whLocation.ID,

                                            UsedDays = usedDays,
                                            RemainingDays = k.RemainingUsedDays,
                                            TotalUsedDays = k.TotalUsedDays,
                                            CurrentLife = k.CurrentLife,
                                            GrindNum = k.GrindCount,
                                            BeforeStatus = KnifeStatusEnum.CheckOut,
                                            AfterStatus = KnifeStatusEnum.InStock,
                                            CreateBy = handledBy.Name,
                                            CreateTime = currentTime,
                                        });
                                        break;
                                    #endregion*/

                                    //调拨的刀报错
                                    case KnifeStatusEnum.Transferring:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为调拨 不允许盘盈");
                                        return;

                                        /*#region 调拨的刀盘盈

                                //修改 刀具
                                k.Status = KnifeStatusEnum.InStock;
                                k.LastOperationDate = currentTime;

                                k.HandledBy = handledBy;
                                k.HandledById = handledBy.ID.ToString();
                                k.HandledByName = handledBy.Name;
                                k.WhLocation = whLocation;
                                k.WhLocationId = whLocation.ID;

                                k.UpdateBy = handledBy.Name;
                                k.UpdateTime = currentTime;
                                //新增 操作记录-盘盈
                                DC.Set<KnifeOperation>().Add(new KnifeOperation
                                {
                                    KnifeId = k.ID,
                                    DocNo = docNo,
                                    OperationType = KnifeOperationTypeEnum.inventorySurplus,
                                    OperationTime = currentTime,
                                    HandledBy = handledBy,
                                    HandledById = handledBy.ID.ToString(),
                                    HandledByName = handledBy.Name,
                                    OperationBy = handledBy_operator,//操作人=归还人
                                    OperationById = handledBy_operator.ID,//操作人=归还人
                                    WhLocation = whLocation,
                                    WhLocationId = whLocation.ID,

                                    UsedDays = 0,
                                    RemainingDays = k.RemainingUsedDays,
                                    TotalUsedDays = k.TotalUsedDays,
                                    CurrentLife = k.CurrentLife,    
                                    GrindNum = k.GrindCount,
                                    BeforeStatus = KnifeStatusEnum.Transferring,
                                    AfterStatus = KnifeStatusEnum.InStock,
                                    CreateBy = handledBy.Name,
                                    CreateTime = currentTime,
                                });
                                //对应的调出单的行关闭 以及可能的单关闭
                                var knifeTranferOutLine = DC.Set<KnifeTransferOutLine>()
                                    .Include(x => x.KnifeTransferOut).ThenInclude(x=>x.KnifeTransferOutLine_KnifeTransferOut)
                                    .Where(x => x.KnifeTransferOut.Status == KnifeOrderStatusEnum.Approved)
                                    .Where(x => x.KnifeId == k.ID &&x.Status == KnifeOrderStatusEnum.Approved)
                                    .FirstOrDefault();
                                if (knifeTranferOutLine == null)
                                {
                                    MSD.AddModelError("", $"{k.SerialNumber}对应的调出行已关闭/不存在 ");
                                    return;
                                }
                                else
                                {
                                    knifeTranferOutLine.Status = KnifeOrderStatusEnum.SuspendClose;
                                    if (knifeTranferOutLine.KnifeTransferOut.KnifeTransferOutLine_KnifeTransferOut.Where(x => x.Status == KnifeOrderStatusEnum.Approved).ToList().Count == 0)
                                    {
                                        knifeTranferOutLine.KnifeTransferOut.Status = KnifeOrderStatusEnum.SuspendClose;
                                    }
                                }
                                break;
                                #endregion*/

                                    //不良退回的刀报错
                                    case KnifeStatusEnum.DefectiveReturned:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为不良退回 条码已失效");
                                        return;
                                    case KnifeStatusEnum.Scrapped:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为报废 不允许盘盈");
                                        return;
                                    case KnifeStatusEnum.ScrapRequested:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为报废申请 不允许盘盈");
                                        return;
                                    case KnifeStatusEnum.GrindRequested:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为修磨申请 不允许盘盈");
                                        return;
                                    case KnifeStatusEnum.GrindingOut:
                                        MSD.AddModelError("", $"{k.SerialNumber}状态为修磨出库 不允许盘盈");
                                        return;
                                    default:
                                        MSD.AddModelError("", $"{k.SerialNumber}未检测到有效的状态");
                                        return;
                                }
                            }
                        }
                    }
                    #endregion

                    #region  U9上的操作 暂无
                    //修磨申请-关闭请购单,采购单 盘点了部分的话?
                    //修磨出库-收货单删除(不一定有) 采购单/行 删除/关闭 请购行-短缺关闭  详细?
                    //报废状态 不涉及U9 OA?部分盘盈?
                    //报废申请状态  考虑一下?
                    //先不提前做了 后期有需求提出后再考虑开发 太早改可能变动较大用不上
                    #endregion
                }
                #endregion

                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", $"库存调整_刀具部分异常:" + e.Message);
                return;
            }
        }

        public List<GetKnifeAndInventoryInStockQtyReturn> GetKnifeAndInventoryInStockQty(GetKnifeAndInventoryInStockQtyInputInfo input = null)
        {
            List<GetKnifeAndInventoryInStockQtyReturn> rr = new List<GetKnifeAndInventoryInStockQtyReturn>();
            try
            {
                // 构建动态 WHERE 条件
                var whereConditions = new List<string>();
                // 如果 input 为 null，创建默认实例
                input = input ?? new GetKnifeAndInventoryInStockQtyInputInfo();
                // 左模糊匹配条件
                if (!string.IsNullOrWhiteSpace(input.ItemCodePart))
                {
                    whereConditions.Add($"i.料品 LIKE '{input.ItemCodePart}%'");
                }

                // 精确 IN 匹配条件
                if (input.ItemCodes != null && input.ItemCodes.Any())
                {
                    whereConditions.Add($"i.料品 IN ('{string.Join("','", input.ItemCodes)}')");
                }

                // 构建完整的 WHERE 子句
                string whereClause = whereConditions.Any()
                    ? $" WHERE 1=1 AND {string.Join(" AND ", whereConditions)}"
                    : "";

                String sql = @$"
                            SET NOCOUNT ON;

                            select 
		                    total.料品,
		                    sum(CASE WHEN total.刀具状态 = '在库' THEN total.数量 ELSE 0 END) 在库数量, 
		                    sum(CASE WHEN total.刀具状态 = '领用' THEN total.数量 ELSE 0 END) 领用数量, 
		                    sum(CASE WHEN total.刀具状态 = '调拨' THEN total.数量 ELSE 0 END) 调拨数量, 
		                    sum(CASE WHEN total.刀具状态 = '报废' THEN total.数量 ELSE 0 END) 报废数量, 
		                    sum(CASE WHEN total.刀具状态 = '修磨申请' THEN total.数量 ELSE 0 END) 修磨申请数量, 
		                    sum(CASE WHEN total.刀具状态 = '修磨出库' THEN total.数量 ELSE 0 END) 修磨出库数量, 
		                    sum(CASE WHEN total.刀具状态 = '不良退回' THEN total.数量 ELSE 0 END) 不良退回数量, 
		                    sum(CASE WHEN total.刀具状态 = '盘亏' THEN total.数量 ELSE 0 END) 盘亏数量, 
		                    sum(CASE WHEN total.刀具状态 = '报废申请' THEN total.数量 ELSE 0 END) 报废申请数量
                            into #tempResult
		                    from (
			                    select i.code 料品,
				                    case k.Status when 0 then '在库'when 1 then '领用'when 2 then '调拨'when 3 then '报废'when 4 then '修磨申请'when 5 then '修磨出库'
				                    when 6 then '不良退回'when 7 then '盘亏'when 8  then '报废申请' end 刀具状态
				                    ,count(*) 数量
			                    from knifes k
			                    join BaseItemMaster i on k.ActualItemCode = i.code
			                    -- --刀具料号类别字段 

			                    group by i.code,k.Status

			                    union all

			                    select 
				                    i.code 料品, '在库' as 刀具状态,sum(inv.qty) 数量
			                    from BaseInventory inv
			                    join BaseItemMaster i on inv.ItemMasterId = i.ID
			                    group by i.code
		                    )  total
		                    group by total.料品
                            select *from #tempResult i {whereClause}
                            ";

            
                rr = DC.Database.SqlQueryRaw<GetKnifeAndInventoryInStockQtyReturn>(sql).ToList();
                if (rr == null)
                {
                    MSD.AddModelError("", "查询库存失败 请检查连接" );
                    return null;
                }
                return rr;
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:" + e.Message);
                return null;
            }
        }
    }
}

