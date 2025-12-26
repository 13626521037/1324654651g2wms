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
using WMS.ViewModel.BaseData.BaseWhAreaVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseWhAreaController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseWhArea.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhArea.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhArea.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhArea.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhArea.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseWhArea.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseWhArea")]
        [HttpPost]
        public IActionResult SearchBaseWhArea(WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaListVM>(passInit: true);
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
        public IActionResult BaseWhAreaExportExcel(WMS.ViewModel.BaseData.BaseWhAreaVMs.BaseWhAreaListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


