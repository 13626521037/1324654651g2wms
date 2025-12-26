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
using WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryOtherReceivementController : BaseController
    {
        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementVM>(id);
            vm.InventoryOtherReceivementLineInventoryOtherReceivementList.Searcher.InventoryOtherReceivementId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.InventoryManagement.InventoryOtherReceivement.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryOtherReceivement")]
        [HttpPost]
        public IActionResult SearchInventoryOtherReceivement(WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementListVM>(passInit: true);
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
        public IActionResult InventoryOtherReceivementExportExcel(WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs.InventoryOtherReceivementListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


