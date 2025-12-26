using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryTransferOutDirectDocType")]
    public partial class InventoryTransferOutDirectDocTypeController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryTransferOutDirectDocTypeVM vm)
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
                    return PartialView("../InventoryTransferOutDirectDocType/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryTransferOutDirectDocTypeVM vm)
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
                    return PartialView("../InventoryTransferOutDirectDocType/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion

        [ActionDescription("同步数据")]
        [HttpPost]
        public ActionResult SyncData(string Code)
        {
            InventoryTransferOutDirectDocTypeVM vm = Wtm.CreateVM<InventoryTransferOutDirectDocTypeVM>();
            DateTime start = DateTime.Now;
            vm.SyncData(Code);
            if (ModelState.IsValid)
            {
                DateTime end = DateTime.Now;
                return FFResult().CloseDialog().RefreshGrid()
                    .Alert($"同步成功<br/>新增：{vm.InsertQty}条<br/>修改：{vm.UpdateQty}条<br/>删除（失效）：{vm.DeleteQty}条<br/>耗时：{(int)((end - start).TotalSeconds)}秒");
            }
            else
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
            }
        }


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(InventoryTransferOutDirectDocTypeBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryTransferOutDirectDocTypeBatchVM>();
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
        public ActionResult Import(InventoryTransferOutDirectDocTypeImportVM vm, IFormCollection nouse)
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




        public ActionResult GetBaseOrganizations()
        {
            return JsonMore(DC.Set<BaseOrganization>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseOrganizationByBaseOrganizationId(List<string> id)
        {
            var rv = DC.Set<BaseOrganization>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryTransferOutDirectDocTypeByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<InventoryTransferOutDirectDocType>().CheckIDs(id, x => x.OrganizationId).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

    }
}