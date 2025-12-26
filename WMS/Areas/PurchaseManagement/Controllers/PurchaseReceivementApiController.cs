using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using Newtonsoft.Json;


namespace WMS.Controllers
{
    [Area("PurchaseManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("采购收货单API")]
    [ApiController]
    [Route("api/PurchaseReceivement")]
    public partial class PurchaseReceivementApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<PurchaseReceivementReturn>> rr = new ReturnResult<List<PurchaseReceivementReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            rr.Entity = vm.GetList(type);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("扫码获取单据（检验）")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<PurchaseReceivementReturn> rr = new ReturnResult<PurchaseReceivementReturn>();
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
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            rr.Entity = vm.GetDoc(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("扫码获取单据（收货）")]
        [HttpPost("GetDocForReceive")]
        public IActionResult GetDocForReceive(string docNo)
        {
            ReturnResult<PurchaseReceivementReturn> rr = new ReturnResult<PurchaseReceivementReturn>();
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
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            rr.Entity = vm.GetDocForReceive(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("扫码获取单据（审核）")]
        [HttpPost("GetDocForApprove")]
        public IActionResult GetDocForApprove(string docNo)
        {
            ReturnResult<PurchaseReceivementReturn> rr = new ReturnResult<PurchaseReceivementReturn>();
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
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
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
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
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

        [ActionDescription("[PDA] 保存检验数据")]
        [HttpPost("SaveInspectionData")]
        public IActionResult SaveInspectionData(ReceivementSaveInspectionDataPara data)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            vm.SaveInspectionData(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 收货操作")]
        [HttpPost("Receiving")]
        public IActionResult Receiving(ReceivementReceivingPara data)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            vm.Receiving(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        //[ActionDescription("取消收货操作")]
        //[HttpPost("CancelReceive")]
        //public IActionResult CancelReceive(string id)
        //{
        //    ReturnResult rr = new ReturnResult();
        //    PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
        //    vm.CancelReceive(Guid.Parse(id));
        //    if (!ModelState.IsValid)
        //    {
        //        rr.SetFail(ModelState.GetErrorJson().GetFirstError());
        //    }
        //    return Ok(rr);
        //}

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("ApproveAndPutaway")]
        public IActionResult ApproveAndPutaway(ReceivementApprovePara data)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseReceivementApiVM vm = Wtm.CreateVM<PurchaseReceivementApiVM>();
            vm.ApproveAndPutaway(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
