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
using Microsoft.IdentityModel.Tokens;
using WMS.ViewModel.KnifeManagement.KnifeCombineVMs;
using Microsoft.EntityFrameworkCore.Storage;
using Elsa;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.ViewModel.KnifeManagement.KnifeScrapVMs;
using WMS.ViewModel.KnifeManagement.KnifeCheckInLineVMs;
namespace WMS.ViewModel.KnifeManagement.KnifeCheckInVMs
{
    public partial class KnifeCheckInVM : BaseCRUDVM<KnifeCheckIn>
    {
        
        public List<string> KnifeManagementKnifeCheckInFTempSelected { get; set; }
        public KnifeCheckInLineKnifeCheckInDetailListVM KnifeCheckInLineKnifeCheckInList { get; set; }
        public KnifeCheckInLineKnifeCheckInDetailListVM1 KnifeCheckInLineKnifeCheckInList1 { get; set; }
        public KnifeCheckInLineKnifeCheckInDetailListVM2 KnifeCheckInLineKnifeCheckInList2 { get; set; }

        public KnifeCheckInVM()
        {
            SetInclude(x => x.CheckInBy);
            SetInclude(x => x.HandledBy);
            SetInclude(x => x.WareHouse);
            SetInclude(x => x.KnifeCheckInLine_KnifeCheckIn);
            
            KnifeCheckInLineKnifeCheckInList = new KnifeCheckInLineKnifeCheckInDetailListVM();
            KnifeCheckInLineKnifeCheckInList.DetailGridPrix = "Entity.KnifeCheckInLine_KnifeCheckIn";
            KnifeCheckInLineKnifeCheckInList1 = new KnifeCheckInLineKnifeCheckInDetailListVM1();
            KnifeCheckInLineKnifeCheckInList1.DetailGridPrix = "Entity.KnifeCheckInLine_KnifeCheckIn";
            KnifeCheckInLineKnifeCheckInList2 = new KnifeCheckInLineKnifeCheckInDetailListVM2();
            KnifeCheckInLineKnifeCheckInList2.DetailGridPrix = "Entity.KnifeCheckInLine_KnifeCheckIn";
        }

