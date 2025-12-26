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
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs;
using WMS.Model;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryPalletVirtualLine")]
    public partial class InventoryPalletVirtualLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryPalletVirtualLineVM vm)
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
                    return PartialView("../InventoryPalletVirtualLine/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryPalletVirtualLineVM vm)
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
                    return PartialView("../InventoryPalletVirtualLine/Edit", vm);
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
        public ActionResult DoBatchEdit(InventoryPalletVirtualLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryPalletVirtualLineBatchVM>();
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
        public ActionResult Import(InventoryPalletVirtualLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetInventoryPalletVirtuals()
        {
            return JsonMore(DC.Set<InventoryPalletVirtual>().GetSelectListItems(Wtm, x => x.ID.ToString()));
        }
        public ActionResult Select_GetInventoryPalletVirtualByInventoryPalletVirtualId(List<string> id)
        {
            var rv = DC.Set<InventoryPalletVirtual>().CheckIDs(id).GetSelectListItems(Wtm, x => x.ID.ToString());
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryPalletVirtualLineByInventoryPalletVirtual(List<string> id)
        {
            var rv = DC.Set<InventoryPalletVirtualLine>().CheckIDs(id, x => x.InventoryPalletId).GetSelectListItems(Wtm,x=>x.Memo);
            return JsonMore(rv);
        }


        public ActionResult GetBaseInventorys()
        {
            return JsonMore(DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber));
        }
        public ActionResult Select_GetBaseInventoryByBaseInventoryId(List<string> id)
        {
            var rv = DC.Set<BaseInventory>().CheckIDs(id).GetSelectListItems(Wtm, x => x.BatchNumber);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryPalletVirtualLineByBaseInventory(List<string> id)
        {
            var rv = DC.Set<InventoryPalletVirtualLine>().CheckIDs(id, x => x.BaseInventoryId).GetSelectListItems(Wtm,x=>x.Memo);
            return JsonMore(rv);
        }

    }
}