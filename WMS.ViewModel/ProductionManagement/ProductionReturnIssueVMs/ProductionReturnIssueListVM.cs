using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueListVM : BasePagedListVM<ProductionReturnIssue_View, ProductionReturnIssueSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("ProductionReturnIssue","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("ProductionReturnIssue","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("ProductionReturnIssue","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("ProductionReturnIssue", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "ProductionManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("ProductionReturnIssue", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "ProductionManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("ProductionReturnIssue","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("ProductionReturnIssue","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                //this.MakeAction("ProductionReturnIssue","ProductionReturnIssueExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"ProductionManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("ProductionReturnIssue", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "ProductionManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<ProductionReturnIssue_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionReturnIssue_View>>{
                
                this.MakeGridHeader(x => x.ProductionReturnIssue_DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.ProductionReturnIssue_CreatePerson, width: 75).SetTitle("ERP提交人"),
                this.MakeGridHeader(x => x.ProductionReturnIssue_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.ProductionReturnIssue_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.ProductionReturnIssue_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionReturnIssue_Status != ProductionReturnIssueStatusEnum.NotReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionReturnIssue_Status == ProductionReturnIssueStatusEnum.PartReceive
                        || a.ProductionReturnIssue_Status == ProductionReturnIssueStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<ProductionReturnIssue_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionReturnIssue>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new ProductionReturnIssue_View
                {
				    ID = x.ID,
                    
                    ProductionReturnIssue_DocNo = x.DocNo,
                    ProductionReturnIssue_BusinessDate = x.BusinessDate,
                    ProductionReturnIssue_DocType = x.DocType,
                    ProductionReturnIssue_Organization = x.Organization.CodeAndName,
                    ProductionReturnIssue_Status = x.Status,
                    ProductionReturnIssue_Memo = x.Memo,
                    ProductionReturnIssue_CreatePerson = x.CreatePerson,
                    ProductionReturnIssue_SubmitDate = x.SubmitDate,
                    ProductionReturnIssue_SourceSystemId = x.SourceSystemId,
                    ProductionReturnIssue_LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class ProductionReturnIssue_View: ProductionReturnIssue
    {
        
        public string ProductionReturnIssue_DocNo { get; set; }
        public DateTime? ProductionReturnIssue_BusinessDate { get; set; }
        public string ProductionReturnIssue_DocType { get; set; }
        public string ProductionReturnIssue_Organization { get; set; }
        public ProductionReturnIssueStatusEnum? ProductionReturnIssue_Status { get; set; }
        public string ProductionReturnIssue_Memo { get; set; }
        public string ProductionReturnIssue_CreatePerson { get; set; }
        public DateTime? ProductionReturnIssue_SubmitDate { get; set; }
        public string ProductionReturnIssue_SourceSystemId { get; set; }
        public DateTime? ProductionReturnIssue_LastUpdateTime { get; set; }

    }

}