        protected override void InitVM()
        {
            KnifeCheckInLineKnifeCheckInList.CopyContext(this);
            KnifeCheckInLineKnifeCheckInList1.CopyContext(this);
            KnifeCheckInLineKnifeCheckInList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeCheckIn> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }
        public override void Validate()
        {
            if (Entity.KnifeCheckInLine_KnifeCheckIn.GroupBy(x => new { x.KnifeCheckInId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }

            base.Validate();
        }
        public override void DoAdd()        
        {
            //新增归还单的时候 固定值写死
            //单号可以考虑在这里 事务转这里 必要性? 报废归还要生成报废单 还是要事务 没必要 写在外面吧
            //状态写死 新建就是开立 到也没必要 都在外面过一遍 不然有漏的不好找 doadd就不加默认值了
            //Entity.Status = KnifeOrderStatusEnum.Open;
            if (Entity.KnifeCheckInLine_KnifeCheckIn.Count == 0)
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
        /// 刀具归还单预处理
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeCheckInInputInfo info, IDbContextTransaction tran)
        {
            try
            {
                //登录校验
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

                //通用校验
                //info 非空校验 归还人和归还类型
                if (info.CheckInType!=KnifeCheckInTypeEnum.NormalCheckIn
                    && info.CheckInType != KnifeCheckInTypeEnum.WrongPickupCheckIn
                    && info.CheckInType != KnifeCheckInTypeEnum.CombineCheckIn)//普通/错领/组合
                {
                    MSD.AddModelError("", "不正确的归还类型");
                    return;
                }
                if (string.IsNullOrEmpty(info.CheckInByID ))
                {
                    MSD.AddModelError("", "归还人id不可空 请检查输入");
                    return;
                }
                //通用参数
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeCheckInDocNo = DocNoVm.GetSequence("KnifeCheckInRule", tran);//生成归还单单号
                var currentTime = DateTime.Now;//当前时间
                var checkInBy = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                    .ThenInclude(x => x.Organization)
                    .FirstOrDefault(x => x.ID ==Guid.Parse( info.CheckInByID));//
                if(checkInBy is null)
                {
                    MSD.AddModelError("", "无效的领用人 请检查输入");
                    return;
                }
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人
                if (handledBy is null)
                {
                    MSD.AddModelError("", "未找到有效的仓管员");
                    return;
                }
                //特化参数
                var knifeCombines = new List<KnifeCombine>();//组合单
                var knifes = new List<Knife>();//错领和普通的都用这个  刀具
                var whLocations = new List<BaseWhLocation>();//错领和普通的都用这个  归还库位
                //分类校验
                //分类校验1 组合归还
                if (info.CheckInType == KnifeCheckInTypeEnum.CombineCheckIn)
                {
                    if(info.CombineKnifeNoLines is null || info.CombineKnifeNoLines.Count == 0)
                    {
                        MSD.AddModelError("", "组合归还组合刀号不可为空");
                        return;
                    }
                    var CombineKnifeNos = info.CombineKnifeNoLines.Select(x => x.CombineKnifeNo).ToList();
                    knifeCombines = DC.Set<KnifeCombine>()
                        .Include(x => x.KnifeCombineLine_KnifeCombine).ThenInclude(x => x.Knife).ThenInclude(x=>x.WhLocation)
                        .Where(x=>x.Status==KnifeOrderStatusEnum.Approved)
                        .Where(x => CombineKnifeNos.Contains(x.CombineKnifeNo))
                        .ToList();
                    //输入信息里的组合单单号都能被查出来
                    foreach(var combineKnifeNoline in info.CombineKnifeNoLines)
                    {
                        if(knifeCombines.All(x=>x.CombineKnifeNo!= combineKnifeNoline.CombineKnifeNo))
                        {
                            MSD.AddModelError("", $"组合刀号{combineKnifeNoline.CombineKnifeNo}不可用");
                            return;
                        }
                    }
                    //组合单的领用人和归还人是同一个人
                    foreach(var knifeCombine in knifeCombines)
                    {
                        if (knifeCombine.KnifeCombineLine_KnifeCombine[0].Knife.CurrentCheckOutById!= Guid.Parse(info.CheckInByID))
                        {
                            MSD.AddModelError("", $"组合刀{knifeCombine.CombineKnifeNo}的领用人与归还人不同 不可用");
                            return;
                        }
                    }
                }
                //分类校验2 错领归还 和 普通归还
                if (info.CheckInType == KnifeCheckInTypeEnum.WrongPickupCheckIn || info.CheckInType == KnifeCheckInTypeEnum.NormalCheckIn)
                {
                    //非空检验
                    if (info.Lines is null || info.Lines.Count == 0)
                    {
                        MSD.AddModelError("", "错领归还刀具信息不可为空");
                        return;
                    }
                    foreach(var line in info.Lines)
                    {
                        if(string.IsNullOrEmpty(line.KnifeId))
                        {
                            MSD.AddModelError("", "错领归还刀具信息不可为空");
                            return;
                        }
                        if (string.IsNullOrEmpty(line.WhLocationId))
                        {
                            MSD.AddModelError("", "错领归还库位信息不可为空");
                            return;
                        }
                    }
                    //库存信息时扫码获取的 要校验是否为空吗?刀具也是扫码获取的 要校验是否生效吗? 库位呢? 要
                    var knifeIds = info.Lines.Select(x => Guid.Parse(x.KnifeId)).ToList();
                    var whLocationIds = info.Lines.Select(x => Guid.Parse(x.WhLocationId)).ToList();
                    knifes = DC.Set<Knife>()
                        .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                        //.Where(x => x.Status == KnifeStatusEnum.CheckOut) 不在这里判定刀状态  待会查出来后遍历判定 不然这里返回信息少了刀号 
                        .Where(x => knifeIds.Contains(x.ID))
                        .ToList();
                    whLocations = DC.Set<BaseWhLocation>()
                        .Include(x => x.WhArea).ThenInclude(x => x.WareHouse)
                        .Where(x => whLocationIds.Contains(x.ID))
                        .ToList();
                    if (knifeIds.Count != knifes.Count)
                    {
                        MSD.AddModelError("", "存在无效刀号");
                        return;
                    }
                    foreach(var k in knifes)
                    {
                        if(k.Status!= KnifeStatusEnum.CheckOut)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}未被领用 无法归还");
                            return;
                        }
                        if (k.WhLocation.Locked == true)
                        {
                            MSD.AddModelError("", $"刀具{k.SerialNumber}所在库位{k.WhLocation.Code}已锁定 不可进行领用操作");
                            return;
                        }
                        if (k.WhLocation.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"此条码所在库位为不良退回库位 不可获取刀具信息");
                            return;
                        }
                        if (k.WhLocation.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"{k.SerialNumber}不属于当前存储地点 不可领用");
                            return;
                        }
                    }

                    if (whLocationIds.Any(x => !whLocations.Select(x => x.ID).ToList().Contains(x)))
                    {
                        MSD.AddModelError("", "存在无效库位");
                        return;
                    }
                   
                    foreach (var w in whLocations)
                    {
                        if (w.WhArea.WareHouseId != whid)
                        {
                            MSD.AddModelError("", $"归还库位{w.Code}不属于当前存储地点 不可操作");
                            return;
                        }
                        if (w.Locked == true)
                        {
                            MSD.AddModelError("", $"库位{w.Code}已锁定 不可进行操作");
                            return;
                        }
                        if (w.IsEffective == EffectiveEnum.Ineffective || w.AreaType != WhLocationEnum.Normal)
                        {
                            MSD.AddModelError("", $"库位{w.Code}无效");
                            return;
                        }
                        if (w.Code.Contains("A9999999"))//不良退回库位
                        {
                            MSD.AddModelError("", $"{w.Code}是不良退回库位 不可进行操作");
                            return;
                        }
                        
                        
                    }

                }
                //校验 错领归还仅限领取的当天
                if (info.CheckInType == KnifeCheckInTypeEnum.WrongPickupCheckIn)
                {
                    foreach (var knife in knifes)
                    {
                        if (knife.LastOperationDate.Date != currentTime.Date)
                        {
                            MSD.AddModelError("", $"{knife.SerialNumber}无法归还 错领归还仅限领取的当天");
                            return;
                        }
                    }
                }



