using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs
{
    public partial class SalesReturnReceivementLineListVM : BasePagedListVM<SalesReturnReceivementLine_View, SalesReturnReceivementLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<SalesReturnReceivementLine_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesReturnReceivementLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<SalesReturnReceivementLine_View> GetSearchQuery()
        {
            var query = DC.Set<SalesReturnReceivementLine>()
                                .Select(x => new SalesReturnReceivementLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class SalesReturnReceivementLine_View: SalesReturnReceivementLine
    {
        
    }

}