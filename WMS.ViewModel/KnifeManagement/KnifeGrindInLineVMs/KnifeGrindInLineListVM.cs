using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineListVM : BasePagedListVM<KnifeGrindInLine_View, KnifeGrindInLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindInLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindInLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindInLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindInLine>()
                                .Select(x => new KnifeGrindInLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindInLine_View: KnifeGrindInLine
    {
        
    }

}