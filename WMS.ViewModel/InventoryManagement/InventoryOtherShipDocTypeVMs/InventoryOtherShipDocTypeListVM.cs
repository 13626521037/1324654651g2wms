using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs
{
    public partial class InventoryOtherShipDocTypeListVM : BasePagedListVM<InventoryOtherShipDocType_View, InventoryOtherShipDocTypeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryOtherShipDocType","SyncData",@Localizer["同步数据"].Value,@Localizer["同步数据"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("InventoryOtherShipDocType","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryOtherShipDocType","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("InventoryOtherShipDocType","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("InventoryOtherShipDocType", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryOtherShipDocType", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryOtherShipDocType","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryOtherShipDocType","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryOtherShipDocType","InventoryOtherShipDocTypeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryOtherShipDocType_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherShipDocType_View>>{
                this.MakeGridHeader(x => x.ID, width: 250),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_CreatePerson, width: 95).SetTitle(@Localizer["Page.ERP系统修改人"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_Organization, width: 220).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_Code, width: 85).SetTitle(@Localizer["Page.编码"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_Name, width: 100).SetTitle(@Localizer["Page.名称"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_SourceSystemId, width: 125).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_LastUpdateTime, width: 135).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.InventoryOtherShipDocType_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryOtherShipDocType_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryOtherShipDocType_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.InventoryOtherShipDocType_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_IsEffective).SetTitle(@Localizer["Page.是否生效"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShipDocType_IsValid).SetTitle("ERP已删除"),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryOtherShipDocType_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherShipDocType>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
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
                .Select(x => new InventoryOtherShipDocType_View
                {
				    ID = x.ID,
                    
                    InventoryOtherShipDocType_CreatePerson = x.CreatePerson,
                    InventoryOtherShipDocType_Organization = x.Organization.SourceSystemId,
                    InventoryOtherShipDocType_IsEffective = x.IsEffective,
                    InventoryOtherShipDocType_Memo = x.Memo,
                    InventoryOtherShipDocType_SourceSystemId = x.SourceSystemId,
                    InventoryOtherShipDocType_LastUpdateTime = x.LastUpdateTime,
                    InventoryOtherShipDocType_Code = x.Code,
                    InventoryOtherShipDocType_Name = x.Name,
                    InventoryOtherShipDocType_CreateTime = x.CreateTime,
                    InventoryOtherShipDocType_UpdateTime = x.UpdateTime,
                    InventoryOtherShipDocType_CreateBy = x.CreateBy,
                    InventoryOtherShipDocType_UpdateBy = x.UpdateBy,
                    InventoryOtherShipDocType_IsValid = x.IsValid,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryOtherShipDocType_View: InventoryOtherShipDocType
    {
        
        public string InventoryOtherShipDocType_CreatePerson { get; set; }
        public string InventoryOtherShipDocType_Organization { get; set; }
        public EffectiveEnum? InventoryOtherShipDocType_IsEffective { get; set; }
        public string InventoryOtherShipDocType_Memo { get; set; }
        public string InventoryOtherShipDocType_SourceSystemId { get; set; }
        public DateTime? InventoryOtherShipDocType_LastUpdateTime { get; set; }
        public string InventoryOtherShipDocType_Code { get; set; }
        public string InventoryOtherShipDocType_Name { get; set; }
        public DateTime? InventoryOtherShipDocType_CreateTime { get; set; }
        public DateTime? InventoryOtherShipDocType_UpdateTime { get; set; }
        public string InventoryOtherShipDocType_CreateBy { get; set; }
        public string InventoryOtherShipDocType_UpdateBy { get; set; }
        public bool? InventoryOtherShipDocType_IsValid { get; set; }

    }

}