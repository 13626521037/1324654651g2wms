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
using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;
using WMS.Model;
using WMS.ViewModel;
using WMS.Util;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryStockTaking")]
    public partial class InventoryStockTakingController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryStockTakingVM vm)
        {
            //ReturnResult<InventoryStockTaking> rr = new ReturnResult<InventoryStockTaking>();
            ModelState.Clear();
            if (vm.Entity.ID == Guid.Empty)
            {
                await vm.DoAddAsync();
            }
            else
            {
                await vm.DoEditAsync();
            }

            if (!ModelState.IsValid)
            {
                vm.DoReInit();
                return PartialView("../InventoryStockTaking/Create", vm);
                //rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                //return Ok(rr);
            }
            else
            {
                vm.InventoryStockTakingLocationsStockTakingList.Searcher.StockTakingId = vm.Entity.ID.ToString();
                return PartialView("../InventoryStockTaking/Create", vm);
                //rr.Entity = vm.Entity;
                //return Ok(rr);
            }
        }
        #endregion

        #region Edit

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public async Task<ActionResult> Edit(InventoryStockTakingVM vm)
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
                    return PartialView("../InventoryStockTaking/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion

        #region Delete

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                //return PartialView(vm);
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }

        #endregion


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(InventoryStockTakingBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryStockTakingBatchVM>();
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
        public ActionResult Import(InventoryStockTakingImportVM vm, IFormCollection nouse)
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
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryStockTakingByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<InventoryStockTaking>().CheckIDs(id, x => x.WhId).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }

        #region Submit

        [ActionDescription("提交")]
        [HttpPost]
        public ActionResult Submit(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.Submit();
            if (!ModelState.IsValid)
            {
                //return PartialView(vm);
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Message("提交成功");
            }
        }

        #endregion

        #region Approve

        [ActionDescription("审核")]
        [HttpPost]
        public ActionResult Approve(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>();
            vm.Approve(id);
            if (!ModelState.IsValid)
            {
                //return PartialView(vm);
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Message("提交成功");
            }
        }

        #endregion

        [ActionDescription("终止关闭")]
        [HttpPost]
        public IActionResult ForceClose(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<InventoryStockTakingVM>(id);
            vm.ForceClose();
            if (!ModelState.IsValid)
            {
                //return PartialView(vm);
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Message("盘点单已终止");
            }
        }
    }
}