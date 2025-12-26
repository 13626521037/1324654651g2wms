using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseItemMasterVMs
{
    public partial class BaseItemMasterListVM : BasePagedListVM<BaseItemMaster_View, BaseItemMasterSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseItemMaster","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseItemMaster","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseItemMaster","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseItemMaster","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseItemMaster", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseItemMaster", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseItemMaster","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseItemMaster","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseItemMaster","BaseItemMasterExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseItemMaster_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseItemMaster_View>>{
                this.MakeGridHeader(x => x.BaseItemMaster_Code, width: 110).SetTitle(@Localizer["Page.物料编码"].Value).SetFixed(GridColumnFixedEnum.Left),
                this.MakeGridHeader(x => x.BaseItemMaster_Name, width: 150).SetTitle(@Localizer["Page.物料名称"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_ItemCategory, width: 120).SetTitle(@Localizer["Page.分类"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_SPECS, width: 150).SetTitle(@Localizer["Page.规格"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_MateriaModel, width: 150).SetTitle(@Localizer["Page.型号"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_Description, width: 200).SetTitle(@Localizer["Page.描述"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_StockUnit, width: 87).SetTitle(@Localizer["Page.库存单位"].Value).SetHide(true),
                this.MakeGridHeader(x => x.BaseItemMaster_FormAttribute, width : 120).SetTitle(@Localizer["Page.形态属性"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_Organization, width: 192).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_ProductionOrg, width: 192).SetTitle(@Localizer["Page.默认生产组织"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_ProductionDept, width : 120).SetTitle(@Localizer["Page.默认生产部门"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_Wh, width : 120).SetTitle(@Localizer["Page.默认存储地点"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_MateriaAttribute, width: 200).SetTitle(@Localizer["Page.属性"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_GearRatio, width: 80).SetTitle(@Localizer["Page.速比"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_Power, width: 80).SetTitle(@Localizer["Page.功率"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_AnalysisType, width: 150).SetTitle(@Localizer["Page.分析类别"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_SafetyStockQty, width: 100).SetTitle(@Localizer["Page.安全库存"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_FixedLT, width: 100).SetTitle(@Localizer["Page.生产周期"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_BuildBatch, width: 100).SetTitle(@Localizer["Page.生产批量"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_NotAnalysisQty, width: 100).SetTitle(@Localizer["Page.自动判定散单数量"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_IsEffective, width: 80).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_SourceSystemId, width: 100).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(true),
                this.MakeGridHeader(x => x.BaseItemMaster_LastUpdateTime, width : 160).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseItemMaster_IsValid, width : 100).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 120)
            };
        }

        

        public override IOrderedQueryable<BaseItemMaster_View> GetSearchQuery()
        {
            var query = DC.Set<BaseItemMaster>()
                
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseItemMaster_View
                {
				    ID = x.ID,
                    BaseItemMaster_Organization = x.Organization.Name,
                    BaseItemMaster_ItemCategory = x.ItemCategory.Name,
                    BaseItemMaster_Code = x.Code,
                    BaseItemMaster_Name = x.Name,
                    BaseItemMaster_SPECS = x.SPECS,
                    BaseItemMaster_MateriaModel = x.MateriaModel,
                    BaseItemMaster_Description = x.Description.SetFontSmallSize(),
                    BaseItemMaster_StockUnit = x.StockUnit.Name,
                    BaseItemMaster_ProductionOrg = x.ProductionOrg.Name,
                    BaseItemMaster_ProductionDept = x.ProductionDept.Name,
                    BaseItemMaster_Wh = x.Wh.Name,
                    BaseItemMaster_FormAttribute = x.FormAttribute,
                    BaseItemMaster_MateriaAttribute = x.MateriaAttribute.SetFontSmallSize(),
                    BaseItemMaster_GearRatio = x.GearRatio,
                    BaseItemMaster_Power = x.Power,
                    BaseItemMaster_SafetyStockQty = x.SafetyStockQty,
                    BaseItemMaster_FixedLT = x.FixedLT,
                    BaseItemMaster_BuildBatch = x.BuildBatch,
                    BaseItemMaster_NotAnalysisQty = x.NotAnalysisQty,
                    BaseItemMaster_AnalysisType = x.AnalysisType.Name,
                    BaseItemMaster_IsEffective = x.IsEffective,
                    BaseItemMaster_SourceSystemId = x.SourceSystemId,
                    BaseItemMaster_LastUpdateTime = x.LastUpdateTime,
                    BaseItemMaster_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseItemMaster_View: BaseItemMaster
    {
        
        public string BaseItemMaster_Organization { get; set; }
        public string BaseItemMaster_ItemCategory { get; set; }
        public string BaseItemMaster_Code { get; set; }
        public string BaseItemMaster_Name { get; set; }
        public string BaseItemMaster_SPECS { get; set; }
        public string BaseItemMaster_MateriaModel { get; set; }
        public string BaseItemMaster_Description { get; set; }
        public string BaseItemMaster_StockUnit { get; set; }
        public string BaseItemMaster_ProductionOrg { get; set; }
        public string BaseItemMaster_ProductionDept { get; set; }
        public string BaseItemMaster_Wh { get; set; }
        public ItemFormAttributeEnum? BaseItemMaster_FormAttribute { get; set; }
        public string BaseItemMaster_MateriaAttribute { get; set; }
        public decimal? BaseItemMaster_GearRatio { get; set; }
        public decimal? BaseItemMaster_Power { get; set; }
        public decimal? BaseItemMaster_SafetyStockQty { get; set; }
        public decimal? BaseItemMaster_FixedLT { get; set; }
        public int? BaseItemMaster_BuildBatch { get; set; }
        public int? BaseItemMaster_NotAnalysisQty { get; set; }
        public string BaseItemMaster_AnalysisType { get; set; }
        public EffectiveEnum? BaseItemMaster_IsEffective { get; set; }
        public string BaseItemMaster_SourceSystemId { get; set; }
        public DateTime? BaseItemMaster_LastUpdateTime { get; set; }
        public bool? BaseItemMaster_IsValid { get; set; }

    }

}