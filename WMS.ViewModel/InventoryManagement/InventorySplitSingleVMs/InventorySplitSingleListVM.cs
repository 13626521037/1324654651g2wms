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

namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleListVM : BasePagedListVM<InventorySplitSingle_View, InventorySplitSingleSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventorySplitSingle","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventorySplitSingle","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventorySplitSingle","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventorySplitSingle", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventorySplitSingle", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventorySplitSingle","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventorySplitSingle","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventorySplitSingle","InventorySplitSingleExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventorySplitSingle_View>> InitGridHeader()
        {
            return new List<GridColumn<InventorySplitSingle_View>>{
                
                this.MakeGridHeader(x => x.InventorySplitSingle_DocNo).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventorySplitSingle_OriginalInv).SetTitle(@Localizer["Page.原库存信息"].Value),
                this.MakeGridHeader(x => x.InventorySplitSingle_OriginalQty).SetTitle(@Localizer["Page.原数量"].Value),
                this.MakeGridHeader(x => x.InventorySplitSingle_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventorySplitSingle_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventorySplitSingle_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventorySplitSingle_View> GetSearchQuery()
        {
            var query = DC.Set<InventorySplitSingle>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                //.CheckEqual(Searcher.OriginalInvId, x=>x.OriginalInvId)
                .CheckEqual(Searcher.OriginalQty, x=>x.OriginalQty)
                .CheckEqual(Searcher.OriginalInvSN, x=>x.OriginalInv.SerialNumber)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventorySplitSingle_View
                {
				    ID = x.ID,
                    
                    InventorySplitSingle_DocNo = x.DocNo,
                    InventorySplitSingle_OriginalInv = x.OriginalInv.BatchNumber,
                    InventorySplitSingle_OriginalQty = x.OriginalQty.TrimZero(),
                    InventorySplitSingle_Memo = x.Memo,
                    InventorySplitSingle_CreateTime = x.CreateTime,
                    InventorySplitSingle_CreateBy = x.CreateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventorySplitSingle_View: InventorySplitSingle
    {
        
        public string InventorySplitSingle_DocNo { get; set; }
        public string InventorySplitSingle_OriginalInv { get; set; }
        public decimal? InventorySplitSingle_OriginalQty { get; set; }
        public string InventorySplitSingle_Memo { get; set; }
        public DateTime? InventorySplitSingle_CreateTime { get; set; }
        public string InventorySplitSingle_CreateBy { get; set; }

    }

}