using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;

namespace WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs
{
    public partial class BaseDocInventoryRelationListVM : BasePagedListVM<BaseDocInventoryRelation_View, BaseDocInventoryRelationSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("BaseDocInventoryRelation","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                this.MakeAction("BaseDocInventoryRelation","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("BaseDocInventoryRelation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("BaseDocInventoryRelation", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeStandardAction("BaseDocInventoryRelation", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "BaseData", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                this.MakeAction("BaseDocInventoryRelation","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                this.MakeAction("BaseDocInventoryRelation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"BaseData",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("BaseDocInventoryRelation","BaseDocInventoryRelationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"BaseData").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<BaseDocInventoryRelation_View>> InitGridHeader()
        {
            return new List<GridColumn<BaseDocInventoryRelation_View>>{
                
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_DocType).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_Inventory).SetTitle(@Localizer["Page.库存信息"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_BusinessId).SetTitle(@Localizer["Page.业务实体ID"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_Qty).SetTitle(@Localizer["Page.数量"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.BaseDocInventoryRelation_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<BaseDocInventoryRelation_View> GetSearchQuery()
        {
            var query = DC.Set<BaseDocInventoryRelation>()
                
                .CheckEqual(Searcher.DocType, x=>x.DocType)
                .CheckEqual(Searcher.InventoryId, x=>x.InventoryId)
                .CheckEqual(Searcher.BusinessId, x=>x.BusinessId)
                .CheckEqual(Searcher.Qty, x=>x.Qty)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new BaseDocInventoryRelation_View
                {
				    ID = x.ID,
                    
                    BaseDocInventoryRelation_DocType = x.DocType,
                    BaseDocInventoryRelation_Inventory = x.Inventory.BatchNumber,
                    BaseDocInventoryRelation_BusinessId = x.BusinessId,
                    BaseDocInventoryRelation_Qty = x.Qty.TrimZero(),
                    BaseDocInventoryRelation_Memo = x.Memo,
                    BaseDocInventoryRelation_CreateTime = x.CreateTime,
                    BaseDocInventoryRelation_UpdateTime = x.UpdateTime,
                    BaseDocInventoryRelation_CreateBy = x.CreateBy,
                    BaseDocInventoryRelation_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class BaseDocInventoryRelation_View: BaseDocInventoryRelation
    {
        
        public DocTypeEnum? BaseDocInventoryRelation_DocType { get; set; }
        public string BaseDocInventoryRelation_Inventory { get; set; }
        public Guid? BaseDocInventoryRelation_BusinessId { get; set; }
        public decimal? BaseDocInventoryRelation_Qty { get; set; }
        public string BaseDocInventoryRelation_Memo { get; set; }
        public DateTime? BaseDocInventoryRelation_CreateTime { get; set; }
        public DateTime? BaseDocInventoryRelation_UpdateTime { get; set; }
        public string BaseDocInventoryRelation_CreateBy { get; set; }
        public string BaseDocInventoryRelation_UpdateBy { get; set; }

    }

}