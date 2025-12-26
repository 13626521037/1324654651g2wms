using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("盘点单API")]
    [ApiController]
    [Route("api/InventoryStockTaking")]
    public partial class InventoryStockTakingApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList()
        {
            ReturnResult<List<InventoryStockTakingReturn>> rr = new ReturnResult<List<InventoryStockTakingReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            rr.Entity = vm.GetList();
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("扫码获取盘点单")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<InventoryStockTakingReturn> rr = new ReturnResult<InventoryStockTakingReturn>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            if (string.IsNullOrEmpty(docNo))
            {
                rr.SetFail("单据号不能为空");
                return Ok(rr);
            }
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            rr.Entity = vm.GetDoc(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("扫码获取库位")]
        [HttpPost("GetLocation")]
        public IActionResult GetLocation(Guid? docId, string code)
        {
            ReturnResult<BaseReturn> rr = new ReturnResult<BaseReturn>();
            if (string.IsNullOrEmpty(code))
            {
                rr.SetFail("库位编码不能为空");
                return Ok(rr);
            }
            if (docId == null || docId == Guid.Empty)
            {
                rr.SetFail("盘点单据ID不能为空");
                return Ok(rr);
            }
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            rr.Entity = vm.GetLocation(docId, code);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 盘点扫码")]
        [HttpPost("StockTakeScan")]
        public IActionResult StockTakeScan(string sn, Guid? locationId, string barCode)
        {
            ReturnResult<StockTakeScanReturn> rr = new ReturnResult<StockTakeScanReturn>();
            if (string.IsNullOrEmpty(sn))
            {
                rr.SetFail("序列号不能为空");
                return Ok(rr);
            }
            if (locationId == null || locationId == Guid.Empty)
            {
                rr.SetFail("库位ID不能为空");
                return Ok(rr);
            }
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            rr.Entity = vm.StockTakeScan(sn, barCode, locationId);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("清除盘点数据")]
        [HttpPost("StockTakeClear")]
        public IActionResult StockTakeClear(Guid? lineId)
        {
            ReturnResult rr = new ReturnResult();
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            vm.StockTakeClear(lineId);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("清除库位盘点数据")]
        [HttpPost("LocationStockTakeClear")]
        public IActionResult LocationStockTakeClear(Guid? docId, Guid? locationId)
        {
            ReturnResult rr = new ReturnResult();
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            vm.LocationStockTakeClear(docId, locationId);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("获取盘点单行数据")]
        [HttpPost("GetLines")]
        public IActionResult GetLines(Guid? docId, Guid? locationId, GainLossStatusEnum? status, int pageNum, int pageSize)
        {
            ReturnResult<List<InventoryStockTakingLineReturn>> rr = new ReturnResult<List<InventoryStockTakingLineReturn>>();
            InventoryStockTakingApiVM vm = Wtm.CreateVM<InventoryStockTakingApiVM>();
            rr.Entity = vm.GetLines(docId, locationId, status, pageNum, pageSize);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
