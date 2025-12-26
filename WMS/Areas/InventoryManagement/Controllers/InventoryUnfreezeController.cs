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
using WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryUnfreezeController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeVM>(id);
            vm.InventoryUnfreezeLineInventoryUnfreezeList1.Searcher.InventoryUnfreezeId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeVM>(id);
            vm.InventoryUnfreezeLineInventoryUnfreezeList2.Searcher.InventoryUnfreezeId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryUnfreeze.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryUnfreeze")]
        [HttpPost]
        public IActionResult SearchInventoryUnfreeze(WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeListVM>(passInit: true);
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
        public IActionResult InventoryUnfreezeExportExcel(WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs.InventoryUnfreezeListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


