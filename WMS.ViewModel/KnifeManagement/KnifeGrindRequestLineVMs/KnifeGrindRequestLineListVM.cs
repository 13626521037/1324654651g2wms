using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs
{
    public partial class KnifeGrindRequestLineListVM : BasePagedListVM<KnifeGrindRequestLine_View, KnifeGrindRequestLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindRequestLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindRequestLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindRequestLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindRequestLine>()
                                .Select(x => new KnifeGrindRequestLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindRequestLine_View: KnifeGrindRequestLine
    {
        
    }

}