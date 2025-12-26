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
using WMS.ViewModel.InventoryManagement.InventoryAdjustVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryAdjustController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustVM>(id);
            vm.InventoryAdjustLineInvAdjustList.Searcher.InvAdjustId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryAdjust.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryAdjust")]
        [HttpPost]
        public IActionResult SearchInventoryAdjust(WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustListVM>(passInit: true);
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
        public IActionResult InventoryAdjustExportExcel(WMS.ViewModel.InventoryManagement.InventoryAdjustVMs.InventoryAdjustListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


