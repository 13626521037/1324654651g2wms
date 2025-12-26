
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.ProductionManagement;
using WMS.Util;


namespace WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs
{
    public partial class ProductionIssueLineProductionIssueDetailListVM : BasePagedListVM<ProductionIssueLine_DetailView, ProductionIssueLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<ProductionIssueLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<ProductionIssueLine_DetailView>>{
                
                //this.MakeGridHeader(x => x.SourceSystemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.LastUpdateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo, width: 85).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.MODocNo, width: 130).SetEditType(EditTypeEnum.Text).SetTitle("生产订单"),
                this.MakeGridHeader(x => x.PickListLineNo, width: 85).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.备料表行号"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeOffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待下架数量"].Value),
                this.MakeGridHeader(x => x.ToBeShipQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待出货数量"].Value),
                this.MakeGridHeader(x => x.OffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已下架数量"].Value),
                this.MakeGridHeader(x => x.ShippedQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已出货数量"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<ProductionIssueLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.ProductionIssueId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<ProductionIssueLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<ProductionIssueLine>()
                .Where(x => id == x.ProductionIssueId)
                .Select(x => new ProductionIssueLine_DetailView
                {
                    ID = x.ID,
                    ProductionIssueId = x.ProductionIssueId,
                    MODocNo = x.MODocNo,
                    PickListLineNo = x.PickListLineNo,
                    DocLineNo = x.DocLineNo,
                    ItemMasterId = x.ItemMasterId,
                    Qty = x.Qty.TrimZero(),
                    ToBeOffQty = x.ToBeOffQty.TrimZero(),
                    ToBeShipQty = x.ToBeShipQty.TrimZero(),
                    OffQty = x.OffQty.TrimZero(),
                    ShippedQty = x.ShippedQty.TrimZero(),
                    WhId = x.WhId,
                    Status = x.Status,
                    ItemCode = x.ItemMaster.Code,
                    WhCode = x.Wh.Code,
                    SourceSystemId = x.SourceSystemId,
                    LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class ProductionIssueLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._ProductionIssueLine._ProductionIssue")]
        public string ProductionIssueId { get; set; }
    }

    public class ProductionIssueLine_DetailView: ProductionIssueLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

