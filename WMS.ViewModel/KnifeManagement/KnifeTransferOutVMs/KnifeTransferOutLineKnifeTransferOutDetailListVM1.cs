
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


namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs
{
    public partial class KnifeTransferOutLineKnifeTransferOutDetailListVM1 : BasePagedListVM<KnifeTransferOutLine, KnifeTransferOutLineDetailSearcher1>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("KnifeTransferOutLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("KnifeTransferOutLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<KnifeTransferOutLine>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferOutLine>>{
                
                this.MakeGridHeader(x => x.KnifeId).SetEditType(EditTypeEnum.ComboBox,DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber,SortByName:true)).SetTitle(@Localizer["Page.刀具"].Value),
                this.MakeGridHeader(x => x.Status).SetEditType(EditTypeEnum.ComboBox,typeof(KnifeOrderStatusEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.行状态"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<KnifeTransferOutLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.KnifeTransferOutId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<KnifeTransferOutLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<KnifeTransferOutLine>()
                .Where(x => id == x.KnifeTransferOutId)

                .OrderBy(x => x.ID);
            return query;
        }

    }

    public partial class KnifeTransferOutLineDetailSearcher1 : BaseSearcher
    {
        
        [Display(Name = "_Model._KnifeTransferOutLine._KnifeTransferOut")]
        public string KnifeTransferOutId { get; set; }
    }

}

