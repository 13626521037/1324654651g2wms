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
using WMS.ViewModel.ProductionManagement.ProductionIssueVMs;

namespace WMS.ProductionManagement.Controllers
{
    public partial class ProductionIssueController : BaseController
    {

        [ActionDescription("_Page.ProductionManagement.ProductionIssue.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionIssue.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<ProductionIssueVM>(id);
            vm.ProductionIssueLineProductionIssueList.Searcher.ProductionIssueId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.ProductionManagement.ProductionIssue.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionIssue.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueVM>(id);
            vm.ProductionIssueLineProductionIssueList.Searcher.ProductionIssueId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionIssue.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionIssue.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchProductionIssue")]
        [HttpPost]
        public IActionResult SearchProductionIssue(WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueListVM>(passInit: true);
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
        public IActionResult ProductionIssueExportExcel(WMS.ViewModel.ProductionManagement.ProductionIssueVMs.ProductionIssueListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消下架操作")]
        public IActionResult CancelOff(string id)
        {
            ProductionIssueApiVM vm = Wtm.CreateVM<ProductionIssueApiVM>();
            vm.CancelOff(Guid.Parse(id));
            if (!ModelState.IsValid)
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().RefreshGridRow(CurrentWindowId);
            }
        }
    }
}


