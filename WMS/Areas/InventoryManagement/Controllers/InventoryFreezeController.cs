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
using WMS.ViewModel.InventoryManagement.InventoryFreezeVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryFreezeController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeVM>(id);
            vm.InventoryFreezeLineInventoryFreezeList1.Searcher.InventoryFreezeId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeVM>(id);
            vm.InventoryFreezeLineInventoryFreezeList2.Searcher.InventoryFreezeId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryFreeze.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryFreeze")]
        [HttpPost]
        public IActionResult SearchInventoryFreeze(WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeListVM>(passInit: true);
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
        public IActionResult InventoryFreezeExportExcel(WMS.ViewModel.InventoryManagement.InventoryFreezeVMs.InventoryFreezeListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


