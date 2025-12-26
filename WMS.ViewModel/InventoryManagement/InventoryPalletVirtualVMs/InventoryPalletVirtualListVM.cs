using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Util;

namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualListVM : BasePagedListVM<InventoryPalletVirtual_View, InventoryPalletVirtualSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryPalletVirtual","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryPalletVirtual","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryPalletVirtual","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryPalletVirtual", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryPalletVirtual", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryPalletVirtual","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryPalletVirtual","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryPalletVirtual","InventoryPalletVirtualExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }

        protected override IEnumerable<IGridColumn<InventoryPalletVirtual_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryPalletVirtual_View>>{
                this.MakeGridHeader(x => x.ID, width: 245).SetTitle("ID").SetHide(),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_Code, width: 200).SetTitle(@Localizer["Page.托盘码"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_Status, width: 90).SetTitle(@Localizer["Page.托盘状态"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_Location, width: 250).SetTitle(@Localizer["Page.库位"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_SysVersion).SetTitle(@Localizer["Page.事务版本"].Value).SetHide(),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_CreateTime, width: 135).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_CreateBy, width: 110).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_UpdateTime, width: 135).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.InventoryPalletVirtual_UpdateBy, width: 110).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<InventoryPalletVirtual_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryPalletVirtual>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.LocationId, x=>x.LocationId)
                .CheckEqual(Searcher.SysVersion, x=>x.SysVersion)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryPalletVirtual_View
                {
				    ID = x.ID,
                    InventoryPalletVirtual_Code = x.Code,
                    InventoryPalletVirtual_Status = x.Status,
                    //InventoryPalletVirtual_Location = $"[{x.Location.WhArea.WareHouse.Code}] [{x.Location.WhArea.Code}] {x.Location.Code}",
                    InventoryPalletVirtual_Location = Common.AddLocationDialog(x.Location),
                    InventoryPalletVirtual_SysVersion = x.SysVersion,
                    InventoryPalletVirtual_Memo = x.Memo,
                    InventoryPalletVirtual_CreateTime = x.CreateTime,
                    InventoryPalletVirtual_UpdateTime = x.UpdateTime,
                    InventoryPalletVirtual_CreateBy = x.CreateBy,
                    InventoryPalletVirtual_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }
    }
    public class InventoryPalletVirtual_View: InventoryPalletVirtual
    {
        public string InventoryPalletVirtual_Code { get; set; }
        public FrozenStatusEnum? InventoryPalletVirtual_Status { get; set; }
        public string InventoryPalletVirtual_Location { get; set; }
        public int? InventoryPalletVirtual_SysVersion { get; set; }
        public string InventoryPalletVirtual_Memo { get; set; }
        public DateTime? InventoryPalletVirtual_CreateTime { get; set; }
        public DateTime? InventoryPalletVirtual_UpdateTime { get; set; }
        public string InventoryPalletVirtual_CreateBy { get; set; }
        public string InventoryPalletVirtual_UpdateBy { get; set; }
    }
}