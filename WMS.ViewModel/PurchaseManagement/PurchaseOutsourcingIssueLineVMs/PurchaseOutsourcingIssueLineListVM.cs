using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueLineVMs
{
    public partial class PurchaseOutsourcingIssueLineListVM : BasePagedListVM<PurchaseOutsourcingIssueLine_View, PurchaseOutsourcingIssueLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseOutsourcingIssueLine_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingIssueLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<PurchaseOutsourcingIssueLine_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseOutsourcingIssueLine>()
                                .Select(x => new PurchaseOutsourcingIssueLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseOutsourcingIssueLine_View: PurchaseOutsourcingIssueLine
    {
        
    }

}