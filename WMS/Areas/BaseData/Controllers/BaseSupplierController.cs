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
using WMS.ViewModel.BaseData.BaseSupplierVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseSupplierController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseSupplier.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSupplier.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSupplier.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSupplier.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSupplier.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseSupplier.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseSupplier")]
        [HttpPost]
        public IActionResult SearchBaseSupplier(WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierListVM>(passInit: true);
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
        public IActionResult BaseSupplierExportExcel(WMS.ViewModel.BaseData.BaseSupplierVMs.BaseSupplierListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


