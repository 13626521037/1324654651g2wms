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
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryTransferOutDirectDocTypeController : BaseController
    {

        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryTransferOutDirectDocType.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryTransferOutDirectDocType")]
        [HttpPost]
        public IActionResult SearchInventoryTransferOutDirectDocType(WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeListVM>(passInit: true);
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
        public IActionResult InventoryTransferOutDirectDocTypeExportExcel(WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs.InventoryTransferOutDirectDocTypeListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }
    }
}


