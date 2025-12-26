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
using WMS.ViewModel.BaseData.BaseUnitVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseUnitController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseUnit.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseUnit.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseUnit.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseUnit.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseUnit.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseUnit.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseUnit")]
        [HttpPost]
        public IActionResult SearchBaseUnit(WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitListVM>(passInit: true);
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
        public IActionResult BaseUnitExportExcel(WMS.ViewModel.BaseData.BaseUnitVMs.BaseUnitListVM vm)
        {
            return vm.GetExportData();
        }

    }
}


