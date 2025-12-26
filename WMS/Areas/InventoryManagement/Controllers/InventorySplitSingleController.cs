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
using WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventorySplitSingleController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleVM>(id);
            vm.InventorySplitSingleLineSplitSingleList.Searcher.SplitSingleId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplitSingle.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventorySplitSingle")]
        [HttpPost]
        public IActionResult SearchInventorySplitSingle(WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleListVM>(passInit: true);
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
        public IActionResult InventorySplitSingleExportExcel(WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs.InventorySplitSingleListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


