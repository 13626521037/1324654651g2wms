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
using WMS.ViewModel.BaseData.BaseShortcutVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseShortcutController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseShortcut.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseShortcut.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseShortcut.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseShortcut.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseShortcut.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseShortcut.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseShortcut")]
        [HttpPost]
        public IActionResult SearchBaseShortcut(WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutListVM>(passInit: true);
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
        public IActionResult BaseShortcutExportExcel(WMS.ViewModel.BaseData.BaseShortcutVMs.BaseShortcutListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


