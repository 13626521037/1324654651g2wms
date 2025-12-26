using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using NPOI.SS.Formula.Functions;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualListVM : BasePagedListVM<InventoryTransferOutManual_View, InventoryTransferOutManualSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryTransferOutManual","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryTransferOutManual","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryTransferOutManual","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryTransferOutManual", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("InventoryTransferOutManual", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryTransferOutManual","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryTransferOutManual","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryTransferOutManual","InventoryTransferOutManualExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("InventoryTransferOutManual", "CancelOff", "取消下架", "取消下架", GridActionParameterTypesEnum.SingleId, "InventoryManagement").SetPromptMessage("操作不可逆，确认取消下架吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferOutManual_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutManual_View>>{

                this.MakeGridHeader(x => x.InventoryTransferOutManual_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferOutManual_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_TransInOrganization, width: 165).SetTitle(@Localizer["Page.调入组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_TransInWh, width: 165).SetTitle(@Localizer["Page.调入存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_TransOutOrganization, width: 165).SetTitle(@Localizer["Page.调出组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_TransOutWh, width: 165).SetTitle(@Localizer["Page.调出存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_Status, width: 85).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_SourceSystemId, width: 120).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_LastUpdateTime, width: 130).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_CreatePerson, width: 100).SetTitle("ERP提交人"),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_CreateTime, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_CreateBy, width: 75).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_UpdateTime, width: 130).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutManual_UpdateBy, width: 75).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryTransferOutManual_Status != InventoryTransferOutManualStatusEnum.InWh)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryTransferOutManual_Status == InventoryTransferOutManualStatusEnum.PartOff
                        || a.InventoryTransferOutManual_Status == InventoryTransferOutManualStatusEnum.AllOff)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryTransferOutManual_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutManual>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .CheckEqual(Searcher.TransInOrganizationId, x=>x.TransInOrganizationId)
                .CheckEqual(Searcher.TransInWhId, x=>x.TransInWhId)
                .CheckEqual(Searcher.TransOutOrganizationId, x=>x.TransOutOrganizationId)
                .CheckEqual(Searcher.TransOutWhId, x=>x.TransOutWhId)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryTransferOutManual_View
                {
				    ID = x.ID,
                    
                    InventoryTransferOutManual_CreatePerson = x.CreatePerson,
                    InventoryTransferOutManual_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    InventoryTransferOutManual_BusinessDate = x.BusinessDate,
                    InventoryTransferOutManual_SubmitDate = x.SubmitDate,
                    InventoryTransferOutManual_DocNo = x.DocNo,
                    InventoryTransferOutManual_DocType = x.DocType,
                    InventoryTransferOutManual_TransInOrganization = x.TransInOrganization.Code.CodeCombinName(x.TransInOrganization.Name),
                    InventoryTransferOutManual_TransInWh = x.TransInWh.Code.CodeCombinName(x.TransInWh.Name),
                    InventoryTransferOutManual_TransOutOrganization = x.TransOutOrganization.Code.CodeCombinName(x.TransOutOrganization.Name),
                    InventoryTransferOutManual_TransOutWh = x.TransOutWh.Code.CodeCombinName(x.TransOutWh.Name),
                    InventoryTransferOutManual_Status = x.Status,
                    InventoryTransferOutManual_Memo = x.Memo,
                    InventoryTransferOutManual_SourceSystemId = x.SourceSystemId,
                    InventoryTransferOutManual_LastUpdateTime = x.LastUpdateTime,
                    InventoryTransferOutManual_CreateTime = x.CreateTime,
                    InventoryTransferOutManual_UpdateTime = x.UpdateTime,
                    InventoryTransferOutManual_CreateBy = x.CreateBy,
                    InventoryTransferOutManual_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.InventoryTransferOutManual_CreateTime);
            return query;
        }

        
    }
    public class InventoryTransferOutManual_View: InventoryTransferOutManual
    {
        
        public string InventoryTransferOutManual_CreatePerson { get; set; }
        public string InventoryTransferOutManual_Organization { get; set; }
        public DateTime? InventoryTransferOutManual_BusinessDate { get; set; }
        public DateTime? InventoryTransferOutManual_SubmitDate { get; set; }
        public string InventoryTransferOutManual_DocNo { get; set; }
        public string InventoryTransferOutManual_DocType { get; set; }
        public string InventoryTransferOutManual_TransInOrganization { get; set; }
        public string InventoryTransferOutManual_TransInWh { get; set; }
        public string InventoryTransferOutManual_TransOutOrganization { get; set; }
        public string InventoryTransferOutManual_TransOutWh { get; set; }
        public InventoryTransferOutManualStatusEnum? InventoryTransferOutManual_Status { get; set; }
        public string InventoryTransferOutManual_Memo { get; set; }
        public string InventoryTransferOutManual_SourceSystemId { get; set; }
        public DateTime? InventoryTransferOutManual_LastUpdateTime { get; set; }
        public DateTime? InventoryTransferOutManual_CreateTime { get; set; }
        public DateTime? InventoryTransferOutManual_UpdateTime { get; set; }
        public string InventoryTransferOutManual_CreateBy { get; set; }
        public string InventoryTransferOutManual_UpdateBy { get; set; }

    }

}