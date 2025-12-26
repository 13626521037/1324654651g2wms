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
using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.Model;
using WMS.ViewModel;

namespace WMS.BaseData.Controllers
{
    [Area("BaseData")]
    [ActionDescription("_Model.BaseItemMaster")]
    public partial class BaseItemMasterController : BaseController
    {
        #region Create
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public async Task<ActionResult> Create(BaseItemMasterVM vm)
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
                    return PartialView("../BaseItemMaster/Create", vm);
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
        public async Task<ActionResult> Edit(BaseItemMasterVM vm)
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
                    return PartialView("../BaseItemMaster/Edit", vm);
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
            BaseItemMasterVM vm = Wtm.CreateVM<BaseItemMasterVM>();
            DateTime start = DateTime.Now;
            if(!string.IsNullOrEmpty(Code))
                vm.SyncData(Code);
            else
                vm.SyncDataByBatch();
            if (ModelState.IsValid)
            {
                DateTime end = DateTime.Now;
                return FFResult().CloseDialog().RefreshGrid()
                    .Alert($"同步成功<br/>新增：{vm.InsertQty}条<br/>修改：{vm.UpdateQty}条<br/>删除（失效）：未处理。料品同步暂无法处理已删除的物料<br/>耗时：{(int)((end - start).TotalSeconds)}秒");
            }
            else
            {
                return FFResult().Alert(ModelState.GetErrorJson().GetFirstError().Replace("'", "\""));
            }
        }


        #region BatchEdit

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(BaseItemMasterBatchVM vm, IFormCollection nouse)
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
            var vm = Wtm.CreateVM<BaseItemMasterBatchVM>();
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
        public ActionResult Import(BaseItemMasterImportVM vm, IFormCollection nouse)
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

        public ActionResult Select_GetBaseItemMasterByBaseOrganization(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.OrganizationId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }


        public ActionResult GetBaseItemCategorys()
        {
            return JsonMore(DC.Set<BaseItemCategory>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseItemCategoryByBaseItemCategoryId(List<string> id)
        {
            var rv = DC.Set<BaseItemCategory>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseItemMasterByBaseItemCategory(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.ItemCategoryId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }


        public ActionResult GetBaseUnits()
        {
            return JsonMore(DC.Set<BaseUnit>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseUnitByBaseUnitId(List<string> id)
        {
            var rv = DC.Set<BaseUnit>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseItemMasterByBaseUnit(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.StockUnitId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }



        public ActionResult GetBaseDepartments()
        {
            return JsonMore(DC.Set<BaseDepartment>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseDepartmentByBaseDepartmentId(List<string> id)
        {
            var rv = DC.Set<BaseDepartment>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseItemMasterByBaseDepartment(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.ProductionDeptId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
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

        public ActionResult Select_GetBaseItemMasterByBaseWareHouse(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.WhId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }


        public ActionResult GetBaseAnalysisTypes()
        {
            return JsonMore(DC.Set<BaseAnalysisType>().GetSelectListItems(Wtm, x => x.SourceSystemId));
        }
        public ActionResult Select_GetBaseAnalysisTypeByBaseAnalysisTypeId(List<string> id)
        {
            var rv = DC.Set<BaseAnalysisType>().CheckIDs(id).GetSelectListItems(Wtm, x => x.SourceSystemId);
            return JsonMore(rv);
        }

        public ActionResult Select_GetBaseItemMasterByBaseAnalysisType(List<string> id)
        {
            var rv = DC.Set<BaseItemMaster>().CheckIDs(id, x => x.AnalysisTypeId).GetSelectListItems(Wtm,x=>x.SourceSystemId);
            return JsonMore(rv);
        }

    }
}