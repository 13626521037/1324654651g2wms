
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


namespace WMS.ViewModel.KnifeManagement.KnifeScrapLineVMs
{
    public partial class KnifeScrapLineKnifeScrapDetailListVM : BasePagedListVM<KnifeScrapLine, KnifeScrapLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeScrapLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeScrapLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeScrapLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeScrapLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeScrapLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeScrapId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeScrapLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeScrapLine>()
                .Where(x => id == x.KnifeScrapId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeScrapLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeScrapLine._KnifeScrap")]
        public string KnifeScrapId { get; set; }
    }

}

