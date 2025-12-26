using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustLineVMs
{
    public partial class InventoryAdjustLineListVM : BasePagedListVM<InventoryAdjustLine_View, InventoryAdjustLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryAdjustLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjustLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryAdjustLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryAdjustLine>()
                                .Select(x => new InventoryAdjustLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryAdjustLine_View: InventoryAdjustLine
    {
        
    }

}