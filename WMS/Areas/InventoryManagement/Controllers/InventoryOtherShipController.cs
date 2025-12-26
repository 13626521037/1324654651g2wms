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
using WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryOtherShipController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipVM>(id);
            vm.InventoryOtherShipLineInventoryOtherShipList.Searcher.InventoryOtherShipId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherShip.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryOtherShip")]
        [HttpPost]
        public IActionResult SearchInventoryOtherShip(WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipListVM>(passInit: true);
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
        public IActionResult InventoryOtherShipExportExcel(WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs.InventoryOtherShipListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


