using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.PurchaseManagement;

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueListVM : BasePagedListVM<PurchaseOutsourcingIssue_View, PurchaseOutsourcingIssueSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PurchaseOutsourcingIssue","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PurchaseOutsourcingIssue","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("PurchaseOutsourcingIssue","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("PurchaseOutsourcingIssue", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "PurchaseManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("PurchaseOutsourcingIssue", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PurchaseManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PurchaseOutsourcingIssue","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PurchaseOutsourcingIssue","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("PurchaseOutsourcingIssue","PurchaseOutsourcingIssueExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PurchaseManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PurchaseOutsourcingIssue", "CancelOff", "取消下架", "取消下架", GridActionParameterTypesEnum.SingleId, "PurchaseManagement").SetPromptMessage("操作不可逆，确认取消下架吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseOutsourcingIssue_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingIssue_View>>{
                
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_Supplier, width: 300).SetTitle(@Localizer["Page.供应商"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_CreatePerson, width: 75).SetTitle(@Localizer["Page.ERP提交人"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_SourceSystemId, width: 130).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.PurchaseOutsourcingIssue_LastUpdateTime, width: 130).SetTitle(@Localizer["Page.最后修改时间"].Value).SetHide(),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseOutsourcingIssue_Status != PurchaseOutsourcingIssueStatusEnum.InWh)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseOutsourcingIssue_Status == PurchaseOutsourcingIssueStatusEnum.PartOff
                        || a.PurchaseOutsourcingIssue_Status == PurchaseOutsourcingIssueStatusEnum.AllOff)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<PurchaseOutsourcingIssue_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseOutsourcingIssue>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .Where(x => x.Supplier.Code.ToLower().Contains(Searcher.Supplier.ToLower()) || x.Supplier.Name.ToLower().Contains(Searcher.Supplier.ToLower()))
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .Select(x => new PurchaseOutsourcingIssue_View
                {
				    ID = x.ID,
                    
                    PurchaseOutsourcingIssue_CreatePerson = x.CreatePerson,
                    PurchaseOutsourcingIssue_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    PurchaseOutsourcingIssue_BusinessDate = x.BusinessDate,
                    PurchaseOutsourcingIssue_SubmitDate = x.SubmitDate,
                    PurchaseOutsourcingIssue_DocNo = x.DocNo,
                    PurchaseOutsourcingIssue_DocType = x.DocType,
                    PurchaseOutsourcingIssue_Supplier = x.Supplier.Code.CodeCombinName(x.Supplier.Name),
                    PurchaseOutsourcingIssue_Status = x.Status,
                    PurchaseOutsourcingIssue_Memo = x.Memo,
                    PurchaseOutsourcingIssue_SourceSystemId = x.SourceSystemId,
                    PurchaseOutsourcingIssue_LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseOutsourcingIssue_View: PurchaseOutsourcingIssue
    {
        
        public string PurchaseOutsourcingIssue_CreatePerson { get; set; }
        public string PurchaseOutsourcingIssue_Organization { get; set; }
        public DateTime? PurchaseOutsourcingIssue_BusinessDate { get; set; }
        public DateTime? PurchaseOutsourcingIssue_SubmitDate { get; set; }
        public string PurchaseOutsourcingIssue_DocNo { get; set; }
        public string PurchaseOutsourcingIssue_DocType { get; set; }
        public string PurchaseOutsourcingIssue_Supplier { get; set; }
        public PurchaseOutsourcingIssueStatusEnum? PurchaseOutsourcingIssue_Status { get; set; }
        public string PurchaseOutsourcingIssue_Memo { get; set; }
        public string PurchaseOutsourcingIssue_SourceSystemId { get; set; }
        public DateTime? PurchaseOutsourcingIssue_LastUpdateTime { get; set; }

    }

}