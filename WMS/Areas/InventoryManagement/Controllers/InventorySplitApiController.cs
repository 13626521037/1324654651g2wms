using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventorySplitVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("库存拆分单")]
    [ApiController]
    [Route("api/InventorySplit")]
    public partial class InventorySplitApiController : BaseApiController
    {
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
        public IActionResult Search(InventorySplitApiSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<InventorySplitApiListVM>(passInit: true);
                vm.Searcher = searcher;
                return Content(vm.GetJson());
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public InventorySplitApiVM Get(string id)
        {
            var vm = Wtm.CreateVM<InventorySplitApiVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(InventorySplitApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(InventorySplitApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

        [HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<InventorySplitApiBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(InventorySplitApiSearcher searcher)
        {
            var vm = Wtm.CreateVM<InventorySplitApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<InventorySplitApiListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<InventorySplitApiImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(InventorySplitApiImportVM vm)
        {
            if (vm != null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


        [HttpGet("GetBaseInventorys")]
        public ActionResult GetBaseInventorys()
        {
            return Ok(DC.Set<BaseInventory>().GetSelectListItems(Wtm, x => x.BatchNumber));
        }

        [ActionDescription("[PDA] 保存库存拆分单")]
        [HttpPost("Save")]
        public ActionResult Save(InventorySplit para)
        {
            ModelState.Clear(); // 未传单号会报错，所以先清除ModelState
            ReturnResult<InventorySplitSaveReturn> rr = new ReturnResult<InventorySplitSaveReturn>();
            var vm = Wtm.CreateVM<InventorySplitApiVM>();
            vm.Save(para);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                rr.Msg = "";
                rr.Entity = vm.SplitBarcode;
            }
            return Ok(rr);
        }
    }
}
