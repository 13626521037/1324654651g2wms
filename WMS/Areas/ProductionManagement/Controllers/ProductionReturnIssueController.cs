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
using WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs;

namespace WMS.ProductionManagement.Controllers
{
    public partial class ProductionReturnIssueController : BaseController
    {

        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<ProductionReturnIssueVM>(id);
            vm.ProductionReturnIssueLineProductionReturnIssueList.Searcher.ProductionReturnIssueId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueVM>(id);
            vm.ProductionReturnIssueLineProductionReturnIssueList.Searcher.ProductionReturnIssueId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.ProductionManagement.ProductionReturnIssue.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchProductionReturnIssue")]
        [HttpPost]
        public IActionResult SearchProductionReturnIssue(WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueListVM>(passInit: true);
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
        public IActionResult ProductionReturnIssueExportExcel(WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs.ProductionReturnIssueListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消收货操作")]
        public IActionResult CancelReceive(string id)
        {
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
            vm.CancelReceive(Guid.Parse(id));
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


