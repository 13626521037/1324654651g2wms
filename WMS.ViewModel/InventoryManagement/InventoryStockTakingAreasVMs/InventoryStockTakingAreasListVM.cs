using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingAreasVMs
{
    public partial class InventoryStockTakingAreasListVM : BasePagedListVM<InventoryStockTakingAreas_View, InventoryStockTakingAreasSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryStockTakingAreas_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingAreas_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingAreas_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTakingAreas>()
                                .Select(x => new InventoryStockTakingAreas_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryStockTakingAreas_View: InventoryStockTakingAreas
    {
        
    }

}