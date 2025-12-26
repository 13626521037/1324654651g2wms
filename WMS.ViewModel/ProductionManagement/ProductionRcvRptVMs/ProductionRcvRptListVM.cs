using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptListVM : BasePagedListVM<ProductionRcvRpt_View, ProductionRcvRptSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("ProductionRcvRpt","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("ProductionRcvRpt","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("ProductionRcvRpt","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("ProductionRcvRpt", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "ProductionManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("ProductionRcvRpt", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "ProductionManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("ProductionRcvRpt","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("ProductionRcvRpt","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"ProductionManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("ProductionRcvRpt","ProductionRcvRptExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"ProductionManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("ProductionRcvRpt", "CancelReceive", "取消收货", "取消收货", GridActionParameterTypesEnum.SingleId, "ProductionManagement").SetPromptMessage("操作不可逆，确认取消收货吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }
 

        protected override IEnumerable<IGridColumn<ProductionRcvRpt_View>> InitGridHeader()
        {
            return new List<GridColumn<ProductionRcvRpt_View>>{
                
                this.MakeGridHeader(x => x.ProductionRcvRpt_DocNo, width: 120).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_Wh, width: 160).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_OrderWh, width: 160).SetTitle(@Localizer["Page.订单存储地点"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_ShipType, width: 85).SetTitle("发货类型"),
                this.MakeGridHeader(x => x.ProductionRcvRpt_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_CreateTime, width: 145).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.ProductionRcvRpt_CreateBy, width: 85).SetTitle(@Localizer["_Admin.CreateBy"].Value),
                //this.MakeGridHeader(x => x.ProductionRcvRpt_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value),
                //this.MakeGridHeader(x => x.ProductionRcvRpt_UpdateTime).SetTitle(@Localizer["_Admin.UpdateTime"].Value),
                //this.MakeGridHeader(x => x.ProductionRcvRpt_UpdateBy).SetTitle(@Localizer["_Admin.UpdateBy"].Value),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionRcvRpt_Status != ProductionRcvRptStatusEnum.Reported)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.ProductionRcvRpt_Status == ProductionRcvRptStatusEnum.PartReceive
                        || a.ProductionRcvRpt_Status == ProductionRcvRptStatusEnum.AllReceive)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<ProductionRcvRpt_View> GetSearchQuery()
        {
            var query = DC.Set<ProductionRcvRpt>()
                
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.OrganizationId, x=>x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckEqual(Searcher.WhId, x=>x.WhId)
                .CheckEqual(Searcher.OrderWhId, x=>x.OrderWhId)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .Select(x => new ProductionRcvRpt_View
                {
				    ID = x.ID,
                    
                    ProductionRcvRpt_ErpID = x.ErpID,
                    ProductionRcvRpt_Organization = x.Organization.CodeAndName,
                    ProductionRcvRpt_BusinessDate = x.BusinessDate,
                    ProductionRcvRpt_DocNo = x.DocNo,
                    ProductionRcvRpt_Wh = x.Wh.CodeAndName,
                    ProductionRcvRpt_OrderWh = x.OrderWh.SourceSystemId,
                    ProductionRcvRpt_Status = x.Status,
                    ProductionRcvRpt_ShipType = x.ShipType,
                    ProductionRcvRpt_Memo = x.Memo,
                    ProductionRcvRpt_CreateTime = x.CreateTime,
                    ProductionRcvRpt_UpdateTime = x.UpdateTime,
                    ProductionRcvRpt_CreateBy = x.CreateBy,
                    ProductionRcvRpt_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class ProductionRcvRpt_View: ProductionRcvRpt
    {
        
        public string ProductionRcvRpt_ErpID { get; set; }
        public string ProductionRcvRpt_Organization { get; set; }
        public DateTime? ProductionRcvRpt_BusinessDate { get; set; }
        public string ProductionRcvRpt_DocNo { get; set; }
        public string ProductionRcvRpt_Wh { get; set; }
        public string ProductionRcvRpt_OrderWh { get; set; }
        public ProductionRcvRptStatusEnum? ProductionRcvRpt_Status { get; set; }
        public WhShipTypeEnum? ProductionRcvRpt_ShipType { get; set; }
        public string ProductionRcvRpt_Memo { get; set; }
        public DateTime? ProductionRcvRpt_CreateTime { get; set; }
        public DateTime? ProductionRcvRpt_UpdateTime { get; set; }
        public string ProductionRcvRpt_CreateBy { get; set; }
        public string ProductionRcvRpt_UpdateBy { get; set; }

    }

}