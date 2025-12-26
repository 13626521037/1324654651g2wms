using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailListVM : BasePagedListVM<BaseSequenceRecordsDetail_View, BaseSequenceRecordsDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSequenceRecordsDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceRecordsDetail_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<BaseSequenceRecordsDetail_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSequenceRecordsDetail>()
                                .Select(x => new BaseSequenceRecordsDetail_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSequenceRecordsDetail_View: BaseSequenceRecordsDetail
    {
        
    }

}