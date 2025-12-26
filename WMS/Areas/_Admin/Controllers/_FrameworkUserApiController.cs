using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.ViewModel._Admin.FrameworkUserVMs;
using WMS.Model;

namespace WMS._Admin.Controllers
{
    [Area("_Admin")]
    [ActionDescription("_Model.FrameworkUser")]
    public partial class FrameworkUserController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(FrameworkUserVM vm)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }
            if (!ModelState.IsValid)
            {
                
                return PartialView(vm.FromView, vm);
            }
            else
            {
                vm.Entity.Password = Utils.GetMD5String(vm.Entity.Password);
                await vm.DoAddAsync();
                
                if (!ModelState.IsValid)
                {
                    
                    vm.DoReInit();
                    return PartialView("../FrameworkUser/Create", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region Edit
       
        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public async Task<ActionResult> Edit(FrameworkUserVM vm)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Content(Localizer["_Admin.HasMainHost"]);
            }
            if (!ModelState.IsValid)
            {
                
                return PartialView(vm.FromView, vm);
            }
            else
            {
                if (vm.Entity.Password != null)
                {
                    vm.Entity.Password = Utils.GetMD5String(vm.Entity.Password);
                }
                await vm.DoEditAsync();
                if (!ModelState.IsValid)
                {
                    
                    vm.DoReInit();
                    return PartialView("../FrameworkUser/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion
      
                


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(FrameworkUserBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchEdit())
            {
                return PartialView(vm.FromView, vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region BatchDelete
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<FrameworkUserBatchVM>();
            if (ids != null && ids.Length > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().RefreshGrid(CurrentWindowId).Alert(Localizer["Sys.BatchDeleteSuccess",vm.Ids.Length]);
            }
        }
        #endregion
      
        #region Import
        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(FrameworkUserImportVM vm, IFormCollection nouse)
        {
            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return PartialView(vm.FromView, vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
            }
        }
        #endregion

        [AllRights]
        public ActionResult GetUserById(string keywords)
        {
            WalkingTec.Mvvm.Admin.Api.AccountController userapi = new WalkingTec.Mvvm.Admin.Api.AccountController();
            userapi.Wtm = Wtm;
            var rv = userapi.GetUserById(keywords) as OkObjectResult;
            List<ComboSelectListItem> users = new List<ComboSelectListItem>();
            if (rv != null && rv.Value is string && rv.Value != null)
            {
                users = System.Text.Json.JsonSerializer.Deserialize<List<ComboSelectListItem>>(rv.Value.ToString());
            }
            else if (rv != null && rv.Value is List<ComboSelectListItem> c)
            {
                users = c;
            }
            return JsonMore(users);
        }

                [AllRights]
        public IActionResult GetFrameworkRoles()
        {
            WalkingTec.Mvvm.Admin.Api.AccountController userapi = new WalkingTec.Mvvm.Admin.Api.AccountController();
            userapi.Wtm = Wtm;
            var rv = userapi.GetFrameworkRoles() as OkObjectResult;
            List<ComboSelectListItem> users = new List<ComboSelectListItem>();
            if (rv != null && rv.Value is string && rv.Value != null)
            {
                users = System.Text.Json.JsonSerializer.Deserialize<List<ComboSelectListItem>>(rv.Value.ToString());
            }
            else if (rv != null && rv.Value is List<ComboSelectListItem> c)
            {
                users = c;
            }
            return JsonMore(users);
        }

        [AllRights]
        public IActionResult GetFrameworkGroups()
        {
            WalkingTec.Mvvm.Admin.Api.AccountController userapi = new WalkingTec.Mvvm.Admin.Api.AccountController();
            userapi.Wtm = Wtm;
            var rv = userapi.GetFrameworkGroupsTree() as OkObjectResult;
            List<TreeSelectListItem> users = new List<TreeSelectListItem>();
            if (rv != null && rv.Value is string && rv.Value != null)
            {
                users = System.Text.Json.JsonSerializer.Deserialize<List<TreeSelectListItem>>(rv.Value.ToString());
            }
            else if (rv != null && rv.Value is List<TreeSelectListItem> c)
            {
                users = c;
            }
            return JsonMore(users);
        }



            }
}