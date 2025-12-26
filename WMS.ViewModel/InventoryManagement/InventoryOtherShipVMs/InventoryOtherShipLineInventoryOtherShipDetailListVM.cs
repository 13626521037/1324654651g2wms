
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


namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipLineVMs
{
    public partial class InventoryOtherShipLineInventoryOtherShipDetailListVM : BasePagedListVM<InventoryOtherShipLine_DetailView, InventoryOtherShipLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryOtherShipLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherShipLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ErpID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ERP行ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle("料号"),
                this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text).SetTitle("序列号"),
                this.MakeGridHeader(x => x.Seiban).SetEditType(EditTypeEnum.Text).SetTitle("番号"),
                this.MakeGridHeader(x => x.Batch).SetEditType(EditTypeEnum.Text).SetTitle("批号"),
                //this.MakeGridHeader(x => x.InventoryId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库存信息"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                //this.MakeGridHeader(x => x.RcvQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已入库数量"].Value),
                //this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryOtherShipLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.InventoryOtherShipId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryOtherShipLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryOtherShipLine>()
                .Where(x => id == x.InventoryOtherShipId)
                .Select(x => new InventoryOtherShipLine_DetailView
                {
                    ID = x.ID,
                    ErpID = x.ErpID,
                    DocLineNo = x.DocLineNo,
                    InventoryId = x.InventoryId,
                    Qty = x.Qty.TrimZero(),
                    RcvQty = x.RcvQty.TrimZero(),
                    Memo = x.Memo,
                    ItemCode = x.Inventory.ItemMaster.Code,
                    SerialNumber = x.Inventory.SerialNumber,
                    Seiban = x.Inventory.Seiban,
                    Batch = x.Inventory.BatchNumber
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryOtherShipLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryOtherShipLine._InventoryOtherShip")]
        public string InventoryOtherShipId { get; set; }
    }

    public class InventoryOtherShipLine_DetailView : InventoryOtherShipLine
    {
        public string ItemCode { get; set; }

        public string SerialNumber { get; set; }

        public string Seiban { get; set; }

        public string Batch { get; set; }
    }
}

