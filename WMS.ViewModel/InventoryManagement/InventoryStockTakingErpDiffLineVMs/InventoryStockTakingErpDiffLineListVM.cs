using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs
{
    public partial class InventoryStockTakingErpDiffLineListVM : BasePagedListVM<InventoryStockTakingErpDiffLine_View, InventoryStockTakingErpDiffLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryStockTakingErpDiffLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingErpDiffLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingErpDiffLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTakingErpDiffLine>()
                                .Select(x => new InventoryStockTakingErpDiffLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryStockTakingErpDiffLine_View: InventoryStockTakingErpDiffLine
    {
        
    }

}