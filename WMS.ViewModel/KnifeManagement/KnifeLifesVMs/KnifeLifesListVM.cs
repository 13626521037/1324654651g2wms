using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeLifesVMs
{
    public partial class KnifeLifesListVM : BasePagedListVM<KnifeLifes_View, KnifeLifesSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeLifes_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeLifes_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeLifes_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeLifes>()
                                .Select(x => new KnifeLifes_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeLifes_View: KnifeLifes
    {
        
    }

}