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

namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitListVM : BasePagedListVM<InventorySplit_View, InventorySplitSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventorySplit","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventorySplit","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventorySplit","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventorySplit", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventorySplit", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventorySplit","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventorySplit","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventorySplit","InventorySplitExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventorySplit_View>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplit_View>>{
                
                this.MakeGridHeader(x => x.InventorySplit_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventorySplit_OldInv_ItemMaster_Code, width: 85).SetTitle("料号"),
                this.MakeGridHeader(x => x.InventorySplit_OldInv_WhLocation_Code, width: 120).SetTitle("库位"),
                this.MakeGridHeader(x => x.InventorySplit_OldInv_BatchNumber, width: 130).SetTitle("批号"),
                this.MakeGridHeader(x => x.InventorySplit_OldInv_Seiban, width: 100).SetTitle("番号"),
                this.MakeGridHeader(x => x.InventorySplit_OldInv_SerialNumber).SetTitle("原序列号"),
                this.MakeGridHeader(x => x.InventorySplit_NewInv_SerialNumber).SetTitle("新序列号"),
                this.MakeGridHeader(x => x.InventorySplit_OrigQty, width: 100).SetTitle(@Localizer["Page.原库存数量"].Value),
                this.MakeGridHeader(x => x.InventorySplit_SplitQty, width: 100).SetTitle(@Localizer["Page.拆分数量"].Value),
                this.MakeGridHeader(x => x.InventorySplit_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventorySplit_CreateTime, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventorySplit_CreateBy, width: 75).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 80)
            };
        }

        

        public override IOrderedQueryable<InventorySplit_View> GetSearchQuery()
        {
            var query = DC.Set<InventorySplit>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventorySplit_View
                {
				    ID = x.ID,
                    
                    InventorySplit_DocNo = x.DocNo,
                    InventorySplit_OldInv = x.OldInv.BatchNumber,
                    InventorySplit_NewInv = x.NewInv.BatchNumber,
                    InventorySplit_OrigQty = x.OrigQty.TrimZero(),
                    InventorySplit_SplitQty = x.SplitQty.TrimZero(),
                    InventorySplit_Memo = x.Memo,
                    InventorySplit_CreateTime = x.CreateTime,
                    InventorySplit_CreateBy = x.CreateBy,
                    InventorySplit_OldInv_ItemMaster_Code = x.OldInv.ItemMaster.Code,
                    InventorySplit_OldInv_WhLocation_Code = x.OldInv.WhLocation.Code,
                    InventorySplit_OldInv_BatchNumber = x.OldInv.BatchNumber,
                    InventorySplit_OldInv_Seiban = x.OldInv.Seiban,
                    InventorySplit_OldInv_SerialNumber = Common.AddInventoryDialog(x.OldInv),
                    InventorySplit_NewInv_SerialNumber = Common.AddInventoryDialog(x.NewInv),
                    CreateTime = x.CreateTime,
                })
                .OrderByDescending(x => x.CreateTime);
            return query;
        }

        
    }
    public class InventorySplit_View: InventorySplit
    {
        
        public string InventorySplit_DocNo { get; set; }
        public string InventorySplit_OldInv { get; set; }
        public string InventorySplit_NewInv { get; set; }
        public decimal? InventorySplit_OrigQty { get; set; }
        public decimal? InventorySplit_SplitQty { get; set; }
        public string InventorySplit_Memo { get; set; }
        public DateTime? InventorySplit_CreateTime { get; set; }
        public string InventorySplit_CreateBy { get; set; }

        public string InventorySplit_OldInv_ItemMaster_Code { get; set; }
        public string InventorySplit_OldInv_WhLocation_Code { get; set; }
        public string InventorySplit_OldInv_BatchNumber { get; set; }
        public string InventorySplit_OldInv_Seiban { get; set; }
        public string InventorySplit_OldInv_SerialNumber { get; set; }
        public string InventorySplit_NewInv_SerialNumber { get; set; }

    }

}