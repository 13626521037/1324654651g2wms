
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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationLineVMs
{
    public partial class InventoryMoveLocationLineInventoryMoveLocationDetailListVM : BasePagedListVM<InventoryMoveLocationLine_DetailView, InventoryMoveLocationLineDetailSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }

        protected override IEnumerable<IGridColumn<InventoryMoveLocationLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryMoveLocationLine_DetailView>>{

                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ItemCode, width: 100).SetEditType(EditTypeEnum.Text).SetTitle("料号"),
                this.MakeGridHeader(x => x.Sn, width: 150).SetEditType(EditTypeEnum.Text).SetTitle("序列号"),
                this.MakeGridHeader(x => x.OutLocationCode, width: 150).SetEditType(EditTypeEnum.Text).SetTitle("出库库位编码"),
                this.MakeGridHeader(x => x.OutLocationName).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.出库库位"].Value),
                this.MakeGridHeader(x => x.InLocationCode, width: 150).SetEditType(EditTypeEnum.Text).SetTitle("入库库位编码"),
                this.MakeGridHeader(x => x.InLocationName).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.入库库位"].Value),
                this.MakeGridHeader(x => x.Qty, width: 100).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.数量"].Value),
                //this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }


        public override IOrderedQueryable<InventoryMoveLocationLine_DetailView> GetSearchQuery()
        {

            var id = (Guid?)Searcher.InventoryMoveLocationId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryMoveLocationLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryMoveLocationLine>()
                .Where(x => id == x.InventoryMoveLocationId)
                .Select(x => new InventoryMoveLocationLine_DetailView
                {
                    ID = x.ID,
                    Sn = x.BaseInventory.SerialNumber,
                    InLocationName = x.InWhLocation.Name,
                    InLocationCode = x.InWhLocation.Code,
                    OutLocationName = x.OutWhLocation.Name,
                    OutLocationCode = x.OutWhLocation.Code,
                    ItemCode = x.BaseInventory.ItemMaster.Code,
                    Qty = x.Qty.TrimZero(),
                    Memo = x.Memo
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class InventoryMoveLocationLineDetailSearcher : BaseSearcher
    {

        [Display(Name = "_Model._InventoryMoveLocationLine._InventoryMoveLocation")]
        public string InventoryMoveLocationId { get; set; }
    }

    public class InventoryMoveLocationLine_DetailView : InventoryMoveLocationLine
    {
        public string Sn { get; set; }

        public string OutLocationCode { get; set; }

        public string OutLocationName { get; set; }

        public string InLocationCode { get; set; }

        public string InLocationName { get; set; }

        public string ItemCode { get; set; }
    }
}

