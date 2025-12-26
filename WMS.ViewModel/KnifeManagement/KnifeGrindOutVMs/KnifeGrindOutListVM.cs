using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs
{
    public partial class KnifeGrindOutListVM : BasePagedListVM<KnifeGrindOut_View, KnifeGrindOutSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeGrindOut","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeGrindOut","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeGrindOut","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeGrindOut", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeGrindOut", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeGrindOut","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeGrindOut","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeGrindOut","KnifeGrindOutExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                //this.MakeAction("KnifeGrindOut","BatchCreate","批量生单","修磨出库批量生单",GridActionParameterTypesEnum.NoId,"KnifeManagement",800).SetShowInRow(false).SetShowDialog(true).SetHideOnToolBar(false),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindOut_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindOut_View>>{
                
                this.MakeGridHeader(x => x.KnifeGrindOut_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindOut_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeGrindOut_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeGrindOut_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindOut_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeGrindOut_U9PODocNo).SetTitle("采购单号"),
                this.MakeGridHeader(x => x.KnifeGrindOut_WareHouse).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeGrindOut_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindOut_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindOut_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeGrindOut_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeGrindOut_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindOut>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeGrindOut_View
                {
				    ID = x.ID,
                    
                    KnifeGrindOut_DocNo = x.DocNo,
                    KnifeGrindOut_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeGrindOut_Status = x.Status,
                    KnifeGrindOut_ApprovedTime = x.ApprovedTime,
                    KnifeGrindOut_OrderNum = x.KnifeGrindOutLine_KnifeGrindOut.Count.ToString(),
                    KnifeGrindOut_U9PODocNo = x.U9PODocNo,
                    KnifeGrindOut_WareHouse = x.WareHouse.Name,
                    KnifeGrindOut_CreateTime = x.CreateTime,
                    KnifeGrindOut_UpdateTime = x.UpdateTime,
                    KnifeGrindOut_CreateBy = x.CreateBy,
                    KnifeGrindOut_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindOut_View: KnifeGrindOut
    {
        
        public string KnifeGrindOut_DocNo { get; set; }
        public string KnifeGrindOut_HandledBy { get; set; }
        public KnifeOrderStatusEnum? KnifeGrindOut_Status { get; set; }
        public DateTime? KnifeGrindOut_ApprovedTime { get; set; }
        public string KnifeGrindOut_OrderNum { get; set; }
        public string KnifeGrindOut_U9PODocNo { get; set; }
        public string KnifeGrindOut_WareHouse { get; set; }
        public DateTime? KnifeGrindOut_CreateTime { get; set; }
        public DateTime? KnifeGrindOut_UpdateTime { get; set; }
        public string KnifeGrindOut_CreateBy { get; set; }
        public string KnifeGrindOut_UpdateBy { get; set; }

    }

}