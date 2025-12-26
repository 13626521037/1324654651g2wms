using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeOperationVMs
{
    public partial class KnifeOperationListVM : BasePagedListVM<KnifeOperation_View, KnifeOperationSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("KnifeOperation","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeAction("KnifeOperation","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeOperation","KnifeOperationExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }


        protected override IEnumerable<IGridColumn<KnifeOperation_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeOperation_View>>{

                this.MakeGridHeader(x => x.KnifeOperation_Knife).SetTitle("刀具").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeOperation_DocNo).SetTitle("操作单号").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeOperation_OperationType).SetTitle("类型").SetWidth(60),
                this.MakeGridHeader(x => x.KnifeOperation_OperationTime).SetTitle("操作时间").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeOperation_OperationBy).SetTitle("操作人").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_HandledBy).SetTitle("经办人").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_UsedDays).SetTitle("本次使用天数").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeOperation_TotalUsedDays).SetTitle("累计使用天数").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeOperation_RemainingDays).SetTitle("剩余天数").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeOperation_CurrentLife).SetTitle("当前寿命").SetWidth(80),
                this.MakeGridHeader(x => x.KnifeOperation_WareHouse).SetTitle("存储地点").SetWidth(150).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_WhLocation).SetTitle("库位").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_GrindNum_Int).SetTitle("修磨次数").SetWidth(60),
                this.MakeGridHeader(x => x.KnifeOperation_IsAccident).SetTitle("意外报废").SetWidth(60),
                this.MakeGridHeader(x => x.KnifeOperation_U9SourceLineID).SetTitle("U9来源id").SetWidth(120).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_BeforeStatus).SetTitle("前状态").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeOperation_AfterStatus).SetTitle("后状态").SetWidth(80).SetAlign(GridColumnAlignEnum.Center),


                this.MakeGridHeaderAction(width: 60)
            };
        }



        public override IOrderedQueryable<KnifeOperation_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeOperation>()
                .CheckEqual(Searcher.KnifeId, x => x.KnifeId)
                .CheckContain(Searcher.DocNo, x => x.DocNo)
                .CheckEqual(Searcher.OperationType, x => x.OperationType)
                .CheckBetween(Searcher.OperationTime?.GetStartTime(), Searcher.OperationTime?.GetEndTime(), x => x.OperationTime, includeMax: false)
                .CheckEqual(Searcher.OperationById, x => x.OperationById)
                .CheckEqual(Searcher.HandledById, x => x.HandledById)
                .CheckEqual(Searcher.UsedDays, x => x.UsedDays)
                .CheckEqual(Searcher.RemainingDays, x => x.RemainingDays)
                .CheckEqual(Searcher.WhLocationId, x => x.WhLocationId)
                .CheckEqual(Searcher.WareHouseId, x => x.WhLocation.WhArea.WareHouse.ID)
                .CheckEqual(Searcher.GrindNum, x => x.GrindNum)
                .CheckContain(Searcher.U9SourceLineID, x => x.U9SourceLineID)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x => x.UpdateBy)
                .Select(x => new KnifeOperation_View
                {
                    ID = x.ID,

                    KnifeOperation_Knife = x.Knife.SerialNumber,
                    KnifeOperation_DocNo = x.DocNo,
                    KnifeOperation_OperationType = x.OperationType,
                    KnifeOperation_OperationTime = x.OperationTime,
                    KnifeOperation_OperationBy = x.OperationBy.Name,
                    KnifeOperation_HandledBy =x.Knife.HandledByName,
                    KnifeOperation_UsedDays = x.UsedDays,
                    KnifeOperation_RemainingDays = x.RemainingDays,
                    KnifeOperation_TotalUsedDays = x.TotalUsedDays,
                    KnifeOperation_CurrentLife=x.CurrentLife,
                    KnifeOperation_WhLocation = x.WhLocation.Code,
                    KnifeOperation_WareHouse = x.WhLocation.WhArea.WareHouse.Name,
                    KnifeOperation_GrindNum = x.GrindNum,
                    KnifeOperation_U9SourceLineID = x.U9SourceLineID,
                    KnifeOperation_IsAccident = x.IsAccident,
                    KnifeOperation_BeforeStatus = x.BeforeStatus,
                    KnifeOperation_AfterStatus = x.AfterStatus,
                    KnifeOperation_CreateTime = x.CreateTime,
                    KnifeOperation_UpdateTime = x.UpdateTime,
                    KnifeOperation_CreateBy = x.CreateBy,
                    KnifeOperation_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.KnifeOperation_OperationTime);
            return query;
        }


    }
    public class KnifeOperation_View : KnifeOperation
    {

        public string KnifeOperation_Knife { get; set; }
        public string KnifeOperation_DocNo { get; set; }
        public KnifeOperationTypeEnum? KnifeOperation_OperationType { get; set; }
        public DateTime? KnifeOperation_OperationTime { get; set; }
        public string KnifeOperation_OperationBy { get; set; }
        public string KnifeOperation_HandledBy { get; set; }
        public decimal? KnifeOperation_UsedDays { get; set; }
        public decimal? KnifeOperation_RemainingDays { get; set; }
        public decimal? KnifeOperation_TotalUsedDays { get; set; }
        public decimal? KnifeOperation_CurrentLife { get; set; }
        public string KnifeOperation_WhLocation { get; set; }
        public string KnifeOperation_WareHouse { get; set; }
        public decimal? KnifeOperation_GrindNum { get; set; }
        public int? KnifeOperation_GrindNum_Int => (int?)KnifeOperation_GrindNum;

        public string KnifeOperation_U9SourceLineID { get; set; }
        public bool? KnifeOperation_IsAccident { get; set; }
        public KnifeStatusEnum? KnifeOperation_BeforeStatus { get; set; }
        public KnifeStatusEnum? KnifeOperation_AfterStatus { get; set; }
        public DateTime? KnifeOperation_CreateTime { get; set; }
        public DateTime? KnifeOperation_UpdateTime { get; set; }
        public string KnifeOperation_CreateBy { get; set; }
        public string KnifeOperation_UpdateBy { get; set; }


    }

}