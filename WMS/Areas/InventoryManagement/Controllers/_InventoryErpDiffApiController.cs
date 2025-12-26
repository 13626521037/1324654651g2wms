using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryErpDiff")]
    public partial class InventoryErpDiffController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryErpDiffVM vm)
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
                    return PartialView("../InventoryErpDiff/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryErpDiffVM vm)
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
                    return PartialView("../InventoryErpDiff/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion

        [ActionDescription("对账")]
        [HttpPost]
        public ActionResult Analysis(InventoryErpDiffVM vm)
        {
            ModelState.Clear();
            vm.Analysis();

            if (!ModelState.IsValid)
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(InventoryErpDiffBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryErpDiffBatchVM>();
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
        public ActionResult Import(InventoryErpDiffImportVM vm, IFormCollection nouse)
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




        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().Where(x => x.IsValid == true && x.IsEffective == EffectiveEnum.Effective).GetSelectListItems(Wtm, x => x.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryErpDiffByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<InventoryErpDiff>().CheckIDs(id, x => x.WhId).GetSelectListItems(Wtm, x => x.Seiban);
            return JsonMore(rv);
        }


        public ActionResult GetBaseItemMasters()
        {
            return JsonMore(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseItemMasterByBaseItemMasterId(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryErpDiffByBaseItemMaster(List<string> id)
        {
            var rv = DC.Set<InventoryErpDiff>().CheckIDs(id, x => x.ItemId).GetSelectListItems(Wtm, x => x.Seiban);
            return JsonMore(rv);
        }

    }
}