using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("BaseData")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("条码表")]
    [ApiController]
    [Route("api/BaseBarCode")]
    public partial class BaseBarCodeApiController : BaseApiController
    {
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
        public IActionResult Search(BaseBarCodeApiSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<BaseBarCodeApiListVM>(passInit: true);
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
        public BaseBarCodeApiVM Get(string id)
        {
            var vm = Wtm.CreateVM<BaseBarCodeApiVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        public IActionResult Add(BaseBarCodeApiVM vm)
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
        public IActionResult Edit(BaseBarCodeApiVM vm)
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
            var vm = Wtm.CreateVM<BaseBarCodeApiBatchVM>();
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
        public IActionResult ExportExcel(BaseBarCodeApiSearcher searcher)
        {
            var vm = Wtm.CreateVM<BaseBarCodeApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<BaseBarCodeApiListVM>();
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
            var vm = Wtm.CreateVM<BaseBarCodeApiImportVM>();
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
        public ActionResult Import(BaseBarCodeApiImportVM vm)
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


        [HttpGet("GetBaseItemMasters")]
        public ActionResult GetBaseItemMasters()
        {
            return Ok(DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, x => x.Name));
        }

        [ActionDescription("创建一个条码")]
        [HttpPost("CreateBarCode")]
        [Public]
        public ActionResult CreateBarCode(bool onlyCode ,string itemCode, decimal qty, string docNo, int doLineNo, string orgCode, int sourceType)  // sourceType: 1-外购；3-自制
        {
            ReturnResult<string> rr = new ReturnResult<string>();
            var vm = Wtm.CreateVM<BaseBarCodeVM>();
            string code = vm.CreateBarCode(onlyCode, itemCode, qty, docNo, doLineNo, orgCode, sourceType);
            if (ModelState.IsValid)
            {
                rr.Entity = code;
                return Ok(rr);
            }
            else
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }
        
        [ActionDescription("获取一个未使用但已存在的条码")]
        [HttpPost("GetUnusedExistingBarcode")]
        public ActionResult GetUnusedExistingBarcode(string code)
        {
            ReturnResult<BaseBarCode> rr = new ReturnResult<BaseBarCode>();
            var vm = Wtm.CreateVM<BaseBarCodeVM>();
            var barCode = vm.GetUnusedExistingBarcode(code);
            if (ModelState.IsValid)
            {
                rr.Entity = barCode;
                return Ok(rr);
            }
            else
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        [ActionDescription("调入单扫码获取条码信息")]
        [HttpPost("GetBarcodeForTransferIn")]
        public ActionResult GetBarcodeForTransferIn(string code)
        {
            ReturnResult<BaseBarCode> rr = new ReturnResult<BaseBarCode>();
            var vm = Wtm.CreateVM<BaseBarCodeVM>();
            var barCode = vm.GetBarcodeForTransferIn(code);
            if (ModelState.IsValid)
            {
                rr.Entity = barCode;
                return Ok(rr);
            }
            else
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }

        [ActionDescription("SRMK创建多个条码")]
        [HttpPost("SRMCreateBarCodes")]
        [Public]
        public ActionResult SRMCreateBarCodes(SRMCreateBarCodesInputInfo info)  // sourceType: 1-外购；3-自制
        {
            ReturnResult<List<SRMCreateBarCodesReturnInfo>> rr = new ReturnResult<List<SRMCreateBarCodesReturnInfo>>();
            var vm = Wtm.CreateVM<BaseBarCodeVM>();
            List< SRMCreateBarCodesReturnInfo> barcodes = vm.SRMCreateBarCodes(info.itemCode, info.qty, info.docNo, info.docLineNo, info.orgCode, info.sourceType, info.entity, info.batchNumber, info.seiban,info.srmUserName);
            if (ModelState.IsValid)
            {
                rr.Entity = barcodes;
                return Ok(rr);
            }
            else
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
        }


    }
    public class SRMCreateBarCodesInputInfo
    {
        public string itemCode { set; get; }
        public List<decimal> qty { set; get; }
        public string docNo { set; get; }
        public int docLineNo { set; get; }
        public string orgCode { set; get; }
        public int sourceType { set; get; }
        public BaseBarCode entity { set; get; }
        public string batchNumber { set; get; }
        public string seiban { set; get; }
        public string srmUserName { set; get; }
        

    }
    


}

