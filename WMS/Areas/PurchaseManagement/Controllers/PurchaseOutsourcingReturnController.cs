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
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs;

namespace WMS.PurchaseManagement.Controllers
{
    public partial class PurchaseOutsourcingReturnController : BaseController
    {
        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<PurchaseOutsourcingReturnVM>(id);
            vm.PurchaseOutsourcingReturnLineOutsourcingReturnList.Searcher.OutsourcingReturnId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnVM>(id);
            vm.PurchaseOutsourcingReturnLineOutsourcingReturnList.Searcher.OutsourcingReturnId = id;
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.PurchaseManagement.PurchaseOutsourcingReturn.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchPurchaseOutsourcingReturn")]
        [HttpPost]
        public IActionResult SearchPurchaseOutsourcingReturn(WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnListVM>(passInit: true);
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
        public IActionResult PurchaseOutsourcingReturnExportExcel(WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs.PurchaseOutsourcingReturnListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消收货操作")]
        public IActionResult CancelReceive(string id)
        {
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
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


