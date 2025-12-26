using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs
{
    public partial class KnifeCheckOutListVM : BasePagedListVM<KnifeCheckOut_View, KnifeCheckOutSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeCheckOut","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeCheckOut","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeCheckOut","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeCheckOut", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeCheckOut", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeCheckOut","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeCheckOut","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeCheckOut","KnifeCheckOutExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeCheckOut_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeCheckOut_View>>{
                
                this.MakeGridHeader(x => x.KnifeCheckOut_DocType).SetTitle(@Localizer["Page.单据类型"].Value).SetWidth(80),
                this.MakeGridHeader(x => x.KnifeCheckOut_DocNo).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.KnifeCheckOut_CheckOutBy).SetTitle(@Localizer["Page.领用人"].Value).SetWidth(60),
                this.MakeGridHeader(x => x.KnifeCheckOut_HandledBy).SetTitle(@Localizer["Page.经办人"].Value).SetWidth(60),
                this.MakeGridHeader(x => x.KnifeCheckOut_Status).SetTitle(@Localizer["Page.状态"].Value).SetWidth(60),
                this.MakeGridHeader(x => x.KnifeCheckOut_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value),
                this.MakeGridHeader(x => x.KnifeCheckOut_WareHouse ).SetTitle("存储地点"),

                this.MakeGridHeader(x => x.KnifeCheckOut_OrderNum).SetTitle("数量").SetWidth(40),
                this.MakeGridHeader(x => x.KnifeCheckOut_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.KnifeCheckOut_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                this.MakeGridHeader(x => x.KnifeCheckOut_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value).SetWidth(80),
                this.MakeGridHeader(x => x.KnifeCheckOut_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value).SetWidth(80),

                this.MakeGridHeaderAction(width: 80)
            };
        }

        

        public override IOrderedQueryable<KnifeCheckOut_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeCheckOut>()
                
                .CheckEqual(Searcher.DocType, x=>x.DocType)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.CheckOutById, x=>x.CheckOutById)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeCheckOut_View
                {
				    ID = x.ID,
                    
                    KnifeCheckOut_DocType = x.DocType,
                    KnifeCheckOut_DocNo = x.DocNo,
                    KnifeCheckOut_CheckOutBy = x.CheckOutBy.Name,
                    KnifeCheckOut_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeCheckOut_Status = x.Status,
                    KnifeCheckOut_ApprovedTime = x.ApprovedTime,
                    KnifeCheckOut_WareHouse = x.WareHouse.Name,
                    KnifeCheckOut_OrderNum = x.KnifeCheckOutLine_KnifeCheckOut.Count.ToString(),
                    KnifeCheckOut_CreateTime = x.CreateTime,
                    KnifeCheckOut_UpdateTime = x.UpdateTime,
                    KnifeCheckOut_CreateBy = x.CreateBy,
                    KnifeCheckOut_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeCheckOut_View: KnifeCheckOut
    {
        
        public KnifeCheckOutTypeEnum? KnifeCheckOut_DocType { get; set; }
        public string KnifeCheckOut_DocNo { get; set; }
        public string KnifeCheckOut_CheckOutBy { get; set; }
        public string KnifeCheckOut_HandledBy { get; set; }
        public KnifeOrderStatusEnum? KnifeCheckOut_Status { get; set; }
        public DateTime? KnifeCheckOut_ApprovedTime { get; set; }
        public string KnifeCheckOut_WareHouse { get; set; }
        public string KnifeCheckOut_OrderNum { get; set; }
        public DateTime? KnifeCheckOut_CreateTime { get; set; }
        public DateTime? KnifeCheckOut_UpdateTime { get; set; }
        public string KnifeCheckOut_CreateBy { get; set; }
        public string KnifeCheckOut_UpdateBy { get; set; }


    }

}