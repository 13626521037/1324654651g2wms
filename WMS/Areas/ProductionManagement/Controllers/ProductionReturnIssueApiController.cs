using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using Newtonsoft.Json;


namespace WMS.Controllers
{
    [Area("ProductionManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("生产退料单")]
    [ApiController]
    [Route("api/ProductionReturnIssue")]
    public partial class ProductionReturnIssueApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<ProductionReturnIssueReturn>> rr = new ReturnResult<List<ProductionReturnIssueReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
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
            ReturnResult<ProductionReturnIssueReturn> rr = new ReturnResult<ProductionReturnIssueReturn>();
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
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
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
            ReturnResult<ProductionReturnIssueReturn> rr = new ReturnResult<ProductionReturnIssueReturn>();
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
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
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
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
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

        [ActionDescription("创建单据（ERP调用）")]
        [HttpPost("Create")]
        public IActionResult Create(ProductionReturnIssue entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
            vm.Create(entity);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 收货操作")]
        [HttpPost("Receiving")]
        public IActionResult Receiving(ReceivingPara data)
        {
            ReturnResult rr = new ReturnResult();
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
            vm.Receiving(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("[PDA] 审核单据")]
        [HttpPost("ApproveAndPutaway")]
        public IActionResult ApproveAndPutaway(ReceivingApprovePara data)
        {
            ReturnResult rr = new ReturnResult();
            ProductionReturnIssueApiVM vm = Wtm.CreateVM<ProductionReturnIssueApiVM>();
            vm.ApproveAndPutaway(data);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