                //通过归还类型  组合需要有组合刀号 其他都不认  错领归还   普通归还要判断归还类型2   除了组合归还之外都会进行组合单全部关闭





                //校验通过 操作阶段
                
                //归还单的通用操作
                Entity.DocType = info.CheckInType;
                Entity.DocNo = KnifeCheckInDocNo;
                Entity.CheckInBy = checkInBy;
                Entity.CheckInById = checkInBy.ID;
                Entity.HandledBy = handledBy;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.HandledById = handledBy.ID.ToString();
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.WareHouseId = whid;
                //Entity.CombineKnifeNo = info.CombineKnifeNo;
                Entity.KnifeCheckInLine_KnifeCheckIn = new List<KnifeCheckInLine>();
                //分类操作
                //分类操作1 组合归还
                if (info.CheckInType == KnifeCheckInTypeEnum.CombineCheckIn)
                {
                    foreach(var order in knifeCombines)
                    {
                        string toWhcationId_string = info.CombineKnifeNoLines
                            .FirstOrDefault(x => x.CombineKnifeNo == order.CombineKnifeNo)
                            .WhLocationId;
                        var toWhcationId = Guid.Parse(toWhcationId_string);
                        foreach (var line in order.KnifeCombineLine_KnifeCombine)
                        {
                            //每一行组合单行 自己变化   生成归还单的行
                            line.ToWhLocationId = toWhcationId;//组合单行的归还库位
                            //生成归还单的行
                            Entity.KnifeCheckInLine_KnifeCheckIn.Add(new KnifeCheckInLine()
                            {
                                KnifeId = line.KnifeId,
                                ToWhLocationId = toWhcationId,
                                FromWhLocationId = line.Knife.WhLocationId,
                            });
                        }
                        
                    }
                }
                //分类操作2 错领归还 与 普通归还  普通归还的归还单和错领是一样的 也都是关闭涉及的组合单 他的类型是额外的其他操作
                if (info.CheckInType == KnifeCheckInTypeEnum.WrongPickupCheckIn || info.CheckInType == KnifeCheckInTypeEnum.NormalCheckIn)
                {
                    foreach (var line in info.Lines)
                    {
                        var knife = knifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                        var whLocation = whLocations.FirstOrDefault(x => x.ID == Guid.Parse(line.WhLocationId));
                        Entity.KnifeCheckInLine_KnifeCheckIn.Add(new KnifeCheckInLine
                        {
                            KnifeId = knife.ID,
                            FromWhLocationId = knife.WhLocationId,
                            ToWhLocationId = whLocation.ID,
                        });
                    }
                    //关闭涉及的组合单
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
                            toCloseCombine.CloseTime = currentTime;
                        }
                    }
                }
                //分类操作2.2 普通归还的特殊操作
                if(info.CheckInType == KnifeCheckInTypeEnum.NormalCheckIn)
                {
                    if(info.CheckInType2 == 0)//默认0普通归还 1维修归还 2报废归还
                    {
                        foreach (var k in knifes)
                        {
                            k.InStockStatus = KnifeInStockStatusEnum.InStock;
                        }
                    }
                    if (info.CheckInType2 == 1)
                    {
                        foreach(var k in knifes)
                        {
                            k.InStockStatus = KnifeInStockStatusEnum.ToGrind;
                        }
                    }
                    if (info.CheckInType2 == 2)
                    {
                        foreach (var k in knifes)
                        {
                            k.InStockStatus = KnifeInStockStatusEnum.ToScrap;
                        }
                    }
                }
                

                DC.SaveChanges();
            }
            catch (Exception e)
            {
                MSD.AddModelError("", "归还初始化vm失败 错误原因:" + e.Message);
            }
        }

        /// <summary>
        /// 归还单审核操作  
        /// </summary>
        /// <param name="tran"></param>
        public void DoApproved(IDbContextTransaction tran)
        {

            try
            {
                // 重新加载 Entity 并包含导航属性
                Entity =  DC.Set<KnifeCheckIn>()
                    .Include(x => x.KnifeCheckInLine_KnifeCheckIn)
                        .ThenInclude(line => line.Knife) // 加载刀具
                    .Include(x => x.KnifeCheckInLine_KnifeCheckIn)
                        .ThenInclude(line => line.ToWhLocation) // 加载库位
                    .FirstOrDefault(x => x.ID == Entity.ID);
                //校验: 归还单 单据状态需要未审核  
                if (Entity.Status != KnifeOrderStatusEnum.Open)
                {
                    MSD.AddModelError("", "归还单单据状态不是开立 无法被审核");
                    return;
                }
                //校验:刀具状态需要领用 才可以归还
                foreach (KnifeCheckInLine line in Entity.KnifeCheckInLine_KnifeCheckIn)
                {
                    if (line.Knife.Status != KnifeStatusEnum.CheckOut)
                    {
                        MSD.AddModelError("", $"刀具{line.Knife.SerialNumber}的状态不是领用 无法被归还");
                        return;
                    }
                }
                /*// 组合归还允许多个组合单号  也不再一一对应了
                //校验 组合归还类型时 组合刀号不可空 刀具和组合单需要一一对应
                if (Entity.DocType == KnifeCheckInTypeEnum.CombineCheckIn)
                {
                    if (Entity.CombineKnifeNo.IsNullOrEmpty())
                    {
                        MSD.AddModelError("", $"组合归还时组合刀号必须填写");
                        return;
                    }
                    var combineOrder = DC.Set<KnifeCombine>()
                                .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                                .Include(x => x.KnifeCombineLine_KnifeCombine)
                                .FirstOrDefault(x => x.CombineKnifeNo == Entity.CombineKnifeNo);
                    if (combineOrder is null)
                    {
                        MSD.AddModelError("", $"此组合刀号在已审核的组合单中不存在,检查是否已归还");
                        return;
                    }
                    // 获取组合单中的所有刀具编码
                    var combineKnifeIDs = combineOrder.KnifeCombineLine_KnifeCombine.Select(x => x.KnifeId).ToList();
                    // 获取归还单中的所有刀具编码
                    var checkInKnifeIDs = Entity.KnifeCheckInLine_KnifeCheckIn.Select(x => x.KnifeId).ToList();
                    // 验证领用明细里的刀具在组合单明细里都能找到且两者总数相等
                    if (combineKnifeIDs.Count != checkInKnifeIDs.Count ||
                        checkInKnifeIDs.Any(x => !combineKnifeIDs.Contains(x)))
                    {
                        MSD.AddModelError("", $"归还单中的刀具与组合单中的刀具无法一一对应 请检查");
                        return;
                    }
                }
                */
                /* 组合刀也允许部分被普通归还了 只是会关闭组合单
                 * //校验 不是组合归还类型时  归还单明细的所有刀具都不应该存在于已审核的组合单中
                if (Entity.DocType != KnifeCheckInTypeEnum.CombineCheckIn)
                {
                    // 获取审核状态下的组合单中的所有刀具编码
                    var combineKnifeIDs =  DC.Set<KnifeCombine>()
                                .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                                .SelectMany(x => x.KnifeCombineLine_KnifeCombine)  // 此时返回的是IQueryable<CombinationOrderDetail>
                                .Select(d => d.KnifeId)
                                .Distinct()
                                .ToList();
                    // 获取归还单中的所有刀具编码
                    var checkInKnifeIDs = Entity.KnifeCheckInLine_KnifeCheckIn.Select(x => x.KnifeId).ToList();
                    // 归还的刀具不能在已审核的组合单中出现过
                    if (checkInKnifeIDs.Any(x => combineKnifeIDs.Contains(x)))
                    {
                        MSD.AddModelError("", $"存在刀具已经被组合领用 只能整刀组合归还");
                        return;
                    }
                }*/
                
                var knifeIds = Entity.KnifeCheckInLine_KnifeCheckIn
                        .Where(line => line.Knife != null)
                        .Select(line => line.KnifeId)
                        .ToList();
                var latestOperations = DC.Set<KnifeOperation>()
                        .Include(x => x.Knife)
                        .Where(x => knifeIds.Contains((Guid)x.KnifeId))
                        .GroupBy(x => x.KnifeId)
                        .Select(g => g.OrderByDescending(x => x.OperationTime).FirstOrDefault())
                        .ToList();
                /* //校验:刀具的最后一次操作是领用/或者系统月初领用 //未必 盘点也可能了  这步不做最后操作的判断了
                foreach (var o in latestOperations)
                {
                    if (o.OperationType != KnifeOperationTypeEnum.CheckOut&& o.OperationType != KnifeOperationTypeEnum.MonthlyCheckOut)
                    {
                        MSD.AddModelError("", $"刀具{o.Knife.SerialNumber}的最后一次操作不是领用 无法被归还");
                        return;
                    }
                }*/

                //修改阶段   归还单自己  刀具的详细信息  操作记录-归还
                var currentTime = DateTime.Now;
                //归还单变化 状态 审核时间 经办人  修改时间 修改人 
                Entity.Status = KnifeOrderStatusEnum.Approved;
                Entity.HandledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);
                Entity.HandledById = Entity.HandledBy.ID.ToString();
                Entity.ApprovedTime = currentTime;
                Entity.UpdateBy = LoginUserInfo.Name;
                Entity.UpdateTime = currentTime;

                //25.8.22 逻辑修改 归还只分普通和错领的逻辑区别   组合单的逻辑在预处理那里 审核这里 组合归还和普通归还的完全一样
                //归还类型:普通归还  错领归还   报废归还 不良品报废归还  组合归还
                //刀具  和  操作记录同步变化  一条归一条
                switch (Entity.DocType)
                {
                    case KnifeCheckInTypeEnum.NormalCheckIn:
                        foreach (KnifeCheckInLine checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                        {
                            var knife = checkInline.Knife;
                            var lastOperation = latestOperations
                                .Where(x => knife.ID.Equals(x.KnifeId))
                                .FirstOrDefault();//通常是领用操作
                            // 计算使用天数
                            DateTime receiveDate = (DateTime)lastOperation.OperationTime;//领用日期
                            var usedDays = (currentTime.Date - receiveDate.Date).Days + 1; // 使用天数包含当天


                            //修改 刀具
                            knife.Status = KnifeStatusEnum.InStock;
                            knife.CurrentCheckOutBy = null;
                            knife.CurrentCheckOutById = null;
                            knife.HandledBy = Entity.HandledBy;
                            knife.HandledById = Entity.HandledBy.ID.ToString();
                            knife.HandledByName = Entity.HandledBy.Name;
                            knife.WhLocation = checkInline.ToWhLocation;
                            knife.WhLocationId = checkInline.ToWhLocationId;
                            knife.LastOperationDate = currentTime;

                            knife.TotalUsedDays += usedDays;//累计使用天数
                            knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数


                            knife.UpdateBy = LoginUserInfo.Name;
                            knife.UpdateTime = currentTime;
                            //新增 操作记录-归还
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                KnifeId = knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckIn,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckInBy,//操作人=归还人
                                OperationById = Entity.CheckInById,//操作人=归还人
                                WhLocation = checkInline.ToWhLocation,
                                WhLocationId = checkInline.ToWhLocationId,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                HandledByName = Entity.HandledBy.Name,
                                UsedDays = usedDays,
                                RemainingDays = knife.RemainingUsedDays,
                                TotalUsedDays = knife.TotalUsedDays,
                                CurrentLife = knife.CurrentLife,
                                GrindNum = knife.GrindCount,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime,
                            });
                        }
                        break;//普通归还
                    case KnifeCheckInTypeEnum.WrongPickupCheckIn:
                        foreach (KnifeCheckInLine checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                        {
                            var knife = checkInline.Knife;
                            /*var lastOperation = latestOperations
                                .Where(x => knife.ID.Equals(x.KnifeId))
                                .FirstOrDefault();*///不需要计算领用时间
                            //校验 错领归还仅限领取的当天
                            if (knife.LastOperationDate.Date != currentTime.Date)
                            {
                                MSD.AddModelError("", $"{knife.SerialNumber}无法归还 错领归还仅限领取的当天");
                                return;
                            }
                            //修改 刀具
                            knife.Status = KnifeStatusEnum.InStock;
                            knife.CurrentCheckOutBy = null;
                            knife.CurrentCheckOutById = null;
                            knife.HandledBy = Entity.HandledBy;
                            knife.HandledById = Entity.HandledById;
                            knife.HandledByName = Entity.HandledBy.Name;
                            knife.LastOperationDate = currentTime;
                            knife.WhLocation = checkInline.ToWhLocation;
                            knife.WhLocationId = checkInline.ToWhLocationId;

                            knife.UpdateBy = LoginUserInfo.Name;
                            knife.UpdateTime = currentTime;
                            //新增 操作记录-归还
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                KnifeId = knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckIn,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckInBy,//操作人=归还人
                                OperationById = Entity.CheckInById,//操作人=归还人
                                WhLocation = checkInline.ToWhLocation,
                                WhLocationId = checkInline.ToWhLocationId,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                HandledByName = Entity.HandledBy.Name,
                                RemainingDays = knife.RemainingUsedDays,
                                UsedDays = 0,
                                TotalUsedDays = knife.TotalUsedDays,
                                CurrentLife = knife.CurrentLife,
                                GrindNum = knife.GrindCount,

                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime,

                               
                            });
                        }
                        break;//错领归还

                    /*//报废归还取消了  不存在这里的报废归还了 报废单要手动生成 报废归还是改在库状态字段
                     case KnifeCheckInTypeEnum.ScrapCheckIn:
                        //正常归还 刀+操作 然后 +报废单的新建审核操作
                        foreach (KnifeCheckInLine checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                        {
                            var knife = checkInline.Knife;
                            var lastOperation = latestOperations
                                .Where(x => knife.ID.Equals(x.KnifeId))
                                .FirstOrDefault();
                            // 计算使用天数
                            DateTime receiveDate = (DateTime)lastOperation.OperationTime;//归还日期
                            var usedDays = (currentTime.Date - receiveDate.Date).Days + 1; // 使用天数包含当天
                            //不计算折旧金额
                            //var depreciationAmount = knife.CurrentAmount / knife.CurrentLife * usedDays;

                            //修改 刀具
                            knife.Status = KnifeStatusEnum.InStock;
                            knife.CurrentCheckOutBy = null;
                            knife.CurrentCheckOutById = null;
                            knife.HandledBy = Entity.HandledBy;
                            knife.HandledById = Entity.HandledBy.ID.ToString();
                            knife.LastOperationDate = currentTime;
                            knife.WhLocation = checkInline.ToWhLocation;
                            knife.WhLocationId = checkInline.ToWhLocationId;

                            knife.TotalUsedDays += usedDays;//累计使用天数
                            knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数
                           

                            knife.UpdateBy = LoginUserInfo.Name;
                            knife.UpdateTime = currentTime;
                            //新增 操作记录-归还
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                KnifeId = knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckIn,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckInBy,//操作人=归还人
                                OperationById = Entity.CheckInById,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                WhLocation = checkInline.ToWhLocation,
                                WhLocationId = checkInline.ToWhLocationId,
                                UsedDays = usedDays,
                                RemainingDays = knife.RemainingUsedDays,
                                TotalUsedDays = knife.TotalUsedDays,
                                CurrentLife = knife.CurrentLife,
                                GrindNum = knife.GrindCount,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime,
                            });
                        }
                        //新建报废单  然后审核报废单
                        //新建:直接数据库新建表和表行?   编数据然后vm.addsync?
                        var scrapVm = Wtm.CreateVM<KnifeScrapVM>();
                        var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                        var KnifeScrapDocNo = DocNoVm.GetSequence("KnifeScrapRule", tran);//生成报废单单号 
                        scrapVm.Entity = new KnifeScrap
                        {
                            DocNo = KnifeScrapDocNo,
                            DocType = KnifeScrapTypeEnum.NormalScrap,
                            Status = KnifeOrderStatusEnum.Open,
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            KnifeScrapLine_KnifeScrap = new List<KnifeScrapLine>(),
                        };
                        //报废单明细赋值
                        foreach (var checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                        {
                            scrapVm.Entity.KnifeScrapLine_KnifeScrap.Add(new KnifeScrapLine
                            {
                                KnifeId = checkInline.KnifeId,
                                FromWhLocationId =checkInline.ToWhLocationId,
                                ToWhLocationId =checkInline .ToWhLocationId,
                            });
                        }
                        scrapVm.DoAdd();
                        scrapVm.DoApproved();
                        break;//报废归还*/
                    case KnifeCheckInTypeEnum.DefectiveCheckIn:
                        MSD.AddModelError("", "未被记录的归还单单据类型 操作中止 数据已回滚");
                        return;//取消开发
                    case KnifeCheckInTypeEnum.CombineCheckIn:
                        //原来的组合归还注释掉 用普通归还的逻辑
                        foreach (KnifeCheckInLine checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                        {
                            var knife = checkInline.Knife;
                            var lastOperation = latestOperations
                                .Where(x => knife.ID.Equals(x.KnifeId))
                                .FirstOrDefault();//通常是领用操作
                            // 计算使用天数
                            DateTime checkOutDate = (DateTime)lastOperation.OperationTime;//领用日期
                            var usedDays = (currentTime.Date - checkOutDate.Date).Days + 1; // 使用天数包含当天
                            //修改 刀具
                            knife.Status = KnifeStatusEnum.InStock;
                            knife.CurrentCheckOutBy = null;
                            knife.CurrentCheckOutById = null;
                            knife.HandledBy = Entity.HandledBy;
                            knife.HandledById = Entity.HandledBy.ID.ToString();
                            knife.HandledByName = Entity.HandledBy.Name;

                            knife.WhLocation = checkInline.ToWhLocation;
                            knife.WhLocationId = checkInline.ToWhLocationId;
                            knife.LastOperationDate = currentTime;
                            knife.TotalUsedDays += usedDays;//累计使用天数
                            knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数
                            knife.UpdateBy = LoginUserInfo.Name;
                            knife.UpdateTime = currentTime;
                            //新增 操作记录-归还
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                KnifeId = knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.CheckIn,
                                OperationTime = currentTime,
                                OperationBy = Entity.CheckInBy,//操作人=归还人
                                OperationById = Entity.CheckInById,//操作人=归还人
                                WhLocation = checkInline.ToWhLocation,
                                WhLocationId = checkInline.ToWhLocationId,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                HandledByName = Entity.HandledBy.Name,

                                UsedDays = usedDays,
                                RemainingDays = knife.RemainingUsedDays,
                                TotalUsedDays = knife.TotalUsedDays,
                                CurrentLife = knife.CurrentLife,
                                GrindNum = knife.GrindCount,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime,
                            });
                        }
                        break;//组合归还 用普通归还的逻辑
                    /*//原来的组合归还注释掉 用普通归还的逻辑
                    //和普通归相同的操作 然后多一步 把组合单关闭了 
                    foreach (KnifeCheckInLine checkInline in Entity.KnifeCheckInLine_KnifeCheckIn)
                    {
                        var knife = checkInline.Knife;
                        var lastOperation = latestOperations
                            .Where(x => knife.ID.Equals(x.KnifeId))
                            .FirstOrDefault();//通常是领用操作
                        // 计算使用天数
                        DateTime receiveDate = (DateTime)lastOperation.OperationTime;//归还日期
                        var usedDays = (currentTime.Date - receiveDate.Date).Days + 1; // 使用天数包含当天


                        //修改 刀具
                        knife.Status = KnifeStatusEnum.InStock;
                        knife.CurrentCheckOutBy = null;
                        knife.CurrentCheckOutById = null;
                        knife.HandledBy = Entity.HandledBy;
                        knife.HandledById = Entity.HandledBy.ID.ToString();
                        knife.WhLocation = checkInline.ToWhLocation;
                        knife.WhLocationId = checkInline.ToWhLocationId;
                        knife.LastOperationDate = currentTime;

                        knife.TotalUsedDays += usedDays;//累计使用天数
                        knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数



                        knife.UpdateBy = LoginUserInfo.Name;
                        knife.UpdateTime = currentTime;
                        //新增 操作记录-归还
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            KnifeId = knife.ID,
                            DocNo = Entity.DocNo,
                            OperationType = KnifeOperationTypeEnum.CheckIn,
                            OperationTime = currentTime,
                            OperationBy = Entity.CheckInBy,//操作人=归还人
                            OperationById = Entity.CheckInById,//操作人=归还人
                            HandledBy = Entity.HandledBy,
                            HandledById = Entity.HandledById,
                            WhLocation = checkInline.ToWhLocation,
                            WhLocationId = checkInline.ToWhLocationId,
                            UsedDays = usedDays,
                            RemainingDays = knife.RemainingUsedDays,
                            TotalUsedDays = knife.TotalUsedDays,
                            CurrentLife = knife.CurrentLife,
                            GrindNum = knife.GrindCount,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime,
                        });
                    }
                    DC.SaveChanges();

                    //关闭刀具组合单
                    var knifeCombineVM = Wtm.CreateVM<KnifeCombineVM>();
                    var combineOrder = DC.Set<KnifeCombine>()
                            .Where(x => x.Status == KnifeOrderStatusEnum.Approved)
                            //.Include(x => x.KnifeCombineLine_KnifeCombine)
                            .FirstOrDefault(x => x.CombineKnifeNo == Entity.CombineKnifeNo);
                    knifeCombineVM.Entity = combineOrder;
                    knifeCombineVM.DoClose(Entity.KnifeCheckInLine_KnifeCheckIn); 
                    break;//组合归还*/
                    default:
                        MSD.AddModelError("", "未被记录的归还单单据类型 操作中止 数据已回滚");
                        return;
                }

                DC.SaveChanges();
                if (!MSD.IsValid) // 校验未通过
                {
                    MSD.AddModelError("", "msd校验不通过 本次审核归还单失败  已回滚数据");
                    return;
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", $"嵌套事务审核归还单失败: {ex.Message} 已回滚");
                return;
            }

        }



    }
}
