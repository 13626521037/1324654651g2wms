using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryTransferOutDirect")]
    public partial class InventoryTransferOutDirectController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryTransferOutDirectVM vm)
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
                    return PartialView("../InventoryTransferOutDirect/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryTransferOutDirectVM vm)
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
                    return PartialView("../InventoryTransferOutDirect/Edit", vm);
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
        public ActionResult DoBatchEdit(InventoryTransferOutDirectBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryTransferOutDirectBatchVM>();
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
        public ActionResult Import(InventoryTransferOutDirectImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetInventoryTransferOutDirectDocTypes()
        {
            return JsonMore(DC.Set<InventoryTransferOutDirectDocType>().GetSelectListItems(Wtm, x => "【" + x.Organization.Code + "】" + x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetInventoryTransferOutDirectDocTypeByInventoryTransferOutDirectDocTypeId(List<string> id)
        {
            var rv = DC.Set<InventoryTransferOutDirectDocType>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name));
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryTransferOutDirectByInventoryTransferOutDirectDocType(List<string> id)
        {
            var rv = DC.Set<InventoryTransferOutDirect>().CheckIDs(id, x => x.DocTypeId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseOrganizations()
        {
            return JsonMore(DC.Set<BaseOrganization>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseOrganizationByBaseOrganizationId(List<string> id)
        {
            var rv = DC.Set<BaseOrganization>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name));
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryTransferOutDirectByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<InventoryTransferOutDirect>().CheckIDs(id, x => x.TransInOrganizationId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name));
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryTransferOutDirectByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<InventoryTransferOutDirect>().CheckIDs(id, x => x.TransInWhId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }



    }
}