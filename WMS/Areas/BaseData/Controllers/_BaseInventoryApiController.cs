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
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.Model;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseInventory")]
    public partial class BaseInventoryController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(BaseInventoryVM vm)
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
                    return PartialView("../BaseInventory/Create", vm);
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
        public async Task<ActionResult> Edit(BaseInventoryVM vm)
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
                    return PartialView("../BaseInventory/Edit", vm);
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
        public ActionResult DoBatchEdit(BaseInventoryBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseInventoryBatchVM>();
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
        public ActionResult Import(BaseInventoryImportVM vm, IFormCollection nouse)
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




        //public ActionResult GetBaseItemMasters()
        //{
        //    // 此方法不能用，会卡死
        //    return JsonMore(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        //}
        //public ActionResult Select_GetBaseItemMasterByBaseItemMasterId(List<string> id)
        //{
        //    var rv = DC.Set<BaseItemMaster>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
        //    return JsonMore(rv);
        //}

        //public ActionResult Select_GetBaseInventoryByBaseItemMaster(List<string> id)
        //{
        //    var rv = DC.Set<BaseInventory>().CheckIDs(id, x => x.ItemMasterId).GetSelectListItems(Wtm, x => x.BatchNumber);
        //    return JsonMore(rv);
        //}


        public ActionResult GetBaseWhLocations()
        {
            return JsonMore(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Code));
        }
        public ActionResult Select_GetBaseWhLocationByBaseWhLocationId(List<string> id)
        {
            var rv = DC.Set<BaseWhLocation>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseInventoryByBaseWhLocation(List<string> id)
        {
            var rv = DC.Set<BaseInventory>().CheckIDs(id, x => x.WhLocationId).GetSelectListItems(Wtm,x=>x.BatchNumber);
            return JsonMore(rv);
        }

    }
}