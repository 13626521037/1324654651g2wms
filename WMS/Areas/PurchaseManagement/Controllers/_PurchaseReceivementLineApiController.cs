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
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;
using WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs;
using WMS.Model;

namespace WMS.PurchaseManagement.Controllers
{
    [Area("PurchaseManagement")]
    [ActionDescription("_Model.PurchaseReceivementLine")]
    public partial class PurchaseReceivementLineController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(PurchaseReceivementLineVM vm)
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
                    return PartialView("../PurchaseReceivementLine/Create", vm);
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
        public async Task<ActionResult> Edit(PurchaseReceivementLineVM vm)
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
                    return PartialView("../PurchaseReceivementLine/Edit", vm);
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
        public ActionResult DoBatchEdit(PurchaseReceivementLineBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<PurchaseReceivementLineBatchVM>();
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
        public ActionResult Import(PurchaseReceivementLineImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetPurchaseReceivements()
        {
            return JsonMore(DC.Set<PurchaseReceivement>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetPurchaseReceivementByPurchaseReceivementId(List<string> id)
        {
            var rv = DC.Set<PurchaseReceivement>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetPurchaseReceivementLineByPurchaseReceivement(List<string> id)
        {
            var rv = DC.Set<PurchaseReceivementLine>().CheckIDs(id, x => x.PurchaseReceivementId).GetSelectListItems(Wtm,x=>x.ID.ToString());
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

        public ActionResult Select_GetPurchaseReceivementLineByBaseItemMaster(List<string> id)
        {
            var rv = DC.Set<PurchaseReceivementLine>().CheckIDs(id, x => x.ItemMasterId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseWareHouseByBaseWareHouseId(List<string> id)
        {
            var rv = DC.Set<BaseWareHouse>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetPurchaseReceivementLineByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<PurchaseReceivementLine>().CheckIDs(id, x => x.WareHouseId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetFrameworkUsers()
        {
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.Name, x => x.ITCode.ToString()));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name, x => x.ITCode.ToString());
            return JsonMore(rv);
        }

        public ActionResult Select_GetPurchaseReceivementLineByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<PurchaseReceivementLine>().CheckIDs(id, x => x.InspectorId).GetSelectListItems(Wtm,x=>x.ID.ToString());
            return JsonMore(rv);
        }

    }
}