using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSupplierVMs
{
    public partial class BaseSupplierListVM : BasePagedListVM<BaseSupplier_View, BaseSupplierSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseSupplier","SyncData",@Localizer["同步数据"].Value,@Localizer["同步数据"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseSupplier","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseSupplier","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("BaseSupplier","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseSupplier", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseSupplier", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseSupplier","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseSupplier","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseSupplier","BaseSupplierExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseSupplier_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseSupplier_View>>{
                
                this.MakeGridHeader(x => x.BaseSupplier_Code, width: 85).SetTitle(@Localizer["Page.供应商编码"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_Name, width: 200).SetTitle(@Localizer["Page.供应商名称"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_ShortName, width: 150).SetTitle(@Localizer["Page.供应商简称"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_Organization, width: 220).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_IsEffective, width: 75).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_SourceSystemId, width: 125).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_LastUpdateTime, width: 135).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.BaseSupplier_CreateTime, width: 135).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.BaseSupplier_UpdateTime, width: 135).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.BaseSupplier_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.BaseSupplier_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.BaseSupplier_IsValid, width: 75).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<BaseSupplier_View> GetSearchQuery()
        {
            var query = DC.Set<BaseSupplier>()
                
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.ShortName, x=>x.ShortName)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseSupplier_View
                {
				    ID = x.ID,
                    
                    BaseSupplier_Code = x.Code,
                    BaseSupplier_Name = x.Name,
                    BaseSupplier_ShortName = x.ShortName,
                    BaseSupplier_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    BaseSupplier_IsEffective = x.IsEffective,
                    BaseSupplier_Memo = x.Memo,
                    BaseSupplier_SourceSystemId = x.SourceSystemId,
                    BaseSupplier_LastUpdateTime = x.LastUpdateTime,
                    BaseSupplier_CreateTime = x.CreateTime,
                    BaseSupplier_UpdateTime = x.UpdateTime,
                    BaseSupplier_CreateBy = x.CreateBy,
                    BaseSupplier_UpdateBy = x.UpdateBy,
                    BaseSupplier_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseSupplier_View: BaseSupplier
    {
        
        public string BaseSupplier_Code { get; set; }
        public string BaseSupplier_Name { get; set; }
        public string BaseSupplier_ShortName { get; set; }
        public string BaseSupplier_Organization { get; set; }
        public EffectiveEnum? BaseSupplier_IsEffective { get; set; }
        public string BaseSupplier_Memo { get; set; }
        public string BaseSupplier_SourceSystemId { get; set; }
        public DateTime? BaseSupplier_LastUpdateTime { get; set; }
        public DateTime? BaseSupplier_CreateTime { get; set; }
        public DateTime? BaseSupplier_UpdateTime { get; set; }
        public string BaseSupplier_CreateBy { get; set; }
        public string BaseSupplier_UpdateBy { get; set; }
        public bool? BaseSupplier_IsValid { get; set; }

    }

}