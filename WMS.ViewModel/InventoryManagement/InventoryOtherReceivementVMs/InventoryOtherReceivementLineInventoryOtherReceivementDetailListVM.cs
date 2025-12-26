
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementLineVMs
{
    public partial class InventoryOtherReceivementLineInventoryOtherReceivementDetailListVM : BasePagedListVM<InventoryOtherReceivementLine_DetailView, InventoryOtherReceivementLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryOtherReceivementLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherReceivementLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ErpID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP行ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                //this.MakeGridHeader(x => x.InventoryOtherShipLineId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.其它出库单行"].Value),
                //this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.序列号"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryOtherReceivementLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryOtherReceivementId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryOtherReceivementLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryOtherReceivementLine>()
                .Where(x => id == x.InventoryOtherReceivementId)
                .Select(x => new InventoryOtherReceivementLine_DetailView
                {
                    ID = x.ID,
                    ErpID = x.ErpID,
                    DocLineNo = x.DocLineNo,
                    ItemMasterId = x.ItemMasterId,
                    SerialNumber = x.SerialNumber,
                    Qty = x.Qty.TrimZero(),
                    Memo = x.Memo,
                    ItemCode = x.ItemMaster.Code
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryOtherReceivementLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherReceivement")]
        public string InventoryOtherReceivementId { get; set; }
    }

    public class InventoryOtherReceivementLine_DetailView : InventoryOtherReceivementLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
    }
}

