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

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnListVM : BasePagedListVM<PurchaseOutsourcingReturn_View, PurchaseOutsourcingReturnSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PurchaseOutsourcingReturn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PurchaseOutsourcingReturn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("PurchaseOutsourcingReturn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("PurchaseOutsourcingReturn", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "PurchaseManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("PurchaseOutsourcingReturn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PurchaseManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PurchaseOutsourcingReturn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PurchaseOutsourcingReturn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("PurchaseOutsourcingReturn","PurchaseOutsourcingReturnExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PurchaseManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PurchaseOutsourcingReturn", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "PurchaseManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseOutsourcingReturn_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseOutsourcingReturn_View>>{
                
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_Supplier, width: 300).SetTitle(@Localizer["Page.供应商"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_CreatePerson, width: 75).SetTitle(@Localizer["Page.ERP提交人"].Value),
                this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.PurchaseOutsourcingReturn_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseOutsourcingReturn_Status != PurchaseOutsourcingReturnStatusEnum.NotReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseOutsourcingReturn_Status == PurchaseOutsourcingReturnStatusEnum.PartReceive
                        || a.PurchaseOutsourcingReturn_Status == PurchaseOutsourcingReturnStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<PurchaseOutsourcingReturn_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseOutsourcingReturn>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                //.CheckEqual(Searcher.SupplierId, x=>x.SupplierId)
                .Where(x => x.Supplier.Code.ToLower().Contains(Searcher.Supplier.ToLower()) || x.Supplier.Name.ToLower().Contains(Searcher.Supplier.ToLower()))
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .Select(x => new PurchaseOutsourcingReturn_View
                {
				    ID = x.ID,
                    
                    PurchaseOutsourcingReturn_CreatePerson = x.CreatePerson,
                    PurchaseOutsourcingReturn_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    PurchaseOutsourcingReturn_BusinessDate = x.BusinessDate,
                    PurchaseOutsourcingReturn_SubmitDate = x.SubmitDate,
                    PurchaseOutsourcingReturn_DocNo = x.DocNo,
                    PurchaseOutsourcingReturn_DocType = x.DocType,
                    PurchaseOutsourcingReturn_Supplier = x.Supplier.Code.CodeCombinName(x.Supplier.Name),
                    PurchaseOutsourcingReturn_Status = x.Status,
                    PurchaseOutsourcingReturn_Memo = x.Memo,
                    PurchaseOutsourcingReturn_SourceSystemId = x.SourceSystemId,
                    PurchaseOutsourcingReturn_LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseOutsourcingReturn_View: PurchaseOutsourcingReturn
    {
        
        public string PurchaseOutsourcingReturn_CreatePerson { get; set; }
        public string PurchaseOutsourcingReturn_Organization { get; set; }
        public DateTime? PurchaseOutsourcingReturn_BusinessDate { get; set; }
        public DateTime? PurchaseOutsourcingReturn_SubmitDate { get; set; }
        public string PurchaseOutsourcingReturn_DocNo { get; set; }
        public string PurchaseOutsourcingReturn_DocType { get; set; }
        public string PurchaseOutsourcingReturn_Supplier { get; set; }
        public PurchaseOutsourcingReturnStatusEnum? PurchaseOutsourcingReturn_Status { get; set; }
        public string PurchaseOutsourcingReturn_Memo { get; set; }
        public string PurchaseOutsourcingReturn_SourceSystemId { get; set; }
        public DateTime? PurchaseOutsourcingReturn_LastUpdateTime { get; set; }

    }

}