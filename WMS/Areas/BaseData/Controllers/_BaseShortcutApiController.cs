using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.BaseData;
using WMS.ViewModel.BaseData.BaseShortcutVMs;
using WMS.Model;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseShortcut")]
    public partial class BaseShortcutController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(BaseShortcutVM vm)
        {
            if (!ModelState.IsValid)
            {
                
                return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoAddAsync();
                
                if (!ModelState.IsValid)
                {
                    
                    vm.DoReInit();
                    return PartialView("../BaseShortcut/Create", vm);
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
        public async Task<ActionResult> Edit(BaseShortcutVM vm)
        {
            if (!ModelState.IsValid)
            {
                
                return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoEditAsync();
                if (!ModelState.IsValid)
                {
                    
                    vm.DoReInit();
                    return PartialView("../BaseShortcut/Edit", vm);
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
        public ActionResult DoBatchEdit(BaseShortcutBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseShortcutBatchVM>();
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
        public ActionResult Import(BaseShortcutImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetFrameworkMenus(bool istree=false, bool istop=false)
        {
            if(istree == true){
                if(istop == false){
                    return JsonMore(DC.Set<FrameworkMenu>().GetTreeSelectListItems(Wtm, x => x.PageName));
                }
                else{
                    return JsonMore(DC.Set<FrameworkMenu>().Where(x=>x.ParentId == null).GetTreeSelectListItems(Wtm, x => x.PageName));
                }
            }
            else{
                if(istop == false){
                    return JsonMore(DC.Set<FrameworkMenu>().GetSelectListItems(Wtm, x => x.PageName));
                }
                else{
                    return JsonMore(DC.Set<FrameworkMenu>().Where(x=>x.ParentId == null).GetSelectListItems(Wtm, x => x.PageName));
                }
            }
        }
        public ActionResult Select_GetFrameworkMenuByFrameworkMenuId(List<string> id)
        {
            var rv = DC.Set<FrameworkMenu>().CheckIDs(id).GetSelectListItems(Wtm, x => x.PageName);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseShortcutByFrameworkMenu(List<string> id)
        {
            var rv = DC.Set<BaseShortcut>().CheckIDs(id, x => x.MenuId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetFrameworkUsers()
        {
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.Name, x => x.ITCode.ToString()));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name, x => x.ITCode.ToString());
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseShortcutByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<BaseShortcut>().CheckIDs(id, x => x.UserId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }

    }
}