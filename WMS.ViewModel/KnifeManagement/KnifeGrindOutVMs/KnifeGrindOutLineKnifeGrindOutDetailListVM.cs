
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


namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutLineVMs
{
    public partial class KnifeGrindOutLineKnifeGrindOutDetailListVM : BasePagedListVM<KnifeGrindOutLine, KnifeGrindOutLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeGrindOutLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeGrindOutLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeGrindOutLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindOutLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeGrindOutLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeGrindOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeGrindOutLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeGrindOutLine>()
                .Where(x => id == x.KnifeGrindOutId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeGrindOutLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeGrindOutLine._KnifeGrindOut")]
        public string KnifeGrindOutId { get; set; }
    }

}

