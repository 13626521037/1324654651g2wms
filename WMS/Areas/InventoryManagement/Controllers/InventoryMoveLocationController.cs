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
using WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryMoveLocationController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationVM>(id);
            vm.InventoryMoveLocationLineInventoryMoveLocationList.Searcher.InventoryMoveLocationId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryMoveLocation.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryMoveLocation")]
        [HttpPost]
        public IActionResult SearchInventoryMoveLocation(WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationListVM>(passInit: true);
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
        public IActionResult InventoryMoveLocationExportExcel(WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs.InventoryMoveLocationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


