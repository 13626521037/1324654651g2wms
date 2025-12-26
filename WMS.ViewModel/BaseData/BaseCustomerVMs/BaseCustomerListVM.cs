using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseCustomerVMs
{
    public partial class BaseCustomerListVM : BasePagedListVM<BaseCustomer_View, BaseCustomerSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseCustomer","SyncData",@Localizer["同步数据"].Value,@Localizer["同步数据"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("BaseCustomer","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseCustomer","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("BaseCustomer","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseCustomer", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseCustomer", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseCustomer","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseCustomer","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseCustomer","BaseCustomerExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseCustomer_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseCustomer_View>>{
                
                this.MakeGridHeader(x => x.BaseCustomer_ShortName).SetTitle(@Localizer["Page.客户简称"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_EnglishShortName).SetTitle(@Localizer["Page.英文缩写"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_Organization).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_Code).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_Name).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.BaseCustomer_IsValid).SetTitle(@Localizer["_Admin.IsValid"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseCustomer_View> GetSearchQuery()
        {
            var query = DC.Set<BaseCustomer>()
                
                .CheckContain(Searcher.EnglishShortName, x=>x.EnglishShortName)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckEqual(Searcher.IsEffective, x=>x.IsEffective)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseCustomer_View
                {
				    ID = x.ID,
                    
                    BaseCustomer_ShortName = x.ShortName,
                    BaseCustomer_EnglishShortName = x.EnglishShortName,
                    BaseCustomer_Organization = x.Organization.SourceSystemId,
                    BaseCustomer_IsEffective = x.IsEffective,
                    BaseCustomer_Memo = x.Memo,
                    BaseCustomer_SourceSystemId = x.SourceSystemId,
                    BaseCustomer_LastUpdateTime = x.LastUpdateTime,
                    BaseCustomer_Code = x.Code,
                    BaseCustomer_Name = x.Name,
                    BaseCustomer_CreateTime = x.CreateTime,
                    BaseCustomer_UpdateTime = x.UpdateTime,
                    BaseCustomer_CreateBy = x.CreateBy,
                    BaseCustomer_UpdateBy = x.UpdateBy,
                    BaseCustomer_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseCustomer_View: BaseCustomer
    {
        
        public string BaseCustomer_ShortName { get; set; }
        public string BaseCustomer_EnglishShortName { get; set; }
        public string BaseCustomer_Organization { get; set; }
        public EffectiveEnum? BaseCustomer_IsEffective { get; set; }
        public string BaseCustomer_Memo { get; set; }
        public string BaseCustomer_SourceSystemId { get; set; }
        public DateTime? BaseCustomer_LastUpdateTime { get; set; }
        public string BaseCustomer_Code { get; set; }
        public string BaseCustomer_Name { get; set; }
        public DateTime? BaseCustomer_CreateTime { get; set; }
        public DateTime? BaseCustomer_UpdateTime { get; set; }
        public string BaseCustomer_CreateBy { get; set; }
        public string BaseCustomer_UpdateBy { get; set; }
        public bool? BaseCustomer_IsValid { get; set; }

    }

}