using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.KnifeManagement;
using WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.ViewModel;

namespace WMS.KnifeManagement.Controllers
{
    [Area("KnifeManagement")]
    [ActionDescription("_Model.KnifeGrindRequest")]
    public partial class KnifeGrindRequestController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(KnifeGrindRequestVM vm)
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
                    return PartialView("../KnifeGrindRequest/Create", vm);
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
        public async Task<ActionResult> Edit(KnifeGrindRequestVM vm)
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
                    return PartialView("../KnifeGrindRequest/Edit", vm);
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
        public ActionResult DoBatchEdit(KnifeGrindRequestBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<KnifeGrindRequestBatchVM>();
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
        public ActionResult Import(KnifeGrindRequestImportVM vm, IFormCollection nouse)
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



        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult GetFrameworkUsers()
        {
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.Name));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeGrindRequestByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<KnifeGrindRequest>().CheckIDs(id, x => x.HandledById).GetSelectListItems(Wtm,x=>x.DocNo);
            return JsonMore(rv);
        }

    }
}