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
using WMS.ViewModel.BaseData.BaseWhLocationVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseWhLocationController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseWhLocation.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhLocation.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhLocation.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhLocation.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhLocation.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhLocation.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseWhLocation")]
        [HttpPost]
        public IActionResult SearchBaseWhLocation(WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationListVM>(passInit: true);
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
        public IActionResult BaseWhLocationExportExcel(WMS.ViewModel.BaseData.BaseWhLocationVMs.BaseWhLocationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


