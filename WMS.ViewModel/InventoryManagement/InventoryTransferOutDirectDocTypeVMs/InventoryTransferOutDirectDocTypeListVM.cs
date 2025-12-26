using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs
{
    public partial class InventoryTransferOutDirectDocTypeListVM : BasePagedListVM<InventoryTransferOutDirectDocType_View, InventoryTransferOutDirectDocTypeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryTransferOutDirectDocType","SyncData",@Localizer["同步数据"].Value,@Localizer["同步数据"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",900,500).SetShowInRow(false).SetHideOnToolBar(false),
                //this.MakeAction("InventoryTransferOutDirectDocType","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryTransferOutDirectDocType","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeAction("InventoryTransferOutDirectDocType","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("InventoryTransferOutDirectDocType", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryTransferOutDirectDocType", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryTransferOutDirectDocType","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryTransferOutDirectDocType","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryTransferOutDirectDocType","InventoryTransferOutDirectDocTypeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferOutDirectDocType_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutDirectDocType_View>>{
                this.MakeGridHeader(x => x.ID, width: 250),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_CreatePerson, width: 95).SetTitle(@Localizer["Page.ERP系统修改人"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_Organization, width: 220).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_Code, width: 85).SetTitle("单据编码"),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_Name, width: 100).SetTitle("单据名称"),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_SourceSystemId, width: 125).SetTitle(@Localizer["Page.来源系统主键"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_LastUpdateTime, width: 135).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_IsEffective, width: 75).SetTitle("是否生效"),
                this.MakeGridHeader(x => x.InventoryTransferOutDirectDocType_IsValid, width: 75).SetTitle("ERP已删除"),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryTransferOutDirectDocType_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutDirectDocType>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryTransferOutDirectDocType_View
                {
				    ID = x.ID,
                    
                    InventoryTransferOutDirectDocType_CreatePerson = x.CreatePerson,
                    InventoryTransferOutDirectDocType_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    InventoryTransferOutDirectDocType_Code = x.Code,
                    InventoryTransferOutDirectDocType_Name = x.Name,
                    InventoryTransferOutDirectDocType_Memo = x.Memo,
                    InventoryTransferOutDirectDocType_SourceSystemId = x.SourceSystemId,
                    InventoryTransferOutDirectDocType_LastUpdateTime = x.LastUpdateTime,
                    InventoryTransferOutDirectDocType_CreateTime = x.CreateTime,
                    InventoryTransferOutDirectDocType_UpdateTime = x.UpdateTime,
                    InventoryTransferOutDirectDocType_CreateBy = x.CreateBy,
                    InventoryTransferOutDirectDocType_UpdateBy = x.UpdateBy,
                    InventoryTransferOutDirectDocType_IsValid = x.IsValid,
                    InventoryTransferOutDirectDocType_IsEffective = x.IsEffective,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryTransferOutDirectDocType_View: InventoryTransferOutDirectDocType
    {
        
        public string InventoryTransferOutDirectDocType_CreatePerson { get; set; }
        public string InventoryTransferOutDirectDocType_Organization { get; set; }
        public string InventoryTransferOutDirectDocType_Code { get; set; }
        public string InventoryTransferOutDirectDocType_Name { get; set; }
        public string InventoryTransferOutDirectDocType_Memo { get; set; }
        public string InventoryTransferOutDirectDocType_SourceSystemId { get; set; }
        public DateTime? InventoryTransferOutDirectDocType_LastUpdateTime { get; set; }
        public DateTime? InventoryTransferOutDirectDocType_CreateTime { get; set; }
        public DateTime? InventoryTransferOutDirectDocType_UpdateTime { get; set; }
        public string InventoryTransferOutDirectDocType_CreateBy { get; set; }
        public string InventoryTransferOutDirectDocType_UpdateBy { get; set; }
        public bool? InventoryTransferOutDirectDocType_IsValid { get; set; }
        public EffectiveEnum? InventoryTransferOutDirectDocType_IsEffective { get; set; }

    }

}