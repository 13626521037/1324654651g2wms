using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnListVM : BasePagedListVM<PurchaseReturn_View, PurchaseReturnSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("PurchaseReturn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("PurchaseReturn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("PurchaseReturn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("PurchaseReturn", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "PurchaseManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("PurchaseReturn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "PurchaseManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("PurchaseReturn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("PurchaseReturn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"PurchaseManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("PurchaseReturn","PurchaseReturnExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"PurchaseManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("PurchaseReturn", "CancelOff", "取消下架", "取消下架", GridActionParameterTypesEnum.SingleId, "PurchaseManagement").SetPromptMessage("操作不可逆，确认取消下架吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<PurchaseReturn_View>> InitGridHeader()
        {
            return new List<GridColumn<PurchaseReturn_View>>{
                this.MakeGridHeader(x => x.PurchaseReturn_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_Supplier, width: 300).SetTitle(@Localizer["Page.供应商"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_LastUpdatePerson, width: 75).SetTitle("ERP修改人"),
                this.MakeGridHeader(x => x.PurchaseReturn_CreateDate).SetTitle("ERP创建时间").SetHide(),
                this.MakeGridHeader(x => x.PurchaseReturn_SourceSystemId, width: 130).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.PurchaseReturn_LastUpdateTime, width: 130).SetTitle(@Localizer["ERP最后修改时间"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_CreateTime, width: 130).SetTitle("同步时间"),
                //this.MakeGridHeader(x => x.PurchaseReturn_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.PurchaseReturn_CreateBy, width: 75).SetTitle("同步人"),
                //this.MakeGridHeader(x => x.PurchaseReturn_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseReturn_Status != PurchaseReturnStatusEnum.InWh)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.PurchaseReturn_Status == PurchaseReturnStatusEnum.PartOff
                        || a.PurchaseReturn_Status == PurchaseReturnStatusEnum.AllOff)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 180)
            };
        }

        

        public override IOrderedQueryable<PurchaseReturn_View> GetSearchQuery()
        {
            var query = DC.Set<PurchaseReturn>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.LastUpdatePerson, x=>x.LastUpdatePerson)
                .CheckBetween(Searcher.CreateDate?.GetStartTime(), Searcher.CreateDate?.GetEndTime(), x => x.CreateDate, includeMax: false)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .Where(x => x.Supplier.Code.ToLower().Contains(Searcher.Supplier.ToLower()) || x.Supplier.Name.ToLower().Contains(Searcher.Supplier.ToLower()))
                .CheckEqual(Searcher.Status, x=>x.Status)
                .Select(x => new PurchaseReturn_View
                {
				    ID = x.ID,
                    
                    PurchaseReturn_LastUpdatePerson = x.LastUpdatePerson,
                    PurchaseReturn_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    PurchaseReturn_BusinessDate = x.BusinessDate,
                    PurchaseReturn_CreateDate = x.CreateDate,
                    PurchaseReturn_DocNo = x.DocNo,
                    PurchaseReturn_DocType = x.DocType,
                    PurchaseReturn_Supplier = x.Supplier.Code.CodeCombinName(x.Supplier.Name),
                    PurchaseReturn_Status = x.Status,
                    PurchaseReturn_Memo = x.Memo,
                    PurchaseReturn_SourceSystemId = x.SourceSystemId,
                    PurchaseReturn_LastUpdateTime = x.LastUpdateTime,
                    PurchaseReturn_CreateTime = x.CreateTime,
                    PurchaseReturn_UpdateTime = x.UpdateTime,
                    PurchaseReturn_CreateBy = x.CreateBy,
                    PurchaseReturn_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class PurchaseReturn_View: PurchaseReturn
    {
        
        public string PurchaseReturn_LastUpdatePerson { get; set; }
        public string PurchaseReturn_Organization { get; set; }
        public DateTime? PurchaseReturn_BusinessDate { get; set; }
        public DateTime? PurchaseReturn_CreateDate { get; set; }
        public string PurchaseReturn_DocNo { get; set; }
        public string PurchaseReturn_DocType { get; set; }
        public string PurchaseReturn_Supplier { get; set; }
        public PurchaseReturnStatusEnum? PurchaseReturn_Status { get; set; }
        public string PurchaseReturn_Memo { get; set; }
        public string PurchaseReturn_SourceSystemId { get; set; }
        public DateTime? PurchaseReturn_LastUpdateTime { get; set; }
        public DateTime? PurchaseReturn_CreateTime { get; set; }
        public DateTime? PurchaseReturn_UpdateTime { get; set; }
        public string PurchaseReturn_CreateBy { get; set; }
        public string PurchaseReturn_UpdateBy { get; set; }

    }

}