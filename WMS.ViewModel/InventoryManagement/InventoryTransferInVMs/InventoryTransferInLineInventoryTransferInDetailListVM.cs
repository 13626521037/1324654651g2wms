
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


namespace WMS.ViewModel.InventoryManagement.InventoryTransferInLineVMs
{
    public partial class InventoryTransferInLineInventoryTransferInDetailListVM : BasePagedListVM<InventoryTransferInLine_DetailView, InventoryTransferInLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryTransferInLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferInLine_DetailView>>{
                
                //this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                //this.MakeGridHeader(x => x.ErpID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP行ID"].Value),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                //this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.序列号"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                //this.MakeGridHeader(x => x.TransferOutLine).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.调出单行"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryTransferInLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryTransferInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryTransferInLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryTransferInLine>()
                .Where(x => id == x.InventoryTransferInId)
                .Select(x => new InventoryTransferInLine_DetailView
                {
                     ID = x.ID,
                     ErpID = x.ErpID,
                     DocLineNo = x.DocLineNo,
                     ItemMasterId = x.ItemMasterId,
                     SerialNumber = x.SerialNumber,
                     Qty = x.Qty.TrimZero(),
                     Status = x.Status,
                     Memo = x.Memo,
                     ItemCode = x.ItemMaster.Code
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryTransferInLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryTransferInLine._InventoryTransferIn")]
        public string InventoryTransferInId { get; set; }
    }

    public class InventoryTransferInLine_DetailView : InventoryTransferInLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
    }
}

