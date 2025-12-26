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
using WMS.ViewModel.InventoryManagement.InventorySplitVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventorySplitController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventorySplit.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventorySplit")]
        [HttpPost]
        public IActionResult SearchInventorySplit(WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitListVM>(passInit: true);
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
        public IActionResult InventorySplitExportExcel(WMS.ViewModel.InventoryManagement.InventorySplitVMs.InventorySplitListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


