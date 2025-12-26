using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeLineVMs
{
    public partial class InventoryUnfreezeLineListVM : BasePagedListVM<InventoryUnfreezeLine_View, InventoryUnfreezeLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryUnfreezeLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryUnfreezeLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryUnfreezeLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryUnfreezeLine>()
                                .Select(x => new InventoryUnfreezeLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryUnfreezeLine_View: InventoryUnfreezeLine
    {
        
    }

}