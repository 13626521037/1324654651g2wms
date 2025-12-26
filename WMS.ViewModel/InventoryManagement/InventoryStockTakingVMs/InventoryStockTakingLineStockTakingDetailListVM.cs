
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLineVMs
{
    public partial class InventoryStockTakingLineStockTakingDetailListVM : BasePagedListVM<InventoryStockTakingLine_DetailView, InventoryStockTakingLineDetailSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryStockTaking", "ExportDetail", "导出盘点明细", "导出盘点明细", GridActionParameterTypesEnum.MultiIdWithNull, "InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 
        protected override IEnumerable<IGridColumn<InventoryStockTakingLine_DetailView>> InitGridHeader()
        {
            return new List<GridColumn<InventoryStockTakingLine_DetailView>>{
                
                this.MakeGridHeader(x => x.ID).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.ID"].Value).SetHide(),
                //this.MakeGridHeader(x => x.DocLineNo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Sys.RowIndex"].Value),
                //this.MakeGridHeader(x => x.InventoryId).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库存信息"].Value),
                this.MakeGridHeader(x => x.ItemCode, width: 95).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Seiban, width: 100).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.SerialNumber, width: 150).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.序列号"].Value),
                this.MakeGridHeader(x => x.ScanBarCode, width: 230).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.扫描条码"].Value),
                this.MakeGridHeader(x => x.LocationCode, width: 150).SetEditType(EditTypeEnum.Text),
                this.MakeGridHeader(x => x.Qty, width: 80).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.库存数量"].Value),
                this.MakeGridHeader(x => x.StockTakingQty, width: 80).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.已盘数量"].Value),
                this.MakeGridHeader(x => x.DiffQty, width: 80).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盈亏数量"].Value),
                this.MakeGridHeader(x => x.GainLossStatus, width: 75).SetEditType(EditTypeEnum.Text).SetTitle("盈亏").SetFormat(
                    (a, b) => 
                    {
                        string backcolor = "";
                        string fontcolor = "#ffffff";
                        switch (a.GainLossStatus)
                        {
                            case GainLossStatusEnum.NotStart:
                                backcolor = "#ccc";
                                //fontcolor = "#000000";
                                break;
                            case GainLossStatusEnum.Equal:
                                backcolor = "#2ee12c";
                                break;
                            case GainLossStatusEnum.Loss:
                                backcolor = "#e13d2c";
                                break;
                            case GainLossStatusEnum.Gain:
                                backcolor = "#dee12c";
                                break;
                        }
                        return $"<div style='background-color:{backcolor};color:{fontcolor}'>" + a.GainLossStatus.GetEnumDisplayName() + "</div>";
                    }),
                //.SetBackGroundFunc
                //(
                //    (a) =>
                //    {
                //        //前端未生效，待排查
                //        string color = "";
                //        switch (a.GainLossStatus)
                //        {
                //            case GainLossStatusEnum.NotStart:
                //                color = "#ffffff";
                //                break;
                //            case GainLossStatusEnum.Equal:
                //                color = "#2ee12c";
                //                break;
                //            case GainLossStatusEnum.Loss:
                //                color = "#e13d2c";
                //                break;
                //            case GainLossStatusEnum.Gain:
                //                color = "#dee12c";
                //                break;
                //        }
                //        return color;
                //    }
                //),
                this.MakeGridHeader(x => x.IsNew, width: 80).SetEditType(EditTypeEnum.Text).SetTitle("新条码"),
                this.MakeGridHeader(x => x.IsKnifeLedger, width: 80).SetEditType(EditTypeEnum.Text).SetTitle("刀具台账"),
                this.MakeGridHeader(x => x.OperatingUser, width: 100).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["Page.盘点人"].Value),
                //this.MakeGridHeader(x => x.Memo).SetEditType(EditTypeEnum.Text).SetTitle(@Localizer["_Admin.Remark"].Value),

            };
        }

        
        public override IOrderedQueryable<InventoryStockTakingLine_DetailView> GetSearchQuery()
        {
                
            var id = (Guid?)Searcher.StockTakingId.ConvertValue(typeof(Guid?));
            if (id == null)
                return new List<InventoryStockTakingLine_DetailView>().AsQueryable().OrderBy(x => x.ID);
            var query = DC.Set<InventoryStockTakingLine>()
                .Where(x => id == x.StockTakingId)
                .CheckEqual(Searcher.GainLossStatus, x => x.GainLossStatus)
                .Select(x => new InventoryStockTakingLine_DetailView
                {
                     ID = x.ID,
                     DocLineNo = x.DocLineNo,
                     ItemCode = x.ItemMaster.Code,
                     SerialNumber = x.SerialNumber,
                     ScanBarCode = x.ScanBarCode,
                     Seiban = x.Seiban,
                     Qty = x.Qty.TrimZero(),
                     LocationCode = x.Inventory.WhLocation.Code ?? x.Knife.WhLocation.Code ?? x.Location.Code,
                     StockTakingQty = x.StockTakingQty.TrimZero(),
                     DiffQty = x.DiffQty.TrimZero(),
                     GainLossStatus = x.GainLossStatus,
                     IsNew = x.IsNew,
                     IsKnifeLedger = x.IsKnifeLedger,
                     OperatingUser = x.OperatingUser,
                })
                .OrderBy(x => x.DocLineNo);
            return query;
        }

    }

    public partial class InventoryStockTakingLineDetailSearcher : BaseSearcher
    {
        
        [Display(Name = "_Model._InventoryStockTakingLine._StockTaking")]
        public string StockTakingId { get; set; }

        [Display(Name = "_Model._InventoryStockTakingLine._GainLossStatus")]
        public GainLossStatusEnum? GainLossStatus { get; set; }
    }

    public class InventoryStockTakingLine_DetailView: InventoryStockTakingLine
    {
        [Display(Name = "料号")]
        public string ItemCode { get; set; }

        [Display(Name = "原/实盘库位")]
        public string LocationCode { get; set; }
    }
}

