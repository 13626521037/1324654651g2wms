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
using Microsoft.EntityFrameworkCore.Storage;
using Elsa;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using Elsa.Activities.ControlFlow;
using WMS.Model.BaseData;
using WMS.Util.U9Para.Knife;
using WMS.Util;
using WMS.ViewModel.KnifeManagement.KnifeScrapLineVMs;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
namespace WMS.ViewModel.KnifeManagement.KnifeScrapVMs
{
    public partial class KnifeScrapVM : BaseCRUDVM<KnifeScrap>
    {

        public List<string> KnifeManagementKnifeScrapFTempSelected { get; set; }
        public KnifeScrapLineKnifeScrapDetailListVM KnifeScrapLineKnifeScrapList { get; set; }
        public KnifeScrapLineKnifeScrapDetailListVM1 KnifeScrapLineKnifeScrapList1 { get; set; }
        public KnifeScrapLineKnifeScrapDetailListVM2 KnifeScrapLineKnifeScrapList2 { get; set; }
        public KnifeScrapVM()
        {
            SetInclude(x => x.HandledBy);
            KnifeScrapLineKnifeScrapList = new KnifeScrapLineKnifeScrapDetailListVM();
            KnifeScrapLineKnifeScrapList.DetailGridPrix = "Entity.KnifeScrapLine_KnifeScrap";
            KnifeScrapLineKnifeScrapList1 = new KnifeScrapLineKnifeScrapDetailListVM1();
            KnifeScrapLineKnifeScrapList1.DetailGridPrix = "Entity.KnifeScrapLine_KnifeScrap";
            KnifeScrapLineKnifeScrapList2 = new KnifeScrapLineKnifeScrapDetailListVM2();
            KnifeScrapLineKnifeScrapList2.DetailGridPrix = "Entity.KnifeScrapLine_KnifeScrap";

        }

        protected override void InitVM()
        {

            KnifeScrapLineKnifeScrapList.CopyContext(this);
            KnifeScrapLineKnifeScrapList1.CopyContext(this);
            KnifeScrapLineKnifeScrapList2.CopyContext(this);
        }

        public override DuplicatedInfo<KnifeScrap> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

        }

