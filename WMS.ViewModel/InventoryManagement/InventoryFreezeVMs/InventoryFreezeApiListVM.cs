using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeApiListVM : BasePagedListVM<InventoryFreezeApi_View, InventoryFreezeApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryFreezeApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryFreezeApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Reason),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryFreezeApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryFreeze>()
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .Select(x => new InventoryFreezeApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    Reason = x.Reason,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryFreezeApi_View : InventoryFreeze{

    }
}
