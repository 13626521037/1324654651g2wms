using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementListVM : BasePagedListVM<PurchaseReceivement_View, PurchaseReceivementSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PurchaseReceivement","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PurchaseReceivement","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("PurchaseReceivement","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("PurchaseReceivement", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "PurchaseManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("PurchaseReceivement", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PurchaseManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PurchaseReceivement","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PurchaseReceivement","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("PurchaseReceivement","PurchaseReceivementExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PurchaseManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PurchaseReceivement", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "PurchaseManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelReceiveable").SetShowInRow().SetHideOnToolBar(),
                this.MakeAction("PurchaseReceivement", "Help", "", "帮助", GridActionParameterTypesEnum.NoId, "PurchaseManagement", dialogWidth: 1000).SetButtonClass("layui-bg-orange").SetIconCls("layui-icon layui-icon-help")
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseReceivement_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReceivement_View>>{

                this.MakeGridHeader(x => x.PurchaseReceivement_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_Supplier, width: 300).SetTitle(@Localizer["Page.供应商"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_BizType, width: 100).SetTitle(@Localizer["Page.业务类型"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_InspectStatus, width: 65).SetTitle(@Localizer["Page.检验状态"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_Memo, width: 100).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_SourceSystemId).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.PurchaseReceivement_LastUpdateTime, width: 130).SetTitle(@Localizer["ERP最后修改时间"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_CreatePerson, width: 75).SetTitle("ERP创建人"),
                //this.MakeGridHeader(x => x.PurchaseReceivement_SubmitDate, width: 130).SetTitle("ERP创建时间"),
                this.MakeGridHeader(x => x.PurchaseReceivement_CreateTime, width: 130).SetTitle("同步时间"),
                //this.MakeGridHeader(x => x.PurchaseReceivement_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.PurchaseReceivement_CreateBy, width: 75).SetTitle("同步人"),
                //this.MakeGridHeader(x => x.PurchaseReceivement_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseReceivement_Status != PurchaseReceivementStatusEnum.NotReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelReceiveable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseReceivement_Status == PurchaseReceivementStatusEnum.PartReceive 
                        || a.PurchaseReceivement_Status == PurchaseReceivementStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 180)
            };
        }

        

        public override IOrderedQueryable<PurchaseReceivement_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReceivement>()
                
                .CheckContain(Searcher.CreatePerson, x=>x.CreatePerson)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .Where(x => x.Supplier.Code.ToLower().Contains(Searcher.Supplier.ToLower()) || x.Supplier.Name.ToLower().Contains(Searcher.Supplier.ToLower()))
                .CheckEqual(Searcher.BizType, x=>x.BizType)
                .CheckEqual(Searcher.InspectStatus, x=>x.InspectStatus)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckContain(Searcher.SourceSystemId, x=>x.SourceSystemId)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new PurchaseReceivement_View
                {
				    ID = x.ID,
                    
                    PurchaseReceivement_CreatePerson = x.CreatePerson,
                    PurchaseReceivement_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    PurchaseReceivement_BusinessDate = x.BusinessDate,
                    PurchaseReceivement_SubmitDate = x.SubmitDate,
                    PurchaseReceivement_DocNo = x.DocNo,
                    PurchaseReceivement_DocType = x.DocType,
                    PurchaseReceivement_Supplier = x.Supplier.Code.CodeCombinName(x.Supplier.Name),
                    PurchaseReceivement_BizType = x.BizType,
                    PurchaseReceivement_InspectStatus = x.InspectStatus,
                    PurchaseReceivement_Status = x.Status,
                    PurchaseReceivement_Memo = x.Memo,
                    PurchaseReceivement_SourceSystemId = x.SourceSystemId,
                    PurchaseReceivement_LastUpdateTime = x.LastUpdateTime,
                    PurchaseReceivement_CreateTime = x.CreateTime,
                    PurchaseReceivement_UpdateTime = x.UpdateTime,
                    PurchaseReceivement_CreateBy = x.CreateBy,
                    PurchaseReceivement_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseReceivement_View: PurchaseReceivement
    {
        
        public string PurchaseReceivement_CreatePerson { get; set; }
        public string PurchaseReceivement_Organization { get; set; }
        public DateTime? PurchaseReceivement_BusinessDate { get; set; }
        public DateTime? PurchaseReceivement_SubmitDate { get; set; }
        public string PurchaseReceivement_DocNo { get; set; }
        public string PurchaseReceivement_DocType { get; set; }
        public string PurchaseReceivement_Supplier { get; set; }
        public BizTypeEnum? PurchaseReceivement_BizType { get; set; }
        public PurchaseReceivementInspectStatusEnum? PurchaseReceivement_InspectStatus { get; set; }
        public PurchaseReceivementStatusEnum? PurchaseReceivement_Status { get; set; }
        public string PurchaseReceivement_Memo { get; set; }
        public string PurchaseReceivement_SourceSystemId { get; set; }
        public DateTime? PurchaseReceivement_LastUpdateTime { get; set; }
        public DateTime? PurchaseReceivement_CreateTime { get; set; }
        public DateTime? PurchaseReceivement_UpdateTime { get; set; }
        public string PurchaseReceivement_CreateBy { get; set; }
        public string PurchaseReceivement_UpdateBy { get; set; }
        public bool? PurchaseReceivement_IsValid { get; set; }

    }

}