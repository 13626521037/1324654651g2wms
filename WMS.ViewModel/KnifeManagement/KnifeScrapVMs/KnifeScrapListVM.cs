using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeScrapVMs
{
    public partial class KnifeScrapListVM : BasePagedListVM<KnifeScrap_View, KnifeScrapSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("KnifeScrap","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("KnifeScrap","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("KnifeScrap","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("KnifeScrap", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("KnifeScrap", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "KnifeManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("KnifeScrap","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("KnifeScrap","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"KnifeManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("KnifeScrap","KnifeScrapExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"KnifeManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("KnifeScrap","KnifeScrapClose","终止关闭","终止关闭",GridActionParameterTypesEnum.SingleId,"KnifeManagement").SetShowInRow(true).SetHideOnToolBar(true).SetBindVisiableColName("IsCloseable"),
                
            };
        }
 

        protected override IEnumerable<IGridColumn<KnifeScrap_View>> InitGridHeader()
        {
            return new List<GridColumn<KnifeScrap_View>>{
                
                this.MakeGridHeader(x => x.KnifeScrap_DocNo).SetTitle(@Localizer["Page.单号"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeScrap_DocType).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.KnifeScrap_Status).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.KnifeScrap_HandledBy).SetTitle(@Localizer["Page.经办人"].Value),
                this.MakeGridHeader(x => x.KnifeScrap_ApprovedTime).SetTitle(@Localizer["Page.审核时间"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeScrap_OrderNum).SetTitle("数量").SetWidth(50),
                this.MakeGridHeader(x => x.KnifeScrap_U9MiscRcvDocNo).SetTitle("杂收单").SetWidth(120),
                this.MakeGridHeader(x => x.KnifeScrap_WareHouse ).SetTitle("存储地点"),
                this.MakeGridHeader(x => x.KnifeScrap_CreateTime).SetTitle(@Localizer["_Admin.CreateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeScrap_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value).SetWidth(120),
                this.MakeGridHeader(x => x.KnifeScrap_CreateBy).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                this.MakeGridHeader(x => x.KnifeScrap_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsCloseable").SetHide().SetFormat((a, b) =>
                {
                    if (a.KnifeScrap_Status != KnifeOrderStatusEnum.Approved&&a.KnifeScrap_DocType==KnifeScrapTypeEnum.NormalScrap)
                    {
                        return "false";
                    }
                    if (a.KnifeScrap_DocType!=KnifeScrapTypeEnum.NormalScrap)
                    {
                        return "false";
                    }
                    if (a.KnifeScrap_CreateBy != LoginUserInfo.ITCode)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeaderAction(width: 250)
            };
        }

        

        public override IOrderedQueryable<KnifeScrap_View> GetSearchQuery()
        {
            var query = DC.Set<KnifeScrap>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.DocType, x=>x.DocType)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.HandledById, x=>x.HandledById)
                .CheckEqual(Searcher.WareHouseId, x => x.WareHouseId)
                .CheckBetween(Searcher.ApprovedTime?.GetStartTime(), Searcher.ApprovedTime?.GetEndTime(), x => x.ApprovedTime, includeMax: false)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new KnifeScrap_View
                {
				    ID = x.ID,
                    
                    KnifeScrap_DocNo = x.DocNo,
                    KnifeScrap_DocType = x.DocType,
                    KnifeScrap_Status = x.Status,
                    KnifeScrap_HandledBy = DC.Set<FrameworkUser>().Where(z0 => z0.ID.ToString() == x.HandledById).Select(y => y.Name).FirstOrDefault(),
                    KnifeScrap_ApprovedTime = x.ApprovedTime,
                    KnifeScrap_OrderNum = x.KnifeScrapLine_KnifeScrap.Count.ToString(),
                    KnifeScrap_U9MiscRcvDocNo = x.U9MiscRcvDocNo,
                    KnifeScrap_WareHouse = x.WareHouse.Name,
                    KnifeScrap_CreateTime = x.CreateTime,
                    KnifeScrap_UpdateTime = x.UpdateTime,
                    KnifeScrap_CreateBy = x.CreateBy,
                    KnifeScrap_UpdateBy = x.UpdateBy,
                })
                .OrderByDescending(x => x.ID);
            return query;
        }

        
    }
    public class KnifeScrap_View: KnifeScrap
    {
        
        public string KnifeScrap_DocNo { get; set; }
        public KnifeScrapTypeEnum? KnifeScrap_DocType { get; set; }
        public KnifeOrderStatusEnum? KnifeScrap_Status { get; set; }
        public string KnifeScrap_HandledBy { get; set; }
        public DateTime? KnifeScrap_ApprovedTime { get; set; }
        public string KnifeScrap_OrderNum { get; set; }
        public string KnifeScrap_U9MiscRcvDocNo { get; set; }
        public string KnifeScrap_WareHouse { get; set; }
        public DateTime? KnifeScrap_CreateTime { get; set; }
        public DateTime? KnifeScrap_UpdateTime { get; set; }
        public string KnifeScrap_CreateBy { get; set; }
        public string KnifeScrap_UpdateBy { get; set; }

    }

}