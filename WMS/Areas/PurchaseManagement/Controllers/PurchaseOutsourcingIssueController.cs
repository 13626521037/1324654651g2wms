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
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs;

namespace WMS.PurchaseManagement.Controllers
{
    public partial class PurchaseOutsourcingIssueController : BaseController
    {
        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<PurchaseOutsourcingIssueVM>(id);
            vm.PurchaseOutsourcingIssueLineOutsourcingIssueList.Searcher.OutsourcingIssueId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueVM>(id);
            vm.PurchaseOutsourcingIssueLineOutsourcingIssueList.Searcher.OutsourcingIssueId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingIssue.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPurchaseOutsourcingIssue")]
        [HttpPost]
        public IActionResult SearchPurchaseOutsourcingIssue(WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueListVM>(passInit: true);
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
        public IActionResult PurchaseOutsourcingIssueExportExcel(WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs.PurchaseOutsourcingIssueListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消下架操作")]
        public IActionResult CancelOff(string id)
        {
            PurchaseOutsourcingIssueApiVM vm = Wtm.CreateVM<PurchaseOutsourcingIssueApiVM>();
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


