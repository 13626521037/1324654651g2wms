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
using WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryTransferOutManualController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<InventoryTransferOutManualVM>(id);
            vm.InventoryTransferOutManualLineInventoryTransferOutManualList.Searcher.InventoryTransferOutManualId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualVM>(id);
            vm.InventoryTransferOutManualLineInventoryTransferOutManualList.Searcher.InventoryTransferOutManualId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutManual.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryTransferOutManual")]
        [HttpPost]
        public IActionResult SearchInventoryTransferOutManual(WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualListVM>(passInit: true);
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
        public IActionResult InventoryTransferOutManualExportExcel(WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs.InventoryTransferOutManualListVM vm)
        {
            return vm.GetExportData();
        }


        [ActionDescription("取消下架操作")]
        public IActionResult CancelOff(string id)
        {
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
            vm.CancelOff(Guid.Parse(id));
            if (!ModelState.IsValid)
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().RefreshGridRow(CurrentWindowId);
            }
        }
    }
}


