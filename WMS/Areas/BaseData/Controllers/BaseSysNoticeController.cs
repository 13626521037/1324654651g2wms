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
using WMS.ViewModel.BaseData.BaseSysNoticeVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseSysNoticeController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseSysNotice.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSysNotice.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSysNotice.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSysNotice.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSysNotice.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseSysNotice.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchBaseSysNotice")]
        [HttpPost]
        public IActionResult SearchBaseSysNotice(WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeListVM>(passInit: true);
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
        public IActionResult BaseSysNoticeExportExcel(WMS.ViewModel.BaseData.BaseSysNoticeVMs.BaseSysNoticeListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


