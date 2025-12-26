using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs
{
    public partial class InventoryPalletVirtualLineListVM : BasePagedListVM<InventoryPalletVirtualLine_View, InventoryPalletVirtualLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryPalletVirtualLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryPalletVirtualLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryPalletVirtualLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryPalletVirtualLine>()
                                .Select(x => new InventoryPalletVirtualLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryPalletVirtualLine_View: InventoryPalletVirtualLine
    {
        
    }

}