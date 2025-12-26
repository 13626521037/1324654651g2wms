
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;



namespace WMS.ViewModel.BaseData.BaseSequenceRecordsDetailVMs
{
    public partial class BaseSequenceRecordsDetailBaseSequenceRecordsDetailListVM : BasePagedListVM<BaseSequenceRecordsDetail, BaseSequenceRecordsDetailDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
            };
        }
 
        protected override IEnumerable<IGridColumn<BaseSequenceRecordsDetail>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceRecordsDetail>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.TextBox).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.DocNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.CreateTime).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.CreateBy).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.CreateBy"].Value),

            };
        }

        
        public override IOrderedQueryable<BaseSequenceRecordsDetail> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.BaseSequenceRecordsId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<BaseSequenceRecordsDetail>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<BaseSequenceRecordsDetail>()
                .Where(x => id == x.BaseSequenceRecordsId)
                .Take(100)  // 因为详情页的控件可能不支持分页，所以这里限制一下最大显示数量
                .OrderByDescending(x => x.ID);
            return query;
        }

    }

    public partial class BaseSequenceRecordsDetailDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._BaseSequenceRecordsDetail._BaseSequenceRecords")]
        public string BaseSequenceRecordsId { get; set; }
    }

}

