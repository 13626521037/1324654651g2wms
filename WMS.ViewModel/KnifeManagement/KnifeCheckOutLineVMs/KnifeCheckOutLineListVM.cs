using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs
{
    public partial class KnifeCheckOutLineListVM : BasePagedListVM<KnifeCheckOutLine_View, KnifeCheckOutLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeCheckOutLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckOutLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeCheckOutLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeCheckOutLine>()
                                .Select(x => new KnifeCheckOutLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeCheckOutLine_View: KnifeCheckOutLine
    {
        
    }

}