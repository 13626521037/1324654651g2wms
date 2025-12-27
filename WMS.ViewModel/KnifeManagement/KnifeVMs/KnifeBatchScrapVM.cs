using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;

namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeBatchScrapVM : BaseBatchVM<Knife, Knife_BatchScrap>
    {
        /// <summary>
        /// 每把刀具的意外报废状态（从表单提交）
        /// </summary>
        public List<KnifeAccidentItem> KnifeAccidentList { get; set; } = new List<KnifeAccidentItem>();

        public KnifeBatchScrapVM()
        {
            ListVM = new KnifeBatchScrapListVM();
            LinkedVM = new Knife_BatchScrap();
        }

        protected override void InitVM()
        {
            base.InitVM();
            // 初始化每把刀具的意外报废状态
            if (Ids != null && Ids.Length > 0)
            {
                KnifeAccidentList = Ids.Select(id => new KnifeAccidentItem 
                { 
                    KnifeId = id, 
                    IsAccident = false 
                }).ToList();
                
                // 加载列表数据以便在视图中显示
                if (ListVM != null)
                {
                    ListVM.Ids = Ids.ToList();
                    ListVM.DoSearch();
                }
            }
        }

        /// <summary>
        /// 批量报废操作 - 直接实现与PDA相同的逻辑
        /// </summary>
        /// <returns>返回错误信息，成功时返回null</returns>
        public string DoBatchScrap()
        {
            try
            {
                // 获取选中的刀具IDs
                if (Ids == null || Ids.Length == 0)
                {
                    return "请选择要报废的刀具";
                }

                // 校验报废类型
                if (LinkedVM.DocType == null)
                {
                    return "请选择报废类型";
                }

                // 获取登录人信息
                var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);
                if (handledBy == null)
                {
                    return "登录人信息无效，请重新登录";
                }

                // 获取业务员信息
                var handledBy_operator = DC.Set<BaseOperator>()
                    .Include(x => x.Department)
                        .ThenInclude(x => x.Organization)
                    .Where(x => x.IsValid == true)
                    .FirstOrDefault(x => x.Code == LoginUserInfo.ITCode && x.Department.Organization.Code == "0410");
                if (handledBy_operator == null)
                {
                    return "请先配置U9仓管员的业务员身份并同步到WMS";
                }

                var currentTime = DateTime.Now;
                var knifeIds = Ids.Select(x => Guid.Parse(x)).ToList();
                var knifes = DC.Set<Knife>()
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Include(x => x.ItemMaster).ThenInclude(x => x.ItemCategory)
                    .Where(x => knifeIds.Contains(x.ID))
                    .ToList();

                if (knifes == null || knifes.Count != Ids.Length)
                {
                    return "部分刀具不存在，请检查";
                }

                // 获取存储地点（从刀具所在库位获取）
                var firstKnife = knifes.First();
                var whid = firstKnife.WhLocation.WhArea.WareHouse.ID;

                // 遍历校验刀具状态
                foreach (var knife in knifes)
                {
                    if (knife.Status != KnifeStatusEnum.InStock)
                    {
                        return $"{knife.SerialNumber}的状态不是在库，无法报废";
                    }
                    if (knife.WhLocation.Locked == true)
                    {
                        return $"{knife.SerialNumber}所在的库位{knife.WhLocation.Code}已锁定，不可用";
                    }
                    if (knife.WhLocation.IsEffective == EffectiveEnum.Ineffective || knife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        return $"刀具所在库位{knife.WhLocation.Code}无效";
                    }
                    if (string.IsNullOrEmpty(knife.MiscShipLineID))
                    {
                        return $"检测到刀具{knife.SerialNumber}的杂发行ID为空，从PDA领用的刀具才可以报废";
                    }
                }

                // 生成报废单号
                var DocNoVm = Wtm.CreateVM<BaseSequenceDefineVM>();
                var tran = DC.Database.BeginTransaction();
                try
                {
                    var KnifeScrapDocNo = DocNoVm.GetSequence("KnifeScrapRule", tran);

                    // 创建报废单
                    var knifeScrap = new KnifeScrap
                    {
                        DocNo = KnifeScrapDocNo,
                        DocType = LinkedVM.DocType,
                        Status = KnifeOrderStatusEnum.Approved, // 直接设为已审核状态
                        HandledBy = handledBy,
                        HandledById = handledBy.ID.ToString(),
                        WareHouseId = whid,
                        ApprovedTime = currentTime,
                        CreateBy = LoginUserInfo.Name,
                        CreateTime = currentTime,
                        UpdateBy = LoginUserInfo.Name,
                        UpdateTime = currentTime,
                        KnifeScrapLine_KnifeScrap = new List<KnifeScrapLine>()
                    };

                    // 创建报废行并更新刀具状态
                    foreach (var knife in knifes)
                    {
                        // 从提交的列表中获取该刀具的意外报废状态
                        var isAccident = KnifeAccidentList?.FirstOrDefault(x => x.KnifeId == knife.ID.ToString())?.IsAccident ?? false;
                        
                        // 添加报废行
                        knifeScrap.KnifeScrapLine_KnifeScrap.Add(new KnifeScrapLine
                        {
                            KnifeId = knife.ID,
                            IsAccident = isAccident,
                            FromWhLocationId = knife.WhLocationId,
                            ToWhLocationId = knife.WhLocationId,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime
                        });

                        // 更新刀具状态为报废申请
                        knife.Status = KnifeStatusEnum.ScrapRequested;
                        knife.HandledBy = handledBy;
                        knife.HandledById = handledBy.ID.ToString();
                        knife.HandledByName = handledBy.Name;
                        knife.LastOperationDate = currentTime;

                        // 创建操作记录
                        DC.Set<KnifeOperation>().Add(new KnifeOperation
                        {
                            Knife = knife,
                            KnifeId = knife.ID,
                            DocNo = KnifeScrapDocNo,
                            OperationType = KnifeOperationTypeEnum.ScrapRequested,
                            OperationTime = currentTime,
                            OperationBy = handledBy_operator,
                            OperationById = handledBy_operator.ID,
                            HandledBy = handledBy,
                            HandledById = handledBy.ID.ToString(),
                            HandledByName = handledBy.Name,
                            UsedDays = 0, // 报废申请 使用天数为0
                            TotalUsedDays = knife.TotalUsedDays,
                            RemainingDays = knife.RemainingUsedDays,
                            CurrentLife = knife.CurrentLife,
                            WhLocationId = knife.WhLocationId,
                            WhLocation = knife.WhLocation,
                            GrindNum = knife.GrindCount,
                            IsAccident = isAccident,
                            CreateBy = LoginUserInfo.Name,
                            CreateTime = currentTime
                        });
                    }

                    // 关闭涉及的组合单
                    var toCloseCombineLines = DC.Set<KnifeCombineLine>()
                        .Include(x => x.KnifeCombine)
                        .Where(x => x.KnifeCombine.Status == KnifeOrderStatusEnum.Approved)
                        .Where(x => knifes.Contains(x.Knife))
                        .ToList();
                    if (toCloseCombineLines != null && toCloseCombineLines.Count > 0)
                    {
                        var toCloseCombines = toCloseCombineLines.Select(x => x.KnifeCombine).Distinct().ToList();
                        foreach (var toCloseCombine in toCloseCombines)
                        {
                            toCloseCombine.Status = KnifeOrderStatusEnum.ApproveClose;
                            toCloseCombine.CloseTime = DateTime.Now;
                        }
                    }

                    DC.Set<KnifeScrap>().Add(knifeScrap);
                    DC.SaveChanges();
                    tran.Commit();
                    return null; // 成功
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return "创建报废单失败：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "批量报废操作失败：" + ex.Message;
            }
        }
    }

    /// <summary>
    /// 批量报废编辑字段
    /// </summary>
    public class Knife_BatchScrap : BaseVM
    {
        [Display(Name = "报废类型")]
        [Required(ErrorMessage = "请选择报废类型")]
        public KnifeScrapTypeEnum? DocType { get; set; }

        protected override void InitVM()
        {
        }
    }

    /// <summary>
    /// 刀具意外报废状态项
    /// </summary>
    public class KnifeAccidentItem
    {
        public string KnifeId { get; set; }
        public bool IsAccident { get; set; }
    }

    /// <summary>
    /// 批量报废刀具列表
    /// </summary>
    public partial class KnifeBatchScrapListVM : BasePagedListVM<Knife_View, KnifeSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>();
        }

        protected override IEnumerable<IGridColumn<Knife_View>> InitGridHeader()
        {
            return new List<GridColumn<Knife_View>>{
                this.MakeGridHeader(x => x.Knife_SerialNumber).SetTitle("序列号").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_Status).SetTitle("状态").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_ItemMaster).SetTitle("料品").SetWidth(100),
                this.MakeGridHeader(x => x.Knife_WareHouse).SetTitle("存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.Knife_WhLocation).SetTitle("库位").SetWidth(80),
                this.MakeGridHeader(x => x.Knife_CurrentLife).SetTitle("当前寿命").SetWidth(60),
                this.MakeGridHeader(x => x.Knife_RemainingUsedDays).SetTitle("剩余使用天数").SetWidth(100),
            };
        }

        public override IOrderedQueryable<Knife_View> GetSearchQuery()
        {
            var query = DC.Set<Knife>()
                .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                .Include(x => x.ItemMaster)
                .CheckIDs(Ids)
                .Select(x => new Knife_View
                {
                    ID = x.ID,
                    Knife_SerialNumber = x.SerialNumber,
                    Knife_Status = x.Status,
                    Knife_ItemMaster = x.ItemMaster.Code,
                    Knife_WareHouse = x.WhLocation.WhArea.WareHouse.Name,
                    Knife_WhLocation = x.WhLocation.Code,
                    Knife_CurrentLife = x.CurrentLife,
                    Knife_RemainingUsedDays = x.RemainingUsedDays,
                })
                .OrderByDescending(x => x.Knife_SerialNumber);
            return query;
        }
    }
}
