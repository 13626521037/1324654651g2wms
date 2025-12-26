
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


namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutLineVMs
{
    public partial class KnifeCheckOutLineKnifeCheckOutDetailListVM1 : BasePagedListVM<KnifeCheckOutLine, KnifeCheckOutLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeCheckOutLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeCheckOutLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeCheckOutLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckOutLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeCheckOutLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeCheckOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeCheckOutLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeCheckOutLine>()
                .Where(x => id == x.KnifeCheckOutId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeCheckOutLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeCheckOutLine._KnifeCheckOut")]
        public string KnifeCheckOutId { get; set; }
    }

}

