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


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectApiListVM : BasePagedListVM<InventoryAdjustDirectApi_View, InventoryAdjustDirectApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryAdjustDirectApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjustDirectApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.BatchNumber_view),
                this.MakeGridHeader(x => x.BatchNumber_view2),
                this.MakeGridHeader(x => x.DiffQty),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryAdjustDirectApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryAdjustDirect>()
                .Select(x => new InventoryAdjustDirectApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    BatchNumber_view = x.OldInv.BatchNumber,
                    BatchNumber_view2 = x.NewInv.BatchNumber,
                    DiffQty = x.DiffQty,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryAdjustDirectApi_View : InventoryAdjustDirect{
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber_view { get; set; }
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public String BatchNumber_view2 { get; set; }

    }
}
