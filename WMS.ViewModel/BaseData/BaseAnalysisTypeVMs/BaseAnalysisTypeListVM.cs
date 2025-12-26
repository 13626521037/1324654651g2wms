using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseAnalysisTypeVMs
{
    public partial class BaseAnalysisTypeListVM : BasePagedListVM<BaseAnalysisType_View, BaseAnalysisTypeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseAnalysisType","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseAnalysisType","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseAnalysisType","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("BaseAnalysisType","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseAnalysisType", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseAnalysisType", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseAnalysisType","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseAnalysisType","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseAnalysisType","BaseAnalysisTypeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseAnalysisType_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseAnalysisType_View>>{
                
                this.MakeGridHeader(x => x.BaseAnalysisType_Code).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseAnalysisType_Name).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseAnalysisType_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseAnalysisType_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseAnalysisType_IsValid).SetTitle(@Localizer["_Admin.IsValid"].Value),

                //this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseAnalysisType_View> GetSearchQuery()
        {
            var query = DC.Set<BaseAnalysisType>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseAnalysisType_View
                {
				    ID = x.ID,
                    
                    BaseAnalysisType_Code = x.Code,
                    BaseAnalysisType_Name = x.Name,
                    BaseAnalysisType_SourceSystemId = x.SourceSystemId,
                    BaseAnalysisType_LastUpdateTime = x.LastUpdateTime,
                    BaseAnalysisType_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseAnalysisType_View: BaseAnalysisType
    {
        
        public string BaseAnalysisType_Code { get; set; }
        public string BaseAnalysisType_Name { get; set; }
        public string BaseAnalysisType_SourceSystemId { get; set; }
        public DateTime? BaseAnalysisType_LastUpdateTime { get; set; }
        public bool? BaseAnalysisType_IsValid { get; set; }

    }

}