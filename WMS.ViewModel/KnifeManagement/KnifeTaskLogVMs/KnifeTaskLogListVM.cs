using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTaskLogVMs
{
    public partial class KnifeTaskLogListVM : BasePagedListVM<KnifeTaskLog_View, KnifeTaskLogSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeTaskLog_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTaskLog_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeTaskLog_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeTaskLog>()
                                .Select(x => new KnifeTaskLog_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeTaskLog_View: KnifeTaskLog
    {
        
    }

}