using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs
{
    public partial class PurchaseReceivementLineListVM : BasePagedListVM<PurchaseReceivementLine_View, PurchaseReceivementLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseReceivementLine_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReceivementLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<PurchaseReceivementLine_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReceivementLine>()
                                .Select(x => new PurchaseReceivementLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseReceivementLine_View: PurchaseReceivementLine
    {
        
    }

}