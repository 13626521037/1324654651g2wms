using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustListVM : BasePagedListVM<InventoryAdjust_View, InventoryAdjustSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryAdjust","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryAdjust","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryAdjust","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryAdjust", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryAdjust", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryAdjust","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryAdjust","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryAdjust","InventoryAdjustExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryAdjust_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjust_View>>{
                
                this.MakeGridHeader(x => x.InventoryAdjust_DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryAdjust_StockTaking, width: 120).SetTitle("来源盘点单"),
                this.MakeGridHeader(x => x.InventoryAdjust_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryAdjust_CreateTime, width: 145).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryAdjust_CreateBy, width: 120).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 100)
            };
        }

        

        public override IOrderedQueryable<InventoryAdjust_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryAdjust>()
                
                .CheckEqual(Searcher.StockTakingId, x=>x.StockTakingId)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventoryAdjust_View
                {
				    ID = x.ID,
                    
                    InventoryAdjust_StockTaking = x.StockTaking.DocNo,
                    InventoryAdjust_DocNo = x.DocNo,
                    InventoryAdjust_Memo = x.Memo,
                    InventoryAdjust_CreateTime = x.CreateTime,
                    InventoryAdjust_UpdateTime = x.UpdateTime,
                    InventoryAdjust_CreateBy = x.CreateBy,
                    InventoryAdjust_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.InventoryAdjust_CreateTime);
            return query;
        }

        
    }
    public class InventoryAdjust_View: InventoryAdjust
    {
        
        public string InventoryAdjust_StockTaking { get; set; }
        public string InventoryAdjust_DocNo { get; set; }
        public string InventoryAdjust_Memo { get; set; }
        public DateTime? InventoryAdjust_CreateTime { get; set; }
        public DateTime? InventoryAdjust_UpdateTime { get; set; }
        public string InventoryAdjust_CreateBy { get; set; }
        public string InventoryAdjust_UpdateBy { get; set; }

    }

}