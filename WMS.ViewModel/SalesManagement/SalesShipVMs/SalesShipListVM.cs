using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipListVM : BasePagedListVM<SalesShip_View, SalesShipSearcher>
    {

        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("SalesShip","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("SalesShip","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("SalesShip","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                this.MakeStandardAction("SalesShip", GridActionStandardTypesEnum.Delete, @Localizer["Sys.Delete"].Value, "SalesManagement", dialogWidth: 1400).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger").SetBindVisiableColName("IsDeleteable"),
                //this.MakeStandardAction("SalesShip", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "SalesManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("SalesShip","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("SalesShip","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"SalesManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("SalesShip","SalesShipExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"SalesManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
                this.MakeAction("SalesShip", "CancelOff", "取消下架", "取消下架", GridActionParameterTypesEnum.SingleId, "SalesManagement").SetPromptMessage("操作不可逆，确认取消下架吗？").SetBindVisiableColName("IsCancelable").SetShowInRow().SetHideOnToolBar(),
            };
        }


        protected override IEnumerable<IGridColumn<SalesShip_View>> InitGridHeader()
        {
            return new List<GridColumn<SalesShip_View>>{

                this.MakeGridHeader(x => x.SalesShip_DocNo, width: 100).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.SalesShip_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.SalesShip_DocType, width: 120).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.SalesShip_Organization, width: 165).SetTitle(@Localizer["Page.组织"].Value),
                this.MakeGridHeader(x => x.SalesShip_Customer, width: 300).SetTitle(@Localizer["Page.客户"].Value),
                this.MakeGridHeader(x => x.SalesShip_Operators, width: 75).SetTitle(@Localizer["Page.业务员"].Value),
                this.MakeGridHeader(x => x.SalesShip_Status, width: 65).SetTitle(@Localizer["Page.状态"].Value),
                this.MakeGridHeader(x => x.SalesShip_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.SalesShip_CreatePerson, width: 75).SetTitle("ERP提交人"),
                this.MakeGridHeader(x => x.SalesShip_SubmitDate, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.SalesShip_SourceSystemId, width: 130).SetTitle(@Localizer["Page.来源系统主键"].Value).SetHide(),
                this.MakeGridHeader(x => x.SalesShip_LastUpdateTime, width: 130).SetTitle(@Localizer["Page.最后修改时间"].Value).SetHide(),
                this.MakeGridHeader(x => "IsDeleteable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesShip_Status != SalesShipStatusEnum.InWh)
                    {
                        return "false";
                    }
                    return "true";
                }),
                this.MakeGridHeader(x => "IsCancelable").SetHide().SetFormat((a, b) =>
                {
                    if (a.SalesShip_Status == SalesShipStatusEnum.PartOff
                        || a.SalesShip_Status == SalesShipStatusEnum.AllOff)
                    {
                        return "true";
                    }
                    return "false";
                }),
                this.MakeGridHeaderAction(width: 200)
            };
        }



        public override IOrderedQueryable<SalesShip_View> GetSearchQuery()
        {
            var query = DC.Set<SalesShip>()

                .CheckContain(Searcher.DocNo, x => x.DocNo)
                .CheckEqual(Searcher.OrganizationId, x => x.OrganizationId)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.CreatePerson, x => x.CreatePerson)
                .Where(x => x.Customer.Code.ToLower().Contains(Searcher.Customer.ToLower()) || x.Customer.Name.ToLower().Contains(Searcher.Customer.ToLower()))
                .CheckContain(Searcher.Operators, x => x.Operators)
                .CheckContain(Searcher.DocType, x => x.DocType)
                .CheckEqual(Searcher.Status, x => x.Status)
                .CheckBetween(Searcher.SubmitDate?.GetStartTime(), Searcher.SubmitDate?.GetEndTime(), x => x.SubmitDate, includeMax: false)
                .CheckBetween(Searcher.LastUpdateTime?.GetStartTime(), Searcher.LastUpdateTime?.GetEndTime(), x => x.LastUpdateTime, includeMax: false)
                .Select(x => new SalesShip_View
                {
                    ID = x.ID,

                    SalesShip_CreatePerson = x.CreatePerson,
                    SalesShip_Organization = x.Organization.Code.CodeCombinName(x.Organization.Name),
                    SalesShip_BusinessDate = x.BusinessDate,
                    SalesShip_SubmitDate = x.SubmitDate,
                    SalesShip_DocNo = x.DocNo,
                    SalesShip_DocType = x.DocType,
                    SalesShip_Operators = x.Operators,
                    SalesShip_Customer = x.Customer.Code.CodeCombinName(x.Customer.Name),
                    SalesShip_Status = x.Status,
                    SalesShip_Memo = x.Memo,
                    SalesShip_SourceSystemId = x.SourceSystemId,
                    SalesShip_LastUpdateTime = x.LastUpdateTime,
                })
                .OrderBy(x => x.ID);
            return query;
        }


    }
    public class SalesShip_View : SalesShip
    {

        public string SalesShip_CreatePerson { get; set; }
        public string SalesShip_Organization { get; set; }
        public DateTime? SalesShip_BusinessDate { get; set; }
        public DateTime? SalesShip_SubmitDate { get; set; }
        public string SalesShip_DocNo { get; set; }
        public string SalesShip_DocType { get; set; }
        public string SalesShip_Operators { get; set; }
        public string SalesShip_Customer { get; set; }
        public SalesShipStatusEnum? SalesShip_Status { get; set; }
        public string SalesShip_Memo { get; set; }
        public string SalesShip_SourceSystemId { get; set; }
        public DateTime? SalesShip_LastUpdateTime { get; set; }

    }

}