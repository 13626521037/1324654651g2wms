using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferInLineVMs
{
    public partial class InventoryTransferInLineListVM : BasePagedListVM<InventoryTransferInLine_View, InventoryTransferInLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferInLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferInLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryTransferInLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferInLine>()
                                .Select(x => new InventoryTransferInLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryTransferInLine_View: InventoryTransferInLine
    {
        
    }

}