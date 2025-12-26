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
using WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs;

namespace WMS.SalesManagement.Controllers
{
    public partial class SalesReturnReceivementController : BaseController
    {

        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<SalesReturnReceivementVM>(id);
            vm.SalesReturnReceivementLineReturnReceivementList.Searcher.ReturnReceivementId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementVM>(id);
            vm.SalesReturnReceivementLineReturnReceivementList.Searcher.ReturnReceivementId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesReturnReceivement.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchSalesReturnReceivement")]
        [HttpPost]
        public IActionResult SearchSalesReturnReceivement(WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementListVM>(passInit: true);
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
        public IActionResult SalesReturnReceivementExportExcel(WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs.SalesReturnReceivementListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消收货操作")]
        public IActionResult CancelReceive(string id)
        {
            SalesReturnReceivementApiVM vm = Wtm.CreateVM<SalesReturnReceivementApiVM>();
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


