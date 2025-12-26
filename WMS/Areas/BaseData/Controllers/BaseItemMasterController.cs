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
using WMS.ViewModel.BaseData.BaseItemMasterVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseItemMasterController : BaseController
    {

        [ActionDescription("_Page.BaseData.BaseItemMaster.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseItemMaster.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseItemMaster.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseItemMaster.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterVM>(id);
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseItemMaster.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterImportVM>();
            return PartialView(vm);
        }


        [ActionDescription("_Page.BaseData.BaseItemMaster.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseItemMaster")]
        [HttpPost]
        public IActionResult SearchBaseItemMaster(WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterListVM>(passInit: true);
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
        public IActionResult BaseItemMasterExportExcel(WMS.ViewModel.BaseData.BaseItemMasterVMs.BaseItemMasterListVM vm)
        {
            return vm.GetExportData();
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }
    }
}


