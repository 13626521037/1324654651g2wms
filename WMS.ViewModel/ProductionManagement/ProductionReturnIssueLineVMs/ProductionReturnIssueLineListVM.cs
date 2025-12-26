using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs
{
    public partial class ProductionReturnIssueLineListVM : BasePagedListVM<ProductionReturnIssueLine_View, ProductionReturnIssueLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<ProductionReturnIssueLine_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionReturnIssueLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<ProductionReturnIssueLine_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionReturnIssueLine>()
                                .Select(x => new ProductionReturnIssueLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class ProductionReturnIssueLine_View: ProductionReturnIssueLine
    {
        
    }

}