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


namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationApiListVM : BasePagedListVM<InventoryMoveLocationApi_View, InventoryMoveLocationApiSearcher>
    {

        protected override IEnumerable<IGridColumn<InventoryMoveLocationApi_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryMoveLocationApi_View>>{
                this.MakeGridHeader(x => x.DocNo),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Memo),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryMoveLocationApi_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryMoveLocation>()
                .Select(x => new InventoryMoveLocationApi_View
                {
				    ID = x.ID,
                    DocNo = x.DocNo,
                    Name_view = x.InWhLocation.Name,
                    Memo = x.Memo,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class InventoryMoveLocationApi_View : InventoryMoveLocation{
        [Display(Name = "_Model._BaseWhLocation._Name")]
        public String Name_view { get; set; }

    }
}
