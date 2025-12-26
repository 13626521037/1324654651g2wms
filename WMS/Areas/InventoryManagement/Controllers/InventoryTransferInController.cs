using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Collections.Generic;
using WMS.Model;
using WMS.ViewModel.InventoryManagement.InventoryTransferInVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryTransferInController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<InventoryTransferInVM>(id);
            vm.InventoryTransferInLineInventoryTransferInList.Searcher.InventoryTransferInId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInVM>(id);
            vm.InventoryTransferInLineInventoryTransferInList.Searcher.InventoryTransferInId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferIn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryTransferIn")]
        [HttpPost]
        public IActionResult SearchInventoryTransferIn(WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInListVM>(passInit: true);
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                return Content(vm.GetJson(false));
            }
            else
            {
                return Content(vm.GetError());
            }
        }
        #endregion

        [ActionDescription("Sys.Export")]
        [HttpPost]
        public IActionResult InventoryTransferInExportExcel(WMS.ViewModel.InventoryManagement.InventoryTransferInVMs.InventoryTransferInListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


