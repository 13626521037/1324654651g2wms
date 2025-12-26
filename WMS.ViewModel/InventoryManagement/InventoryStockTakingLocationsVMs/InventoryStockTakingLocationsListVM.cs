using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs
{
    public partial class InventoryStockTakingLocationsListVM : BasePagedListVM<InventoryStockTakingLocations_View, InventoryStockTakingLocationsSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryStockTakingLocations_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingLocations_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingLocations_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTakingLocations>()
                                .Select(x => new InventoryStockTakingLocations_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryStockTakingLocations_View: InventoryStockTakingLocations
    {
        
    }

}