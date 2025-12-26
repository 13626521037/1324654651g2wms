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
using WMS.ViewModel.SalesManagement.SalesRMAVMs;

namespace WMS.SalesManagement.Controllers
{
    public partial class SalesRMAController : BaseController
    {

        [ActionDescription("_Page.SalesManagement.SalesRMA.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesRMA.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<SalesRMAVM>(id);
            vm.SalesRMALineRMAIdList.Searcher.RMAIdId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.SalesManagement.SalesRMA.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesRMA.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAVM>(id);
            vm.SalesRMALineRMAIdList.Searcher.RMAIdId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesRMA.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesRMA.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMABatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchSalesRMA")]
        [HttpPost]
        public IActionResult SearchSalesRMA(WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMASearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAListVM>(passInit: true);
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
        public IActionResult SalesRMAExportExcel(WMS.ViewModel.SalesManagement.SalesRMAVMs.SalesRMAListVM vm)
        {
            return vm.GetExportData();
        }

    }
}


