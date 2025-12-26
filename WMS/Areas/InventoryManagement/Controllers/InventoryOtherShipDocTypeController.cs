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
using WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryOtherShipDocTypeController : BaseController
    {

        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryOtherShipDocType.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryOtherShipDocType")]
        [HttpPost]
        public IActionResult SearchInventoryOtherShipDocType(WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeListVM>(passInit: true);
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
        public IActionResult InventoryOtherShipDocTypeExportExcel(WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs.InventoryOtherShipDocTypeListVM vm)
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


