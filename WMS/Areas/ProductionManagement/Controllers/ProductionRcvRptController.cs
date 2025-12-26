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
using WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs;

namespace WMS.ProductionManagement.Controllers
{
    public partial class ProductionRcvRptController : BaseController
    {
        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptVM>(id);
            vm.ProductionRcvRptLineProductionRcvRptList.Searcher.ProductionRcvRptId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.ProductionManagement.ProductionRcvRpt.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchProductionRcvRpt")]
        [HttpPost]
        public IActionResult SearchProductionRcvRpt(WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptListVM>(passInit: true);
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
        public IActionResult ProductionRcvRptExportExcel(WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs.ProductionRcvRptListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


