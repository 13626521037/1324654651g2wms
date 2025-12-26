using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualLineVMs
{
    public partial class InventoryTransferOutManualLineListVM : BasePagedListVM<InventoryTransferOutManualLine_View, InventoryTransferOutManualLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferOutManualLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutManualLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryTransferOutManualLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutManualLine>()
                                .Select(x => new InventoryTransferOutManualLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryTransferOutManualLine_View: InventoryTransferOutManualLine
    {
        
    }

}