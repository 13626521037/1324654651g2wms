using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustApiListVM : BasePagedListVM<InventoryAdjustApi_View, InventoryAdjustApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryAdjustApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjustApi_View>>{
                this.MakeGridHeader(x => x.ApproveUser_view),
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryAdjustApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryAdjust>()
                .Select(x => new InventoryAdjustApi_View
                {
				    ID = x.ID,
                    ApproveUser_view = x.StockTaking.ApproveUser,
                    DocNo = x.DocNo,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryAdjustApi_View : InventoryAdjust{
        [Display(Name = "_Model._InventoryStockTaking._ApproveUser")]
        public String ApproveUser_view { get; set; }

    }
}
