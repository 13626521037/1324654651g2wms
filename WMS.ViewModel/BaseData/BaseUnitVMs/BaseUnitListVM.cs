using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseUnitVMs
{
    public partial class BaseUnitListVM : BasePagedListVM<BaseUnit_View, BaseUnitSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseUnit","SyncData","同步数据","同步数据",GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseUnit","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseUnit","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseUnit","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseUnit", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseUnit", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseUnit","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseUnit","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseUnit","BaseUnitExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseUnit_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseUnit_View>>{
                
                this.MakeGridHeader(x => x.BaseUnit_Code).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseUnit_Name).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseUnit_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseUnit_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseUnit_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseUnit_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseUnit_IsValid).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseUnit_View> GetSearchQuery()
        {
            var query = DC.Set<BaseUnit>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new BaseUnit_View
                {
				    ID = x.ID,
                    
                    BaseUnit_Code = x.Code,
                    BaseUnit_Name = x.Name,
                    BaseUnit_IsEffective = x.IsEffective,
                    BaseUnit_Memo = x.Memo,
                    BaseUnit_SourceSystemId = x.SourceSystemId,
                    BaseUnit_LastUpdateTime = x.LastUpdateTime,
                    BaseUnit_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseUnit_View: BaseUnit
    {
        
        public string BaseUnit_Code { get; set; }
        public string BaseUnit_Name { get; set; }
        public EffectiveEnum? BaseUnit_IsEffective { get; set; }
        public string BaseUnit_Memo { get; set; }
        public string BaseUnit_SourceSystemId { get; set; }
        public DateTime? BaseUnit_LastUpdateTime { get; set; }
        public bool? BaseUnit_IsValid { get; set; }

    }

}