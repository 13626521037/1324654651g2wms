using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementListVM : BasePagedListVM<InventoryOtherReceivement_View, InventoryOtherReceivementSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryOtherReceivement","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryOtherReceivement","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryOtherReceivement","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("InventoryOtherReceivement", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryOtherReceivement", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryOtherReceivement","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryOtherReceivement","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryOtherReceivement","InventoryOtherReceivementExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryOtherReceivement_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherReceivement_View>>{
                
                this.MakeGridHeader(x => x.InventoryOtherReceivement_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.ID, width: 240).SetHide(),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_IsScrap, width: 100).SetTitle(@Localizer["Page.是否报废"].Value),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryOtherReceivement_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryOtherReceivement_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherReceivement>()
                
                .CheckContain(Searcher.ErpID, x=>x.ErpID)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.IsScrap, x=>x.IsScrap)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new InventoryOtherReceivement_View
                {
				    ID = x.ID,
                    
                    InventoryOtherReceivement_ErpID = x.ErpID,
                    InventoryOtherReceivement_BusinessDate = x.BusinessDate,
                    InventoryOtherReceivement_DocNo = x.DocNo,
                    InventoryOtherReceivement_IsScrap = x.IsScrap,
                    InventoryOtherReceivement_Memo = x.Memo,
                    InventoryOtherReceivement_CreateTime = x.CreateTime,
                    InventoryOtherReceivement_UpdateTime = x.UpdateTime,
                    InventoryOtherReceivement_CreateBy = x.CreateBy,
                    InventoryOtherReceivement_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryOtherReceivement_View: InventoryOtherReceivement
    {
        
        public string InventoryOtherReceivement_ErpID { get; set; }
        public DateTime? InventoryOtherReceivement_BusinessDate { get; set; }
        public string InventoryOtherReceivement_DocNo { get; set; }
        public bool? InventoryOtherReceivement_IsScrap { get; set; }
        public string InventoryOtherReceivement_Memo { get; set; }
        public DateTime? InventoryOtherReceivement_CreateTime { get; set; }
        public DateTime? InventoryOtherReceivement_UpdateTime { get; set; }
        public string InventoryOtherReceivement_CreateBy { get; set; }
        public string InventoryOtherReceivement_UpdateBy { get; set; }

    }

}