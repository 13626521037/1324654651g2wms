
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;



namespace WMS.ViewModel.BaseData.BaseSequenceDefineLineVMs
{
    public partial class BaseSequenceDefineLineSequenceDefineDetailListVM : BasePagedListVM<BaseSequenceDefineLine, BaseSequenceDefineLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("BaseSequenceDefineLine", GridActionStandardTypesEnum.AddRow, "新建","", dialogWidth: 800),
                this.MakeStandardAction("BaseSequenceDefineLine", GridActionStandardTypesEnum.RemoveRow, "删除","", dialogWidth: 800),
            };
        }
 
        protected override IEnumerable<IGridColumn<BaseSequenceDefineLine>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceDefineLine>>{
                
                this.MakeGridHeader(x => x.SegmentOrder).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.段顺序"].Value),
                this.MakeGridHeader(x => x.SegmentType).SetEditType(EditTypeEnum.ComboBox,typeof(SegmentTypeEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.段类型"].Value),
                this.MakeGridHeader(x => x.SegmentValue).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.段值"].Value),
                this.MakeGridHeader(x => x.SerialLength).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.流水号位数"].Value),
                this.MakeGridHeader(x => x.PadChar).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.补位字符"].Value),
                this.MakeGridHeader(x => x.DateFormat).SetEditType(EditTypeEnum.ComboBox,typeof(DateFormatEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.日期格式"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["_Admin.Remark"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        
        public override IOrderedQueryable<BaseSequenceDefineLine> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.SequenceDefineId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<BaseSequenceDefineLine>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<BaseSequenceDefineLine>()
                .Where(x => id == x.SequenceDefineId)

                .OrderBy(x => x.SegmentOrder);
            return query;
        }

    }

    public partial class BaseSequenceDefineLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._BaseSequenceDefineLine._SequenceDefine")]
        public string SequenceDefineId { get; set; }
    }

}

