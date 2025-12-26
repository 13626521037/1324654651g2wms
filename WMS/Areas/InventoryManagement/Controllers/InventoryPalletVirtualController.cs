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
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryPalletVirtualController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualVM>(id);
            vm.InventoryPalletVirtualLineInventoryPalletList1.Searcher.InventoryPalletId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualVM>(id);
            vm.InventoryPalletVirtualLineInventoryPalletList2.Searcher.InventoryPalletId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryPalletVirtual.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryPalletVirtual")]
        [HttpPost]
        public IActionResult SearchInventoryPalletVirtual(WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualListVM>(passInit: true);
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
        public IActionResult InventoryPalletVirtualExportExcel(WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs.InventoryPalletVirtualListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


