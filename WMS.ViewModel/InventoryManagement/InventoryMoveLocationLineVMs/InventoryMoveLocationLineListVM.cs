using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationLineVMs
{
    public partial class InventoryMoveLocationLineListVM : BasePagedListVM<InventoryMoveLocationLine_View, InventoryMoveLocationLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryMoveLocationLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryMoveLocationLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryMoveLocationLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryMoveLocationLine>()
                                .Select(x => new InventoryMoveLocationLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryMoveLocationLine_View: InventoryMoveLocationLine
    {
        
    }

}