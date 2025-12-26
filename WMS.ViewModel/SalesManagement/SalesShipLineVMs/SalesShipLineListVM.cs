using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesShipLineVMs
{
    public partial class SalesShipLineListVM : BasePagedListVM<SalesShipLine_View, SalesShipLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<SalesShipLine_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesShipLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<SalesShipLine_View> GetSearchQuery()
        {
            var query = DC.Set<SalesShipLine>()
                                .Select(x => new SalesShipLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class SalesShipLine_View: SalesShipLine
    {
        
    }

}