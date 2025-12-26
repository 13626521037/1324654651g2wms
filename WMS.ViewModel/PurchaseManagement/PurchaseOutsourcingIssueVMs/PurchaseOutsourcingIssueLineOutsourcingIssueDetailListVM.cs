
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;
using WMS.Util;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueLineVMs
{
    public partial class PurchaseOutsourcingIssueLineOutsourcingIssueDetailListVM : BasePagedListVM<PurchaseOutsourcingIssueLine_DetailView, PurchaseOutsourcingIssueLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<PurchaseOutsourcingIssueLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingIssueLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeOffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待下架数量"].Value),
                this.MakeGridHeader(x => x.ToBeShipQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待出货数量"].Value),
                this.MakeGridHeader(x => x.OffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已下架数量"].Value),
                this.MakeGridHeader(x => x.ShippedQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已出货数量"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                //this.MakeGridHeader(x => x.SourceSystemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.LastUpdateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value),

            };
        }

        
        public override IOrderedQueryable<PurchaseOutsourcingIssueLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.OutsourcingIssueId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<PurchaseOutsourcingIssueLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<PurchaseOutsourcingIssueLine>()
                .Where(x => id == x.OutsourcingIssueId)
                .Select(x => new PurchaseOutsourcingIssueLine_DetailView
                {
                     ID = x.ID,
                     OutsourcingIssueId = x.OutsourcingIssueId,
                     DocLineNo = x.DocLineNo,
                     ItemMasterId = x.ItemMasterId,
                     Qty = x.Qty.TrimZero(),
                     ToBeOffQty = x.ToBeOffQty.TrimZero(),
                     ToBeShipQty = x.ToBeShipQty.TrimZero(),
                     OffQty = x.OffQty.TrimZero(),
                     ShippedQty = x.ShippedQty.TrimZero(),
                     WareHouseId = x.WareHouseId,
                     Status = x.Status,
                     ItemCode = x.ItemMaster.Code,
                     WhCode = x.WareHouse.Code,
                     SourceSystemId = x.SourceSystemId,
                     LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class PurchaseOutsourcingIssueLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._OutsourcingIssue")]
        public string OutsourcingIssueId { get; set; }
    }

    public class PurchaseOutsourcingIssueLine_DetailView : PurchaseOutsourcingIssueLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

