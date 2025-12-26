
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


namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs
{
    public partial class ProductionReturnIssueLineProductionReturnIssueDetailListVM : BasePagedListVM<ProductionReturnIssueLine_DetailView, ProductionReturnIssueLineDetailSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }

        protected override IEnumerable<IGridColumn<ProductionReturnIssueLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<ProductionReturnIssueLine_DetailView>>{

                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo, width: 85).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.MODocNo, width: 130).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.生产订单"].Value),
                this.MakeGridHeader(x => x.PickListLineNo, width: 85).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.备料表行号"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待收货数量"].Value),
                this.MakeGridHeader(x => x.ToBeInWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待入库数量"].Value),
                this.MakeGridHeader(x => x.ReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已收货数量"].Value),
                this.MakeGridHeader(x => x.InWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已入库数量"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),
                //this.MakeGridHeader(x => x.SourceSystemId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.LastUpdateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.最后修改时间"].Value),

            };
        }


        public override IOrderedQueryable<ProductionReturnIssueLine_DetailView> GetSearchQuery()
        {

            var id = (Guid?)Searcher.ProductionReturnIssueId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<ProductionReturnIssueLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<ProductionReturnIssueLine>()
                .Where(x => id == x.ProductionReturnIssueId)
                .Select(x => new ProductionReturnIssueLine_DetailView
                {
                    ID = x.ID,
                    ProductionReturnIssueId = x.ProductionReturnIssueId,
                    MODocNo = x.MODocNo,
                    PickListLineNo = x.PickListLineNo,
                    DocLineNo = x.DocLineNo,
                    ItemMasterId = x.ItemMasterId,
                    Qty = x.Qty.TrimZero(),
                    ToBeInWhQty = x.ToBeInWhQty.TrimZero(),
                    ToBeReceiveQty = x.ToBeReceiveQty.TrimZero(),
                    InWhQty = x.InWhQty.TrimZero(),
                    ReceiveQty = x.ReceiveQty.TrimZero(),
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

    public partial class ProductionReturnIssueLineDetailSearcher : BaseSearcher
    {

        [Display(Name = "_Model._ProductionReturnIssueLine._ProductionReturnIssue")]
        public string ProductionReturnIssueId { get; set; }
    }

    public class ProductionReturnIssueLine_DetailView : ProductionReturnIssueLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

