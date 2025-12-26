using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptLineVMs
{
    public partial class ProductionRcvRptLineListVM : BasePagedListVM<ProductionRcvRptLine_View, ProductionRcvRptLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<ProductionRcvRptLine_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionRcvRptLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<ProductionRcvRptLine_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionRcvRptLine>()
                                .Select(x => new ProductionRcvRptLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class ProductionRcvRptLine_View: ProductionRcvRptLine
    {
        
    }

}