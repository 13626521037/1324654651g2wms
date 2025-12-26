using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("组托单API")]
    [ApiController]
    [Route("api/InventoryPalletVirtual")]
    public partial class InventoryPalletVirtualApiController : BaseApiController
    {
        #region 系统自动生成的代码（全部注释掉）

        //[ActionDescription("Sys.Search")]
        //[HttpPost("Search")]
        //public IActionResult Search(InventoryPalletVirtualApiSearcher searcher)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var vm = Wtm.CreateVM<InventoryPalletVirtualApiListVM>(passInit: true);
        //        vm.Searcher = searcher;
        //        return Content(vm.GetJson());
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //}

        //[ActionDescription("Sys.Get")]
        //[HttpGet("{id}")]
        //public InventoryPalletVirtualApiVM Get(string id)
        //{
        //    var vm = Wtm.CreateVM<InventoryPalletVirtualApiVM>(id);
        //    return vm;
        //}

        //[ActionDescription("Sys.Create")]
        //[HttpPost("Add")]
        //public IActionResult Add(InventoryPalletVirtualApiVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        vm.DoAdd();
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState.GetErrorJson());
        //        }
        //        else
        //        {
        //            return Ok(vm.Entity);
        //        }
        //    }

        //}

        //[ActionDescription("Sys.Edit")]
        //[HttpPut("Edit")]
        //public IActionResult Edit(InventoryPalletVirtualApiVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        vm.DoEdit(false);
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState.GetErrorJson());
        //        }
        //        else
        //        {
        //            return Ok(vm.Entity);
        //        }
        //    }
        //}

        //[HttpPost("BatchDelete")]
        //[ActionDescription("Sys.Delete")]
        //public IActionResult BatchDelete(string[] ids)
        //{
        //    var vm = Wtm.CreateVM<InventoryPalletVirtualApiBatchVM>();
        //    if (ids != null && ids.Count() > 0)
        //    {
        //        vm.Ids = ids;
        //    }
        //    else
        //    {
        //        return Ok();
        //    }
        //    if (!ModelState.IsValid || !vm.DoBatchDelete())
        //    {
        //        return BadRequest(ModelState.GetErrorJson());
        //    }
        //    else
        //    {
        //        return Ok(ids.Count());
        //    }
        //}


        //[ActionDescription("Sys.Export")]
        //[HttpPost("ExportExcel")]
        //public IActionResult ExportExcel(InventoryPalletVirtualApiSearcher searcher)
        //{
        //    var vm = Wtm.CreateVM<InventoryPalletVirtualApiListVM>();
        //    vm.Searcher = searcher;
        //    vm.SearcherMode = ListVMSearchModeEnum.Export;
        //    return vm.GetExportData();
        //}

        //[ActionDescription("Sys.CheckExport")]
        //[HttpPost("ExportExcelByIds")]
        //public IActionResult ExportExcelByIds(string[] ids)
        //{
        //    var vm = Wtm.CreateVM<InventoryPalletVirtualApiListVM>();
        //    if (ids != null && ids.Count() > 0)
        //    {
        //        vm.Ids = new List<string>(ids);
        //        vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
        //    }
        //    return vm.GetExportData();
        //}

        //[ActionDescription("Sys.DownloadTemplate")]
        //[HttpGet("GetExcelTemplate")]
        //public IActionResult GetExcelTemplate()
        //{
        //    var vm = Wtm.CreateVM<InventoryPalletVirtualApiImportVM>();
        //    var qs = new Dictionary<string, string>();
        //    foreach (var item in Request.Query.Keys)
        //    {
        //        qs.Add(item, Request.Query[item]);
        //    }
        //    vm.SetParms(qs);
        //    var data = vm.GenerateTemplate(out string fileName);
        //    return File(data, "application/vnd.ms-excel", fileName);
        //}

        //[ActionDescription("Sys.Import")]
        //[HttpPost("Import")]
        //public ActionResult Import(InventoryPalletVirtualApiImportVM vm)
        //{
        //    if (vm != null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
        //    {
        //        return BadRequest(vm.GetErrorJson());
        //    }
        //    else
        //    {
        //        return Ok(vm?.EntityList?.Count ?? 0);
        //    }
        //}


        //[HttpGet("GetBaseWhLocations")]
        //public ActionResult GetBaseWhLocations()
        //{
        //    return Ok(DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, x => x.Name));
        //}

        #endregion

        /// <summary>
        /// 根据托盘码获取托盘信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost("GetDataByCode")]
        [ActionDescription("根据托盘码获取托盘信息")]
        public ActionResult GetDataByCode(string code)
        {
            ReturnResult<InventoryPalletVirtualReturn> rr = new ReturnResult<InventoryPalletVirtualReturn>();
            if (string.IsNullOrEmpty(code))
            {
                rr.SetFail("托盘码不能为空");
                return Ok(rr);
            }
            var vm = Wtm.CreateVM<InventoryPalletVirtualApiVM>();
            rr.Entity = vm.GetDataByCode(code);
            if (rr.Entity == null)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        /// <summary>
        /// 保存托盘信息（增、改）
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [ActionDescription("[PDA] 保存托盘信息（增、改）")]
        public ActionResult Save(InventoryPalletVirtual data)
        {
            ModelState.Clear();
            ReturnResult rr = new ReturnResult();
            InventoryPalletVirtualApiVM vm = Wtm.CreateVM<InventoryPalletVirtualApiVM>();
            vm.Save(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            return Ok(rr);
        }
    }
}
