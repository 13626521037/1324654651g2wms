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
using WMS.Model.KnifeManagement;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using WMS.Model;
using WMS.ViewModel;
using WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs;

namespace WMS.KnifeManagement.Controllers
{
    [Area("KnifeManagement")]
    [ActionDescription("_Model.Knife")]
    public partial class KnifeController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(KnifeVM vm)
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
                    return PartialView("../Knife/Create", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        #endregion

        #region Edit
       
        [ActionDescription("普通修改")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult EditInStockStatus(KnifeVM vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return PartialView(vm.FromView, vm);
                }
                else
                {
                    vm.Entity.RowVersion = DC.Set<Knife>().Where(x => x.ID == vm.Entity.ID).Select(x => x.RowVersion).FirstOrDefault();
                    vm.Entity.LastOperationDate = DateTime.Now;
                    vm.DoEdit();
                    if (!ModelState.IsValid)
                    {

                        vm.DoReInit();
                        return PartialView("../Knife/EditInStockStatus", vm);
                    }
                    else
                    {
                        return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                    }
                }
            }
            catch (Exception ex )
            {
                return FFResult().Alert(ex.Message);
            }
            
        }
        #endregion


        #region EditKnifeStatus
        [ActionDescription("修改刀具状态")]
        [HttpPost]
        public ActionResult EditKnifeStatus(KnifeVM vm)
        {
            var tran = DC.Database.BeginTransaction();
            ModelState.Clear();
            try
            {

                if (!ModelState.IsValid)
                {
                    return PartialView(vm.FromView, vm);
                }
                else
                {
                    vm.DoEditKnifeStatus();//取原来的刀的状态记下来  取当前vm的状态记下来 调用doedit方法  建一条操作记录
                    if (!ModelState.IsValid)
                    {

                        vm.DoReInit();
                        return FFResult().RefreshGrid().Alert(ModelState.GetErrorJson().GetFirstError());
                    }
                    else
                    {
                        tran.Commit();
                        return FFResult().RefreshGrid().CloseDialog();
                    }
                }

            }
            catch
            {
                tran.Rollback();
                return FFResult().RefreshGrid().Alert(ModelState.GetErrorJson().GetFirstError());
            }

        }
        #endregion



        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(KnifeBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<KnifeBatchVM>();
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
        public ActionResult Import(KnifeImportVM vm, IFormCollection nouse)
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
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.ITCode.ToString().CodeCombinName(x.Name)));
        }
        public ActionResult GetBaseOperators()
        {
            return JsonMore(DC.Set<BaseOperator>().GetSelectListItems(Wtm, x => x.Department.Organization.Name.CodeCombinName(x.Department.Name.CodeCombinName(x.Name))));

            //return JsonMore(DC.Set<BaseOperator>().GetSelectListItems(Wtm, x => x.Department.Organization.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id, x => x.HandledById).GetSelectListItems(Wtm,x=>x.SerialNumber);
            return JsonMore(rv);
        }


        public ActionResult GetBaseWhLocations()
        {
            return JsonMore(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult GetBaseWareHouses()
        {
            return JsonMore(DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, x => x.Code.CodeCombinName(x.Name)));
        }
        public ActionResult Select_GetBaseWhLocationByBaseWhLocationId(List<string> id)
        {
            var rv = DC.Set<BaseWhLocation>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeByBaseWhLocation(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id, x => x.WhLocationId).GetSelectListItems(Wtm,x=>x.SerialNumber);
            return JsonMore(rv);
        }


        public ActionResult GetBaseItemMasters()
        {
                return JsonMore(DC.Set<BaseItemMaster>().Where(x=>x.Code.StartsWith("17")).GetSelectListItems(Wtm, x => x.Code));
        }

        public ActionResult Select_GetBaseItemMasterByBaseItemMasterId(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Code);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeByBaseItemMaster(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id, x => x.ItemMasterId).GetSelectListItems(Wtm,x=>x.SerialNumber);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseOperatorByBaseOperatorId(List<string> id)
        {
            var rv = DC.Set<BaseOperator>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeByBaseOperator(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id, x => x.CurrentCheckOutById).GetSelectListItems(Wtm, x => x.SerialNumber);
            return JsonMore(rv);
        }
    }
}