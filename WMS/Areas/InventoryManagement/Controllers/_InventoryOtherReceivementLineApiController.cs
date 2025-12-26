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
using WMS.ViewModel.InventoryManagement.InventoryOtherReceivementLineVMs;
using WMS.Model;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryOtherReceivementLine")]
    public partial class InventoryOtherReceivementLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryOtherReceivementLineVM vm)
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
                    return PartialView("../InventoryOtherReceivementLine/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryOtherReceivementLineVM vm)
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
                    return PartialView("../InventoryOtherReceivementLine/Edit", vm);
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
        public ActionResult DoBatchEdit(InventoryOtherReceivementLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryOtherReceivementLineBatchVM>();
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
        public ActionResult Import(InventoryOtherReceivementLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetInventoryOtherReceivements()
        {
            return JsonMore(DC.Set<InventoryOtherReceivement>().GetSelectListItems(Wtm, x => x.ErpID));
        }
        public ActionResult Select_GetInventoryOtherReceivementByInventoryOtherReceivementId(List<string> id)
        {
            var rv = DC.Set<InventoryOtherReceivement>().CheckIDs(id).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherReceivementLineByInventoryOtherReceivement(List<string> id)
        {
            var rv = DC.Set<InventoryOtherReceivementLine>().CheckIDs(id, x => x.InventoryOtherReceivementId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetInventoryOtherShipLines()
        {
            return JsonMore(DC.Set<InventoryOtherShipLine>().GetSelectListItems(Wtm, x => x.ErpID));
        }
        public ActionResult Select_GetInventoryOtherShipLineByInventoryOtherShipLineId(List<string> id)
        {
            var rv = DC.Set<InventoryOtherShipLine>().CheckIDs(id).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherReceivementLineByInventoryOtherShipLine(List<string> id)
        {
            var rv = DC.Set<InventoryOtherReceivementLine>().CheckIDs(id, x => x.InventoryOtherShipLineId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }


        public ActionResult GetBaseItemMasters()
        {
            return JsonMore(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseItemMasterByBaseItemMasterId(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryOtherReceivementLineByBaseItemMaster(List<string> id)
        {
            var rv = DC.Set<InventoryOtherReceivementLine>().CheckIDs(id, x => x.ItemMasterId).GetSelectListItems(Wtm,x=>x.ErpID);
            return JsonMore(rv);
        }

    }
}