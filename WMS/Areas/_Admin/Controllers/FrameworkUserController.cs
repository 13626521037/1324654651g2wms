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

namespace WMS._Admin.Controllers
{
    public partial class FrameworkUserController : BaseController
    {
        
        [ActionDescription("_Page._Admin.FrameworkUser.Create")]
        public ActionResult Create()
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.Edit")]
        public ActionResult Edit(string id)
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserVM>(id);
            vm.Entity.Password = "";
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.Index", IsPage = true)]
        public ActionResult Index(string id)
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserListVM>();
            if (string.IsNullOrEmpty(id) == false)
            {
            }
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.Password")]
        public ActionResult Password(string id)
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserVM>(id);
            vm.Entity.Password = "";
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.Details")]
        public ActionResult Details(string id)
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserVM>(id);
            vm.SetWhDetail();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.Import")]
        public ActionResult Import()
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserImportVM>();
            return PartialView(vm);
        }

        
        [ActionDescription("_Page._Admin.FrameworkUser.BatchEdit")]
        [HttpPost]
        public ActionResult BatchEdit(string[] IDs)
        {

            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }

            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserBatchVM>(Ids: IDs);
            return PartialView(vm);
        }


        #region Search
        [ActionDescription("SearchFrameworkUser")]
        [HttpPost]
        public IActionResult SearchFrameworkUser(WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserSearcher searcher)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }
            var vm = Wtm.CreateVM<WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserListVM>(passInit: true);
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
        public IActionResult FrameworkUserExportExcel(WMS.ViewModel._Admin.FrameworkUserVMs.FrameworkUserListVM vm)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }
            return vm.GetExportData();
        }
        
    }
}


