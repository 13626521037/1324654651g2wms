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

namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs
{
    public partial class InventoryErpDiffListVM : BasePagedListVM<InventoryErpDiff_View, InventoryErpDiffSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryErpDiff","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryErpDiff","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                //this.MakeStandardAction("InventoryErpDiff", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryErpDiff", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryErpDiff","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryErpDiff","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryErpDiff","Details","WMS详情",@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal").SetBindVisiableColName("IsShowDetail"),
                this.MakeAction("InventoryErpDiff", "Analysis", "对账", "对账", GridActionParameterTypesEnum.NoId, "InventoryManagement", 800, 600),
                this.MakeAction("InventoryErpDiff","InventoryErpDiffExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryErpDiff_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryErpDiff_View>>{
                
                this.MakeGridHeader(x => x.InventoryErpDiff_Wh).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryErpDiff_Item).SetTitle(@Localizer["Page.料品"].Value),
                this.MakeGridHeader(x => x.InventoryErpDiff_Seiban).SetTitle(@Localizer["Page.番号"].Value),
                this.MakeGridHeader(x => x.InventoryErpDiff_WmsQty).SetTitle(@Localizer["Page.WMS数量"].Value),
                this.MakeGridHeader(x => x.InventoryErpDiff_ErpQty).SetTitle(@Localizer["Page.ERP数量"].Value),
                this.MakeGridHeader(x => x.InventoryErpDiff_CreateTime).SetTitle("对账时间"),
                this.MakeGridHeader(x => x.InventoryErpDiff_CreateBy).SetTitle("操作人"),
                //this.MakeGridHeader(x => x.InventoryErpDiff_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryErpDiff_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => x.SyncItem),
                this.MakeGridHeader(x => "IsShowDetail").SetHide().SetFormat((a, b) =>
                {
                    if (a.InventoryErpDiff_WmsQty > 0)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryErpDiff_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryErpDiff>()
                
                .CheckEqual(Searcher.WhId, x=>x.WhId)
                .CheckContain(Searcher.ItemCode, x=>x.Item.Code)
                .CheckContain(Searcher.Seiban, x=>x.Seiban)
                .CheckEqual(Searcher.WmsQty, x=>x.WmsQty)
                .CheckEqual(Searcher.ErpQty, x=>x.ErpQty)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryErpDiff_View
                {
				    ID = x.ID,
                    SyncItem = x.SyncItem,
                    InventoryErpDiff_Wh = x.Wh.Code,
                    InventoryErpDiff_Item = x.Item == null ? "" :x.Item.Code,
                    InventoryErpDiff_Seiban = x.Seiban,
                    InventoryErpDiff_WmsQty = x.WmsQty.TrimZero(),
                    InventoryErpDiff_ErpQty = x.ErpQty.TrimZero(),
                    InventoryErpDiff_CreateTime = x.CreateTime,
                    InventoryErpDiff_UpdateTime = x.UpdateTime,
                    InventoryErpDiff_CreateBy = x.CreateBy,
                    InventoryErpDiff_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.InventoryErpDiff_Wh).ThenBy(x => x.InventoryErpDiff_Item);
            return query;
        }

        
    }
    public class InventoryErpDiff_View: InventoryErpDiff
    {
        
        public string InventoryErpDiff_Wh { get; set; }
        public string InventoryErpDiff_Item { get; set; }
        public string InventoryErpDiff_Seiban { get; set; }
        public decimal? InventoryErpDiff_WmsQty { get; set; }
        public decimal? InventoryErpDiff_ErpQty { get; set; }
        public DateTime? InventoryErpDiff_CreateTime { get; set; }
        public DateTime? InventoryErpDiff_UpdateTime { get; set; }
        public string InventoryErpDiff_CreateBy { get; set; }
        public string InventoryErpDiff_UpdateBy { get; set; }

    }

}