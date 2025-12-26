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
using WMS.ViewModel.SalesManagement.SalesShipVMs;

namespace WMS.SalesManagement.Controllers
{
    public partial class SalesShipController : BaseController
    {

        [ActionDescription("_Page.SalesManagement.SalesShip.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesShip.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<SalesShipVM>(id);
            vm.SalesShipLineShipList.Searcher.ShipId = id;
            return PartialView(vm);
        }

        [ActionDescription("_Page.SalesManagement.SalesShip.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesShip.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipVM>(id);
            vm.SalesShipLineShipList.Searcher.ShipId = id;
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesShip.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.SalesManagement.SalesShip.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchSalesShip")]
        [HttpPost]
        public IActionResult SearchSalesShip(WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipListVM>(passInit: true);
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
        public IActionResult SalesShipExportExcel(WMS.ViewModel.SalesManagement.SalesShipVMs.SalesShipListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("取消下架操作")]
        public IActionResult CancelOff(string id)
        {
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
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


