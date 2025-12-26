using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineListVM : BasePagedListVM<KnifeTransferOutLine_View, KnifeTransferOutLineSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeTransferOutLine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferOutLine_View>>{
                
                this.MakeGridHeaderAction(width: 200).SetHide(true)
            };
        }

        
        public override IOrderedQueryable<KnifeTransferOutLine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeTransferOutLine>()
                                .Select(x => new KnifeTransferOutLine_View
                {
				    ID = x.ID,
                                    })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class KnifeTransferOutLine_View: KnifeTransferOutLine
    {
        
    }

}