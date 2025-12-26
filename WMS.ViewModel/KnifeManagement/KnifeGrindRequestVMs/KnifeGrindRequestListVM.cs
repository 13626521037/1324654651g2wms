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

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    public partial class KnifeGrindRequestListVM : BasePagedListVM<KnifeGrindRequest_View, KnifeGrindRequestSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeGrindRequest","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeGrindRequest","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeGrindRequest","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeGrindRequest", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeGrindRequest", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeGrindRequest","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeGrindRequest","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeGrindRequest","KnifeGrindRequestExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeGrindRequest_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeGrindRequest_View>>{
                
                this.MakeGridHeader(x => x.KnifeGrindRequest_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindRequest_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeGrindRequest_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeGrindRequest_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeGrindRequest_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeGrindRequest_U9PRDocNo).SetTitle("请购单号"),
                this.MakeGridHeader(x => x.KnifeGrindRequest_LastU9PODocNo).SetTitle("上次采购单号"),
                this.MakeGridHeader(x => x.KnifeGrindRequest_WareHouse).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeGrindRequest_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindRequest_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.KnifeGrindRequest_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeGrindRequest_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeGrindRequest_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeGrindRequest>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeGrindRequest_View
                {
				    ID = x.ID,
                    
                    KnifeGrindRequest_DocNo = x.DocNo,
                    KnifeGrindRequest_Status = x.Status,
                    KnifeGrindRequest_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeGrindRequest_ApprovedTime = x.ApprovedTime,
                    KnifeGrindRequest_OrderNum = x.KnifeGrindRequestLine_KnifeGrindRequest.Count.ToString(),
                    KnifeGrindRequest_U9PRDocNo = x.U9PRDocNo,
                    KnifeGrindRequest_LastU9PODocNo = Common.AddBarCodeDialog(x.LastU9PODocNo),
                    KnifeGrindRequest_WareHouse = x.WareHouse.Name,
                    KnifeGrindRequest_CreateTime = x.CreateTime,
                    KnifeGrindRequest_UpdateTime = x.UpdateTime,
                    KnifeGrindRequest_CreateBy = x.CreateBy,
                    KnifeGrindRequest_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeGrindRequest_View: KnifeGrindRequest
    {
        
        public string KnifeGrindRequest_DocNo { get; set; }
        public KnifeOrderStatusEnum? KnifeGrindRequest_Status { get; set; }
        public string KnifeGrindRequest_HandledBy { get; set; }
        public DateTime? KnifeGrindRequest_ApprovedTime { get; set; }
        public string KnifeGrindRequest_OrderNum { get; set; }
        public string KnifeGrindRequest_U9PRDocNo { get; set; }
        public string KnifeGrindRequest_LastU9PODocNo { get; set; }
        public string KnifeGrindRequest_WareHouse { get; set; }
        public DateTime? KnifeGrindRequest_CreateTime { get; set; }
        public DateTime? KnifeGrindRequest_UpdateTime { get; set; }
        public string KnifeGrindRequest_CreateBy { get; set; }
        public string KnifeGrindRequest_UpdateBy { get; set; }

    }

}