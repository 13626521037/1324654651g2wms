
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
using WMS.Model.BaseData;


namespace WMS.ViewModel.KnifeManagement.KnifeTransferInLineVMs
{
    public partial class KnifeTransferInLineKnifeTransferInDetailListVM1 : BasePagedListVM<KnifeTransferInLine, KnifeTransferInLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeTransferInLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeTransferInLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeTransferInLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferInLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),
                this.MakeGridHeader(x => x.ToWhLocationId).SetEditType(EditTypeEnum.ComboBox,DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Code,SortByName:true)).SetTitle(@Localizer["Page.库位"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeTransferInLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeTransferInId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeTransferInLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeTransferInLine>()
                .Where(x => id == x.KnifeTransferInId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeTransferInLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeTransferInLine._KnifeTransferIn")]
        public string KnifeTransferInId { get; set; }
    }

}

