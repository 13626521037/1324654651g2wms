
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

using WMS.Model.KnifeManagement;


namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestLineVMs
{
    public partial class KnifeGrindRequestLineKnifeGrindRequestDetailListVM : BasePagedListVM<KnifeGrindRequestLine, KnifeGrindRequestLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeGrindRequestLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeGrindRequestLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindRequestLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindRequestLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindRequestLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindRequestId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindRequestLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindRequestLine>()
                .Where(x => id == x.KnifeGrindRequestId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeGrindRequestLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindRequestLine._KnifeGrindRequest")]
        public string KnifeGrindRequestId { get; set; }
    }

}

