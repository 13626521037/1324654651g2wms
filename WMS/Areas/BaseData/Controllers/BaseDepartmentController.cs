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
using WMS.ViewModel.BaseData.BaseDepartmentVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseDepartmentController : BaseController
    {
        
        //[ActionDescription("_Page.BaseData.BaseDepartment.Create")]
        //public ActionResult Create()
        //{

        //    var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentVM>();
        //    return PartialView(vm);
        //}

        
        [ActionDescription("_Page.BaseData.BaseDepartment.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDepartment.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDepartment.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseDepartment.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentImportVM>();
            return PartialView(vm);
        }

        
        //[ActionDescription("_Page.BaseData.BaseDepartment.BatchEdit")]
        //[HttpPost]
        //public ActionResult BatchEdit(string[] IDs)
        //{

        //    var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentBatchVM>(Ids: IDs);
        //    return PartialView(vm);
        //}

        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {
            return PartialView();
        }


        #region Search
        [ActionDescription("SearchBaseDepartment")]
        [HttpPost]
        public IActionResult SearchBaseDepartment(WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentListVM>(passInit: true);
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
        public IActionResult BaseDepartmentExportExcel(WMS.ViewModel.BaseData.BaseDepartmentVMs.BaseDepartmentListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