        public override async Task DoAddAsync()        
        {
            
            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            
            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
        public override void Validate()
        {
            if (Entity.KnifeScrapLine_KnifeScrap.GroupBy(x => new { x.KnifeScrapId, x.KnifeId }).Any(x => x.Count() > 1))
            {
                MSD.AddModelError("", "刀具重复");
                return;
            }
            base.Validate();
        }
        public override void DoAdd()
        {
            if (Entity.KnifeScrapLine_KnifeScrap.Count == 0)
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
        /// 刀具报废单初始化
        /// </summary>
        /// <param name="info"></param>
        /// <param name="tran"></param>
        public void DoInitByInfo(KnifeScrapInputInfo info, IDbContextTransaction tran)
        {
            try
            {
                
                //校验阶段
                //1.获取并校验 当前登录人信息和登录存储地点  当前时间
                Guid whid;
                // PDA从登录属性获取
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);//经办人 可以在此校验是否与存储地点有联系 但没必要 暂时不加
                if(handledBy is null)
                {
                    MSD.AddModelError("", "登录人信息无效，请重新登录");
                    return;
                }
                var currentTime = DateTime.Now;//当前时间
                //2.检验输入参数非空 刀具存在
                if (!Enum.IsDefined(typeof(KnifeScrapTypeEnum), info.ScrapType))
                {
                    MSD.AddModelError("", "不正确的报废类型");
                    return;
                }
                if (info.Knifes==null|| info.Knifes.Count == 0)
                {
                    MSD.AddModelError("", "刀具列表为空 请扫描刀具");
                    return;
                }
                var knifeIDs = info.Knifes.Select(x => x.KnifeId).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x=>x.WhLocation).ThenInclude(x=>x.WhArea).ThenInclude(x=>x.WareHouse)
                    .Include(x=>x.ItemMaster).ThenInclude(x=>x.ItemCategory)
                    .Where(x => knifeIDs.Contains(x.ID.ToString())).ToList();
                if (knifes is null || knifes.Count != info.Knifes.Count)
                {
                    MSD.AddModelError("", $"错误的刀具id 请检查输入");
                    return;
                }
                //3.遍历刀具判断   刀具的状态  /库位状态
                foreach (var knife in knifes)
                {
                    if (knife.Status != KnifeStatusEnum.InStock)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}的状态不是在库 无法报废或者不良退回");
                        return;
                    }
                    if (knife.WhLocation.Locked == true)
                    {
                        MSD.AddModelError("", $"{knife.SerialNumber}所在的库位{knife.WhLocation.Code}已锁定 不可用");
                        return;
                    }
                    if (knife.WhLocation.IsEffective == EffectiveEnum.Ineffective || knife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"刀具所在库位{knife.WhLocation.Code}无效");
                        return ;
                    }
                }
                //校验通过 初始化阶段
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();//单号vm
                var KnifeScrapDocNo = DocNoVm.GetSequence("KnifeScrapRule", tran);//生成报废单单号
                Entity.DocType = info.ScrapType;
                Entity.DocNo = KnifeScrapDocNo;
                Entity.HandledBy = handledBy;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.Status = KnifeOrderStatusEnum.Open;
                Entity.WareHouseId = whid;
                Entity.KnifeScrapLine_KnifeScrap = new List<KnifeScrapLine>();

                foreach (var line in info.Knifes)
                {
                    var knife = knifes.FirstOrDefault(x => x.ID == Guid.Parse(line.KnifeId));
                    Entity.KnifeScrapLine_KnifeScrap.Add(new KnifeScrapLine { 
                        KnifeId = knife.ID,
                        IsAccident = line.IsAccident ,
                        FromWhLocationId =knife.WhLocationId,
                        ToWhLocationId = knife.WhLocationId,
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
            catch (Exception ex)
            {
                MSD.AddModelError("", "异常 错误信息:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 刀具报废单审核
        /// </summary>
        public void DoApproved()
        {
            try
            {
                Guid whid;
                // PDA从登录属性获取
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                //重载entity
                Entity = DC.Set<KnifeScrap>()
                    .Include(x=>x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x=>x.Knife)
                            .ThenInclude(x=>x.ItemMaster)
                    .Include(x => x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                    .Where(x => x.ID == Entity.ID).FirstOrDefault();
                if(Entity is null)
                {
                    MSD.AddModelError("", "报废单vm无效 请检查输入");
                    return;
                }
                //校验阶段 :审核单单据状态正常  
                if (string.IsNullOrEmpty(Entity.DocNo))
                {
                    MSD.AddModelError("", "报废单审核时单号为不可空");
                    return;
                }
                if (!Enum.IsDefined(typeof(KnifeScrapTypeEnum), Entity.DocType))
                {
                    MSD.AddModelError("", "报废单审核时无有效单据类型");
                    return;
                }
                if ( Entity.Status!=KnifeOrderStatusEnum.Open)
                {
                    MSD.AddModelError("", "报废单审核时状态不可为开立");
                    return;
                }
                if (Entity.HandledById is null)
                {
                    MSD.AddModelError("", "报废单审核时未检测到经办人");
                    return;
                }
                //校验阶段: 对应的刀状态在库
                foreach(var scrapLine in Entity.KnifeScrapLine_KnifeScrap)
                {
                    if (scrapLine.Knife.Status != KnifeStatusEnum.InStock)
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}不在库 无法进行报废单审核");
                        return;
                    }
                    if (string.IsNullOrEmpty(scrapLine.Knife.MiscShipLineID))
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}的杂发行id为空 从PDA领用的刀才可以报废");
                        return;
                    }
                }
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
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                var knifes = Entity.KnifeScrapLine_KnifeScrap
                    .Select(x => x.Knife)
                    .ToList();

                //修改阶段
                switch (Entity.DocType)
                {
                    case KnifeScrapTypeEnum.NormalScrap://正常报废
                        //修改自身报废单  刀具信息   操作记录
                        Entity.Status = KnifeOrderStatusEnum.Approved;
                        Entity.HandledBy = handledBy;
                        Entity.HandledById = handledBy.ID.ToString();
                        Entity.ApprovedTime = currentTime;
                        Entity.UpdateBy = LoginUserInfo.Name;
                        Entity.UpdateTime = currentTime;
                        //遍历刀具列表 刀具信息和操作
                        foreach(var line in Entity.KnifeScrapLine_KnifeScrap)
                        {
                            var knife = line.Knife;
                            var usedays = knife.RemainingUsedDays;
                            line.Knife.Status = KnifeStatusEnum.ScrapRequested;
                            line.Knife.HandledBy = Entity.HandledBy;
                            line.Knife.HandledById = Entity.HandledById;
                            line.Knife.HandledByName = Entity.HandledBy.Name;
                            line.Knife.LastOperationDate = currentTime;
                            //knife.RemainingUsedDays = 0;
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                Knife = line.Knife,
                                KnifeId = line.Knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.ScrapRequested,
                                OperationTime = currentTime,
                                OperationBy = handledBy_operator,
                                OperationById = handledBy_operator.ID,
                                HandledBy = Entity.HandledBy,
                                HandledById = handledBy.ID.ToString(),
                                HandledByName = handledBy.Name,
                                UsedDays = 0,//报废申请  使用天数为0      //剩余天数  方便折旧时计算
                                TotalUsedDays = knife.TotalUsedDays,//累计使用天数不变
                                RemainingDays = line.Knife.RemainingUsedDays,//剩余不动
                                CurrentLife = line.Knife.CurrentLife,
                                WhLocationId = line.Knife.WhLocationId,
                                WhLocation = line.Knife.WhLocation,
                                GrindNum = knife.GrindCount,
                                IsAccident = line.IsAccident,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime
                            });

                        }

                        break;
                    case KnifeScrapTypeEnum.DefectiveScrap://不良品退回 生成杂收单
                        //准备杂收单数据
                        List<(WMS.Model.KnifeManagement.Knife, string)> knifeTuples = new List<(WMS.Model.KnifeManagement.Knife, string)> ();
                        
                        foreach (var knife in knifes)//
                        {
                            knifeTuples.Add(( knife, knife.MiscShipLineID));

                        }
                        //修改自身报废单  刀具信息   操作记录  U9杂收单
                        //U9
                        string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                        string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                        U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0410", LoginUserInfo.ITCode);//不良品退回固定组织
                        U9Return<MiscRcvTransData> u9Return = apiHelper.CreateAndApprovedMiscRcvTrans(handledBy_operator, knifes);
                        if (!u9Return.Success)
                        {
                            MSD.AddModelError("", u9Return.Msg);
                            return;
                        }
                        //修改自身报废单  刀具信息   操作记录
                        Entity.Status = KnifeOrderStatusEnum.ApproveClose;
                        Entity.HandledBy = handledBy;
                        Entity.HandledById = handledBy.ID.ToString();
                        Entity.ApprovedTime = currentTime;
                        //Entity.U9MiscRcvDocNo = u9Return.Entity.DocNo;//杂收单单号 目前放在msg里
                        Entity.U9MiscRcvDocNo = u9Return.Msg;//杂收单单号 目前放在msg里
                        Entity.UpdateBy = LoginUserInfo.Name;
                        Entity.UpdateTime = currentTime;
                        //遍历刀具列表 刀具信息和操作
                        foreach (var line in Entity.KnifeScrapLine_KnifeScrap)
                        {
                            var knife = line.Knife;
                            var usedays = knife.RemainingUsedDays;
                            knife.Status = KnifeStatusEnum.DefectiveReturned;
                            knife.HandledBy = Entity.HandledBy;
                            knife.HandledById = Entity.HandledById;
                            knife.LastOperationDate = currentTime;
                            knife.RemainingUsedDays = 0;//剩余使用天数归零 为了操作记录里写入剩余使用天数所以后置
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                Knife = knife,
                                KnifeId = knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.Scrap,
                                OperationTime = currentTime,
                                OperationBy = handledBy_operator,
                                OperationById = handledBy_operator.ID,
                                HandledBy = Entity.HandledBy,
                                HandledById = handledBy.ID.ToString(),
                                UsedDays = usedays,//报废 使用天数为剩余天数  方便折旧时计算
                                TotalUsedDays =knife.TotalUsedDays,//累计使用天数不变
                                RemainingDays = 0,//剩余归零
                                CurrentLife = line.Knife.CurrentLife,
                                WhLocationId = line.Knife.WhLocationId,
                                WhLocation = line.Knife.WhLocation,
                                GrindNum =knife.GrindCount,
                                IsAccident = line.IsAccident,
                                CreateBy = LoginUserInfo.Name,
                                CreateTime = currentTime
                            });
                            //knife.RemainingUsedDays = 0;//剩余使用天数归零 为了操作记录里写入剩余使用天数所以后置

                            //每一把报废的刀  新增一条对应的库存信息   一条流水记录
                            //每个存储地点   一个不良退回库位
                            var wareHouseCode = DC.Set<BaseWareHouse>().Where(x => x.ID == whid).FirstOrDefault().Code;
                            var whLocationCode_DefectiveScrap = wareHouseCode + "A9999999";//
                            var whLocation_DefectiveScrap = DC.Set<BaseWhLocation>().Where(x => x.Code == whLocationCode_DefectiveScrap).FirstOrDefault();
                            if(whLocation_DefectiveScrap is null)
                            {
                                MSD.AddModelError("", "该存储地点的不良退回库位 未配置");
                                return;
                            }
                            line.ToWhLocationId = whLocation_DefectiveScrap.ID;
                            //生成对应库存信息
                            BaseInventoryApiVM InventoryVm = Wtm.CreateVM<BaseInventoryApiVM>();
                            InventoryVm.Create(knife.ItemMaster.Code, whLocationCode_DefectiveScrap, 1, null, false, 1, 0);//无番号 非作废 外购  非冻结 
                            InventoryVm.Entity.SerialNumber = knife.SerialNumber;
                            //生成对应流水记录
                            this.CreateInvLog(OperationTypeEnum.InventoryOtherReceivementCreate, Entity.U9MiscRcvDocNo, null, InventoryVm.Entity.ID, null, 1);//流水记录  类型其他出库  出完

                        }


                        break;
                    default:
                        MSD.AddModelError("", "无效的报废单类型 报废单审核失败");
                        return;
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "报废单审核操作失败 异常 错误信息:" + ex.Message);
                return;
            }
        }


        /// <summary>
        /// OA归档导致的报废单审核关闭 
        /// </summary>
        /// <param name="info"></param>
        public void OADoApproved(OAKnifeScrapInputInfo info)
        {
            try
            {
               
                //重载entity
                Entity = DC.Set<KnifeScrap>()
                    .Include(x => x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.ItemMaster)
                    .Include(x => x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                    .Where(x => x.DocNo == info.ScrapDocNo)
                    .FirstOrDefault();
                if (Entity is null)
                {
                    MSD.AddModelError("", "报废单单号无效");
                    return;
                }
                if (Entity.Status == KnifeOrderStatusEnum.Open)
                {
                    MSD.AddModelError("", $"报废单{Entity.DocNo}未审核 ");
                    return;
                }
                if (Entity.Status == KnifeOrderStatusEnum.ApproveClose|| Entity.Status == KnifeOrderStatusEnum.SuspendClose)
                {
                    MSD.AddModelError("", $"报废单{Entity.DocNo}已关闭 ");
                    return;
                }
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == info.OperatorCode);//系统OA触发 经办人 就是申请人  待定
                if (handledBy is null)
                {
                    MSD.AddModelError("", $"WMS未找到有效用户{info.OperatorCode}");
                    return;
                }
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code == info.OperatorCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型 
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "刀具报废请先配置用户在u9刀具中心的业务员身份并同步到wms");
                    return;
                }


                //校验阶段 :审核单单据状态正常  
                if (string.IsNullOrEmpty(Entity.DocNo))
                {
                    MSD.AddModelError("", "报废单审核时单号为不可空");
                    return;
                }
                if (!Enum.IsDefined(typeof(KnifeScrapTypeEnum), Entity.DocType))
                {
                    MSD.AddModelError("", "报废单审核时无有效单据类型");
                    return;
                }
                if (Entity.Status != KnifeOrderStatusEnum.Approved)
                {
                    MSD.AddModelError("", "此报废单状态不为审核");
                    return;
                }
                /*if (Entity.HandledById is null)
                {
                    MSD.AddModelError("", "报废单审核时未检测到经办人");
                    return;
                }*///OA触发 不用经办人

                //校验阶段: 对应的刀状态在库
                foreach (var scrapLine in Entity.KnifeScrapLine_KnifeScrap)
                {
                    if (scrapLine.Knife.Status != KnifeStatusEnum.ScrapRequested)
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}刀具状态不为报废申请 无法进行报废单审核");
                        return;
                    }
                    if (string.IsNullOrEmpty(scrapLine.Knife.MiscShipLineID))
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}的杂发行id为空 从PDA领用的刀才可以报废");
                        return;
                    }
                }

                //修改阶段
                var currentTime = DateTime.Now;
                var knifes = Entity.KnifeScrapLine_KnifeScrap
                    .Select(x => x.Knife)
                    .ToList();

                switch (Entity.DocType)
                {
                    case KnifeScrapTypeEnum.NormalScrap://正常报废通过了
                        //修改自身报废单  刀具信息   操作记录
                        Entity.Status = KnifeOrderStatusEnum.ApproveClose;
                        Entity.HandledBy = handledBy;
                        Entity.HandledById = handledBy.ID.ToString();
                        Entity.CloseTime = currentTime;
                        Entity.UpdateBy = handledBy.Name;
                        Entity.UpdateTime = currentTime;

                        //遍历刀具列表 刀具信息和操作
                        foreach (var line in Entity.KnifeScrapLine_KnifeScrap)
                        {
                            var knife = line.Knife;
                            var usedays = knife.RemainingUsedDays;
                            line.Knife.Status = KnifeStatusEnum.Scrapped;
                            line.Knife.HandledBy = Entity.HandledBy;
                            line.Knife.HandledById = Entity.HandledById;
                            line.Knife.HandledByName = Entity.HandledBy.Name;
                            line.Knife.LastOperationDate = currentTime;
                            knife.RemainingUsedDays = 0;
                            DC.Set<KnifeOperation>().Add(new KnifeOperation
                            {
                                Knife = line.Knife,
                                KnifeId = line.Knife.ID,
                                DocNo = Entity.DocNo,
                                OperationType = KnifeOperationTypeEnum.Scrap,
                                OperationTime = currentTime,
                                HandledBy = Entity.HandledBy,
                                HandledById = Entity.HandledById,
                                HandledByName = Entity.HandledBy.Name,
                                OperationBy = handledBy_operator,
                                OperationById = handledBy_operator.ID,
                                UsedDays = usedays,//报废  使用天数为剩余天数  方便折旧时计算
                                TotalUsedDays = knife.TotalUsedDays,//累计使用天数不变
                                RemainingDays = 0,//剩余归零
                                CurrentLife = line.Knife.CurrentLife,
                                WhLocationId = line.Knife.WhLocationId,
                                WhLocation = line.Knife.WhLocation,
                                GrindNum = knife.GrindCount,
                                IsAccident = line.IsAccident,
                                CreateBy = handledBy.Name,
                                CreateTime = currentTime
                            });

                        }

                        break;
                    case KnifeScrapTypeEnum.DefectiveScrap://不良品退回 不走OA
                        MSD.AddModelError("", "不良退回的刀具不走OA审批流 报废单审核失败");
                        return;
                    default:
                        MSD.AddModelError("", "无效的报废单类型 报废单审核失败");
                        return;
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "报废单审核操作失败 异常错误信息:" + ex.Message);
                return;
            }
        }
        /// <summary>
        /// 单张刀具报废单手动关闭
        /// </summary>
        /// <param name="id"></param>
        public void DoClose(string id)
        {
            try
            {

                //登录存储地点未失效
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid whid;//当前pda持有者(仓管员)的存储地点
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
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
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code.ToString() == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");//仓管员_业务员类型
                if (handledBy_operator is null)
                {
                    MSD.AddModelError("", "请先配置U9仓管员的业务员身份并同步到wms");
                    return;
                }
                //重载entity
                Entity = DC.Set<KnifeScrap>()
                    .Include(x => x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.ItemMaster)
                    .Include(x => x.KnifeScrapLine_KnifeScrap)
                        .ThenInclude(x => x.Knife)
                            .ThenInclude(x => x.WhLocation)
                                .ThenInclude(x => x.WhArea)
                                    .ThenInclude(x => x.WareHouse)
                    .Where(x => x.ID == Guid.Parse(id)).FirstOrDefault();
                if (Entity is null)
                {
                    MSD.AddModelError("", "未找到报废单");
                    return;
                }
                var knifes = Entity.KnifeScrapLine_KnifeScrap
                    .Select(x => x.Knife)
                    .ToList();
                //校验阶段 :审核单单据状态正常  
                if (Entity.Status != KnifeOrderStatusEnum.Approved)
                {
                    MSD.AddModelError("", "报废单状态不为审核 无法关闭");
                    return;
                }

                //校验阶段: 对应的刀状态为报废申请
                foreach (var scrapLine in Entity.KnifeScrapLine_KnifeScrap)
                {
                    if (scrapLine.Knife.Status != KnifeStatusEnum.ScrapRequested)
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}不为报废申请 无法关闭报废单");
                        return;
                    }
                    if (string.IsNullOrEmpty(scrapLine.Knife.MiscShipLineID))
                    {
                        MSD.AddModelError("", $"检测到刀具{scrapLine.Knife.SerialNumber}的杂发行id为空 从PDA领用的刀才可以进行报废操作");
                        return;
                    }
                }

                //修改阶段 关闭报废单  修改自身报废单  刀具信息   操作记录

                Entity.Status = KnifeOrderStatusEnum.SuspendClose;
                Entity.HandledBy = handledBy;
                Entity.HandledById = handledBy.ID.ToString();
                Entity.ApprovedTime = currentTime;
                Entity.UpdateBy = LoginUserInfo.Name;
                Entity.UpdateTime = currentTime;
                //遍历刀具列表 刀具信息和操作
                foreach (var line in Entity.KnifeScrapLine_KnifeScrap)
                {
                    var knife = line.Knife;
                    line.Knife.Status = KnifeStatusEnum.InStock;
                    line.Knife.HandledBy = Entity.HandledBy;
                    line.Knife.HandledById = Entity.HandledById;
                    line.Knife.LastOperationDate = currentTime;
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        Knife = line.Knife,
                        KnifeId = line.Knife.ID,
                        DocNo = Entity.DocNo,
                        OperationType = KnifeOperationTypeEnum.SuspendClose,
                        OperationTime = currentTime,
                        OperationBy = handledBy_operator,
                        OperationById = handledBy_operator.ID,
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

                   


            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "报废单审核操作失败 异常 错误信息:" + ex.Message);
                return;
            }
        }
    }
}
