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
using WMS.ViewModel.InventoryManagement.InventorySplitSingleLineVMs;
using WMS.Model;

namespace WMS.InventoryManagement.Controllers
{
    [Area("InventoryManagement")]
    [ActionDescription("_Model.InventorySplitSingleLine")]
    public partial class InventorySplitSingleLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(InventorySplitSingleLineVM vm)
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
                    return PartialView("../InventorySplitSingleLine/Create", vm);
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
        public async Task<ActionResult> Edit(InventorySplitSingleLineVM vm)
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
                    return PartialView("../InventorySplitSingleLine/Edit", vm);
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
        public ActionResult DoBatchEdit(InventorySplitSingleLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<InventorySplitSingleLineBatchVM>();
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
        public ActionResult Import(InventorySplitSingleLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetInventorySplitSingles()
        {
            return JsonMore(DC.Set<InventorySplitSingle>().GetSelectListItems(Wtm, x => x.DocNo));
        }
        public ActionResult Select_GetInventorySplitSingleByInventorySplitSingleId(List<string> id)
        {
            var rv = DC.Set<InventorySplitSingle>().CheckIDs(id).GetSelectListItems(Wtm, x => x.DocNo);
            return JsonMore(rv);
        }

        public ActionResult Select_GetInventorySplitSingleLineByInventorySplitSingle(List<string> id)
        {
            var rv = DC.Set<InventorySplitSingleLine>().CheckIDs(id, x => x.SplitSingleId).GetSelectListItems(Wtm,x=>x.Memo);
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

        public ActionResult Select_GetInventorySplitSingleLineByBaseInventory(List<string> id)
        {
            var rv = DC.Set<InventorySplitSingleLine>().CheckIDs(id, x => x.NewInvId).GetSelectListItems(Wtm,x=>x.Memo);
            return JsonMore(rv);
        }

    }
}