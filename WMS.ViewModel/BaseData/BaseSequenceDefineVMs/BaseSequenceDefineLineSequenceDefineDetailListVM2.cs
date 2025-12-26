
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
    public partial class BaseSequenceDefineLineSequenceDefineDetailListVM2 : BasePagedListVM<BaseSequenceDefineLine, BaseSequenceDefineLineDetailSearcher2>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<BaseSequenceDefineLine>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceDefineLine>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.SegmentOrder).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.段顺序"].Value),
                this.MakeGridHeader(x => x.SegmentType).SetEditType(EditTypeEnum.Text,typeof(SegmentTypeEnum).ToListItems(null,true)).SetTitle(@Localizer["Page.段类型"].Value),
                this.MakeGridHeader(x => x.SegmentValue).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.段值"].Value),
                this.MakeGridHeader(x => x.SerialLength).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.流水号位数"].Value),
                this.MakeGridHeader(x => x.PadChar).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.补位字符"].Value),
                this.MakeGridHeader(x => x.DateFormat).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.日期格式"].Value),
                this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

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

    public partial class BaseSequenceDefineLineDetailSearcher2 : BaseSearcher
    {
        
        [Display(Name = "_Model._BaseSequenceDefineLine._SequenceDefine")]
        public string SequenceDefineId { get; set; }
    }

}

