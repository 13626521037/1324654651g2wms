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
using WMS.Model.SalesManagement;
using WMS.Util;
using WMS.ViewModel.SalesManagement.SalesRMAVMs;


namespace WMS.Controllers
{
    [Area("SalesManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("退回处理Api")]
    [ApiController]
    [Route("api/SalesRMA")]
    public partial class SalesRMAApiController : BaseApiController
    {
        [ActionDescription("获取单据列表")]
        [HttpPost("GetList")]
        public IActionResult GetList(int type)
        {
            ReturnResult<List<SalesRMAReturn>> rr = new ReturnResult<List<SalesRMAReturn>>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            SalesRMAApiVM vm = Wtm.CreateVM<SalesRMAApiVM>();
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
        public IActionResult Create(SalesRMA entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            SalesRMAApiVM vm = Wtm.CreateVM<SalesRMAApiVM>();
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
            SalesRMAApiVM vm = Wtm.CreateVM<SalesRMAApiVM>();
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

        [ActionDescription("扫码获取单据")]
        [HttpPost("GetDoc")]
        public IActionResult GetDoc(string docNo)
        {
            ReturnResult<SalesRMAReturn> rr = new ReturnResult<SalesRMAReturn>();
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
            SalesRMAApiVM vm = Wtm.CreateVM<SalesRMAApiVM>();
            rr.Entity = vm.GetDoc(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            var ret = Ok(rr);
            return ret;
        }
    }
}
