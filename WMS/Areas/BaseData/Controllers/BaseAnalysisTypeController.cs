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
using WMS.ViewModel.BaseData.BaseAnalysisTypeVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseAnalysisTypeController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseAnalysisType.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseAnalysisType.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseAnalysisType.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseAnalysisType.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseAnalysisType.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseAnalysisType.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseAnalysisType")]
        [HttpPost]
        public IActionResult SearchBaseAnalysisType(WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeListVM>(passInit: true);
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
        public IActionResult BaseAnalysisTypeExportExcel(WMS.ViewModel.BaseData.BaseAnalysisTypeVMs.BaseAnalysisTypeListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


