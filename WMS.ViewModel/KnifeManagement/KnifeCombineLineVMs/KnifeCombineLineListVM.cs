using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCombineLineVMs
{
    public partial class KnifeCombineLineListVM : BasePagedListVM<KnifeCombineLine_View, KnifeCombineLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeCombineLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCombineLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeCombineLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeCombineLine>()
                                .Select(x => new KnifeCombineLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeCombineLine_View: KnifeCombineLine
    {
        
    }

}