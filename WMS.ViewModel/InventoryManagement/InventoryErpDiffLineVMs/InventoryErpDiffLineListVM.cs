using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffLineVMs
{
    public partial class InventoryErpDiffLineListVM : BasePagedListVM<InventoryErpDiffLine_View, InventoryErpDiffLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryErpDiffLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryErpDiffLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryErpDiffLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryErpDiffLine>()
                                .Select(x => new InventoryErpDiffLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryErpDiffLine_View: InventoryErpDiffLine
    {
        
    }

}