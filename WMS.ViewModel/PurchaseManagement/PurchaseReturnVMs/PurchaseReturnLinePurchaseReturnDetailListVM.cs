
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnLineVMs
{
    public partial class PurchaseReturnLinePurchaseReturnDetailListVM : BasePagedListVM<PurchaseReturnLine_DetailView, PurchaseReturnLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<PurchaseReturnLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReturnLine_DetailView>>{
                
                //this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value),
                this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.ToBeOffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待下架数量"].Value),
                this.MakeGridHeader(x => x.ToBeShipQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.待出货数量"].Value),
                this.MakeGridHeader(x => x.OffQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已下架数量"].Value),
                this.MakeGridHeader(x => x.ShippedQty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已出货数量"].Value),
                this.MakeGridHeader(x => x.WhCode).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.Text,typeof(PurchaseReturnLineStatusEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.状态"].Value),

            };
        }

        
        public override IOrderedQueryable<PurchaseReturnLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.PurchaseReturnId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<PurchaseReturnLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<PurchaseReturnLine>()
                .Where(x => id == x.PurchaseReturnId)
                .Select(x => new PurchaseReturnLine_DetailView
                 {
                     ID = x.ID,
                     ItemCode = x.ItemMaster.Code,
                     WhCode = x.WareHouse.Code,
                     DocLineNo = x.DocLineNo,
                     Qty = x.Qty.TrimZero(),
                     ToBeOffQty = x.ToBeOffQty.TrimZero(),
                     ToBeShipQty = x.ToBeShipQty.TrimZero(),
                     OffQty = x.OffQty.TrimZero(),
                     ShippedQty = x.ShippedQty.TrimZero(),
                     Status = x.Status
                 })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class PurchaseReturnLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._PurchaseReturnLine._PurchaseReturn")]
        public string PurchaseReturnId { get; set; }
    }

    public class PurchaseReturnLine_DetailView : PurchaseReturnLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "存储地点")]
        public string WhCode { get; set; }
    }
}

