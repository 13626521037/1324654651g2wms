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
using WMS.Model.PurchaseManagement;
using WMS.Util;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs;


namespace WMS.Controllers
{
    [Area("PurchaseManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("委外退料单Api")]
    [ApiController]
    [Route("api/PurchaseOutsourcingReturn")]
	public partial class PurchaseOutsourcingReturnApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<PurchaseOutsourcingReturnReturn>> rr = new ReturnResult<List<PurchaseOutsourcingReturnReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            rr.Entity = vm.GetList(type);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("扫码获取单据（收货）")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<PurchaseOutsourcingReturnReturn> rr = new ReturnResult<PurchaseOutsourcingReturnReturn>();
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
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            rr.Entity = vm.GetDoc(docNo);
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
            ReturnResult<PurchaseOutsourcingReturnReturn> rr = new ReturnResult<PurchaseOutsourcingReturnReturn>();
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
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            rr.Entity = vm.GetDocForApprove(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("创建单据（ERP调用）")]
        [HttpPost("Create")]
        public IActionResult Create(PurchaseOutsourcingReturn entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            vm.Create(entity);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("单据是否存在")]
        [HttpPost("IsDocExist")]
        public IActionResult IsDocExist(string id)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
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

        [ActionDescription("[PDA] 收货操作")]
        [HttpPost("Receiving")]
        public IActionResult Receiving(PurchaseOutsourcingReturnReceivingPara data)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            vm.Receiving(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("ApproveAndPutaway")]
        public IActionResult ApproveAndPutaway(PurchaseOutsourcingReturnApprovePara data)
        {
            ReturnResult rr = new ReturnResult();
            PurchaseOutsourcingReturnApiVM vm = Wtm.CreateVM<PurchaseOutsourcingReturnApiVM>();
            vm.ApproveAndPutaway(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
