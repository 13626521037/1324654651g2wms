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
using WMS.ViewModel.BaseData.BaseUserWhRelationVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseUserWhRelation")]
    public partial class BaseUserWhRelationController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(BaseUserWhRelationVM vm)
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
                    return PartialView("../BaseUserWhRelation/Create", vm);
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
        public async Task<ActionResult> Edit(BaseUserWhRelationVM vm)
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
                    return PartialView("../BaseUserWhRelation/Edit", vm);
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
        public ActionResult DoBatchEdit(BaseUserWhRelationBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseUserWhRelationBatchVM>();
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
        public ActionResult Import(BaseUserWhRelationImportVM vm, IFormCollection nouse)
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




        public ActionResult GetFrameworkUsers()
        {
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.ITCode.ToString().CodeCombinName(x.Name), x => x.ITCode));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name, x => x.ITCode.ToString());
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseUserWhRelationByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<BaseUserWhRelation>().CheckIDs(id, x => x.UserId).GetSelectListItems(Wtm, x => x.Memo);
            return JsonMore(rv);
        }


        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().Where(x => x.IsEffective == EffectiveEnum.Effective && x.IsValid).GetSelectListItems(Wtm, x => x.Organization.Code.CodeCombinName(x.Code.CodeCombinName(x.Name))));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseUserWhRelationByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<BaseUserWhRelation>().CheckIDs(id, x => x.WhId).GetSelectListItems(Wtm, x => x.Memo);
            return JsonMore(rv);
        }

    }
}