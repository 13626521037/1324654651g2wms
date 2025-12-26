using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs
{
    public partial class KnifeTransferOutListVM : BasePagedListVM<KnifeTransferOut_View, KnifeTransferOutSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeTransferOut","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeTransferOut","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeTransferOut","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeTransferOut", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeTransferOut", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeTransferOut","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeTransferOut","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeTransferOut","KnifeTransferOutExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeTransferOut_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferOut_View>>{
                
                this.MakeGridHeader(x => x.KnifeTransferOut_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(140),
                this.MakeGridHeader(x => x.KnifeTransferOut_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeTransferOut_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeTransferOut_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(140),
                this.MakeGridHeader(x => x.KnifeTransferOut_FromWh).SetTitle("调出存储地点"),
                this.MakeGridHeader(x => x.KnifeTransferOut_ToWh).SetTitle(@Localizer["Page.调入存储地点"].Value),
                this.MakeGridHeader(x => x.KnifeTransferOut_WareHouse).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeTransferOut_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeTransferOut_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferOut_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferOut_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeTransferOut_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeTransferOut_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeTransferOut>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckEqual(Searcher.ToWHId, x=>x.ToWhId)
                .CheckEqual(Searcher.FromWHId, x=>x.FromWHId)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeTransferOut_View
                {
				    ID = x.ID,
                    
                    KnifeTransferOut_DocNo = x.DocNo,
                    KnifeTransferOut_Status = x.Status,
                    KnifeTransferOut_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeTransferOut_ApprovedTime = x.ApprovedTime,
                    KnifeTransferOut_ToWh = x.ToWh.Name,
                    KnifeTransferOut_FromWh = x.FromWH.Name,
                    KnifeTransferOut_WareHouse = x.WareHouse.Name,
                    KnifeTransferOut_OrderNum = x.KnifeTransferOutLine_KnifeTransferOut.Count.ToString(),
                    KnifeTransferOut_CreateTime = x.CreateTime,
                    KnifeTransferOut_UpdateTime = x.UpdateTime,
                    KnifeTransferOut_CreateBy = x.CreateBy,
                    KnifeTransferOut_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeTransferOut_View: KnifeTransferOut
    {
        
        public string KnifeTransferOut_DocNo { get; set; }
        public KnifeOrderStatusEnum? KnifeTransferOut_Status { get; set; }
        public string KnifeTransferOut_HandledBy { get; set; }
        public DateTime? KnifeTransferOut_ApprovedTime { get; set; }
        public string KnifeTransferOut_ToWh { get; set; }
        public string KnifeTransferOut_FromWh { get; set; }
        public string KnifeTransferOut_OrderNum { get; set; }
        public string KnifeTransferOut_WareHouse { get; set; }
        public DateTime? KnifeTransferOut_CreateTime { get; set; }
        public DateTime? KnifeTransferOut_UpdateTime { get; set; }
        public string KnifeTransferOut_CreateBy { get; set; }
        public string KnifeTransferOut_UpdateBy { get; set; }

    }

}