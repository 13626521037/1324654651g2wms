using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipLineVMs
{
    public partial class InventoryOtherShipLineListVM : BasePagedListVM<InventoryOtherShipLine_View, InventoryOtherShipLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryOtherShipLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherShipLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryOtherShipLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherShipLine>()
                                .Select(x => new InventoryOtherShipLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryOtherShipLine_View: InventoryOtherShipLine
    {
        
    }

}