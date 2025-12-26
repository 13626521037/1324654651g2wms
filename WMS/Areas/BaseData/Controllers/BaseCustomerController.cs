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
using WMS.ViewModel.BaseData.BaseCustomerVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseCustomerController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseCustomer.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseCustomer.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseCustomer.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseCustomer.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseCustomer.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseCustomer.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseCustomer")]
        [HttpPost]
        public IActionResult SearchBaseCustomer(WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerListVM>(passInit: true);
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
        public IActionResult BaseCustomerExportExcel(WMS.ViewModel.BaseData.BaseCustomerVMs.BaseCustomerListVM vm)
        {
            return vm.GetExportData();
        }

    }
}


