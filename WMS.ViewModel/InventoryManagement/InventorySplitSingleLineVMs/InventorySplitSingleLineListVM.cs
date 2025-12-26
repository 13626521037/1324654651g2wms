using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleLineVMs
{
    public partial class InventorySplitSingleLineListVM : BasePagedListVM<InventorySplitSingleLine_View, InventorySplitSingleLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventorySplitSingleLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplitSingleLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventorySplitSingleLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventorySplitSingleLine>()
                                .Select(x => new InventorySplitSingleLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventorySplitSingleLine_View: InventorySplitSingleLine
    {
        
    }

}