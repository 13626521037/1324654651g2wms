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
using WMS.ViewModel.BaseData.BaseInventoryLogVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseInventoryLogController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseInventoryLog.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseInventoryLog")]
        [HttpPost]
        public IActionResult SearchBaseInventoryLog(WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogListVM>(passInit: true);
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
        public IActionResult BaseInventoryLogExportExcel(WMS.ViewModel.BaseData.BaseInventoryLogVMs.BaseInventoryLogListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


