using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using WMS.Model.KnifeManagement;
using WMS.Model.BaseData;
using WMS.ViewModel.KnifeManagement.KnifeOperationVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.KnifeManagement.Controllers
{
    [Area("KnifeManagement")]
    [ActionDescription("_Model.KnifeOperation")]
    public partial class KnifeOperationController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(KnifeOperationVM vm)
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
                    return PartialView("../KnifeOperation/Create", vm);
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
        public async Task<ActionResult> Edit(KnifeOperationVM vm)
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
                    return PartialView("../KnifeOperation/Edit", vm);
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
        public ActionResult DoBatchEdit(KnifeOperationBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<KnifeOperationBatchVM>();
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
        public ActionResult Import(KnifeOperationImportVM vm, IFormCollection nouse)
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



        
        public ActionResult GetKnifes()
        {
            return JsonMore(DC.Set<Knife>().GetSelectListItems(Wtm, x => x.SerialNumber));
        }
        public ActionResult Select_GetKnifeByKnifeId(List<string> id)
        {
            var rv = DC.Set<Knife>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SerialNumber);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeOperationByKnife(List<string> id)
        {
            var rv = DC.Set<KnifeOperation>().CheckIDs(id, x => x.KnifeId).GetSelectListItems(Wtm, x => x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetBaseOperators()
        {
            //return JsonMore(DC.Set<BaseOperator>().GetSelectListItems(Wtm, x => x.Code.ToString().CodeCombinName(x.Name)));
            return JsonMore(DC.Set<BaseOperator>().GetSelectListItems(Wtm, x => x.Department.Organization.Name.CodeCombinName(x.Department.Name.CodeCombinName(x.Name))));

        }
        public ActionResult Select_GetBaseOperatorByBaseOperatorId(List<string> id)
        {
            var rv = DC.Set<BaseOperator>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeOperationByBaseOperator(List<string> id)
        {
            var rv = DC.Set<KnifeOperation>().CheckIDs(id, x => x.OperationById).GetSelectListItems(Wtm, x => x.ID.ToString());
            return JsonMore(rv);
        }


        public ActionResult GetFrameworkUsers()
        {
            return JsonMore(DC.Set<FrameworkUser>().GetSelectListItems(Wtm, x => x.Name));
        }
        public ActionResult Select_GetFrameworkUserByFrameworkUserId(List<string> id)
        {
            var rv = DC.Set<FrameworkUser>().CheckIDs(id).GetSelectListItems(Wtm, x => x.Name);
            return JsonMore(rv);
        }

        public ActionResult Select_GetKnifeOperationByFrameworkUser(List<string> id)
        {
            var rv = DC.Set<KnifeOperation>().CheckIDs(id, x => x.HandledById).GetSelectListItems(Wtm, x => x.ID.ToString());
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

        public ActionResult Select_GetKnifeOperationByBaseWhLocation(List<string> id)
        {
            var rv = DC.Set<KnifeOperation>().CheckIDs(id, x => x.WhLocationId).GetSelectListItems(Wtm, x => x.ID.ToString());
            return JsonMore(rv);
        }

    }
}