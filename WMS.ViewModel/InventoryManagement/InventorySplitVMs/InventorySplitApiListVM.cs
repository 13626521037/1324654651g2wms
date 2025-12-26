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


namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitApiListVM : BasePagedListVM<InventorySplitApi_View, InventorySplitApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventorySplitApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplitApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.BatchNumber_view),
                this.MakeGridHeader(x => x.BatchNumber_view2),
                this.MakeGridHeader(x => x.OrigQty),
                this.MakeGridHeader(x => x.SplitQty),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventorySplitApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventorySplit>()
                .Select(x => new InventorySplitApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    BatchNumber_view = x.OldInv.BatchNumber,
                    BatchNumber_view2 = x.NewInv.BatchNumber,
                    OrigQty = x.OrigQty,
                    SplitQty = x.SplitQty,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventorySplitApi_View : InventorySplit{
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber_view { get; set; }
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber_view2 { get; set; }

    }
}
