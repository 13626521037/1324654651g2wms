using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeListVM : BasePagedListVM<InventoryFreeze_View, InventoryFreezeSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("InventoryFreeze","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("InventoryFreeze","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryFreeze","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1200).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("InventoryFreeze", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("InventoryFreeze", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("InventoryFreeze","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("InventoryFreeze","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryFreeze","InventoryFreezeExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryFreeze_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryFreeze_View>>{
                
                this.MakeGridHeader(x => x.InventoryFreeze_DocNo).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryFreeze_Reason).SetTitle(@Localizer["Page.冻结原因"].Value),
                this.MakeGridHeader(x => x.InventoryFreeze_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryFreeze_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryFreeze_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryFreeze_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryFreeze>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventoryFreeze_View
                {
				    ID = x.ID,
                    
                    InventoryFreeze_DocNo = x.DocNo,
                    InventoryFreeze_Reason = x.Reason,
                    InventoryFreeze_Memo = x.Memo,
                    InventoryFreeze_CreateTime = x.CreateTime,
                    InventoryFreeze_CreateBy = x.CreateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class InventoryFreeze_View: InventoryFreeze
    {
        
        public string InventoryFreeze_DocNo { get; set; }
        public string InventoryFreeze_Reason { get; set; }
        public string InventoryFreeze_Memo { get; set; }
        public DateTime? InventoryFreeze_CreateTime { get; set; }
        public string InventoryFreeze_CreateBy { get; set; }

    }

}