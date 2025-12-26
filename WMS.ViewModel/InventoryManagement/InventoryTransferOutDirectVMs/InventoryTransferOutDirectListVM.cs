using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectListVM : BasePagedListVM<InventoryTransferOutDirect_View, InventoryTransferOutDirectSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryTransferOutDirect","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryTransferOutDirect","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryTransferOutDirect","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("InventoryTransferOutDirect", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryTransferOutDirect", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryTransferOutDirect","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryTransferOutDirect","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryTransferOutDirect","InventoryTransferOutDirectExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryTransferOutDirect_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryTransferOutDirect_View>>{

                this.MakeGridHeader(x => x.ID).SetHide(),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_TransInOrganization, width: 165).SetTitle(@Localizer["Page.调入组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_TransInWh, width: 165).SetTitle(@Localizer["Page.调入存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_TransOutOrganization, width: 165).SetTitle(@Localizer["Page.调出组织"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_TransOutWh, width: 165).SetTitle(@Localizer["Page.调出存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_CreateTime, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryTransferOutDirect_CreateBy, width: 75).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryTransferOutDirect_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryTransferOutDirect>()
                
                .CheckContain(Searcher.ErpID, x=>x.ErpID)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckEqual(Searcher.DocTypeId, x=>x.DocTypeId)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.TransInOrganizationId, x=>x.TransInOrganizationId)
                .CheckEqual(Searcher.TransInWhId, x=>x.TransInWhId)
                .CheckEqual(Searcher.TransOutWhId, x=>x.TransOutWhId)
                .CheckEqual(Searcher.TransOutOrganizationId, x=>x.TransOutOrganizationId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryTransferOutDirect_View
                {
				    ID = x.ID,
                    
                    InventoryTransferOutDirect_ErpID = x.ErpID,
                    InventoryTransferOutDirect_BusinessDate = x.BusinessDate,
                    InventoryTransferOutDirect_DocType = x.DocType.Name,
                    InventoryTransferOutDirect_DocNo = x.DocNo,
                    InventoryTransferOutDirect_TransInOrganization = x.TransInOrganization.Code.CodeCombinName(x.TransInOrganization.Name),
                    InventoryTransferOutDirect_TransInWh = x.TransInWh.Code.CodeCombinName(x.TransInWh.Name),
                    InventoryTransferOutDirect_TransOutWh = x.TransOutWh.Code.CodeCombinName(x.TransOutWh.Name),
                    InventoryTransferOutDirect_TransOutOrganization = x.TransOutOrganization.Code.CodeCombinName(x.TransOutOrganization.Name),
                    InventoryTransferOutDirect_Memo = x.Memo,
                    InventoryTransferOutDirect_CreateTime = x.CreateTime,
                    InventoryTransferOutDirect_UpdateTime = x.UpdateTime,
                    InventoryTransferOutDirect_CreateBy = x.CreateBy,
                    InventoryTransferOutDirect_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.InventoryTransferOutDirect_CreateTime);
            return query;
        }

        
    }
    public class InventoryTransferOutDirect_View: InventoryTransferOutDirect
    {
        
        public string InventoryTransferOutDirect_ErpID { get; set; }
        public DateTime? InventoryTransferOutDirect_BusinessDate { get; set; }
        public string InventoryTransferOutDirect_DocType { get; set; }
        public string InventoryTransferOutDirect_DocNo { get; set; }
        public string InventoryTransferOutDirect_TransInOrganization { get; set; }
        public string InventoryTransferOutDirect_TransInWh { get; set; }
        public string InventoryTransferOutDirect_TransOutWh { get; set; }
        public string InventoryTransferOutDirect_TransOutOrganization { get; set; }
        public string InventoryTransferOutDirect_Memo { get; set; }
        public DateTime? InventoryTransferOutDirect_CreateTime { get; set; }
        public DateTime? InventoryTransferOutDirect_UpdateTime { get; set; }
        public string InventoryTransferOutDirect_CreateBy { get; set; }
        public string InventoryTransferOutDirect_UpdateBy { get; set; }

    }

}