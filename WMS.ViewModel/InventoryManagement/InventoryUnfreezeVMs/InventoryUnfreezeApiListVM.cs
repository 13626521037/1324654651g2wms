using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeApiListVM : BasePagedListVM<InventoryUnfreezeApi_View, InventoryUnfreezeApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryUnfreezeApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryUnfreezeApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Reason),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryUnfreezeApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryUnfreeze>()
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .Select(x => new InventoryUnfreezeApi_View
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

    public class InventoryUnfreezeApi_View : InventoryUnfreeze{

    }
}
