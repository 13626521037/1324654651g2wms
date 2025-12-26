
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


namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffLineVMs
{
    public partial class InventoryErpDiffLineErpDiffDetailListVM : BasePagedListVM<InventoryErpDiffLine_DetailView, InventoryErpDiffLineDetailSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }

        protected override IEnumerable<IGridColumn<InventoryErpDiffLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryErpDiffLine_DetailView>>{

                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                //this.MakeGridHeader(x => x.ItemCode).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.SerialNumber).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Qty).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.LocationCode).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.AreaCode).SetEditType(EditTypeEnum.Text),

            };
        }


        public override IOrderedQueryable<InventoryErpDiffLine_DetailView> GetSearchQuery()
        {

            var id = (Guid?)Searcher.ErpDiffId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryErpDiffLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryErpDiffLine>()
                .Where(x => id == x.ErpDiffId)
                .Select(x => new InventoryErpDiffLine_DetailView
                {
                     ID = x.ID,
                     InventoryId = x.InventoryId,
                     Qty = x.Qty.TrimZero(),
                     ItemCode = x.Inventory.ItemMaster.Code,
                     LocationCode = x.Inventory.WhLocation.Code,
                     AreaCode = x.Inventory.WhLocation.WhArea.Code,
                     SerialNumber = Common.AddInventoryDialog(x.Inventory)
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryErpDiffLineDetailSearcher : BaseSearcher
    {

        [Display(Name = "_Model._InventoryErpDiffLine._ErpDiff")]
        public string ErpDiffId { get; set; }
    }

    public class InventoryErpDiffLine_DetailView : InventoryErpDiffLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "库位")]
        public string LocationCode { get; set; }

        [Display(Name = "库区")]
        public string AreaCode { get; set; }

        [Display(Name = "序列号")]
        public string SerialNumber { get; set; }
    }
}

