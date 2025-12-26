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
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryTransferOutDirectController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectVM>(id);
            vm.InventoryTransferOutDirectLineInventoryTransferOutDirectList.Searcher.InventoryTransferOutDirectId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirect.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryTransferOutDirect")]
        [HttpPost]
        public IActionResult SearchInventoryTransferOutDirect(WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectListVM>(passInit: true);
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
        public IActionResult InventoryTransferOutDirectExportExcel(WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs.InventoryTransferOutDirectListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


