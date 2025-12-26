using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationListVM : BasePagedListVM<InventoryMoveLocation_View, InventoryMoveLocationSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryMoveLocation","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryMoveLocation","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryMoveLocation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryMoveLocation", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryMoveLocation", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryMoveLocation","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryMoveLocation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryMoveLocation","InventoryMoveLocationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryMoveLocation_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryMoveLocation_View>>{
                
                this.MakeGridHeader(x => x.InventoryMoveLocation_DocNo).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryMoveLocation_InWhLocation).SetTitle(@Localizer["Page.入库库位"].Value),
                this.MakeGridHeader(x => x.InventoryMoveLocation_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryMoveLocation_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                //this.MakeGridHeader(x => x.InventoryMoveLocation_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.InventoryMoveLocation_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.InventoryMoveLocation_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryMoveLocation_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryMoveLocation>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.InWhLocationId, x=>x.InWhLocationId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryMoveLocation_View
                {
				    ID = x.ID,
                    
                    InventoryMoveLocation_DocNo = x.DocNo,
                    InventoryMoveLocation_InWhLocation = x.InWhLocation.Code,
                    InventoryMoveLocation_Memo = x.Memo,
                    InventoryMoveLocation_CreateTime = x.CreateTime,
                    InventoryMoveLocation_UpdateTime = x.UpdateTime,
                    InventoryMoveLocation_CreateBy = x.CreateBy,
                    InventoryMoveLocation_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.InventoryMoveLocation_CreateTime);
            return query;
        }

        
    }
    public class InventoryMoveLocation_View: InventoryMoveLocation
    {
        
        public string InventoryMoveLocation_DocNo { get; set; }
        public string InventoryMoveLocation_InWhLocation { get; set; }
        public string InventoryMoveLocation_Memo { get; set; }
        public DateTime? InventoryMoveLocation_CreateTime { get; set; }
        public DateTime? InventoryMoveLocation_UpdateTime { get; set; }
        public string InventoryMoveLocation_CreateBy { get; set; }
        public string InventoryMoveLocation_UpdateBy { get; set; }

    }

}