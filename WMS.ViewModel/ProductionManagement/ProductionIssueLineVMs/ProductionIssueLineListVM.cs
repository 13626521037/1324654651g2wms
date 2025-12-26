using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionIssueLineVMs
{
    public partial class ProductionIssueLineListVM : BasePagedListVM<ProductionIssueLine_View, ProductionIssueLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<ProductionIssueLine_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionIssueLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<ProductionIssueLine_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionIssueLine>()
                                .Select(x => new ProductionIssueLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class ProductionIssueLine_View: ProductionIssueLine
    {
        
    }

}