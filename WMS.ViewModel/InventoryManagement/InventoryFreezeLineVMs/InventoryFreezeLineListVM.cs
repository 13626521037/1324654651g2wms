using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryFreezeLineVMs
{
    public partial class InventoryFreezeLineListVM : BasePagedListVM<InventoryFreezeLine_View, InventoryFreezeLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryFreezeLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryFreezeLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryFreezeLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryFreezeLine>()
                                .Select(x => new InventoryFreezeLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryFreezeLine_View: InventoryFreezeLine
    {
        
    }

}