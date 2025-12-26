using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingListVM : BasePagedListVM<InventoryStockTaking_View, InventoryStockTakingSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryStockTaking","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                // 注意：此处修改共用新增页面
                this.MakeAction("InventoryStockTaking","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"), //.SetMax(),
                this.MakeAction("InventoryStockTaking", "Submit", "提交", "提交盘点单", GridActionParameterTypesEnum.SingleId, "InventoryManagement", dialogWidth: 1400).SetButtonClass("layui-btn-normal").SetShowInRow(true).SetHideOnToolBar(true).SetBindVisiableColName("IsDeleteable"),
                this.MakeAction("InventoryStockTaking", "Approve", "审核", "审核盘点单", GridActionParameterTypesEnum.SingleId, "InventoryManagement", dialogWidth: 1400).SetButtonClass("layui-btn-normal").SetShowInRow(true).SetHideOnToolBar(true).SetBindVisiableColName("IsApproveable"),
                this.MakeAction("InventoryStockTaking","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm").SetBindVisiableColName("IsDeleteable"),
                this.MakeStandardAction("InventoryStockTaking", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                this.MakeStandardAction("InventoryStockTaking", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryStockTaking","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryStockTaking","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryStockTaking","InventoryStockTakingExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("InventoryStockTaking", "ForceClose", "终止关闭", "终止盘点单", GridActionParameterTypesEnum.SingleId, "InventoryManagement", dialogWidth: 1400).SetButtonClass("layui-btn-danger").SetShowInRow(true).SetHideOnToolBar(true).SetBindVisiableColName("IsCloseable"),
                this.MakeAction("InventoryStockTaking", "Help", "", "帮助", GridActionParameterTypesEnum.NoId, "InventoryManagement", dialogWidth: 1000).SetButtonClass("layui-bg-orange").SetIconCls("layui-icon layui-icon-help")
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryStockTaking_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTaking_View>>{
                
                this.MakeGridHeader(x => x.ID, width: 240).SetHide(),
                this.MakeGridHeader(x => x.InventoryStockTaking_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.InventoryStockTaking_DocNo, width: 105).SetTitle(@Localizer["Page.单号"].Value).SetFixed(GridColumnFixedEnum.Left),
                //this.MakeGridHeader(x => x.InventoryStockTaking_Dimension).SetTitle(@Localizer["Page.盘点维度"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_Wh, width: 210).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_Status, width: 80).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_ErpDocNo, width: 120).SetTitle(@Localizer["Page.ERP单号"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_SubmitTime, width: 135).SetTitle(@Localizer["Page.提交时间"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_SubmitUser, width: 90).SetTitle(@Localizer["Page.提交人"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_ApproveTime, width: 135).SetTitle(@Localizer["Page.审核时间"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_ApproveUser, width: 90).SetTitle(@Localizer["Page.审核人"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_CloseTime, width: 135).SetTitle(@Localizer["Page.关闭时间"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_CloseUser, width: 90).SetTitle(@Localizer["Page.关闭人"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_CreateTime, width: 135).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryStockTaking_CreateBy, width: 90).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.Mode, width: 90),
                this.MakeGridHeader(x => x.InventoryStockTaking_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                //this.MakeGridHeader(x => x.InventoryStockTaking_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryStockTaking_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryStockTaking_Status != InventoryStockTakingStatusEnum.Opened)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsApproveable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryStockTaking_Status != InventoryStockTakingStatusEnum.Approving)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCloseable").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryStockTaking_Status == InventoryStockTakingStatusEnum.Opened 
                        || a.InventoryStockTaking_Status == InventoryStockTakingStatusEnum.Closed
                        || a.InventoryStockTaking_Status == InventoryStockTakingStatusEnum.ForceClosed)
                    {
                        return "false";
                    }
                    if (a.InventoryStockTaking_CreateBy != LoginUserInfo.ITCode)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeaderAction(width: 200).SetAlign(GridColumnAlignEnum.Left)
            };
        }

        

        public override IOrderedQueryable<InventoryStockTaking_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryStockTaking>()
                
                .CheckContain(Searcher.ErpID, x=>x.ErpID)
                .CheckEqual(Searcher.ErpDocNo, x=>x.ErpDocNo)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Dimension, x=>x.Dimension)
                //.CheckEqual(Searcher.WhId, x=>x.WhId)
                .Where(x => string.IsNullOrEmpty(Searcher.Wh) || x.Wh.Code.Contains(Searcher.Wh) || x.Wh.Name.Contains(Searcher.Wh))
                .Where(x => string.IsNullOrEmpty(Searcher.Location) || x.InventoryStockTakingLocations_StockTaking.Any(y => y.Location.Code.Contains(Searcher.Location) || y.Location.Name.Contains(Searcher.Location)))
                .CheckBetween(Searcher.SubmitTime?.GetStartTime(), Searcher.SubmitTime?.GetEndTime(), x => x.SubmitTime, includeMax: false)
                .CheckContain(Searcher.SubmitUser, x=>x.SubmitUser)
                .CheckBetween(Searcher.ApproveTime?.GetStartTime(), Searcher.ApproveTime?.GetEndTime(), x => x.ApproveTime, includeMax: false)
                .CheckContain(Searcher.ApproveUser, x=>x.ApproveUser)
                .CheckBetween(Searcher.CloseTime?.GetStartTime(), Searcher.CloseTime?.GetEndTime(), x => x.CloseTime, includeMax: false)
                .CheckContain(Searcher.CloseUser, x=>x.CloseUser)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryStockTaking_View
                {
				    ID = x.ID,
                    
                    InventoryStockTaking_ErpID = x.ErpID,
                    InventoryStockTaking_ErpDocNo = x.ErpDocNo,
                    InventoryStockTaking_DocNo = x.DocNo,
                    InventoryStockTaking_Dimension = x.Dimension,
                    InventoryStockTaking_Wh = x.Wh.Code.CodeCombinName(x.Wh.Name),
                    InventoryStockTaking_SubmitTime = x.SubmitTime,
                    InventoryStockTaking_SubmitUser = x.SubmitUser,
                    InventoryStockTaking_ApproveTime = x.ApproveTime,
                    InventoryStockTaking_ApproveUser = x.ApproveUser,
                    InventoryStockTaking_CloseTime = x.CloseTime,
                    InventoryStockTaking_CloseUser = x.CloseUser,
                    InventoryStockTaking_Status = x.Status,
                    InventoryStockTaking_Memo = x.Memo,
                    InventoryStockTaking_CreateTime = x.CreateTime,
                    InventoryStockTaking_UpdateTime = x.UpdateTime,
                    InventoryStockTaking_CreateBy = x.CreateBy,
                    InventoryStockTaking_UpdateBy = x.UpdateBy,
                    Mode = x.Mode,
                })
                .OrderByDescending(x => x.InventoryStockTaking_CreateTime);
            return query;
        }

        
    }
    public class InventoryStockTaking_View: InventoryStockTaking
    {
        
        public string InventoryStockTaking_ErpID { get; set; }
        public string InventoryStockTaking_ErpDocNo { get; set; }
        public string InventoryStockTaking_DocNo { get; set; }
        public InventoryStockTakingDimensionEnum? InventoryStockTaking_Dimension { get; set; }
        public string InventoryStockTaking_Wh { get; set; }
        public DateTime? InventoryStockTaking_SubmitTime { get; set; }
        public string InventoryStockTaking_SubmitUser { get; set; }
        public DateTime? InventoryStockTaking_ApproveTime { get; set; }
        public string InventoryStockTaking_ApproveUser { get; set; }
        public DateTime? InventoryStockTaking_CloseTime { get; set; }
        public string InventoryStockTaking_CloseUser { get; set; }
        public InventoryStockTakingStatusEnum? InventoryStockTaking_Status { get; set; }
        public string InventoryStockTaking_Memo { get; set; }
        public DateTime? InventoryStockTaking_CreateTime { get; set; }
        public DateTime? InventoryStockTaking_UpdateTime { get; set; }
        public string InventoryStockTaking_CreateBy { get; set; }
        public string InventoryStockTaking_UpdateBy { get; set; }

    }

}