using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindInVMs
{
    public partial class KnifeGrindInListVM : BasePagedListVM<KnifeGrindIn_View, KnifeGrindInSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeGrindIn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeGrindIn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeGrindIn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeGrindIn", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeGrindIn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeGrindIn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeGrindIn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeGrindIn","KnifeGrindInExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindIn_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindIn_View>>{
                
                this.MakeGridHeader(x => x.KnifeGrindIn_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindIn_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeGrindIn_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeGrindIn_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindIn_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeGrindIn_U9RcvDocNo).SetTitle("收货单号"),
                this.MakeGridHeader(x => x.KnifeGrindIn_WareHouse).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeGrindIn_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindIn_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindIn_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeGrindIn_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeGrindIn_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindIn>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeGrindIn_View
                {
				    ID = x.ID,
                    
                    KnifeGrindIn_DocNo = x.DocNo,
                    KnifeGrindIn_Status = x.Status,
                    KnifeGrindIn_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeGrindIn_ApprovedTime = x.ApprovedTime,
                    KnifeGrindIn_OrderNum = x.KnifeGrindInLine_KnifeGrindIn.Count.ToString(),
                    KnifeGrindIn_U9RcvDocNo = x.U9RcvDocNo,
                    KnifeGrindIn_WareHouse = x.WareHouse.Name,
                    KnifeGrindIn_CreateTime = x.CreateTime,
                    KnifeGrindIn_UpdateTime = x.UpdateTime,
                    KnifeGrindIn_CreateBy = x.CreateBy,
                    KnifeGrindIn_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindIn_View: KnifeGrindIn
    {
        
        public string KnifeGrindIn_DocNo { get; set; }
        public KnifeOrderStatusEnum? KnifeGrindIn_Status { get; set; }
        public string KnifeGrindIn_HandledBy { get; set; }
        public DateTime? KnifeGrindIn_ApprovedTime { get; set; }
        public string KnifeGrindIn_OrderNum { get; set; }
        public string KnifeGrindIn_U9RcvDocNo { get; set; }
        public string KnifeGrindIn_WareHouse { get; set; }
        public DateTime? KnifeGrindIn_CreateTime { get; set; }
        public DateTime? KnifeGrindIn_UpdateTime { get; set; }
        public string KnifeGrindIn_CreateBy { get; set; }
        public string KnifeGrindIn_UpdateBy { get; set; }

    }

}