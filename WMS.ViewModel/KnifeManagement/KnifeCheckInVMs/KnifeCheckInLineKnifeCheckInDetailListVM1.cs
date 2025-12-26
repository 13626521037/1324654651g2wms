
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


namespace WMS.ViewModel.KnifeManagement.KnifeCheckInLineVMs
{
    public partial class KnifeCheckInLineKnifeCheckInDetailListVM1 : BasePagedListVM<KnifeCheckInLine, KnifeCheckInLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeCheckInLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeCheckInLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeCheckInLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckInLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeCheckInLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeCheckInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeCheckInLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeCheckInLine>()
                .Where(x => id == x.KnifeCheckInId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeCheckInLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeCheckInLine._KnifeCheckIn")]
        public string KnifeCheckInId { get; set; }
    }

}

