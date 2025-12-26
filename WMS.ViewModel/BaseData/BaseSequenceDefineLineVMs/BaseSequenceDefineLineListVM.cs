using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceDefineLineVMs
{
    public partial class BaseSequenceDefineLineListVM : BasePagedListVM<BaseSequenceDefineLine_View, BaseSequenceDefineLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSequenceDefineLine_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceDefineLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<BaseSequenceDefineLine_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSequenceDefineLine>()
                                .Select(x => new BaseSequenceDefineLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSequenceDefineLine_View: BaseSequenceDefineLine
    {
        
    }

}