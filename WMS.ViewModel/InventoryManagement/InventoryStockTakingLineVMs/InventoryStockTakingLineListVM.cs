using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLineVMs
{
    public partial class InventoryStockTakingLineListVM : BasePagedListVM<InventoryStockTakingLine_View, InventoryStockTakingLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryStockTakingLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTakingLine>()
                                .Select(x => new InventoryStockTakingLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryStockTakingLine_View: InventoryStockTakingLine
    {
        
    }

}