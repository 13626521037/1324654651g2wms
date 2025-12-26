using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCheckInVMs
{
    public partial class KnifeCheckInListVM : BasePagedListVM<KnifeCheckIn_View, KnifeCheckInSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeCheckIn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeCheckIn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeCheckIn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeCheckIn", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeCheckIn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeCheckIn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeCheckIn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeCheckIn","KnifeCheckInExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeCheckIn_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckIn_View>>{
                
                this.MakeGridHeader(x => x.KnifeCheckIn_DocType).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.KnifeCheckIn_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCheckIn_CheckInBy).SetTitle(@Localizer["Page.归还人"].Value),
                this.MakeGridHeader(x => x.KnifeCheckIn_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeCheckIn_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeCheckIn_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCheckIn_CombineKnifeNo).SetTitle(@Localizer["Page.组合刀号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCheckIn_WareHouse).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeCheckIn_OrderNum).SetTitle("数量").SetWidth(60),
                this.MakeGridHeader(x => x.KnifeCheckIn_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCheckIn_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeCheckIn_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeCheckIn_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeCheckIn_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeCheckIn>()
                
                .CheckEqual(Searcher.DocType, x=>x.DocType)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.CheckInById, x=>x.CheckInById)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckContain(Searcher.CombineKnifeNo, x=>x.CombineKnifeNo)
                .CheckEqual(Searcher.WareHouseId,x=>x.WareHouseId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeCheckIn_View
                {
				    ID = x.ID,
                    
                    KnifeCheckIn_DocType = x.DocType,
                    KnifeCheckIn_DocNo = x.DocNo,
                    KnifeCheckIn_CheckInBy = x.CheckInBy.Name,
                    KnifeCheckIn_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeCheckIn_Status = x.Status,
                    KnifeCheckIn_ApprovedTime = x.ApprovedTime,
                    KnifeCheckIn_CombineKnifeNo = x.CombineKnifeNo,
                    KnifeCheckIn_WareHouse = x.WareHouse.Name,
                    KnifeCheckIn_OrderNum = x.KnifeCheckInLine_KnifeCheckIn.Count.ToString(),
                    KnifeCheckIn_CreateTime = x.CreateTime,
                    KnifeCheckIn_UpdateTime = x.UpdateTime,
                    KnifeCheckIn_CreateBy = x.CreateBy,
                    KnifeCheckIn_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeCheckIn_View: KnifeCheckIn
    {
        
        public KnifeCheckInTypeEnum? KnifeCheckIn_DocType { get; set; }
        public string KnifeCheckIn_DocNo { get; set; }
        public string KnifeCheckIn_CheckInBy { get; set; }
        public string KnifeCheckIn_HandledBy { get; set; }
        public KnifeOrderStatusEnum? KnifeCheckIn_Status { get; set; }
        public DateTime? KnifeCheckIn_ApprovedTime { get; set; }
        public string KnifeCheckIn_CombineKnifeNo { get; set; }
        public string KnifeCheckIn_WareHouse { get; set; }
        public string KnifeCheckIn_OrderNum { get; set; }
        public DateTime? KnifeCheckIn_CreateTime { get; set; }
        public DateTime? KnifeCheckIn_UpdateTime { get; set; }
        public string KnifeCheckIn_CreateBy { get; set; }
        public string KnifeCheckIn_UpdateBy { get; set; }

    }

}