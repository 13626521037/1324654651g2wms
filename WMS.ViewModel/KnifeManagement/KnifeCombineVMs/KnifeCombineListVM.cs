using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Util;

namespace WMS.ViewModel.KnifeManagement.KnifeCombineVMs
{
    public partial class KnifeCombineListVM : BasePagedListVM<KnifeCombine_View, KnifeCombineSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeAction("KnifeCombine","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeAction("KnifeCombine","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeCombine","KnifeCombineExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("KnifeCombine","Print", "打印", "打印", GridActionParameterTypesEnum.SingleId, "KnifeManagement", dialogHeight: 800, dialogWidth: 1000).SetShowInRow(true).SetHideOnToolBar(true),

            };
        }


        protected override IEnumerable<IGridColumn<KnifeCombine_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCombine_View>>{

                this.MakeGridHeader(x => x.KnifeCombine_DocNo).SetTitle("单号").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombine_HandledBy).SetTitle("经办人").SetWidth(50).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeCombine_CheckOutBy).SetTitle("领用人").SetWidth(50).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeCombine_CheckOutByDept).SetTitle("领用部门").SetWidth(150).SetAlign(GridColumnAlignEnum.Center),
                this.MakeGridHeader(x => x.KnifeCombine_Status).SetTitle("状态").SetWidth(70),
                this.MakeGridHeader(x => x.KnifeCombine_ApprovedTime).SetTitle("审核时间").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombine_CloseTime).SetTitle("关闭时间").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombine_CombineKnifeNo).SetTitle("组合刀号").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombine_WareHouse).SetTitle("存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCombine_OrderNum).SetTitle("数量").SetWidth(60),


                this.MakeGridHeaderAction(width: 120),
                this.MakeGridHeader(x => x.KnifeCombine_CombineKnifeNos).SetTitle("成员刀号").SetWidth(350),
            };
        }



        public override IOrderedQueryable<KnifeCombine_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeCombine>()

                .CheckContain(Searcher.DocNo, x => x.DocNo)
                .CheckEqual(Searcher.HandledById, x => x.HandledById)
                .CheckEqual(Searcher.CheckOutById, x => x.CheckOutById)
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckBetween(Searcher.CloseTime?.GetStartTime(), Searcher.CloseTime?.GetEndTime(), x => x.CloseTime, includeMax: false)
                .CheckContain(Searcher.CombineKnifeNo, x => x.CombineKnifeNo)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x => x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x => x.UpdateBy)
                .WhereIf(
                    !string.IsNullOrEmpty(Searcher.MemberKnifeNo),
                    x => x.KnifeCombineLine_KnifeCombine.Any(
                        line => line.Knife != null && line.Knife.SerialNumber.Contains(Searcher.MemberKnifeNo)
                    )
                )
                .Select(x => new KnifeCombine_View
                {
                    ID = x.ID,

                    KnifeCombine_DocNo = x.DocNo,
                    KnifeCombine_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeCombine_Status = x.Status,
                    KnifeCombine_CheckOutBy = x.CheckOutBy.Name,
                    KnifeCombine_CheckOutByDept = x.CheckOutBy.Department.Name,
                    KnifeCombine_ApprovedTime = x.ApprovedTime,
                    KnifeCombine_CloseTime = x.CloseTime,
                    KnifeCombine_CombineKnifeNo = Common.AddBarCodeDialog($"{x.CombineKnifeNo}"),
                    KnifeCombine_WareHouse =x.WareHouse.Name,
                    KnifeCombine_OrderNum = x.KnifeCombineLine_KnifeCombine.Count.ToString(),
                    KnifeCombine_CreateTime = x.CreateTime,
                    KnifeCombine_UpdateTime = x.UpdateTime,
                    KnifeCombine_CreateBy = x.CreateBy,
                    KnifeCombine_UpdateBy = x.UpdateBy,
                    KnifeCombine_CombineKnifeNos = string.Join(",",x.KnifeCombineLine_KnifeCombine.Select(x=>x.Knife.SerialNumber)),
                })
                .OrderByDescending(x => x.ID);
            return query;
        }


    }
    public class KnifeCombine_View : KnifeCombine
    {

        public string KnifeCombine_DocNo { get; set; }
        public string KnifeCombine_HandledBy { get; set; }
        public KnifeOrderStatusEnum? KnifeCombine_Status { get; set; }
        public DateTime? KnifeCombine_ApprovedTime { get; set; }
        public DateTime? KnifeCombine_CloseTime { get; set; }
        public string KnifeCombine_CombineKnifeNo { get; set; }
        public string KnifeCombine_CombineKnifeNos { get; set; }
        public string KnifeCombine_OrderNum { get; set; }
        public string KnifeCombine_CheckOutBy { get; set; }
        public string KnifeCombine_CheckOutByDept { get; set; }
        public string KnifeCombine_WareHouse { get; set; }
        public DateTime? KnifeCombine_CreateTime { get; set; }
        public DateTime? KnifeCombine_UpdateTime { get; set; }
        public string KnifeCombine_CreateBy { get; set; }
        public string KnifeCombine_UpdateBy { get; set; }

    }

}