using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;
using WMS.Util;
using WMS.ViewModel.SalesManagement.SalesShipVMs;


namespace WMS.Controllers
{
    [Area("SalesManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("出货单")]
    [ApiController]
    [Route("api/SalesShip")]
    public partial class SalesShipApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<SalesShipReturn>> rr = new ReturnResult<List<SalesShipReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            rr.Entity = vm.GetList(type);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("创建单据（ERP调用）")]
        [HttpPost("Create")]
        public IActionResult Create(SalesShip entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            vm.Create(entity);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("扫码获取单据")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<SalesShipReturn> rr = new ReturnResult<SalesShipReturn>();
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
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            rr.Entity = vm.GetDoc(docNo);
            if (rr.Entity != null)
            {
                vm.GetSuggestLocs(rr.Entity);   // 获取推荐库位
                vm.GetOffDetails(rr.Entity);   // 获取下架明细
            }
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            var ret = Ok(rr);
            return ret;
        }

        [ActionDescription("扫码获取单据（审核）")]
        [HttpPost("GetDocForApprove")]
        public IActionResult GetDocForApprove(string docNo)
        {
            ReturnResult<SalesShipReturn> rr = new ReturnResult<SalesShipReturn>();
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
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            rr.Entity = vm.GetDocForApprove(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("单据是否存在")]
        [HttpPost("IsDocExist")]
        public IActionResult IsDocExist(string id)
        {
            ReturnResult rr = new ReturnResult();
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            if (!vm.IsDocExist(id))
            {
                rr.SetFail("Doc not exist");
            }
            else
            {
                rr.SetSuccess(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 下架操作")]
        [HttpPost("SaveOffDetails")]
        public IActionResult SaveOffDetails(SalesShipDataPara data, bool autoSplit)
        {
            ReturnResult<List<InventorySplitSaveReturn>> rr = new ReturnResult<List<InventorySplitSaveReturn>>();
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            vm.SaveOffDetails(data, autoSplit);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            if (autoSplit)
            {
                rr.Entity = vm.AutoSplitResult;
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("Approve")]
        public IActionResult Approve(Guid? id)
        {
            ReturnResult rr = new ReturnResult();
            SalesShipApiVM vm = Wtm.CreateVM<SalesShipApiVM>();
            vm.Approve(id);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
