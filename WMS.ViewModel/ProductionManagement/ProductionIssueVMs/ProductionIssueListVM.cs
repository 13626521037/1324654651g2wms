using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueListVM : BasePagedListVM<ProductionIssue_View, ProductionIssueSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("ProductionIssue","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("ProductionIssue","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("ProductionIssue","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("ProductionIssue", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "ProductionManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("ProductionIssue", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "ProductionManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("ProductionIssue","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("ProductionIssue","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("ProductionIssue","ProductionIssueExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"ProductionManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("ProductionIssue", "CancelOff", "取消下架", "取消下架", GridActionParameterTypesEnum.SingleId, "ProductionManagement").SetPromptMessage("操作不可逆，确认取消下架吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }


        protected override IEnumerable<IGridColumn<ProductionIssue_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionIssue_View>>{

                this.MakeGridHeader(x => x.ProductionIssue_DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_CreatePerson, width: 75).SetTitle("ERP提交人"),
                this.MakeGridHeader(x => x.ProductionIssue_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.ProductionIssue_SourceSystemId, width: 130).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.ProductionIssue_LastUpdateTime, width: 130).SetTitle(@Localizer["Page.最后修改时间"].Value).SetHide(),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionIssue_Status != ProductionIssueStatusEnum.InWh)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionIssue_Status == ProductionIssueStatusEnum.PartOff
                        || a.ProductionIssue_Status == ProductionIssueStatusEnum.AllOff)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }



        public override IOrderedQueryable<ProductionIssue_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionIssue>()

                .CheckContain(Searcher.CreatePerson, x => x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x => x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x => x.DocNo)
                .CheckContain(Searcher.DocType, x => x.DocType)
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckContain(Searcher.SourceSystemId, x => x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new ProductionIssue_View
                {
                    ID = x.ID,

                    ProductionIssue_CreatePerson = x.CreatePerson,
                    ProductionIssue_Organization = x.Organization.CodeAndName,
                    ProductionIssue_BusinessDate = x.BusinessDate,
                    ProductionIssue_SubmitDate = x.SubmitDate,
                    ProductionIssue_DocNo = x.DocNo,
                    ProductionIssue_DocType = x.DocType,
                    ProductionIssue_Status = x.Status,
                    ProductionIssue_Memo = x.Memo,
                    ProductionIssue_SourceSystemId = x.SourceSystemId,
                    ProductionIssue_LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }


    }
    public class ProductionIssue_View : ProductionIssue
    {

        public string ProductionIssue_CreatePerson { get; set; }
        public string ProductionIssue_Organization { get; set; }
        public DateTime? ProductionIssue_BusinessDate { get; set; }
        public DateTime? ProductionIssue_SubmitDate { get; set; }
        public string ProductionIssue_DocNo { get; set; }
        public string ProductionIssue_DocType { get; set; }
        public ProductionIssueStatusEnum? ProductionIssue_Status { get; set; }
        public string ProductionIssue_Memo { get; set; }
        public string ProductionIssue_SourceSystemId { get; set; }
        public DateTime? ProductionIssue_LastUpdateTime { get; set; }

    }

}