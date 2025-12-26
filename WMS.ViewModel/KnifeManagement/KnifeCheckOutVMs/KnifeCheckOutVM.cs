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
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using System.Numerics;
using WMS.Model.BaseData;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using Microsoft.EntityFrameworkCore.Storage;
using WMS.Util;
using Elsa;
using NPOI.OpenXmlFormats.Spreadsheet;
using WMS.Util.U9Para.Knife;
using WMS.Model.PurchaseManagement;
using WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using Elsa.Models;
using WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs;
namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs
{
    public partial class KnifeCheckOutVM : BaseCRUDVM<KnifeCheckOut>
    {
        public List<string> KnifeManagementKnifeFTempSelected { get; set; }
        public List<ComboSelectListItem> PrintModules { get; set; }
        [Display(Name = "打印模板")]
        public string SelectedPrintModule { get; set; }


        public List<string> KnifeManagementKnifeCheckOutFTempSelected { get; set; }
        public KnifeCheckOutLineKnifeCheckOutDetailListVM KnifeCheckOutLineKnifeCheckOutList { get; set; }
        public KnifeCheckOutLineKnifeCheckOutDetailListVM1 KnifeCheckOutLineKnifeCheckOutList1 { get; set; }
        public KnifeCheckOutLineKnifeCheckOutDetailListVM2 KnifeCheckOutLineKnifeCheckOutList2 { get; set; }
        public KnifeCheckOutVM()
        {
            SetInclude(x => x.CheckOutBy);
            SetInclude(x => x.HandledBy);
            KnifeCheckOutLineKnifeCheckOutList = new KnifeCheckOutLineKnifeCheckOutDetailListVM();
            KnifeCheckOutLineKnifeCheckOutList.DetailGridPrix = "Entity.KnifeCheckOutLine_KnifeCheckOut";
            KnifeCheckOutLineKnifeCheckOutList1 = new KnifeCheckOutLineKnifeCheckOutDetailListVM1();
            KnifeCheckOutLineKnifeCheckOutList1.DetailGridPrix = "Entity.KnifeCheckOutLine_KnifeCheckOut";
            KnifeCheckOutLineKnifeCheckOutList2 = new KnifeCheckOutLineKnifeCheckOutDetailListVM2();
            KnifeCheckOutLineKnifeCheckOutList2.DetailGridPrix = "Entity.KnifeCheckOutLine_KnifeCheckOut";
        }

        protected override void InitVM()
        {
            KnifeCheckOutLineKnifeCheckOutList.CopyContext(this);
            KnifeCheckOutLineKnifeCheckOutList1.CopyContext(this);
            KnifeCheckOutLineKnifeCheckOutList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeCheckOut> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }
        public override void Validate()
        {
            if (Entity.KnifeCheckOutLine_KnifeCheckOut.GroupBy(x => new { x.KnifeCheckOutId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }

            base.Validate();
        }

        public override void DoAdd()
        {
            if (Entity.KnifeCheckOutLine_KnifeCheckOut.Count == 0)
            {
                MSD.AddModelError("", "请输入刀具");
                return;
            }
            base.DoAdd();

        }

        public override void DoEdit(bool updateAllFields = false)
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以修改");
                return;
            }
            base.DoEdit();
        }

        public override void DoDelete()
        {
            if (Entity.Status != KnifeOrderStatusEnum.Open)
            {
                MSD.AddModelError("", "只有开立状态的单据可以删除");
                return;
            }
             base.DoDelete();

        }
        /// <summary>
        /// 领用单审核操作  需要嵌套事务吗? 会被循环调用吗?  不需要 方法从控制器出发 事务都在控制器里 异常需要回滚呢?执行完vm操作都有校验 有异常已实现回滚 vm里不需要考虑事务相关.成立 不考虑事务
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="rr"></param>
        public void DoApproved(IDbContextTransaction tran, ReturnResult<List<BlueToothPrintDataLineReturn>> rr)
        {
            try
            {
                // 重新加载 Entity 并包含导航属性  
                Entity =  DC.Set<KnifeCheckOut>()
                    .Include(x=>x.CheckOutBy).ThenInclude(x=>x.Department).ThenInclude(x=>x.Organization)
                    .Include(x => x.KnifeCheckOutLine_KnifeCheckOut).ThenInclude(line => line.Knife)//.ThenInclude(x=>x) 
                    .FirstOrDefault(x => x.ID == Entity.ID);
                var currentTime = DateTime.Now;
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);

                //校验: 领用单自身的数据:单据类型  单号 领用人 状态 审核时间 单据状态需要未审核  
                if (Entity.DocType == null || string.IsNullOrEmpty(Entity.DocNo) || Entity.CheckOutBy is null || Entity.Status != KnifeOrderStatusEnum.Open || string.IsNullOrEmpty(Entity.DocNo))
                {
                    MSD.AddModelError("", "领用单数据有误 请检查");
                    return;
                }
                //校验:刀具状态需要在库且剩余天数不为0
                foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                {
                    if (line.Knife.Status != KnifeStatusEnum.InStock)
                    {
                        MSD.AddModelError("", $"刀具{line.Knife.SerialNumber}不在库 无法领用");
                        return;
                    }
                    /*if (line.Knife.RemainingUsedDays <= 0)
                    {
                        MSD.AddModelError("", $"刀具{line.Knife.SerialNumber}寿命已归零 无法领用");
                        return;
                    }*/
                }

                //修改阶段
                //组合单的生成逻辑在预处理那里了 这里不需要针对组合单做操作了 所以只是普通的领用 变状态 加操作记录就可以了
                /*switch ((KnifeCheckOutTypeEnum)Entity.DocType)
                {
                    case KnifeCheckOutTypeEnum.NormalCheckOut:

                        //领用单变化 状态 审核时间 经办人  修改时间 修改人 
                        Entity.Status = KnifeOrderStatusEnum.Approved;
                        Entity.HandledBy = handledBy;
                        Entity.ApprovedTime = currentTime;

                        //刀具变化  状态 当前领用人 经办人 最近操作日期
                        foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                        {
                            line.Knife.Status = KnifeStatusEnum.CheckOut;
                            line.Knife.CurrentCheckOutBy = Entity.CheckOutBy;
                            line.Knife.HandledBy = handledBy;
                            line.Knife.LastOperationDate = currentTime;
                        }
                        //操作记录新增 操作记录-领用
                        foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                        {
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                Knife = line.Knife,
                                KnifeId = line.Knife.ID,
                                DocNo = line.KnifeCheckOut.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckOut,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckOutBy,
                                OperationById = Entity.CheckOutBy.ID,
                                HandledBy = Entity.HandledBy,
                                HandledById = handledBy.ID.ToString(),
                                UsedDays = 0,
                                TotalUsedDays = line.Knife.TotalUsedDays,
                                RemainingDays = line.Knife.RemainingUsedDays,
                                CurrentLife = line.Knife.CurrentLife,
                                WhLocationId = line.Knife.WhLocationId,
                                WhLocation = line.Knife.WhLocation,
                                GrindNum = line.Knife.GrindCount,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime
                            });
                        }
                        break;
                    case KnifeCheckOutTypeEnum.CombinedCheckOut:
                        //组合领用 创建并审核一张组合单
                        var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                        var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                        var combineKnifeNo = baseSequenceDefineVM.SetProperty("ItemCategory", "ZHDH").GetSequence("KnifeNoRule", tran);//组合刀号就前缀不同 组合刀号的首拼音
                        knifeCombineVM.Entity = new KnifeCombine
                        {
                            DocNo = baseSequenceDefineVM.GetSequence("KnifeCombineDocNoRule",tran),//组合单号
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            Status = KnifeOrderStatusEnum.Open,//
                            CombineKnifeNo = combineKnifeNo,
                            CheckOutBy =Entity.CheckOutBy,
                            CheckOutById =Entity.CheckOutById,
                            KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                        };
                        //头和明细
                        foreach (KnifeCheckOutLine checkOutLine in Entity.KnifeCheckOutLine_KnifeCheckOut)
                        {
                            knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine
                            {
                                KnifeId = checkOutLine.KnifeId,
                                FromWhLocationId = checkOutLine.FromWhLocationId,
                            });
                        }
                        knifeCombineVM.DoAdd();
                        knifeCombineVM.DoApproved();

                        //领用单变化 状态 审核时间 经办人  修改时间 修改人 
                        Entity.Status = KnifeOrderStatusEnum.Approved;
                        Entity.HandledBy =Entity.HandledBy;
                        Entity.ApprovedTime = currentTime;
                        Entity.UpdateBy = LoginUserInfo.Name;
                        Entity.UpdateTime = currentTime;
                        //刀具变化  状态 当前领用人 经办人 最近操作日期
                        foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                        {
                            line.Knife.Status = KnifeStatusEnum.CheckOut;
                            line.Knife.CurrentCheckOutBy = Entity.CheckOutBy;
                            line.Knife.HandledBy = Entity.HandledBy;
                            line.Knife.LastOperationDate = currentTime;
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                Knife = line.Knife,
                                KnifeId = line.Knife.ID,
                                DocNo = line.KnifeCheckOut.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckOut,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckOutBy,
                                OperationById = Entity.CheckOutBy.ID,
                                HandledBy = Entity.HandledBy,
                                HandledById = handledBy.ID.ToString(),
                                UsedDays = 0,
                                TotalUsedDays = line.Knife.TotalUsedDays,
                                RemainingDays = line.Knife.RemainingUsedDays,
                                CurrentLife = line.Knife.CurrentLife,
                                WhLocationId = line.Knife.WhLocationId,
                                WhLocation = line.Knife.WhLocation,
                                GrindNum = line.Knife.GrindCount,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime
                            });
                        }
                        rr.Entity.Add(new BlueToothPrintDataLineReturn
                        {
                            IsKnife = false,
                            bar = combineKnifeNo,
                            Qty = Entity.KnifeCheckOutLine_KnifeCheckOut.Count.ToString(),
                            CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                        });
                        break;
                    case KnifeCheckOutTypeEnum.CombinePartCheckOut:
                        //配件替换功能  不存在使用场景 替换是整单归还然后整单领用 不会部分领用 部分是业务角度 代码实现就是全部 不开发.
                        //配件都要在审核状态的组合单的组合单行中存在
                        MSD.AddModelError("", $"领用组合属于配件替换 开发中 请等待");
                        return;
                    default:
                        MSD.AddModelError("", $"预期外的领用单类型 请检查");
                        return;

                }*/
                //领用单变化 状态 审核时间 经办人  修改时间 修改人 
                Entity.Status = KnifeOrderStatusEnum.Approved;
                Entity.HandledBy = handledBy;
                Entity.ApprovedTime = currentTime;
                //刀具变化  状态 当前领用人 经办人 最近操作日期
                foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                {
                    line.Knife.Status = KnifeStatusEnum.CheckOut;
                    line.Knife.CurrentCheckOutBy = Entity.CheckOutBy;
                    line.Knife.CurrentCheckOutById = Entity.CheckOutBy.ID;
                    line.Knife.HandledBy = handledBy;
                    line.Knife.HandledById = handledBy.ID.ToString();
                    line.Knife.HandledByName = handledBy.Name;

                    line.Knife.LastOperationDate = currentTime;
                }
                //操作记录新增 操作记录-领用
                foreach (KnifeCheckOutLine line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                {
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        Knife = line.Knife,
                        KnifeId = line.Knife.ID,
                        DocNo = line.KnifeCheckOut.DocNo,
                        OperationType = KnifeOperationTypeEnum.CheckOut,
                        OperationTime = currentTime,
                        OperationBy = Entity.CheckOutBy,
                        OperationById = Entity.CheckOutBy.ID,
                        HandledBy = Entity.HandledBy,
                        HandledById = handledBy.ID.ToString(),
                        HandledByName = handledBy.Name,
                        UsedDays = 0,
                        TotalUsedDays = line.Knife.TotalUsedDays,
                        RemainingDays = line.Knife.RemainingUsedDays,
                        CurrentLife = line.Knife.CurrentLife,
                        WhLocationId = line.Knife.WhLocationId,
                        WhLocation = line.Knife.WhLocation,
                        GrindNum = line.Knife.GrindCount,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = currentTime
                    });
                }

                if (!MSD.IsValid)
                {
                    MSD.AddModelError("", "msd校验不通过 本次审核领用单失败");
                    return;
                }

            }
            catch (Exception ex)
            {
                MSD.AddModelError("", $"审核领用单失败 异常信息: {ex.Message}");
                return;
            }

        }

        /// <summary>
        /// 刀具领用预处理  根据接口信息初始化领用单vm 要对新刀做领用入台账的操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        /// <param name="rr"></param>
        public void DoInitByInfo(KnifeCheckOutInputInfo info, IDbContextTransaction tran, ReturnResult<List<BlueToothPrintDataLineReturn>> rr)//
        {
            try
            {
                #region 校验登录存储地点
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return ;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                #endregion
                #region 输入参数info非空校验 
                if (!Enum.IsDefined(typeof(KnifeCheckOutTypeEnum), info.CheckOutType))
                {
                    MSD.AddModelError("", "领用类型有误 请检查输入");
                    return;
                }
                if (string.IsNullOrEmpty(info.CheckOutByID))
                {
                    MSD.AddModelError("", "领用人不存在 请检查输入");
                    return;
                }
                if (info.Knifes.Count == 0)
                {
                    MSD.AddModelError("", "刀具列表为空 请扫描刀具");
                    return;
                }
                if (info.Knifes.Any(x => x.IsNew==false && string.IsNullOrEmpty(x.KnifeId)))//老刀没有刀id
                {
                    MSD.AddModelError("", "刀具领用输入参数不足");
                    return;
                }
                if (info.Knifes.Any(x => x.IsNew==true && (string.IsNullOrEmpty(x.bar)||string.IsNullOrEmpty(x.SerialNumber))))//库存信息bar或者SerialNumber为空
                {
                    MSD.AddModelError("", "刀具领用输入参数不足");
                    return;
                }
                #endregion
                #region 校验信息有效性
                var ledgerKnifesIds = info.Knifes.Where(x => !x.IsNew).Select(x => Guid.Parse(x.KnifeId)).ToList();
                var ledgerKnifes = DC.Set<Knife>()
                    .Include(x => x.CurrentCheckOutBy).ThenInclude(x => x.Department).ThenInclude(x => x.Organization)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                    .Where(x => ledgerKnifesIds.Contains(x.ID))
                    .ToList();
                if(ledgerKnifes.Count!= ledgerKnifesIds.Count)
                {
                    MSD.AddModelError("", $"台账刀具信息有变动 请刷新重试");
                    return;
                }
                var inventorySerialNumbers = info.Knifes.Where(x => x.IsNew).Select(x => x.SerialNumber).ToList();
                var inventorys = DC.Set<BaseInventory>()//单次复杂查询 优于 多次简单查询
                    .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Where(x => x.IsAbandoned != true && x.Qty != 0)//未失效 不为零 
                    .Where(x => inventorySerialNumbers.Contains(x.SerialNumber))
                    .ToList();
                if (inventorySerialNumbers.Count != inventorys.Count)
                {
                    MSD.AddModelError("", $"库存信息有变动 请刷新重试");
                    return;
                }
                var inventoryItemCodes = inventorys.Select(x => x.ItemMaster.Code).Distinct().ToList();//未必有值 可能是空
                //不用66的  用20的
                //var inventoryItemLivesReturn = GetKnifesLivesSV.GetKnifesLives_1(inventoryItemCodes);//没有库存信息这里的刀具寿命信息就是false和空值 但是这里不会报错 下面对库存信息非空判定后的操作才会报错
                var tempKnifeVM = Wtm.CreateVM<KnifeVM>();
                var inventoryItemLivesReturn = tempKnifeVM.GetKnifesLives_2(inventoryItemCodes);


                var inventoryItemLives = new List<U9KnifeLivesReturn_Line>();//库存信息寿命
                var notLedgerItemLives = new List<U9KnifeLivesReturn_Line>();//不进台账库存信息寿命
                var notLedgerItemCodes = new List<string>();
                var notLedgerInventorys = new List<BaseInventory>();//不进台账库存信息  库存信息变化有用
                var inledgerItemLives = new List<U9KnifeLivesReturn_Line>();//进台账库存信息寿命
                var inLedgerItemCodes = new List<string>();
                var inLedgerInventorys = new List<BaseInventory>();//进台账库存信息(可能1可能n  不能直接用)
                var knifeCombinesInfo_combineType2 = new List<(string BarCode,List<BaseInventory> Line)>();//生成组合单情况2用的  记录库存拆零信息
                var knifes_combineType2 = new List<Knife>();//生成组合单情况2用的 记录刀具信息
                var knifeCombine_combineType3 = new KnifeCombine();//生成组合领用情况3用的 原来的组合单
                var inLedgerTotalSplitInventorys = new List<BaseInventory>();//进台账的高价值的库存信息(数量为1) 库存信息变化有用
                string U9MiscShipDocNo_High = null;
                string U9MiscShipDocNo_Low = null;
                if (ledgerKnifesIds != null && ledgerKnifesIds.Count != 0)//台账刀非空
                {
                    #region 台账刀校验 台账刀有效性校验 存在/状态/库位锁定/不良退回/非当前登录存储地点
                    if (ledgerKnifesIds is not null && ledgerKnifes is not null && ledgerKnifes.Count != ledgerKnifesIds.Count
                        || ledgerKnifesIds is not null && ledgerKnifes is null)
                    {
                        MSD.AddModelError("", $"存在无效刀具");
                        return;
                    }
                    foreach (var k in ledgerKnifes)
                    {
                        if (ledgerKnifesIds != null && !ledgerKnifesIds.Contains(k.ID))
                        {
                            MSD.AddModelError("", $"刀具列表中存在无效id 请检查输入");
                            return;
                        }
                        if (k.Status != KnifeStatusEnum.InStock)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}不在库 不可领用");
                            return;
                        }
                        if (k.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}已锁定 不可进行领用操作");
                            return;
                        }
                        if (k.WhLocation.IsEffective == EffectiveEnum.Ineffective || k.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}无效");
                            return ;
                        }
                        if (k.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可操作");
                            return;
                        }
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前存储地点 不可领用");
                            return;
                        }
                    }
                    #endregion
                }
                if (inventorySerialNumbers != null && inventorySerialNumbers.Count != 0)//库存信息非空
                {
                    #region 库存信息处理 库存信息有效性校验 存在/状态/库位锁定/不良退回/非当前登录存储地点/料品主分类不是刀具
                    if (inventorySerialNumbers is not null && inventorys is not null && inventorySerialNumbers.Count != inventorys.Count
                        || inventorySerialNumbers is not null && inventorys is null)
                    {
                        MSD.AddModelError("", $"存在无效库存信息");
                        return;
                    }
                    foreach (var i in inventorys)
                    {
                        if (inventorySerialNumbers != null && !inventorySerialNumbers.Contains(i.SerialNumber))
                        {
                            MSD.AddModelError("", $"存在无效库存信息 请检查输入");//Contains获取的  这里基本不可能执行到
                            return;
                        }
                        if (i.FrozenStatus == FrozenStatusEnum.Freezed)
                        {
                            MSD.AddModelError("", $"库存信息{i.SerialNumber}已冻结 不可领用");
                            return;
                        }
                        if (i.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"库存信息{i.SerialNumber}所在库位{i.WhLocation.Code}已锁定 不可进行领用操作");
                            return;
                        }
                        if (i.WhLocation.IsEffective == EffectiveEnum.Ineffective ||i.WhLocation.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库存信息{i.SerialNumber}所在库位{i.WhLocation.Code}不可用");
                            return;
                        }
                        
                        if (i.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此库存信息所在库位为不良退回库位 不可获取刀具信息");
                            return;
                        }
                        if (i.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{i.SerialNumber}不属于当前存储地点 不可领用");
                            return;
                        }
                        if (!i.ItemMaster.ItemCategory.Code.StartsWith("17"))
                        {
                            MSD.AddModelError("", $"条码{i.SerialNumber}的物料并非刀具 不可用");
                            return;
                        }
                    }
                    #endregion
                    if (inventoryItemLivesReturn.Success == false)//库存信息非空时 对涉及料品寿命做校验
                    {
                        MSD.AddModelError("", "" + inventoryItemLivesReturn.Message);
                        return;
                    }
                    inventoryItemLives = inventoryItemLivesReturn.Data;
                    //生效字段
                    foreach(var temp in inventoryItemLives)
                    {
                        if (temp.IsActive == false)
                        {
                            MSD.AddModelError("", $"{temp.ItemMaster}在U9刀具寿命管理里未生效 无法用于台账的刀具领用");
                            return;
                        }
                    }
                    //不进台账的料品信息和库存信息
                    notLedgerItemLives = inventoryItemLives.Where(x => x.LedgerIncluded == false).ToList();
                    notLedgerItemCodes = notLedgerItemLives.Select(x => x.ItemMaster).ToList();
                    notLedgerInventorys = inventorys.Where(x => notLedgerItemCodes.Contains(x.ItemMaster.Code)).ToList();
                    //进台账的料品信息和库存信息
                    inledgerItemLives = inventoryItemLives.Where(x => x.LedgerIncluded == true).ToList();
                    inLedgerItemCodes = inledgerItemLives.Select(x => x.ItemMaster).ToList();
                    inLedgerInventorys = inventorys.Where(x => inLedgerItemCodes.Contains(x.ItemMaster.Code)).ToList();
                }


                //类型为组合领用做校验:非台账库存信息不计入在内
                //1.台账刀/库存信息都为1 ZHDH(必定两条及以上 不然报错)
                //2.只有库存信息(数量必定2及以上 不然报错)
                //3.一个组合单的原样领用
                //4.不进台账的杂发 其他都没有
                int combineType = 0;//默认 不是组合领用类型
                if (info.CheckOutType == KnifeCheckOutTypeEnum.CombinedCheckOut)
                {
                    //全空的情况在非空检验里已经排除掉了
                    //4.不进台账的杂发
                    if (ledgerKnifes.Count == 0 && inLedgerInventorys.Count == 0)
                    {
                        combineType = 4;
                    }
                    //3.都是一张组合单的刀具信息 的逻辑判定:
                    //根据刀具获取审核状态下的组合行
                    //组合行和刀一一对应(数量不等说明存在刀不是组合单行的)
                    //组合行都来自同一张组合单  而且数量和这张组合单的行数量相等(组合单3条 扫了两条就不是同一张 要关闭原来的生成新的组合单号)
                    if (ledgerKnifes.Count != 0 && inLedgerInventorys.Count == 0)
                    {
                        var combineLines_temp = DC.Set<KnifeCombineLine>()
                            .Include(x=>x.KnifeCombine)
                            .Include(x=>x.Knife)
                            .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)
                            .Where(x => ledgerKnifes.Contains(x.Knife))
                            .ToList();
                        if(combineLines_temp.Count== ledgerKnifes.Count)
                        {
                            var combines_temp = combineLines_temp.Select(x => x.KnifeCombine).Distinct().ToList();
                            if (combines_temp.Count == 1)
                            {
                                if( combines_temp[0].KnifeCombineLine_KnifeCombine.Count == ledgerKnifes.Count)
                                {
                                    combineType = 3;
                                    knifeCombine_combineType3 = combines_temp[0];
                                }
                            }
                        }
                    }
                    //2.只有库存信息(数量必定2及以上) 的逻辑判定:
                    if (ledgerKnifes.Count == 0 && inLedgerInventorys.Count != 0)
                    {
                        if (inLedgerInventorys.All(x => x.Qty >= 2))
                        {
                            combineType = 2;
                        }
                    }
                    //1.台账刀/库存信息都为1 ZHDH(必定两条及以上 不然报错)的逻辑判定
                    //只有台账刀  只有库存刀   库存台账混合
                    if (ledgerKnifes.Count != 0 && inLedgerInventorys.Count == 0)
                    {
                        if (ledgerKnifes.Count>=2&& combineType==0)//3组合单原样领取的情况也会判定到 要排除
                        {
                            combineType = 1;
                        }
                    }
                    if (ledgerKnifes.Count == 0 && inLedgerInventorys.Count != 0)
                    {
                        if (inLedgerInventorys.All(x => x.Qty == 1)&& inLedgerInventorys.Count >= 2)
                        {
                            combineType = 1;
                        }
                    }
                    if (ledgerKnifes.Count != 0 && inLedgerInventorys.Count != 0)
                    {
                        if (inLedgerInventorys.All(x => x.Qty == 1) )
                        {
                            combineType = 1;
                        }
                    }
                    //不属于1/2/3/4   报错
                    if (combineType == 0)
                    {
                        MSD.AddModelError("", "组合领用库存信息时 除了低值外只允许两种情况:1数量为1的多条(至少2)的台账刀/库存信息 2.数量大于1的库存信息(不能有台账刀)");
                        return;
                    }
                }
                #endregion
                #region 领用单基本信息生成
                var currentTime = DateTime.Now;//当前时间
                var checkOutBy = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .FirstOrDefault(x => x.ID.ToString() == info.CheckOutByID);//领用人
                if (checkOutBy is null)
                {
                    MSD.AddModelError("", "未找到有效的领用人");
                    return;
                }
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
                //领用单单号
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeCheckOutDocNo = DocNoVm.GetSequence("KnifeCheckOutRule", tran);//生成领用单单号
                //领用单vm的基本信息进行赋值
                Entity.DocType = info.CheckOutType;
                Entity.CheckOutBy = checkOutBy;
                Entity.CheckOutById = checkOutBy.ID;
                Entity.DocNo = KnifeCheckOutDocNo;
                Entity.HandledBy = handledBy;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.WareHouseId = whid;
                Entity.KnifeCheckOutLine_KnifeCheckOut = new List<KnifeCheckOutLine>();
                #endregion
                #region 刀具序列号唯一校验(不良退回后移库后再领要报错 此序列号已使用 不可再次领用)
                /*  //不进台账的和组合刀号还能否使用这个序列号?  暂定:不能  (不进台账能用的话就用inLedgerInventorys进行校验  组合刀号要的话用barcode 不用SerialNumber就行)
                //进台账的库存信息
                if (inLedgerInventorys!=null && inLedgerInventorys.Count != 0)
                {
                    var inLedgerInventorySerialNumbers = inLedgerInventorys.Select(x => x.SerialNumber);
                    var sameKnife = DC.Set<Knife>()
                        .Where(x => inLedgerInventorySerialNumbers.Contains(x.SerialNumber))
                        .ToList();
                    if (sameKnife.Count != 0)
                    {
                        MSD.AddModelError("", $"{sameKnife[0].SerialNumber}此序列号已使用 不可再次使用");
                        return;
                    }
                }
                 */
                //所有库存信息 不管进不进台账都要保证在台账里唯一序列号 包括作为组合刀号使用时 也是在台账中不存在才可以作为组合刀号使用 但是组合刀号两次是没关系的
                if (inventorys != null && inventorys.Count != 0)
                {
                    var sameKnife = DC.Set<Knife>()
                        .Where(x => inventorySerialNumbers.Contains(x.SerialNumber))
                        .ToList();
                    if (sameKnife.Count != 0)
                    {
                        MSD.AddModelError("", $"{sameKnife[0].SerialNumber}此序列号已使用 不可再次使用");
                        return;
                    }
                }
                #endregion

                //操作阶段 分开主要是为了U9等不可逆的操作
                //台账刀和库存信息分开处理  先处理台账刀 
                #region 台账刀非空  台账刀进行处理
                if (ledgerKnifesIds!=null && ledgerKnifesIds.Count != 0)
                {
                    #region 台账刀处理 除了原样的组合领用之外  都会解除组合单
                    if (combineType != 3)
                    {
                        var ledgerKnifeCombineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine)
                        .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)//组合单是已核准状态
                        .Where(x => ledgerKnifesIds.Contains((Guid)x.KnifeId))//刀id和组合行的刀id匹配 
                        .ToList();
                        if (ledgerKnifeCombineLines != null) //涉及的组合单不一定有 有的话关闭 
                        {
                            var ledgerKnifeCombines = ledgerKnifeCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                            foreach (var ledgerKnifeCombine in ledgerKnifeCombines)
                            {
                                ledgerKnifeCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                                ledgerKnifeCombine.CloseTime = currentTime;
                            }
                        }
                    }
                    
                    #endregion
                    #region 台账刀处理 台账刀加入领用行
                    foreach (var knife in ledgerKnifes)
                    {
                        Entity.KnifeCheckOutLine_KnifeCheckOut.Add(new KnifeCheckOutLine
                        {
                            Knife = knife,
                            KnifeId = knife.ID,
                            KnifeCheckOutId = Entity.ID,
                            FromWhLocation = knife.WhLocation,
                            FromWhLocationId = knife.WhLocationId,
                        });
                    }
                    #endregion
                }
                #endregion
                //台账刀和库存信息分开处理  现在处理库存信息
                #region 库存信息非空 库存信息处理
                if (inventorySerialNumbers != null && inventorySerialNumbers.Count != 0)
                {
                    #region 库存信息处理 不进台账的库存信息不为零时 杂发掉
                    if(notLedgerInventorys != null&& notLedgerInventorys.Count != 0)
                    {
                        // U9生成杂发单  低值刀具杂发
                        string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                        string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                        U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//刀具领用固定组织
                        U9Return<MiscShipmentData> u9Return = (U9Return<MiscShipmentData>)apiHelper.CreateAndApprovedMiscShipment(handledBy_operator, notLedgerInventorys, checkOutBy, true);//低值刀具杂发
                        if (!u9Return.Success)
                        {
                            MSD.AddModelError("", "U9创建并审核杂发单失败:" + u9Return.Msg);
                            return;
                        }
                        U9MiscShipDocNo_Low = u9Return.Entity.DocNo;
                    }
                    #endregion
                    #region 库存信息处理 进台账的库存信息不为零时  拆分成数量为1的库存信息集合 (组合领用组合单) 杂发 返回杂发行id-生成刀信息  找到采购单行id-建档操作信息 
                    if (inLedgerInventorys != null && inLedgerInventorys.Count != 0)
                    {
                        var inventorySplitSingleVM = Wtm.CreateVM<InventorySplitSingleVM>();//用于拆分的vm
                        //遍历进台账的库存信息  拆分数量大于1的库存信息 生成打印信息 生成用于杂发的库存信息    组合放后面
                        foreach (var inLedgerInventory in inLedgerInventorys)
                        {
                            if (inLedgerInventory.Qty == 1)
                            {
                                //库存信息结果:数量为1的库存信息直接加入
                                inLedgerTotalSplitInventorys.Add(inLedgerInventory);
                                //打印信息:数量为1的库存信息直接加入
                                rr.Entity.Add(new BlueToothPrintDataLineReturn
                                {
                                    IsKnife = true,
                                    IsNew = true,
                                    SerialNumber = inLedgerInventory.SerialNumber,
                                    bar = $"{(int)inLedgerInventory.ItemSourceType}@{inLedgerInventory.ItemMaster.Code}@{(int)inLedgerInventory.Qty}@{inLedgerInventory.SerialNumber}",
                                    ItemCode = inLedgerInventory.ItemMaster.Code,
                                    ItemName = inLedgerInventory.ItemMaster.Name,
                                    Specs = inLedgerInventory.ItemMaster.SPECS,
                                    Qty = inLedgerInventory.Qty.ToString(),//1
                                    CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                                    NotG ="",
                                });
                            }
                            else if(inLedgerInventory.Qty > 1)//组合领用的情况再上面已经校验过了 出现大于1的必定是情况2 只有库存信息且必定大于1  此时可以直接生成组合单
                            {
                                var tempCombineKnifeNo = $"{(int)inLedgerInventory.ItemSourceType}@{inLedgerInventory.ItemMaster.Code}@{inLedgerInventory.Qty.TrimZero()}@{inLedgerInventory.SerialNumber}";
                                //拆分掉成数量为1的list 
                                var tempSplitInventorys =inventorySplitSingleVM.Save(inLedgerInventory.ID,true,"");
                                if (!MSD.IsValid)
                                {
                                    return ;
                                }
                                //加到一会要生成杂发单里的库存信息list里去
                                inLedgerTotalSplitInventorys.AddRange(tempSplitInventorys);
                                //加到一会要生成组合单的list里
                                knifeCombinesInfo_combineType2.Add(new(tempCombineKnifeNo, tempSplitInventorys));
                                //打印信息 这里生成的刀具不需要组合刀号  只是是原有的条码
                                foreach (var tempInventory in tempSplitInventorys)
                                {
                                    rr.Entity.Add(new BlueToothPrintDataLineReturn
                                    {
                                        IsKnife = true,
                                        IsNew = true,
                                        SerialNumber = tempInventory.SerialNumber,
                                        bar = $"{(int)tempInventory.ItemSourceType}@{tempInventory.ItemMaster.Code}@{(int)tempInventory.Qty}@{tempInventory.SerialNumber}",
                                        ItemCode = tempInventory.ItemMaster.Code,
                                        ItemName = tempInventory.ItemMaster.Name,
                                        Specs = tempInventory.ItemMaster.SPECS,
                                        Qty = tempInventory.Qty.ToString(),//1
                                        CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                                        NotG = "",
                                    });
                                }
                            }
                        }


                        // U9生成杂发单   台账刀具杂发
                        string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                        string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                        U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//刀具领用固定组织
                        U9Return<MiscShipmentData> u9Return = (U9Return<MiscShipmentData>)apiHelper.CreateAndApprovedMiscShipment(handledBy_operator, inLedgerTotalSplitInventorys, checkOutBy, false);//台账高价值刀具杂发
                        if (!u9Return.Success)
                        {
                            MSD.AddModelError("", "U9创建并审核杂发单失败:" + u9Return.Msg);
                            return;
                        }
                        U9MiscShipDocNo_High = u9Return.Entity.DocNo;
                        //杂发成功 for遍历 带着杂发行id 寿命表 新建刀具  刀具建档记录(还需要对应的采购单行)      每把刀的生成 然后组进领用单行 减掉库存信息 库存流水 刀具领用记录不在此处生成 在审核的时候生成
                        foreach (var inLedgerSplitInventory in inLedgerTotalSplitInventorys)
                        {
                            //获取关联的采购收货行id 需要条码是由采购收货行或者库存拆分操作生成  允许空
                            string U9RcvLineId = GetRcvLineIdBySerialNumber(inLedgerSplitInventory.SerialNumber);
                            if (!MSD.IsValid)//|| string.IsNullOrEmpty(U9RcvLineId) 盘点或者其他方式的进入方式也允许
                            {
                                MSD.AddModelError("", "新刀获取原采购行失败:" + MSD.GetFirstError());
                                return;
                            }
                            //获取对应的杂发单行id
                            string U9MiscShipLineId = u9Return.Entity.Lines.Where(x => x.KnifeNo == inLedgerSplitInventory.SerialNumber).Select(x => x.ID).FirstOrDefault();
                            //获取寿命信息
                            var inledgerItemLive = inledgerItemLives.Where(x => x.ItemMaster == inLedgerSplitInventory.ItemMaster.Code).FirstOrDefault();
                            //新建刀具  在新建的时候就有建档的刀具操作记录新建了
                            var knifeVM = Wtm.CreateVM<KnifeVM>();
                            var line = inLedgerSplitInventory;
                            knifeVM.Entity = new Knife() 
                            {
                                CreatedDate = currentTime,
                                SerialNumber = line.SerialNumber,
                                BarCode = $"{(int)line.ItemSourceType}@{line.ItemMaster.Code}@{line.Qty.TrimZero()}@{line.SerialNumber}",
                                Status = KnifeStatusEnum.InStock,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                HandledByName = Entity.HandledBy.Name,
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
                            knifes_combineType2.Add(knifeVM.Entity);//刀具信息记下来 待会组合用
                            //所有进台账的 杂发成功的  生成领用单行记录
                            Entity.KnifeCheckOutLine_KnifeCheckOut.Add(new KnifeCheckOutLine()
                            {
                                Knife = knifeVM.Entity,
                                KnifeId = knifeVM.Entity.ID,
                                FromWhLocation = line.WhLocation,
                                FromWhLocationId = line.WhLocationId,
                                IsNewLine = "1",
                            });

                            //理论上来说这里可以进行库存流水/库存信息变化  不过为了组合单的生成 库存信息变化后置 不能在这里 不然list.add是浅拷贝 这里清除了库存信息就不能组成原条码了
                        }

                    }
                    #endregion
                }

                #endregion

                #region 组合单生成 通过组合类型和list的count

                //4 不进台账杂发-无组合单        3 原样领用-无组合单?要更改行上的库位信息 要考虑       0 不是组合领用-无组合单   只考虑2/1
                if(combineType==1) //一张ZHDH 数量在检验时已经做过了 这里说明是正确的 生成组合单就行
                {
                    //生成组合刀号  台账刀+库存刀
                    var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                    var combineKnifeNo = baseSequenceDefineVM.SetProperty("ItemCategory", "ZHDH").GetSequence("KnifeNoRule", tran);//组合刀号就前缀不同 组合刀号的首拼音
                    var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                    knifeCombineVM.Entity = new KnifeCombine()
                    {
                        DocNo = baseSequenceDefineVM.GetSequence("KnifeCombineDocNoRule", tran),//组合单号
                        HandledBy = Entity.HandledBy,
                        HandledById = Entity.HandledById,
                        Status = KnifeOrderStatusEnum.Approved,//直接已审核
                        ApprovedTime = currentTime,//currentTime.ToString("yyyyy-MM-dd HH:mm:ss"),
                        CombineKnifeNo = combineKnifeNo,
                        CheckOutBy = Entity.CheckOutBy,
                        CheckOutById = Entity.CheckOutById,
                        WareHouseId = whid,
                        KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                    };
                    foreach(var line in Entity.KnifeCheckOutLine_KnifeCheckOut)
                    {
                        knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine() 
                        { 
                            KnifeId = line.KnifeId,
                            FromWhLocationId = line.FromWhLocationId,
                        });

                    }
                    knifeCombineVM.DoAdd();//直接生成已审核的组合单 一张 刀号 ZHDH

                    //ZHDH的蓝牙打印
                    rr.Entity.Add(new BlueToothPrintDataLineReturn()
                    {
                        IsKnife = false,
                        IsNew = true,
                        SerialNumber = "",
                        ItemCode = "",
                        ItemName = "",
                        Specs = "",
                        bar = $"{combineKnifeNo}",
                        Qty = Entity.KnifeCheckOutLine_KnifeCheckOut.Count.ToString(),//
                        CurrentDate = currentTime.ToString("yyyy-MM-dd"),
                        NotG = "not",
                    });


                }
                if(combineType==2)//2.只有库存信息(数量必定2及以上 不然报错)  在这里就说明已经校验过了是组合且没有台账刀 都是大于2的库存信息(不算不进入台账的)
                {
                    //生成已审核的组合单 组合刀号是条码
                    var baseSequenceDefineVM = Wtm.CreateVM<BaseSequenceDefineVM>();
                    
                    foreach (var (tempCombineKnifeNo, lines) in knifeCombinesInfo_combineType2)
                    {
                        var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                        knifeCombineVM.Entity = new KnifeCombine()
                        {
                            DocNo = baseSequenceDefineVM.GetSequence("KnifeCombineDocNoRule", tran),//组合单号
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            Status = KnifeOrderStatusEnum.Approved,//直接已审核
                            ApprovedTime = currentTime,//currentTime.ToString("yyyyy-MM-dd HH:mm:ss"),
                            CombineKnifeNo = tempCombineKnifeNo,
                            CheckOutBy = Entity.CheckOutBy,
                            CheckOutById = Entity.CheckOutById,
                            WareHouseId = whid,
                            KnifeCombineLine_KnifeCombine = new List<KnifeCombineLine>(),
                        };
                        foreach(var line in lines)
                        {
                            var tempKnife = knifes_combineType2.Where(x => x.SerialNumber == line.SerialNumber).FirstOrDefault();
                            knifeCombineVM.Entity.KnifeCombineLine_KnifeCombine.Add(new KnifeCombineLine()
                            {
                                Knife = tempKnife,
                                KnifeId = tempKnife.ID,
                                FromWhLocation = tempKnife.WhLocation,
                                FromWhLocationId = tempKnife.WhLocationId,
                            });
                        }
                        knifeCombineVM.DoAdd();//
                    }

                }
                if(combineType==3)//原样领用  领用库位用当前库位 归还库位置空 必定不包含进台账的库存信息  只有台账刀
                {
                    //重新加载组合单
                    var originalKnifeCombine = DC.Set<KnifeCombine>()
                        .Include(x => x.KnifeCombineLine_KnifeCombine).ThenInclude(x => x.Knife).ThenInclude(x => x.WhLocation)
                        .Where(x => x.ID == knifeCombine_combineType3.ID)
                        .FirstOrDefault();
                    originalKnifeCombine.CheckOutById = checkOutBy.ID;
                    originalKnifeCombine.CheckOutBy = checkOutBy;
                    foreach (var line in originalKnifeCombine.KnifeCombineLine_KnifeCombine)
                    {
                        line.FromWhLocation = line.Knife.WhLocation;
                        line.FromWhLocationId = line.Knife.WhLocation.ID;//领用库位改为刀具当前库位
                        line.ToWhLocation = null;
                        line.ToWhLocationId = null;//归还库位改为空
                    }
                    
                }
                #endregion

                #region 库存信息变化+流水日志变化
                //首先原来的库存信息里 进入台账部分的大于2的已经在拆分时拆掉了  这里都是进入台账库存为1的库存信息(原有的和新生成的)
                //不进入台账的  库存信息还是原来的  所以这里两次for 一次是生成高价值杂发的的list  一次是低值 不进台账的list
                foreach (var inLedgerTotalSplitInventory in inLedgerTotalSplitInventorys)
                {
                    //新增库存流水
                    this.CreateInvLog(OperationTypeEnum.InventoryOtherShipCreate, U9MiscShipDocNo_High, inLedgerTotalSplitInventory.ID, null, -inLedgerTotalSplitInventory.Qty, null);//流水记录  类型其他出库  出完
                    //库存数量归零并作废 
                    inLedgerTotalSplitInventory.Qty = 0;
                    inLedgerTotalSplitInventory.IsAbandoned = true;
                }
                
                foreach (var notLedgerInventory in notLedgerInventorys)
                {
                    //新增库存流水
                    this.CreateInvLog(OperationTypeEnum.InventoryOtherShipCreate, U9MiscShipDocNo_Low, notLedgerInventory.ID, null, -notLedgerInventory.Qty, null);//流水记录  类型其他出库  出完
                    //库存数量归零并作废 
                    notLedgerInventory.Qty = 0;
                    notLedgerInventory.IsAbandoned = true;
                }
                #endregion


               
                DC.SaveChanges();

            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:"+e.Message);
                return;
            }
            



        }
        /// <summary>
        /// 通过序列号获取对应的采购收货行的行id(如果存在) 否则返回""
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public  string GetRcvLineIdBySerialNumber(string serialNumber)
        {
            //通过库存信息获取原来的采购收货行id
            try
            {
                string U9RcvLineId = null;
                var serialNumber_temp = serialNumber;
                var log_temp = DC.Set<BaseInventoryLog>()
                    .Include(x => x.SourceInventory)
                    .Include(x => x.TargetInventory)
                    .Where(x => x.TargetInventory.SerialNumber == serialNumber_temp)
                    .OrderBy(x => x.CreateTime)   //升序 取最早的记录
                    .FirstOrDefault();
                /*if (log_temp == null)
                {
                    MSD.AddModelError("", $"未找到{serialNumber}的库存日志信息");
                    return null;
                }*/
                
                if (log_temp!= null)
                {
                    //找来源的采购收货收货  不一定有
                    while (log_temp.OperationType != OperationTypeEnum.PurchaseReceivementReceive)//类型不是采购收货收货
                    {
                        if (log_temp.OperationType == OperationTypeEnum.InventorySplitCreate|| log_temp.OperationType == OperationTypeEnum.InventorySplitSingleCreate)//如果是拆行或者拆零拆出来的  找到上一层继续循环
                        {
                            serialNumber_temp = log_temp.SourceInventory.SerialNumber;//可能会出问题 有目标库存信息未必有来源库存信息 会空指针异常 | 不会有问题  已经是判定为拆零/拆分单了 一定会有来源库存信息  
                            log_temp = DC.Set<BaseInventoryLog>()
                                .Include(x => x.SourceInventory)
                                .Include(x => x.TargetInventory)
                                .Where(x => x.TargetInventory.SerialNumber == serialNumber_temp)
                                .OrderBy(x => x.CreateTime)   //升序 取最早的记录
                                .FirstOrDefault();
                            if(log_temp == null)
                            {
                                break;
                            }
                            continue;
                        }
                        //其他存储地点调入/重新打条码的杂收/重新打条码的盘盈  不能定位 没有采购收货行
                        log_temp = null;
                        break;
                    }
                    //出了循环 不为空 说明是采购收货收货类型的日志记录了
                    if (log_temp != null)
                    {
                        var RcvLineId = DC.Set<BaseDocInventoryRelation>().Where(x => x.InventoryId == log_temp.TargetInventoryId && x.DocType == DocTypeEnum.PurchaseReceivement).FirstOrDefault().BusinessLineId;
                        U9RcvLineId = DC.Set<PurchaseReceivementLine>().Where(X => X.ID == RcvLineId).FirstOrDefault().SourceSystemId;//采购收货行id对应每张条码
                        return U9RcvLineId;
                    }//出了循环 为空 说明是没有采购收货
                    else 
                    {
                        return "";
                    }


                }
                    
                //出了循环/没进循环  为空 说明是其他方式进来的
                //if (log_temp == null)
                //{
                    return "";
                //}
                
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常:" + e.Message);
                return null;
            }

        }

        /// <summary>
        /// 期初刀具信息导入  默认信息正确 不完整考虑报错  尽量少用
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void BeginningOfPeriodKnifeCheckOut(List<BeginningOfPeriodKnifeCheckOutInputInfo> info, IDbContextTransaction tran)
        {
            try
            {
                #region  info数据预处理与校验
                var handledByNames = info.Select(x => x.HandledByName).Distinct().ToList();
                var checkOutByNames = info.Select(x => x.CheckOutByName).Distinct().ToList();
                var barCodes_info = info.Select(x => x.BarCode).Distinct().ToList();
                var barCodes_SerialNumbers_info = barCodes_info
                    .Select(x => x.Split("@"))
                    .Where(p => p.Length == 4)
                    .Select(p => p[3])
                    .ToList();
                var currentTime = DateTime.Now;//当前时间
                var checkOutBys = DC.Set<BaseOperator>()
                    .Include(x => x.Department).ThenInclude(x => x.Organization)
                    .Where(x => checkOutByNames.Contains(x.Name) && x.IsValid == true && x.Department.Organization.Code == "0410")//有效且刀具中心
                    .ToList();//领用人
                if (checkOutBys.Count!= checkOutByNames.Count )
                {
                    MSD.AddModelError("", "存在无效领用人 请检查");
                    return;
                }
                var handledBys = DC.Set<FrameworkUser>()
                    .Where(x => handledByNames.Contains(x.Name) && x.IsValid == true)
                    .ToList();//仓管员
                if (handledBys.Count != handledByNames.Count )
                {
                    MSD.AddModelError("", "存在无效仓管员 请检查");
                    return;
                }
                var handledBy_operators = DC.Set<BaseOperator>()
                    .Include(x => x.Department).ThenInclude(x => x.Organization)
                    .Where(x => handledByNames.Contains(x.Name) && x.IsValid == true && x.Department.Organization.Code == "0410")
                    .ToList();//仓管员_业务员类型
                if (handledBy_operators.Count != handledByNames.Count)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                var baseInventorys_info = DC.Set<BaseInventory>()
                    .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Where(x => barCodes_SerialNumbers_info.Contains(x.SerialNumber) && x.Qty!=0&&x.IsAbandoned == false)
                    .ToList();//库存信息
                if (baseInventorys_info.Count != barCodes_SerialNumbers_info.Count)
                {
                    MSD.AddModelError("", "存在无效库存信息 请检查");
                    return;
                }
                //9/16路分开导 第一把刀就是这一次调用的所有的存储地点
                var whid = baseInventorys_info[0].WhLocation.WhArea.WareHouse.ID;
                #region 刀具序列号唯一校验(不良退回后移库后再领要报错 此序列号已使用 不可再次领用)
                var sameKnife = DC.Set<Knife>()
                        .Where(x => barCodes_SerialNumbers_info.Contains(x.SerialNumber))
                        .ToList();
                if (sameKnife.Count != 0)
                {
                    MSD.AddModelError("", $"{sameKnife[0].SerialNumber}此序列号已使用 不可再次使用");
                    return;
                }
                #endregion
                var EntityInfos = new List <BeginningOfPeriodKnifeCheckOutInfo> ();
                foreach(var tempLine in info)
                {
                    var tempHandledBy = handledBys.FirstOrDefault(x => x.Name == tempLine.HandledByName);
                    var tempCheckOutBy = checkOutBys.FirstOrDefault(x => x.Name == tempLine.CheckOutByName);
                    var tempHandledBy_Operator = handledBy_operators.FirstOrDefault(x => x.Name == tempLine.HandledByName);
                    var tempBaseInventory = baseInventorys_info.FirstOrDefault(x => (((int)x.ItemSourceType).ToString() + "@" + x.ItemMaster.Code + "@" + ((int)x.Qty).ToString() + "@" + x.SerialNumber) == tempLine.BarCode);
                    if(tempHandledBy is null|| tempCheckOutBy is null || tempHandledBy_Operator is null || tempBaseInventory is null)
                    {
                        MSD.AddModelError("", "存在无效数据 请检查");
                        return;
                    }
                    EntityInfos.Add(new BeginningOfPeriodKnifeCheckOutInfo
                    {
                        HandledBy = tempHandledBy,
                        HandledBy_Operator = tempHandledBy_Operator,
                        CheckOutBy = tempCheckOutBy,
                        Inventory = tempBaseInventory
                    });
                }
                if (EntityInfos.Count == 0)
                {
                    MSD.AddModelError("", "数据为空 请检查");
                    return;
                }
                //U9刀具寿命表并校验
                var inventoryItemCodes = baseInventorys_info.Select(x => x.ItemMaster.Code).Distinct().ToList();//必有值
                //var inventoryItemLivesReturn = GetKnifesLivesSV.GetKnifesLives_1(inventoryItemCodes);//
                var inventoryItemLivesReturn = Wtm.CreateVM<KnifeVM>().GetKnifesLives_2(inventoryItemCodes,"handledBy");//获取刀具寿命信息 不生成数据 wtmuser不会写入U9 
                if (inventoryItemLivesReturn == null)//
                {
                    //MSD.AddModelError("", "" + inventoryItemLivesReturn.Message);//为null的报错在里面写过了
                    return;
                }
                if (inventoryItemLivesReturn.Success == false)//
                {
                    MSD.AddModelError("", "" + inventoryItemLivesReturn.Message);
                    return;
                }
                var inventoryItemLives = inventoryItemLivesReturn.Data;
                foreach (var temp in inventoryItemLives)
                {
                    if (temp.IsActive == false)
                    {
                        MSD.AddModelError("", $"{temp.ItemMaster}在U9刀具寿命管理里未生效 无法用于台账的刀具领用");
                        return;
                    }
                    if (temp.LedgerIncluded == false)
                    {
                        MSD.AddModelError("", $"{temp.ItemMaster}未关联台账 属于低值 无法用于期初刀具导入");
                        return;
                    }
                }
                #endregion
                #region 校验通过 开始进行处理
                //分组
                var groupedData = EntityInfos
                    .GroupBy(x => new {
                        x.HandledBy,
                        x.CheckOutBy,
                        x.HandledBy_Operator
                    });

                // 遍历分组结果 依次实现 创建领用单 刀具杂发 领用单行数据记录
                foreach (var group in groupedData)
                {
                    var handledBy = group.Key.HandledBy;
                    var checkOutBy = group.Key.CheckOutBy;
                    var handledBy_Operator = group.Key.HandledBy_Operator;
                    var baseInventorys = group.ToList().Select(x=>x.Inventory).ToList();
                    Wtm.LoginUserInfo = new LoginUserInfo
                    {
                        ITCode = handledBy.ITCode,
                        Name = handledBy.Name
                    };
                    //领用单基本信息生成 直接已审核
                    var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                    var KnifeCheckOutDocNo = DocNoVm.GetSequence("KnifeCheckOutRule", tran);//生成领用单单号
                    KnifeCheckOutVM tempCheckOutVm = Wtm.CreateVM<KnifeCheckOutVM>();
                    tempCheckOutVm.Entity.DocType = KnifeCheckOutTypeEnum.NormalCheckOut;
                    tempCheckOutVm.Entity.ApprovedTime = currentTime; 
                    tempCheckOutVm.Entity.CheckOutBy = checkOutBy;
                    tempCheckOutVm.Entity.CheckOutById = checkOutBy.ID;
                    tempCheckOutVm.Entity.DocNo = KnifeCheckOutDocNo;
                    tempCheckOutVm.Entity.HandledBy = handledBy;
                    tempCheckOutVm.Entity.HandledById = handledBy.ID.ToString();
                    tempCheckOutVm.Entity.Status = KnifeOrderStatusEnum.Approved;
                    tempCheckOutVm.Entity.WareHouseId = whid;
                    tempCheckOutVm.Entity.KnifeCheckOutLine_KnifeCheckOut = new List<KnifeCheckOutLine>();

                    string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                    string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                    U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//刀具领用固定组织
                    U9Return<MiscShipmentData> u9Return = (U9Return<MiscShipmentData>)apiHelper.CreateAndApprovedMiscShipment(handledBy_Operator, baseInventorys, checkOutBy, false);//高值刀具杂发
                    if (!u9Return.Success)
                    {
                        MSD.AddModelError("", "U9创建并审核杂发单失败:" + u9Return.Msg);
                        return;
                    }
                    var U9MiscShipDocNo = u9Return.Entity.DocNo;

                    //杂发成功 for遍历 带着杂发行id 寿命表 新建刀具  刀具建档记录(还需要对应的采购单行)      每把刀的生成 然后组进领用单行 减掉库存信息 库存流水 刀具领用记录不在此处生成 在审核的时候生成
                    foreach (var baseInventory in baseInventorys)
                    {
                        //获取关联的采购收货行id 需要条码是由采购收货行或者库存拆分操作生成  允许空
                        string U9RcvLineId = GetRcvLineIdBySerialNumber(baseInventory.SerialNumber);
                        if (!MSD.IsValid)//|| string.IsNullOrEmpty(U9RcvLineId) 盘点或者其他方式的进入方式也允许
                        {
                            MSD.AddModelError("", "新刀获取原采购行失败:" + MSD.GetFirstError());
                            return;
                        }
                        //获取对应的杂发单行id
                        string U9MiscShipLineId = u9Return.Entity.Lines.Where(x => x.KnifeNo == baseInventory.SerialNumber).Select(x => x.ID).FirstOrDefault();
                        //获取寿命信息
                        var inledgerItemLive = inventoryItemLives.Where(x => x.ItemMaster == baseInventory.ItemMaster.Code).FirstOrDefault();
                        //新建刀具  在新建的时候就有建档的刀具操作记录新建了
                        var knifeVM = Wtm.CreateVM<KnifeVM>();
                        var line = baseInventory;
                        //刀具直接领用状态
                        knifeVM.Entity = new Knife()
                        {
                            CreatedDate = currentTime,
                            SerialNumber = line.SerialNumber,
                            BarCode = $"{(int)line.ItemSourceType}@{line.ItemMaster.Code}@{line.Qty.TrimZero()}@{line.SerialNumber}",
                            Status = KnifeStatusEnum.CheckOut,
                            HandledBy = tempCheckOutVm.Entity.HandledBy,
                            HandledById = tempCheckOutVm.Entity.HandledById,
                            HandledByName = tempCheckOutVm.Entity.HandledBy.Name,
                            CurrentCheckOutBy = checkOutBy,
                            CurrentCheckOutById = checkOutBy.ID,
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
                        knifeVM.DoAdd(U9RcvLineId, u9Return.Entity.DocNo);//采购行id(可能为空)杂发单单号 在刀具操作记录里默认建档操作
                        //刀具新增操作
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            Knife = knifeVM.Entity,
                            KnifeId = knifeVM.Entity.ID,
                            DocNo = tempCheckOutVm.Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.CheckOut,
                            OperationTime = currentTime,
                            OperationBy = tempCheckOutVm.Entity.CheckOutBy,
                            OperationById = tempCheckOutVm.Entity.CheckOutBy.ID,
                            HandledBy = tempCheckOutVm.Entity.HandledBy,
                            HandledById = handledBy.ID.ToString(),
                            HandledByName = handledBy.Name,
                            UsedDays = 0,
                            TotalUsedDays = knifeVM.Entity.TotalUsedDays,
                            RemainingDays = knifeVM.Entity.RemainingUsedDays,
                            CurrentLife = knifeVM.Entity.CurrentLife,
                            WhLocationId = knifeVM.Entity.WhLocationId,
                            WhLocation = knifeVM.Entity.WhLocation,
                            GrindNum = knifeVM.Entity.GrindCount,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime
                        });
                        tempCheckOutVm.Entity.KnifeCheckOutLine_KnifeCheckOut.Add(new KnifeCheckOutLine()
                        {
                            Knife = knifeVM.Entity,
                            KnifeId = knifeVM.Entity.ID,
                            FromWhLocation = line.WhLocation,
                            FromWhLocationId = line.WhLocationId,
                        });
                        #region 库存信息变化+流水日志变化

                        //新增库存流水/库存信息变化 
                        this.CreateInvLog(OperationTypeEnum.InventoryOtherShipCreate, U9MiscShipDocNo, line.ID, null, -line.Qty, null);//流水记录  类型其他出库  出完
                        line.Qty = 0;
                        line.IsAbandoned = true;
                        #endregion
                    }
                    tempCheckOutVm.DoAdd();

                }

                #endregion
                DC.SaveChanges();
                return ;
            }
            catch(Exception e)
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
            Entity = DC.Set<KnifeCheckOut>().Include(x => x.KnifeCheckOutLine_KnifeCheckOut).ThenInclude(x => x.Knife).FirstOrDefault(x=>x.ID==Entity.ID);
            var needtoPrint = Entity.KnifeCheckOutLine_KnifeCheckOut.Where(x => x.IsNewLine == "1").Select(x=>x.Knife).ToList();
            if (needtoPrint.Count == 0)
            {
                MSD.AddModelError("", "此领用单没有新条码生成");
                return null;
            }
            //准备打印信息
            //先是头
            CreatePrintDataPara data = new CreatePrintDataPara();
            data.ModuleId = SelectedPrintModule;
            data.OperatorName = LoginUserInfo.ITCode;
            data.Records = new List<CreatePrintDataLinePara>();
            //然后遍历行
            foreach(var k in needtoPrint)
            {
                CreatePrintDataLinePara line = new CreatePrintDataLinePara();
                BaseItemMaster item = DC.Set<BaseItemMaster>().Include(x => x.Organization).Include(x => x.StockUnit).Where(x => x.Code == k.ActualItemCode&&x.Organization.Code=="0410").AsNoTracking().FirstOrDefault();
                line.Fields = [
                    new CreatePrintDataSubLinePara { FieldName = "条码", FieldValue = k.BarCode },
                    new CreatePrintDataSubLinePara { FieldName = "刀具料号", FieldValue = item.Code },
                    new CreatePrintDataSubLinePara { FieldName = "刀具规格", FieldValue = item.SPECS },
                ];
                data.Records.Add(line);
            }
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


    }
}
