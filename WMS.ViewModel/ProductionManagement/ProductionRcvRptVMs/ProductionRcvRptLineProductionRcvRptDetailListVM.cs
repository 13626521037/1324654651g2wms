
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

using WMS.Model.BaseData;


namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptLineVMs
{
    public partial class ProductionRcvRptLineProductionRcvRptDetailListVM : BasePagedListVM<ProductionRcvRptLine_DetailView, ProductionRcvRptLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<ProductionRcvRptLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<ProductionRcvRptLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo, width: 85).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.MODocNo, width: 130).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.生产订单"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待收货数量"].Value),
                this.MakeGridHeader(x => x.ToBeInWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待入库数量"].Value),
                this.MakeGridHeader(x => x.ReceiveQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已收货数量"].Value),
                this.MakeGridHeader(x => x.InWhQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已入库数量"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),
                //this.MakeGridHeader(x => x.ErpID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP行ID"].Value),

            };
        }

        
        public override IOrderedQueryable<ProductionRcvRptLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.ProductionRcvRptId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<ProductionRcvRptLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<ProductionRcvRptLine>()
                .Where(x => id == x.ProductionRcvRptId)
                .Select(x => new ProductionRcvRptLine_DetailView
                {
                    ID = x.ID,
                    DocLineNo = x.DocLineNo,
                    MODocNo = x.MODocNo,
                    ItemMasterId = x.ItemMasterId,
                    Qty = x.Qty,
                    ToBeReceiveQty = x.ToBeReceiveQty,
                    ToBeInWhQty = x.ToBeInWhQty,
                    ReceiveQty = x.ReceiveQty,
                    InWhQty = x.InWhQty,
                    Status = x.Status,
                    Memo = x.Memo,
                    ErpID = x.ErpID,
                    ItemCode = x.ItemMaster.Code,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class ProductionRcvRptLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._ProductionRcvRptLine._ProductionRcvRpt")]
        public string ProductionRcvRptId { get; set; }
    }

    public class ProductionRcvRptLine_DetailView: ProductionRcvRptLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
    }
}

