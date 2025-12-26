
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectLineVMs
{
    public partial class InventoryTransferOutDirectLineInventoryTransferOutDirectDetailListVM : BasePagedListVM<InventoryTransferOutDirectLine_DetailView, InventoryTransferOutDirectLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryTransferOutDirectLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutDirectLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ErpID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP行ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.序列号"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                //this.MakeGridHeader(x => x.TransInQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.调入数量"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryTransferOutDirectLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryTransferOutDirectId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryTransferOutDirectLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryTransferOutDirectLine>()
                .Where(x => id == x.InventoryTransferOutDirectId)
                .Select(x => new InventoryTransferOutDirectLine_DetailView
                {
                     ID = x.ID,
                     ErpID = x.ErpID,
                     DocLineNo = x.DocLineNo,
                     ItemMasterId = x.ItemMasterId,
                     SerialNumber = x.SerialNumber,
                     Qty = x.Qty.TrimZero(),
                     TransInQty = x.TransInQty.TrimZero(),
                     Memo = x.Memo,
                     ItemCode = x.ItemMaster.Code
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryTransferOutDirectLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryTransferOutDirectLine._InventoryTransferOutDirect")]
        public string InventoryTransferOutDirectId { get; set; }
    }
    public class InventoryTransferOutDirectLine_DetailView : InventoryTransferOutDirectLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
    }
}

