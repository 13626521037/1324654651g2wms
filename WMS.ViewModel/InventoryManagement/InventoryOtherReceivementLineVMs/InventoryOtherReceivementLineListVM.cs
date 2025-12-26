using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementLineVMs
{
    public partial class InventoryOtherReceivementLineListVM : BasePagedListVM<InventoryOtherReceivementLine_View, InventoryOtherReceivementLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryOtherReceivementLine_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherReceivementLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<InventoryOtherReceivementLine_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherReceivementLine>()
                                .Select(x => new InventoryOtherReceivementLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryOtherReceivementLine_View: InventoryOtherReceivementLine
    {
        
    }

}