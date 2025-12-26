using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeListVM : BasePagedListVM<InventoryUnfreeze_View, InventoryUnfreezeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryUnfreeze","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryUnfreeze","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryUnfreeze","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryUnfreeze", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryUnfreeze", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryUnfreeze","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryUnfreeze","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryUnfreeze","InventoryUnfreezeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryUnfreeze_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryUnfreeze_View>>{
                
                this.MakeGridHeader(x => x.InventoryUnfreeze_DocNo).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryUnfreeze_Reason).SetTitle(@Localizer["Page.解冻原因"].Value),
                this.MakeGridHeader(x => x.InventoryUnfreeze_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryUnfreeze_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryUnfreeze_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryUnfreeze_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryUnfreeze>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                .Select(x => new InventoryUnfreeze_View
                {
				    ID = x.ID,
                    
                    InventoryUnfreeze_DocNo = x.DocNo,
                    InventoryUnfreeze_Reason = x.Reason,
                    InventoryUnfreeze_Memo = x.Memo,
                    InventoryUnfreeze_CreateTime = x.CreateTime,
                    InventoryUnfreeze_CreateBy = x.CreateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class InventoryUnfreeze_View: InventoryUnfreeze
    {
        
        public string InventoryUnfreeze_DocNo { get; set; }
        public string InventoryUnfreeze_Reason { get; set; }
        public string InventoryUnfreeze_Memo { get; set; }
        public DateTime? InventoryUnfreeze_CreateTime { get; set; }
        public string InventoryUnfreeze_CreateBy { get; set; }

    }

}