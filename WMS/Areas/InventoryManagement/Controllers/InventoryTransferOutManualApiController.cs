using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using Newtonsoft.Json;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("手动调出单Api")]
    [ApiController]
    [Route("api/InventoryTransferOutManual")]
    public partial class InventoryTransferOutManualApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<InventoryTransferOutManualReturn>> rr = new ReturnResult<List<InventoryTransferOutManualReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
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
        public IActionResult Create(InventoryTransferOutManual entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
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
            ReturnResult<InventoryTransferOutManualReturn> rr = new ReturnResult<InventoryTransferOutManualReturn>();
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
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
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
            ReturnResult<InventoryTransferOutManualReturn> rr = new ReturnResult<InventoryTransferOutManualReturn>();
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
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
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
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
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
        public IActionResult SaveOffDetails(InventoryTransferOutManualSaveOffPara data)
        {
            ReturnResult rr = new ReturnResult();
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
            vm.SaveOffDetails(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("Approve")]
        public IActionResult Approve(Guid? id)
        {
            ReturnResult rr = new ReturnResult();
            InventoryTransferOutManualApiVM vm = Wtm.CreateVM<InventoryTransferOutManualApiVM>();
            vm.Approve(id);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
