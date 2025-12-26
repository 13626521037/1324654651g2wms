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
using WMS.ViewModel.InventoryManagement.InventoryStockTakingLineVMs;
using WMS.Model;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventoryStockTakingLine")]
    public partial class InventoryStockTakingLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventoryStockTakingLineVM vm)
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
                    return PartialView("../InventoryStockTakingLine/Create", vm);
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
        public async Task<ActionResult> Edit(InventoryStockTakingLineVM vm)
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
                    return PartialView("../InventoryStockTakingLine/Edit", vm);
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
        public ActionResult DoBatchEdit(InventoryStockTakingLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventoryStockTakingLineBatchVM>();
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
        public ActionResult Import(InventoryStockTakingLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetInventoryStockTakings()
        {
            return JsonMore(DC.Set<InventoryStockTaking>().GetSelectListItems(Wtm, x => x.ErpID));
        }
        public ActionResult Select_GetInventoryStockTakingByInventoryStockTakingId(List<string> id)
        {
            var rv = DC.Set<InventoryStockTaking>().CheckIDs(id).GetSelectListItems(Wtm, x => x.ErpID);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryStockTakingLineByInventoryStockTaking(List<string> id)
        {
            var rv = DC.Set<InventoryStockTakingLine>().CheckIDs(id, x => x.StockTakingId).GetSelectListItems(Wtm,x=>x.SerialNumber);
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

        public ActionResult Select_GetInventoryStockTakingLineByBaseInventory(List<string> id)
        {
            var rv = DC.Set<InventoryStockTakingLine>().CheckIDs(id, x => x.InventoryId).GetSelectListItems(Wtm,x=>x.SerialNumber);
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

        public ActionResult Select_GetInventoryStockTakingLineByBaseItemMaster(List<string> id)
        {
            var rv = DC.Set<InventoryStockTakingLine>().CheckIDs(id, x => x.ItemMasterId).GetSelectListItems(Wtm,x=>x.SerialNumber);
            return JsonMore(rv);
        }


        public ActionResult GetBaseWhLocations()
        {
            return JsonMore(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Code));
        }
        public ActionResult Select_GetBaseWhLocationByBaseWhLocationId(List<string> id)
        {
            var rv = DC.Set<BaseWhLocation>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventoryStockTakingLineByBaseWhLocation(List<string> id)
        {
            var rv = DC.Set<InventoryStockTakingLine>().CheckIDs(id, x => x.LocationId).GetSelectListItems(Wtm,x=>x.SerialNumber);
            return JsonMore(rv);
        }

    }
}