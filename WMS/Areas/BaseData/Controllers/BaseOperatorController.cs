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
using WMS.ViewModel.BaseData.BaseOperatorVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseOperatorController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseOperator.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseOperator.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseOperator.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseOperator.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseOperator.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseOperator.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseOperator")]
        [HttpPost]
        public IActionResult SearchBaseOperator(WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorListVM>(passInit: true);
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
        public IActionResult BaseOperatorExportExcel(WMS.ViewModel.BaseData.BaseOperatorVMs.BaseOperatorListVM vm)
        {
            return vm.GetExportData();
        }

    }
}


