using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipListVM : BasePagedListVM<InventoryOtherShip_View, InventoryOtherShipSearcher>
    {
        
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //this.MakeAction("InventoryOtherShip","Create",@Localizer["Sys.Create"].Value,@Localizer["Sys.Create"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-plus"),
                //this.MakeAction("InventoryOtherShip","Edit",@Localizer["Sys.Edit"].Value,@Localizer["Sys.Edit"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-pencil-square").SetButtonClass("layui-btn-warm"),
                this.MakeAction("InventoryOtherShip","Details",@Localizer["Page.详情"].Value,@Localizer["Page.详情"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",1400).SetShowInRow(true).SetHideOnToolBar(true).SetIconCls("fa fa-info-circle").SetButtonClass("layui-btn-normal"),
                //this.MakeStandardAction("InventoryOtherShip", GridActionStandardTypesEnum.SimpleDelete, @Localizer["Sys.Delete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeStandardAction("InventoryOtherShip", GridActionStandardTypesEnum.SimpleBatchDelete, Localizer["Sys.BatchDelete"].Value, "InventoryManagement", dialogWidth: 800).SetIconCls("fa fa-trash").SetButtonClass("layui-btn-danger"),
                //this.MakeAction("InventoryOtherShip","BatchEdit",@Localizer["Sys.BatchEdit"].Value,@Localizer["Sys.BatchEdit"].Value,GridActionParameterTypesEnum.MultiIds,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-pencil-square"),
                //this.MakeAction("InventoryOtherShip","Import",@Localizer["Sys.Import"].Value,@Localizer["Sys.Import"].Value,GridActionParameterTypesEnum.SingleIdWithNull,"InventoryManagement",800).SetShowInRow(false).SetHideOnToolBar(false).SetIconCls("fa fa-tasks"),
                this.MakeAction("InventoryOtherShip","InventoryOtherShipExportExcel",@Localizer["Sys.Export"].Value,@Localizer["Sys.Export"].Value,GridActionParameterTypesEnum.MultiIdWithNull,"InventoryManagement").SetShowInRow(false).SetShowDialog(false).SetHideOnToolBar(false).SetIsExport(true).SetIconCls("fa fa-arrow-circle-down"),
            };
        }
 

        protected override IEnumerable<IGridColumn<InventoryOtherShip_View>> InitGridHeader()
        {
            return new List<GridColumn<InventoryOtherShip_View>>{
                this.MakeGridHeader(x => x.ID).SetHide(),
                this.MakeGridHeader(x => x.InventoryOtherShip_ErpID).SetTitle(@Localizer["Page.ERP单据ID"].Value).SetHide(),
                this.MakeGridHeader(x => x.InventoryOtherShip_DocNo, width: 110).SetTitle(@Localizer["Page.单号"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_BusinessDate, width: 85).SetTitle(@Localizer["Page.业务日期"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_DocType, width: 100).SetTitle(@Localizer["Page.单据类型"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_BenefitOrganization, width: 165).SetTitle(@Localizer["Page.受益组织"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_BenefitDepartment, width: 165).SetTitle(@Localizer["Page.受益部门"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_BenefitPerson, width: 165).SetTitle(@Localizer["Page.受益人"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_Wh, width: 165).SetTitle(@Localizer["Page.存储地点"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_OwnerOrganization, width: 165).SetTitle(@Localizer["Page.货主组织"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_Memo).SetTitle(@Localizer["_Admin.Remark"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_CreateTime, width: 130).SetTitle(@Localizer["_Admin.CreateTime"].Value),
                this.MakeGridHeader(x => x.InventoryOtherShip_CreateBy, width: 75).SetTitle(@Localizer["_Admin.CreateBy"].Value),

                this.MakeGridHeaderAction(width: 200)
            };
        }

        

        public override IOrderedQueryable<InventoryOtherShip_View> GetSearchQuery()
        {
            var query = DC.Set<InventoryOtherShip>()
                
                .CheckContain(Searcher.ErpID, x=>x.ErpID)
                .CheckBetween(Searcher.BusinessDate?.GetStartTime(), Searcher.BusinessDate?.GetEndTime(), x => x.BusinessDate, includeMax: false)
                .CheckContain(Searcher.DocNo, x=>x.DocNo)
                .CheckEqual(Searcher.DocTypeId, x=>x.DocTypeId)
                .CheckEqual(Searcher.BenefitOrganizationId, x=>x.BenefitOrganizationId)
                .CheckEqual(Searcher.BenefitDepartmentId, x=>x.BenefitDepartmentId)
                .CheckEqual(Searcher.BenefitPersonId, x=>x.BenefitPersonId)
                .CheckEqual(Searcher.WhId, x=>x.WhId)
                .CheckEqual(Searcher.OwnerOrganizationId, x=>x.OwnerOrganizationId)
                .CheckBetween(Searcher.CreateTime?.GetStartTime(), Searcher.CreateTime?.GetEndTime(), x => x.CreateTime, includeMax: false)
                .CheckBetween(Searcher.UpdateTime?.GetStartTime(), Searcher.UpdateTime?.GetEndTime(), x => x.UpdateTime, includeMax: false)
                .CheckContain(Searcher.CreateBy, x=>x.CreateBy)
                .CheckContain(Searcher.UpdateBy, x=>x.UpdateBy)
                .Select(x => new InventoryOtherShip_View
                {
				    ID = x.ID,
                    
                    InventoryOtherShip_ErpID = x.ErpID,
                    InventoryOtherShip_BusinessDate = x.BusinessDate,
                    InventoryOtherShip_DocNo = x.DocNo,
                    InventoryOtherShip_DocType = x.DocType.Name,
                    InventoryOtherShip_BenefitOrganization = x.BenefitOrganization.Code.CodeCombinName(x.BenefitOrganization.Name),
                    InventoryOtherShip_BenefitDepartment = x.BenefitDepartment.Code.CodeCombinName(x.BenefitDepartment.Name),
                    InventoryOtherShip_BenefitPerson = x.BenefitPerson.Name,
                    InventoryOtherShip_Wh = x.Wh.Code.CodeCombinName(x.Wh.Name),
                    InventoryOtherShip_OwnerOrganization = x.OwnerOrganization.Code.CodeCombinName(x.OwnerOrganization.Name),
                    InventoryOtherShip_Memo = x.Memo,
                    InventoryOtherShip_CreateTime = x.CreateTime,
                    InventoryOtherShip_UpdateTime = x.UpdateTime,
                    InventoryOtherShip_CreateBy = x.CreateBy,
                    InventoryOtherShip_UpdateBy = x.UpdateBy,
                })
                .OrderBy(x => x.ID);
            return query;
        }

        
    }
    public class InventoryOtherShip_View: InventoryOtherShip
    {
        
        public string InventoryOtherShip_ErpID { get; set; }
        public DateTime? InventoryOtherShip_BusinessDate { get; set; }
        public string InventoryOtherShip_DocNo { get; set; }
        public string InventoryOtherShip_DocType { get; set; }
        public string InventoryOtherShip_BenefitOrganization { get; set; }
        public string InventoryOtherShip_BenefitDepartment { get; set; }
        public string InventoryOtherShip_BenefitPerson { get; set; }
        public string InventoryOtherShip_Wh { get; set; }
        public string InventoryOtherShip_OwnerOrganization { get; set; }
        public string InventoryOtherShip_Memo { get; set; }
        public DateTime? InventoryOtherShip_CreateTime { get; set; }
        public DateTime? InventoryOtherShip_UpdateTime { get; set; }
        public string InventoryOtherShip_CreateBy { get; set; }
        public string InventoryOtherShip_UpdateBy { get; set; }

    }

}