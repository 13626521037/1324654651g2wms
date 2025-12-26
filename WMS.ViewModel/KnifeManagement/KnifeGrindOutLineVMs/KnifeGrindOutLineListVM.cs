using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineListVM : BasePagedListVM<KnifeGrindOutLine_View, KnifeGrindOutLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindOutLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindOutLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindOutLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindOutLine>()
                                .Select(x => new KnifeGrindOutLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindOutLine_View: KnifeGrindOutLine
    {
        
    }

}