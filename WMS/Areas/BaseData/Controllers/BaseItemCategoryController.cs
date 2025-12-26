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
using WMS.ViewModel.BaseData.BaseItemCategoryVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseItemCategoryController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseItemCategory.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseItemCategory.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseItemCategory.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseItemCategory.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseItemCategory.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryImportVM>();
            return PartialView(vm);
        }

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }

        [ActionDescription("_Page.BaseData.BaseItemCategory.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseItemCategory")]
        [HttpPost]
        public IActionResult SearchBaseItemCategory(WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategorySearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryListVM>(passInit: true);
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
        public IActionResult BaseItemCategoryExportExcel(WMS.ViewModel.BaseData.BaseItemCategoryVMs.BaseItemCategoryListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


