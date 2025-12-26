using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesRMALineVMs
{
    public partial class SalesRMALineListVM : BasePagedListVM<SalesRMALine_View, SalesRMALineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<SalesRMALine_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesRMALine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<SalesRMALine_View> GetSearchQuery()
        {
            var query = DC.Set<SalesRMALine>()
                                .Select(x => new SalesRMALine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class SalesRMALine_View: SalesRMALine
    {
        
    }

}