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
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseDepartment")]
    public partial class BaseDepartmentController : BaseController
    {
        #region Create
        //[HttpPost]
        //[ActionDescription("Sys.Create")]
        //public async Task<ActionResult> Create(BaseDepartmentVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
                
        //        return PartialView(vm.FromView, vm);
        //    }
        //    else
        //    {
        //        await vm.DoAddAsync();
                
        //        if (!ModelState.IsValid)
        //        {
                    
        //            vm.DoReInit();
        //            return PartialView("../BaseDepartment/Create", vm);
        //        }
        //        else
        //        {
        //            return FFResult().CloseDialog().RefreshGrid();
        //        }
        //    }
        //}
        #endregion

        #region Edit
       
        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public async Task<ActionResult> Edit(BaseDepartmentVM vm)
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
                    return PartialView("../BaseDepartment/Edit", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(CurrentWindowId);
                }
            }
        }
        #endregion

        [ActionDescription("同步数据")]
        [HttpPost]
        public ActionResult SyncData(string Code)
        {
            BaseDepartmentVM vm = Wtm.CreateVM<BaseDepartmentVM>();
            vm.SyncData(Code);
            if (ModelState.IsValid)
            {
                return FFResult().CloseDialog().RefreshGrid()
                    .Alert($"同步成功。新增：{vm.InsertQty}条，修改：{vm.UpdateQty}条，删除（失效）：{vm.DeleteQty}条");
            }
            else
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError().Replace("'", "\""));
            }
        }


        #region BatchEdit

        //[HttpPost]
        //[ActionDescription("Sys.BatchEdit")]
        //public ActionResult DoBatchEdit(BaseDepartmentBatchVM vm, IFormCollection nouse)
        //{
        //    if (!ModelState.IsValid || !vm.DoBatchEdit())
        //    {
        //        return PartialView(vm.FromView, vm);
        //    }
        //    else
        //    {
        //        return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
        //    }
        //}
        #endregion

        #region BatchDelete
        [HttpPost]
        //[ActionDescription("Sys.BatchDelete")]
        //public ActionResult BatchDelete(string[] ids)
        //{
        //    var vm = Wtm.CreateVM<BaseDepartmentBatchVM>();
        //    if (ids != null && ids.Length > 0)
        //    {
        //        vm.Ids = ids;
        //    }
        //    else
        //    {
        //        return Ok();
        //    }
        //    if (!ModelState.IsValid || !vm.DoBatchDelete())
        //    {
        //        return FFResult().Alert(ModelState.GetErrorJson().GetFirstError());
        //    }
        //    else
        //    {
        //        return FFResult().RefreshGrid(CurrentWindowId).Alert(Localizer["Sys.BatchDeleteSuccess",vm.Ids.Length]);
        //    }
        //}
        #endregion
      
        #region Import
        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(BaseDepartmentImportVM vm, IFormCollection nouse)
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

        public ActionResult Select_GetBaseDepartmentByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<BaseDepartment>().CheckIDs(id, x => x.OrganizationId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }

    }
}