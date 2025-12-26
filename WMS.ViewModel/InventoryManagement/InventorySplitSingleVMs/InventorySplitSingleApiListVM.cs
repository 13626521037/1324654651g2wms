using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleApiListVM : BasePagedListVM<InventorySplitSingleApi_View, InventorySplitSingleApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventorySplitSingleApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplitSingleApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.BatchNumber_view),
                this.MakeGridHeader(x => x.OriginalQty),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventorySplitSingleApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventorySplitSingle>()
                .Select(x => new InventorySplitSingleApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    BatchNumber_view = x.OriginalInv.BatchNumber,
                    OriginalQty = x.OriginalQty,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventorySplitSingleApi_View : InventorySplitSingle{
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber_view { get; set; }

    }
}
