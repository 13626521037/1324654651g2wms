using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;

namespace WMS.ViewModel.BaseData.BaseInventoryLogVMs
{
    public partial class BaseInventoryLogListVM : BasePagedListVM<BaseInventoryLog_View, BaseInventoryLogSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("BaseInventoryLog","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("BaseInventoryLog","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseInventoryLog","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("BaseInventoryLog", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("BaseInventoryLog", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("BaseInventoryLog","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("BaseInventoryLog","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseInventoryLog","BaseInventoryLogExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }


        protected override IEnumerable<IGridColumn<BaseInventoryLog_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseInventoryLog_View>>{

                this.MakeGridHeader(x => x.BaseInventoryLog_OperationType, width: 160).SetTitle(@Localizer["Page.业务操作"].Value),
                this.MakeGridHeader(x => x.BaseInventoryLog_DocNo, width: 120).SetTitle(@Localizer["Page.业务单据"].Value),
                //this.MakeGridHeaderParent("来源库存信息").SetChildren(
                    this.MakeGridHeader(x => x.BaseInventoryLog_SourceInventory, width: 240).SetTitle("<div style='color:#f18511'>来源库存ID</div>").SetHide(),
                    this.MakeGridHeader(x => x.BaseInventoryLog_SourceInventory_SerialNumber, width: 240).SetTitle("<div style='color:#f18511'>[来源] 库存信息</div>"),
                    this.MakeGridHeader(x => x.BaseInventoryLog_SourceInventory_LocationCode, width: 130).SetTitle("<div style='color:#f18511'>[来源] 库位</div>"),
                    this.MakeGridHeader(x => x.BaseInventoryLog_SourceDiffQty, width: 110).SetTitle("<div style='color:#f18511'>[来源] 数量变化</div>"),
                //),
                //this.MakeGridHeaderParent("目标库存信息").SetChildren(
                    this.MakeGridHeader(x => x.BaseInventoryLog_TargetInventory, width: 240).SetTitle("<div style='color:#009688'>目标库存ID</div>").SetHide(),
                    this.MakeGridHeader(x => x.BaseInventoryLog_TargetInventory_SerialNumber, width: 240).SetTitle("<div style='color:#009688'>[目标] 库存信息</div>"),
                    this.MakeGridHeader(x => x.BaseInventoryLog_TargetInventory_LocationCode, width: 130).SetTitle("<div style='color:#009688'>[目标] 库位</div>"),
                    this.MakeGridHeader(x => x.BaseInventoryLog_TargetDiffQty, width: 110).SetTitle("<div style='color:#009688'>[目标] 数量变化</div>"),
                //),
                this.MakeGridHeader(x => x.BaseInventoryLog_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseInventoryLog_CreateTime, width: 135).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.BaseInventoryLog_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseInventoryLog_CreateBy, width: 80).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.BaseInventoryLog_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 80)
            };
        }



        public override IOrderedQueryable<BaseInventoryLog_View> GetSearchQuery()
        {
            var query = DC.Set<BaseInventoryLog>()

                .CheckEqual(Searcher.OperationType, x => x.OperationType)
                .Where(x => x.SourceInventory.SerialNumber.Contains(Searcher.Sn) || x.TargetInventory.SerialNumber.Contains(Searcher.Sn))
                .CheckContain(Searcher.DocNo, x => x.DocNo)
                //.CheckEqual(Searcher.Qty, x=>x.Qty)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x => x.UpdateBy)
                .Select(x => new BaseInventoryLog_View
                {
                    ID = x.ID,

                    BaseInventoryLog_OperationType = x.OperationType,
                    BaseInventoryLog_DocNo = x.DocNo,
                    BaseInventoryLog_SourceInventory = x.SourceInventoryId.ToString(),
                    BaseInventoryLog_SourceInventory_SerialNumber = Common.AddInventoryDialog(x.SourceInventory),
                    BaseInventoryLog_SourceInventory_LocationCode = Common.AddLocationDialog(x.SourceInventory.WhLocation),
                    BaseInventoryLog_TargetInventory = x.TargetInventoryId.ToString(),
                    BaseInventoryLog_TargetInventory_SerialNumber = Common.AddInventoryDialog(x.TargetInventory),
                    BaseInventoryLog_TargetInventory_LocationCode = Common.AddLocationDialog(x.TargetInventory.WhLocation),
                    BaseInventoryLog_SourceDiffQty = x.SourceDiffQty.TrimZero(),
                    BaseInventoryLog_TargetDiffQty = x.TargetDiffQty.TrimZero(),
                    BaseInventoryLog_Memo = x.Memo,
                    BaseInventoryLog_CreateTime = x.CreateTime,
                    //BaseInventoryLog_UpdateTime = x.UpdateTime,
                    BaseInventoryLog_CreateBy = x.CreateBy,
                    //BaseInventoryLog_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }


    }
    public class BaseInventoryLog_View : BaseInventoryLog
    {

        public OperationTypeEnum? BaseInventoryLog_OperationType { get; set; }
        public string BaseInventoryLog_DocNo { get; set; }
        public string BaseInventoryLog_SourceInventory { get; set; }
        public string BaseInventoryLog_SourceInventory_LocationCode { get; set; }
        public string BaseInventoryLog_SourceInventory_SerialNumber { get; set; }
        public string BaseInventoryLog_TargetInventory { get; set; }
        public string BaseInventoryLog_TargetInventory_LocationCode { get; set; }
        public string BaseInventoryLog_TargetInventory_SerialNumber { get; set; }
        public decimal? BaseInventoryLog_SourceDiffQty { get; set; }
        public decimal? BaseInventoryLog_TargetDiffQty { get; set; }
        public string BaseInventoryLog_Memo { get; set; }
        public DateTime? BaseInventoryLog_CreateTime { get; set; }
        public DateTime? BaseInventoryLog_UpdateTime { get; set; }
        public string BaseInventoryLog_CreateBy { get; set; }
        public string BaseInventoryLog_UpdateBy { get; set; }

    }

}