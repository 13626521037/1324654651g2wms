using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs;
using WMS.Model.InventoryManagement;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("其它入库单API")]
    [ApiController]
    [Route("api/InventoryOtherReceivement")]
    public partial class InventoryOtherReceivementApiController : BaseApiController
    {
        [ActionDescription("杂收单获取来源杂发单")]
        [HttpPost("GetMiscShipForMiscRcv")]
        public IActionResult GetMiscShipForMiscRcv(string docNo)
        {
            ReturnResult<GetMiscShipForMiscRcvResult> rr = new ReturnResult<GetMiscShipForMiscRcvResult>();
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
            InventoryOtherReceivementApiVM vm = Wtm.CreateVM<InventoryOtherReceivementApiVM>();
            rr.Entity = vm.GetMiscShipForMiscRcv(docNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            var ret = Ok(rr);
            return ret;
        }

        [ActionDescription("[PDA] 创建单据")]
        [HttpPost("CreateDoc")]
        public ActionResult CreateDoc(CreateMiscRcvPara para)
        {
            ModelState.Clear();
            ReturnResult rr = new ReturnResult();
            InventoryOtherReceivementApiVM vm = Wtm.CreateVM<InventoryOtherReceivementApiVM>();
            vm.CreateDoc(para);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
