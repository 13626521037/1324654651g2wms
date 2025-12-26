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
using WMS.ViewModel.BaseData.BaseWareHouseVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseWareHouseController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseWareHouse.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWareHouse.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWareHouse.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWareHouse.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWareHouse.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWareHouse.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseWareHouse")]
        [HttpPost]
        public IActionResult SearchBaseWareHouse(WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseListVM>(passInit: true);
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
        public IActionResult BaseWareHouseExportExcel(WMS.ViewModel.BaseData.BaseWareHouseVMs.BaseWareHouseListVM vm)
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


