using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnLineVMs
{
    public partial class PurchaseOutsourcingReturnLineListVM : BasePagedListVM<PurchaseOutsourcingReturnLine_View, PurchaseOutsourcingReturnLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseOutsourcingReturnLine_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingReturnLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<PurchaseOutsourcingReturnLine_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseOutsourcingReturnLine>()
                                .Select(x => new PurchaseOutsourcingReturnLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseOutsourcingReturnLine_View: PurchaseOutsourcingReturnLine
    {
        
    }

}