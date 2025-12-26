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
using WMS.Model.PurchaseManagement;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.PurchaseManagement.Controllers
{
    [Area("PurchaseManagement")]
    [ActionDescription("_Model.PurchaseOutsourcingReturn")]
    public partial class PurchaseOutsourcingReturnController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(PurchaseOutsourcingReturnVM vm)
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
                    return PartialView("../PurchaseOutsourcingReturn/Create", vm);
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
        public async Task<ActionResult> Edit(PurchaseOutsourcingReturnVM vm)
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
                    return PartialView("../PurchaseOutsourcingReturn/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion

        #region Delete

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<PurchaseOutsourcingReturnVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                //return PartialView(vm);
                return FFResult().Message(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }

        #endregion        


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(PurchaseOutsourcingReturnBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<PurchaseOutsourcingReturnBatchVM>();
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
        public ActionResult Import(PurchaseOutsourcingReturnImportVM vm, IFormCollection nouse)
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

        public ActionResult Select_GetPurchaseOutsourcingReturnByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<PurchaseOutsourcingReturn>().CheckIDs(id, x => x.OrganizationId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }


        public ActionResult GetBaseSuppliers()
        {
            return JsonMore(DC.Set<BaseSupplier>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseSupplierByBaseSupplierId(List<string> id)
        {
            var rv = DC.Set<BaseSupplier>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetPurchaseOutsourcingReturnByBaseSupplier(List<string> id)
        {
            var rv = DC.Set<PurchaseOutsourcingReturn>().CheckIDs(id, x => x.SupplierId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }

    }
}