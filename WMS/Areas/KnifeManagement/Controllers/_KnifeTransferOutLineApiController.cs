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
using WMS.ViewModel.KnifeManagement.KnifeTransferOutLineVMs;
using WMS.Model;

namespace WMS.KnifeManagement.Controllers
{
    [Area("KnifeManagement")]
    [ActionDescription("_Model.KnifeTransferOutLine")]
    public partial class KnifeTransferOutLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(KnifeTransferOutLineVM vm)
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
                    return PartialView("../KnifeTransferOutLine/Create", vm);
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
        public async Task<ActionResult> Edit(KnifeTransferOutLineVM vm)
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
                    return PartialView("../KnifeTransferOutLine/Edit", vm);
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
        public ActionResult DoBatchEdit(KnifeTransferOutLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<KnifeTransferOutLineBatchVM>();
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
        public ActionResult Import(KnifeTransferOutLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetKnifeTransferOuts()
        {
            return JsonMore(DC.Set<KnifeTransferOut>().GetSelectListItems(Wtm, x => x.DocNo));
        }
        public ActionResult Select_GetKnifeTransferOutByKnifeTransferOutId(List<string> id)
        {
            var rv = DC.Set<KnifeTransferOut>().CheckIDs(id).GetSelectListItems(Wtm, x => x.DocNo);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeTransferOutLineByKnifeTransferOut(List<string> id)
        {
            var rv = DC.Set<KnifeTransferOutLine>().CheckIDs(id, x => x.KnifeTransferOutId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetKnifes()
        {
            return JsonMore(DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber));
        }
        public ActionResult Select_GetKnifeByKnifeId(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SerialNumber);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeTransferOutLineByKnife(List<string> id)
        {
            var rv = DC.Set<KnifeTransferOutLine>().CheckIDs(id, x => x.KnifeId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }

    }
}