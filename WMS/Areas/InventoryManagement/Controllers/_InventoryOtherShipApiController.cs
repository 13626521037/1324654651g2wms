using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryOtherShip")]
    public partial class InventoryOtherShipController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryOtherShipVM vm)
        {
            if (!ModelState.IsValid)
            {

                return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoAddAsync();

                if (!ModelState.IsValid)
                {

                    vm.DoReInit();
                    return PartialView("../InventoryOtherShip/Create", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region Edit

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public async Task<ActionResult> Edit(InventoryOtherShipVM vm)
        {
            if (!ModelState.IsValid)
            {

                return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoEditAsync();
                if (!ModelState.IsValid)
                {

                    vm.DoReInit();
                    return PartialView("../InventoryOtherShip/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion




        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(InventoryOtherShipBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchEdit())
            {
                return PartialView(vm.FromView, vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region BatchDelete
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<InventoryOtherShipBatchVM>();
            if (ids != null && ids.Length > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().RefreshGrid(CurrentWindowId).Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(InventoryOtherShipImportVM vm, IFormCollection nouse)
        {
            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return PartialView(vm.FromView, vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
            }
        }
        #endregion




        public ActionResult GetInventoryOtherShipDocTypes()
        {
            return JsonMore(DC.Set<InventoryOtherShipDocType>().GetSelectListItems(Wtm, x => x.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetInventoryOtherShipDocTypeByInventoryOtherShipDocTypeId(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShipDocType>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherShipByInventoryOtherShipDocType(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShip>().CheckIDs(id, x => x.DocTypeId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseOrganizations()
        {
            return JsonMore(DC.Set<BaseOrganization>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseOrganizationByBaseOrganizationId(List<string> id)
        {
            var rv = DC.Set<BaseOrganization>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherShipByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShip>().CheckIDs(id, x => x.BenefitOrganizationId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseDepartments()
        {
            return JsonMore(DC.Set<BaseDepartment>().GetSelectListItems(Wtm, x => x.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetBaseDepartmentByBaseDepartmentId(List<string> id)
        {
            var rv = DC.Set<BaseDepartment>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherShipByBaseDepartment(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShip>().CheckIDs(id, x => x.BenefitDepartmentId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseOperators()
        {
            return JsonMore(DC.Set<BaseOperator>().GetSelectListItems(Wtm, x => x.Department.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetBaseOperatorByBaseOperatorId(List<string> id)
        {
            var rv = DC.Set<BaseOperator>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherShipByBaseOperator(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShip>().CheckIDs(id, x => x.BenefitPersonId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherShipByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShip>().CheckIDs(id, x => x.WhId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }


    }
}