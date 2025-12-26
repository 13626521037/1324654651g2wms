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
using WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs;

namespace WMS.InventoryManagement.Controllers
{
    public partial class InventoryErpDiffController : BaseController
    {

        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffVM>(id);
            vm.InventoryErpDiffLineErpDiffList.Searcher.ErpDiffId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.InventoryManagement.InventoryErpDiff.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchInventoryErpDiff")]
        [HttpPost]
        public IActionResult SearchInventoryErpDiff(WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffListVM>(passInit: true);
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
        public IActionResult InventoryErpDiffExportExcel(WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("对账")]
        public IActionResult Analysis()
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs.InventoryErpDiffVM>();
            return PartialView(vm);
        }
    }
}


