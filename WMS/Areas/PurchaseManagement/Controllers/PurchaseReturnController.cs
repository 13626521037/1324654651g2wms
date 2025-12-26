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
using WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs;
using WMS.Util;

namespace WMS.PurchaseManagement.Controllers
{
    public partial class PurchaseReturnController : BaseController
    {
        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<PurchaseReturnVM>(id);
            vm.PurchaseReturnLinePurchaseReturnList.Searcher.PurchaseReturnId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnVM>(id);
            vm.PurchaseReturnLinePurchaseReturnList.Searcher.PurchaseReturnId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseReturn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPurchaseReturn")]
        [HttpPost]
        public IActionResult SearchPurchaseReturn(WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnListVM>(passInit: true);
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
        public IActionResult PurchaseReturnExportExcel(WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs.PurchaseReturnListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消下架操作")]
        public IActionResult CancelOff(string id)
        {
            PurchaseReturnApiVM vm = Wtm.CreateVM<PurchaseReturnApiVM>();
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


