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

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectListVM : BasePagedListVM<InventoryAdjustDirect_View, InventoryAdjustDirectSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryAdjustDirect","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryAdjustDirect","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryAdjustDirect","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryAdjustDirect", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryAdjustDirect", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryAdjustDirect","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryAdjustDirect","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryAdjustDirect","InventoryAdjustDirectExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryAdjustDirect_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryAdjustDirect_View>>{

                this.MakeGridHeader(x => x.InventoryAdjustDirect_DocNo, width: 108).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeaderParent("原库存信息").SetChildren(
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_OldInv, width: 240).SetTitle("原库存ID").SetHide(),
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_OldInv_SerialNumber, width: 240).SetTitle("库存信息"),
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_OldInv_LocationCode, width: 130).SetTitle("库位")
                ),
                this.MakeGridHeaderParent("新库存信息").SetChildren(
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_NewInv, width: 240).SetTitle("新库存ID").SetHide(),
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_NewInv_SerialNumber, width: 240).SetTitle("库存信息"),
                    this.MakeGridHeader(x => x.InventoryAdjustDirect_NewInv_LocationCode, width: 130).SetTitle("库位")
                ),
                this.MakeGridHeader(x => x.InventoryAdjustDirect_DiffQty, width: 80).SetTitle(@Localizer["Page.差异数量"].Value),
                this.MakeGridHeader(x => x.InventoryAdjustDirect_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryAdjustDirect_CreateTime, width: 135).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryAdjustDirect_CreateBy, width: 80).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryAdjustDirect_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryAdjustDirect>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventoryAdjustDirect_View
                {
				    ID = x.ID,
                    
                    InventoryAdjustDirect_DocNo = x.DocNo,
                    InventoryAdjustDirect_OldInv = x.OldInvId.ToString(),
                    InventoryAdjustDirect_OldInv_SerialNumber = Common.AddInventoryDialog(x.OldInv),
                    InventoryAdjustDirect_OldInv_LocationCode = Common.AddLocationDialog(x.OldInv.WhLocation),
                    InventoryAdjustDirect_NewInv = x.NewInvId.ToString(),
                    InventoryAdjustDirect_NewInv_SerialNumber = Common.AddInventoryDialog(x.NewInv),
                    InventoryAdjustDirect_NewInv_LocationCode = Common.AddLocationDialog(x.NewInv.WhLocation),
                    InventoryAdjustDirect_DiffQty = x.DiffQty.TrimZero(),
                    InventoryAdjustDirect_Memo = x.Memo,
                    InventoryAdjustDirect_CreateTime = x.CreateTime,
                    InventoryAdjustDirect_CreateBy = x.CreateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class InventoryAdjustDirect_View: InventoryAdjustDirect
    {
        
        public string InventoryAdjustDirect_DocNo { get; set; }
        public string InventoryAdjustDirect_OldInv { get; set; }
        public string InventoryAdjustDirect_OldInv_SerialNumber { get; set; }
        public string InventoryAdjustDirect_OldInv_LocationCode { get; set; }
        public string InventoryAdjustDirect_NewInv { get; set; }
        public string InventoryAdjustDirect_NewInv_SerialNumber { get; set; }
        public string InventoryAdjustDirect_NewInv_LocationCode { get; set; }
        public decimal? InventoryAdjustDirect_DiffQty { get; set; }
        public string InventoryAdjustDirect_Memo { get; set; }
        public DateTime? InventoryAdjustDirect_CreateTime { get; set; }
        public string InventoryAdjustDirect_CreateBy { get; set; }

    }

}