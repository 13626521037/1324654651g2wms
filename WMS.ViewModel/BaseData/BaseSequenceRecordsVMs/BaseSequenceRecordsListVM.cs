using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsVMs
{
    public partial class BaseSequenceRecordsListVM : BasePagedListVM<BaseSequenceRecords_View, BaseSequenceRecordsSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseSequenceRecords","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseSequenceRecords","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseSequenceRecords","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseSequenceRecords", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseSequenceRecords", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseSequenceRecords","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseSequenceRecords","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseSequenceRecords","BaseSequenceRecordsExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSequenceRecords_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSequenceRecords_View>>{
                
                this.MakeGridHeader(x => x.BaseSequenceRecords_SequenceDefine).SetTitle(@Localizer["Page.批次规则定义主表"].Value),
                this.MakeGridHeader(x => x.BaseSequenceRecords_SegmentFlag).SetTitle(@Localizer["Page.段标识"].Value),
                this.MakeGridHeader(x => x.BaseSequenceRecords_SerialValue).SetTitle(@Localizer["Page.流水值"].Value),
                this.MakeGridHeader(x => x.BaseSequenceRecords_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseSequenceRecords_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.BaseSequenceRecords_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.BaseSequenceRecords_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseSequenceRecords_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSequenceRecords>()
                
                .CheckEqual(Searcher.SequenceDefineId, x=>x.SequenceDefineId)
                .CheckContain(Searcher.SegmentFlag, x=>x.SegmentFlag)
                .CheckEqual(Searcher.SerialValue, x=>x.SerialValue)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseSequenceRecords_View
                {
				    ID = x.ID,
                    
                    BaseSequenceRecords_SequenceDefine = x.SequenceDefine.Code,
                    BaseSequenceRecords_SegmentFlag = x.SegmentFlag,
                    BaseSequenceRecords_SerialValue = x.SerialValue,
                    BaseSequenceRecords_CreateTime = x.CreateTime,
                    BaseSequenceRecords_UpdateTime = x.UpdateTime,
                    BaseSequenceRecords_CreateBy = x.CreateBy,
                    BaseSequenceRecords_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSequenceRecords_View: BaseSequenceRecords
    {
        
        public string BaseSequenceRecords_SequenceDefine { get; set; }
        public string BaseSequenceRecords_SegmentFlag { get; set; }
        public int? BaseSequenceRecords_SerialValue { get; set; }
        public DateTime? BaseSequenceRecords_CreateTime { get; set; }
        public DateTime? BaseSequenceRecords_UpdateTime { get; set; }
        public string BaseSequenceRecords_CreateBy { get; set; }
        public string BaseSequenceRecords_UpdateBy { get; set; }

    }

}