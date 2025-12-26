using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInListVM : BasePagedListVM<InventoryTransferIn_View, InventoryTransferInSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryTransferIn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryTransferIn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryTransferIn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryTransferIn", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("InventoryTransferIn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryTransferIn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryTransferIn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryTransferIn","InventoryTransferInExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferIn_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferIn_View>>{
                
                //this.MakeGridHeader(x => x.InventoryTransferIn_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value),
                this.MakeGridHeader(x => x.ID, width: 240).SetHide(),
                this.MakeGridHeader(x => x.InventoryTransferIn_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_TransInOrganization, width: 165).SetTitle(@Localizer["Page.调入组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_TransInWh, width: 165).SetTitle(@Localizer["Page.调入存储地点"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferIn_TransferOut).SetTitle(@Localizer["Page.调出单"].Value),
                //this.MakeGridHeader(x => x.InventoryTransferIn_TransferOutType).SetTitle(@Localizer["Page.调出单类型"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_Status, width: 85).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_CreateTime, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_CreateBy, width: 75).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_UpdateTime, width: 130).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.InventoryTransferIn_UpdateBy, width: 75).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryTransferIn_Status != InventoryTransferInStatusEnum.AllReceive)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryTransferIn_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferIn>()
                
                .CheckContain(Searcher.ErpID, x=>x.ErpID)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.DocType, x=>x.DocType)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.TransInOrganizationId, x=>x.TransInOrganizationId)
                .CheckEqual(Searcher.TransInWhId, x=>x.TransInWhId)
                .CheckEqual(Searcher.TransferOut, x=>x.TransferOut)
                .CheckEqual(Searcher.TransferOutType, x=>x.TransferOutType)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryTransferIn_View
                {
				    ID = x.ID,
                    
                    InventoryTransferIn_ErpID = x.ErpID,
                    InventoryTransferIn_BusinessDate = x.BusinessDate,
                    InventoryTransferIn_DocType = x.DocType,
                    InventoryTransferIn_DocNo = x.DocNo,
                    InventoryTransferIn_TransInOrganization = x.TransInOrganization.Code.CodeCombinName(x.TransInOrganization.Name),
                    InventoryTransferIn_TransInWh = x.TransInWh.Code.CodeCombinName(x.TransInWh.Name),
                    InventoryTransferIn_TransferOut = x.TransferOut,
                    InventoryTransferIn_TransferOutType = x.TransferOutType,
                    InventoryTransferIn_Status = x.Status,
                    InventoryTransferIn_Memo = x.Memo,
                    InventoryTransferIn_CreateTime = x.CreateTime,
                    InventoryTransferIn_UpdateTime = x.UpdateTime,
                    InventoryTransferIn_CreateBy = x.CreateBy,
                    InventoryTransferIn_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.InventoryTransferIn_CreateTime);
            return query;
        }

        
    }
    public class InventoryTransferIn_View: InventoryTransferIn
    {
        
        public string InventoryTransferIn_ErpID { get; set; }
        public DateTime? InventoryTransferIn_BusinessDate { get; set; }
        public string InventoryTransferIn_DocType { get; set; }
        public string InventoryTransferIn_DocNo { get; set; }
        public string InventoryTransferIn_TransInOrganization { get; set; }
        public string InventoryTransferIn_TransInWh { get; set; }
        public Guid? InventoryTransferIn_TransferOut { get; set; }
        public InventoryTransferOutTypeEnum? InventoryTransferIn_TransferOutType { get; set; }
        public InventoryTransferInStatusEnum? InventoryTransferIn_Status { get; set; }
        public string InventoryTransferIn_Memo { get; set; }
        public DateTime? InventoryTransferIn_CreateTime { get; set; }
        public DateTime? InventoryTransferIn_UpdateTime { get; set; }
        public string InventoryTransferIn_CreateBy { get; set; }
        public string InventoryTransferIn_UpdateBy { get; set; }

    }

}