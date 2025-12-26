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
using WMS.ViewModel.BaseData.BaseOrganizationVMs;

namespace WMS.BaseData.Controllers
{
    public partial class BaseOrganizationController : BaseController
    {
        
        [ActionDescription("_Page.BaseData.BaseOrganization.Create")]
        public ActionResult Create()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseOrganization.Edit")]
        public ActionResult Edit(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseOrganization.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseOrganization.Details")]
        public ActionResult Details(string id)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationVM>(id);
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseOrganization.Import")]
        public ActionResult Import()
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page.BaseData.BaseOrganization.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        
        [ActionDescription("同步数据")]
        public ActionResult SyncData()
        {

            //var vm = Wtm.CreateVM<BaseOrganizationVM>();
            return PartialView();
        }


        #region Search
        [ActionDescription("SearchBaseOrganization")]
        [HttpPost]
        public IActionResult SearchBaseOrganization(WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationSearcher searcher)
        {
            var vm = Wtm.CreateVM<WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationListVM>(passInit: true);
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
        public IActionResult BaseOrganizationExportExcel(WMS.ViewModel.BaseData.BaseOrganizationVMs.BaseOrganizationListVM vm)
        {
            return vm.GetExportData();
        }
        
    }
}


