using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseItemCategoryVMs
{
    public partial class BaseItemCategoryListVM : BasePagedListVM<BaseItemCategory_View, BaseItemCategorySearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseItemCategory","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseItemCategory","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseItemCategory","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseItemCategory","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseItemCategory", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseItemCategory", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseItemCategory","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseItemCategory","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseItemCategory","BaseItemCategoryExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseItemCategory_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseItemCategory_View>>{
                
                this.MakeGridHeader(x => x.BaseItemCategory_Organization).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseItemCategory_Code).SetTitle(@Localizer["Page.编码"].Value).SetWidth(160),
                this.MakeGridHeader(x => x.BaseItemCategory_Name).SetTitle(@Localizer["Page.分类名称"].Value),
                this.MakeGridHeader(x => x.BaseItemCategory_AnalysisType).SetTitle(@Localizer["Page.分析类别"].Value),
                this.MakeGridHeader(x => x.BaseItemCategory_Department).SetTitle(@Localizer["Page.生产部门"].Value),
                this.MakeGridHeader(x => x.BaseItemCategory_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value).SetWidth(152),
                this.MakeGridHeader(x => x.BaseItemCategory_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value).SetWidth(160),
                this.MakeGridHeader(x => x.BaseItemCategory_IsValid).SetTitle("ERP是否存在").SetWidth(90),
                this.MakeGridHeaderAction(width: 120)
            };
        }

        

        public override IOrderedQueryable<BaseItemCategory_View> GetSearchQuery()
        {
            var query = DC.Set<BaseItemCategory>()
                
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.AnalysisTypeId, x=>x.AnalysisTypeId)
                .CheckEqual(Searcher.DepartmentId, x=>x.DepartmentId)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseItemCategory_View
                {
				    ID = x.ID,
                    
                    BaseItemCategory_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name).SetFontSmallSize(),
                    BaseItemCategory_Code = x.Code,
                    BaseItemCategory_Name = x.Name,
                    BaseItemCategory_AnalysisType = x.AnalysisType.Code.CodeCombinName(x.AnalysisType.Name).SetFontSmallSize(),
                    BaseItemCategory_Department = x.Department.Code.CodeCombinName(x.Department.Name).SetFontSmallSize(),
                    BaseItemCategory_SourceSystemId = x.SourceSystemId,
                    BaseItemCategory_LastUpdateTime = x.LastUpdateTime,
                    BaseItemCategory_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseItemCategory_View: BaseItemCategory
    {
        
        public string BaseItemCategory_Organization { get; set; }
        public string BaseItemCategory_Code { get; set; }
        public string BaseItemCategory_Name { get; set; }
        public string BaseItemCategory_AnalysisType { get; set; }
        public string BaseItemCategory_Department { get; set; }
        public string BaseItemCategory_SourceSystemId { get; set; }
        public DateTime? BaseItemCategory_LastUpdateTime { get; set; }
        public bool? BaseItemCategory_IsValid { get; set; }

    }

}