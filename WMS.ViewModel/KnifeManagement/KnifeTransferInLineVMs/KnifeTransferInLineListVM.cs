using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs
{
    public partial class KnifeTransferInLineListVM : BasePagedListVM<KnifeTransferInLine_View, KnifeTransferInLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeTransferInLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferInLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeTransferInLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeTransferInLine>()
                                .Select(x => new KnifeTransferInLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeTransferInLine_View: KnifeTransferInLine
    {
        
    }

}