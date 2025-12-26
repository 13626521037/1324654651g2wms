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
using WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryAdjustDirectController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjustDirect.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryAdjustDirect")]
        [HttpPost]
        public IActionResult SearchInventoryAdjustDirect(WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectListVM>(passInit: true);
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
        public IActionResult InventoryAdjustDirectExportExcel(WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs.InventoryAdjustDirectListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


