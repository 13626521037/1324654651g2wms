
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


namespace WMS.ViewModel.KnifeManagement.KnifeGrindInLineVMs
{
    public partial class KnifeGrindInLineKnifeGrindInDetailListVM : BasePagedListVM<KnifeGrindInLine, KnifeGrindInLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeGrindInLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeGrindInLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindInLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindInLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindInLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindInLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindInLine>()
                .Where(x => id == x.KnifeGrindInId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeGrindInLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindInLine._KnifeGrindIn")]
        public string KnifeGrindInId { get; set; }
    }

}

