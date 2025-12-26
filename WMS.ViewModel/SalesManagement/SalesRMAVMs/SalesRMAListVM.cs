using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMAListVM : BasePagedListVM<SalesRMA_View, SalesRMASearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("SalesRMA","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("SalesRMA","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("SalesRMA","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement", dialogWidth: 1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("SalesRMA", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "SalesManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("SalesRMA", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "SalesManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("SalesRMA","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("SalesRMA","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("SalesRMA","SalesRMAExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"SalesManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("SalesRMA", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "SalesManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<SalesRMA_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesRMA_View>>{
                
                this.MakeGridHeader(x => x.SalesRMA_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.SalesRMA_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.SalesRMA_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.SalesRMA_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.SalesRMA_Customer, width: 300).SetTitle(@Localizer["Page.客户"].Value),
                this.MakeGridHeader(x => x.SalesRMA_Operators, width: 75).SetTitle(@Localizer["Page.业务员"].Value),
                this.MakeGridHeader(x => x.SalesRMA_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.SalesRMA_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.SalesRMA_CreatePerson, width: 75).SetTitle("ERP审核人"),
                this.MakeGridHeader(x => x.SalesRMA_ApproveDate, width: 130).SetTitle("审核时间"),
                //this.MakeGridHeader(x => x.SalesRMA_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value),
                //this.MakeGridHeader(x => x.SalesRMA_LastUpdateTime).SetTitle(@Localizer["Page.最后修改时间"].Value),
                //this.MakeGridHeader(x => x.SalesRMA_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.SalesRMA_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.SalesRMA_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.SalesRMA_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesRMA_Status != SalesRMAStatusEnum.NotReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesRMA_Status == SalesRMAStatusEnum.PartReceive
                        || a.SalesRMA_Status == SalesRMAStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<SalesRMA_View> GetSearchQuery()
        {
            var query = DC.Set<SalesRMA>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.ApproveDate?.GetStartTime(), Searcher.ApproveDate?.GetEndTime(), x => x.ApproveDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .CheckContain(Searcher.Operators, x=>x.Operators)
                .Where(x => x.Customer.Code.ToLower().Contains(Searcher.Customer.ToLower()) || x.Customer.Name.ToLower().Contains(Searcher.Customer.ToLower()))
                //.CheckEqual(Searcher.CustomerId, x=>x.CustomerId)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new SalesRMA_View
                {
				    ID = x.ID,
                    
                    SalesRMA_CreatePerson = x.CreatePerson,
                    SalesRMA_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    SalesRMA_BusinessDate = x.BusinessDate,
                    SalesRMA_ApproveDate = x.ApproveDate,
                    SalesRMA_DocNo = x.DocNo,
                    SalesRMA_DocType = x.DocType,
                    SalesRMA_Operators = x.Operators,
                    SalesRMA_Customer = x.Customer.Code.CodeCombinName(x.Customer.Name),
                    SalesRMA_Status = x.Status,
                    SalesRMA_Memo = x.Memo,
                    SalesRMA_SourceSystemId = x.SourceSystemId,
                    SalesRMA_LastUpdateTime = x.LastUpdateTime,
                    SalesRMA_CreateTime = x.CreateTime,
                    SalesRMA_UpdateTime = x.UpdateTime,
                    SalesRMA_CreateBy = x.CreateBy,
                    SalesRMA_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class SalesRMA_View: SalesRMA
    {
        
        public string SalesRMA_CreatePerson { get; set; }
        public string SalesRMA_Organization { get; set; }
        public DateTime? SalesRMA_BusinessDate { get; set; }
        public DateTime? SalesRMA_ApproveDate { get; set; }
        public string SalesRMA_DocNo { get; set; }
        public string SalesRMA_DocType { get; set; }
        public string SalesRMA_Operators { get; set; }
        public string SalesRMA_Customer { get; set; }
        public SalesRMAStatusEnum? SalesRMA_Status { get; set; }
        public string SalesRMA_Memo { get; set; }
        public string SalesRMA_SourceSystemId { get; set; }
        public DateTime? SalesRMA_LastUpdateTime { get; set; }
        public DateTime? SalesRMA_CreateTime { get; set; }
        public DateTime? SalesRMA_UpdateTime { get; set; }
        public string SalesRMA_CreateBy { get; set; }
        public string SalesRMA_UpdateBy { get; set; }

    }

}