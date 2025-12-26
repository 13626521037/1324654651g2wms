using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferInVMs
{
    public partial class KnifeTransferInListVM : BasePagedListVM<KnifeTransferIn_View, KnifeTransferInSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeTransferIn","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeTransferIn","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeTransferIn","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeTransferIn", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeTransferIn", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeTransferIn","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeTransferIn","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeTransferIn","KnifeTransferInExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeTransferIn_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeTransferIn_View>>{
                
                this.MakeGridHeader(x => x.KnifeTransferIn_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(140),
                this.MakeGridHeader(x => x.KnifeTransferIn_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeTransferIn_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(140),
                this.MakeGridHeader(x => x.KnifeTransferIn_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeTransferIn_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.KnifeTransferIn_TransferOutDocNo).SetTitle(@Localizer["Page.调出单单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferIn_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeTransferIn_FromWH).SetTitle("调出存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferIn_ToWH).SetTitle("调入存储地点").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferIn_WareHouse ).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeTransferIn_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferIn_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeTransferIn_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeTransferIn_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),

                this.MakeGridHeaderAction(width: 60)
            };
        }

        

        public override IOrderedQueryable<KnifeTransferIn_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeTransferIn>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckContain(Searcher.Memo, x=>x.Memo)
                .CheckEqual(Searcher.FromWHId, x=>x.FromWHId)
                .CheckEqual(Searcher.ToWHId, x=>x.ToWHId)
                .CheckContain(Searcher.TransferOutDocNo, x=>x.TransferOutDocNo)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeTransferIn_View
                {
				    ID = x.ID,
                    
                    KnifeTransferIn_DocNo = x.DocNo,
                    KnifeTransferIn_Status = x.Status,
                    KnifeTransferIn_ApprovedTime = x.ApprovedTime,
                    KnifeTransferIn_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeTransferIn_Memo = x.Memo,
                    KnifeTransferIn_TransferOutDocNo = x.TransferOutDocNo,
                    KnifeTransferIn_OrderNum = x.KnifeTransferInLine_KnifeTransferIn.Count.ToString(),
                    KnifeTransferIn_FromWH =x.FromWH.Name,
                    KnifeTransferIn_ToWH = x.ToWH.Name,
                    KnifeTransferIn_WareHouse = x.WareHouse.Name,
                    KnifeTransferIn_CreateTime = x.CreateTime,
                    KnifeTransferIn_UpdateTime = x.UpdateTime,
                    KnifeTransferIn_CreateBy = x.CreateBy,
                    KnifeTransferIn_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeTransferIn_View: KnifeTransferIn
    {
        
        public string KnifeTransferIn_DocNo { get; set; }
        public KnifeOrderStatusEnum? KnifeTransferIn_Status { get; set; }
        public DateTime? KnifeTransferIn_ApprovedTime { get; set; }
        public string KnifeTransferIn_HandledBy { get; set; }
        public string KnifeTransferIn_Memo { get; set; }
        public string KnifeTransferIn_TransferOutDocNo { get; set; }
        public string KnifeTransferIn_OrderNum { get; set; }
        public string KnifeTransferIn_FromWH { get; set; }
        public string KnifeTransferIn_ToWH { get; set; }
        public string KnifeTransferIn_WareHouse { get; set; }
        public DateTime? KnifeTransferIn_CreateTime { get; set; }
        public DateTime? KnifeTransferIn_UpdateTime { get; set; }
        public string KnifeTransferIn_CreateBy { get; set; }
        public string KnifeTransferIn_UpdateBy { get; set; }

    }

}