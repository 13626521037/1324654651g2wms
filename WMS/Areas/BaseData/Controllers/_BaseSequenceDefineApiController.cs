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
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.Model;
using WMS.Util;
using WMS.ViewModel;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseSequenceDefine")]
    public partial class BaseSequenceDefineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(BaseSequenceDefineVM vm)
        {
            if (!ModelState.IsValid)
            {
                return FFResult().AlertAllErrors(ModelState);
                // return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoAddAsync();

                if (!ModelState.IsValid)
                {
                    return FFResult().AlertAllErrors(ModelState);
                    //vm.DoReInit();
                    //return PartialView("../BaseSequenceDefine/Create", vm);
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
        public async Task<ActionResult> Edit(BaseSequenceDefineVM vm)
        {
            if (!ModelState.IsValid)
            {
                return FFResult().AlertAllErrors(ModelState);
                //return PartialView(vm.FromView, vm);
            }
            else
            {
                await vm.DoEditAsync();
                if (!ModelState.IsValid)
                {
                    return FFResult().AlertAllErrors(ModelState);
                    //vm.DoReInit();
                    //return PartialView("../BaseSequenceDefine/Edit", vm);
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
        public ActionResult DoBatchEdit(BaseSequenceDefineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseSequenceDefineBatchVM>();
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
                return FFResult().RefreshGrid(CurrentWindowId).Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(BaseSequenceDefineImportVM vm, IFormCollection nouse)
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
    }
}