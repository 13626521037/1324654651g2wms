using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnLineVMs
{
    public partial class PurchaseReturnLineListVM : BasePagedListVM<PurchaseReturnLine_View, PurchaseReturnLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseReturnLine_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReturnLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<PurchaseReturnLine_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReturnLine>()
                                .Select(x => new PurchaseReturnLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseReturnLine_View: PurchaseReturnLine
    {
        
    }

}