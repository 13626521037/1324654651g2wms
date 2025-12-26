using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.SalesManagement;

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementListVM : BasePagedListVM<SalesReturnReceivement_View, SalesReturnReceivementSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("SalesReturnReceivement","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("SalesReturnReceivement","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("SalesReturnReceivement","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("SalesReturnReceivement", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "SalesManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("SalesReturnReceivement", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "SalesManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("SalesReturnReceivement","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("SalesReturnReceivement","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("SalesReturnReceivement","SalesReturnReceivementExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"SalesManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("SalesReturnReceivement", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "SalesManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<SalesReturnReceivement_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesReturnReceivement_View>>{
                
                this.MakeGridHeader(x => x.SalesReturnReceivement_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_Customer, width: 300).SetTitle(@Localizer["Page.客户"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.SalesReturnReceivement_CreatePerson, width: 75).SetTitle("ERP提交人"),
                this.MakeGridHeader(x => x.SalesReturnReceivement_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.SalesReturnReceivement_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesReturnReceivement_Status != SalesReturnReceivementStatusEnum.NotReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesReturnReceivement_Status == SalesReturnReceivementStatusEnum.PartReceive
                        || a.SalesReturnReceivement_Status == SalesReturnReceivementStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<SalesReturnReceivement_View> GetSearchQuery()
        {
            var query = DC.Set<SalesReturnReceivement>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                //.CheckEqual(Searcher.CustomerId, x=>x.CustomerId)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .Where(x => x.Customer.Code.ToLower().Contains(Searcher.Customer.ToLower()) || x.Customer.Name.ToLower().Contains(Searcher.Customer.ToLower()))
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new SalesReturnReceivement_View
                {
				    ID = x.ID,
                    
                    SalesReturnReceivement_CreatePerson = x.CreatePerson,
                    SalesReturnReceivement_Organization = x.Organization.CodeAndName,
                    SalesReturnReceivement_BusinessDate = x.BusinessDate,
                    SalesReturnReceivement_SubmitDate = x.SubmitDate,
                    SalesReturnReceivement_DocNo = x.DocNo,
                    SalesReturnReceivement_DocType = x.DocType,
                    SalesReturnReceivement_Customer = x.Customer.CodeAndName,
                    SalesReturnReceivement_Status = x.Status,
                    SalesReturnReceivement_Memo = x.Memo,
                    SalesReturnReceivement_SourceSystemId = x.SourceSystemId,
                    SalesReturnReceivement_LastUpdateTime = x.LastUpdateTime,
                    SalesReturnReceivement_CreateTime = x.CreateTime,
                    SalesReturnReceivement_UpdateTime = x.UpdateTime,
                    SalesReturnReceivement_CreateBy = x.CreateBy,
                    SalesReturnReceivement_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class SalesReturnReceivement_View: SalesReturnReceivement
    {
        
        public string SalesReturnReceivement_CreatePerson { get; set; }
        public string SalesReturnReceivement_Organization { get; set; }
        public DateTime? SalesReturnReceivement_BusinessDate { get; set; }
        public DateTime? SalesReturnReceivement_SubmitDate { get; set; }
        public string SalesReturnReceivement_DocNo { get; set; }
        public string SalesReturnReceivement_DocType { get; set; }
        public string SalesReturnReceivement_Customer { get; set; }
        public SalesReturnReceivementStatusEnum? SalesReturnReceivement_Status { get; set; }
        public string SalesReturnReceivement_Memo { get; set; }
        public string SalesReturnReceivement_SourceSystemId { get; set; }
        public DateTime? SalesReturnReceivement_LastUpdateTime { get; set; }
        public DateTime? SalesReturnReceivement_CreateTime { get; set; }
        public DateTime? SalesReturnReceivement_UpdateTime { get; set; }
        public string SalesReturnReceivement_CreateBy { get; set; }
        public string SalesReturnReceivement_UpdateBy { get; set; }

    }

}