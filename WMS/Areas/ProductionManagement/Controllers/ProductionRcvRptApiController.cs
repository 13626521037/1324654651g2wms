using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.ProductionManagement;
using WMS.Util;
using WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs;


namespace WMS.Controllers
{
    [Area("ProductionManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("生产报工单")]
    [ApiController]
    [Route("api/ProductionRcvRpt")]
	public partial class ProductionRcvRptApiController : BaseApiController
    {
        [ActionDescription("获取MO")]
        [HttpPost("GetRcvRptMo")]
        public IActionResult GetRcvRptMo(string moDocNo)
        {
            ReturnResult<ProductionRcvRptMO> rr = new ReturnResult<ProductionRcvRptMO>();
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
                return Ok(rr);
            }
            if (string.IsNullOrEmpty(moDocNo))
            {
                rr.SetFail("生产订单号不能为空");
                return Ok(rr);
            }
            ProductionRcvRptApiVM vm = Wtm.CreateVM<ProductionRcvRptApiVM>();
            rr.Entity = vm.GetRcvRptMo(moDocNo);
            if (!ModelState.IsValid)
            {
                rr.Entity = null;
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }

        [ActionDescription("创建单据（ERP调用）")]
        [HttpPost("CreateRcvRpt")]
        public IActionResult CreateRcvRpt(ProductionRcvRpt entity)
        {
            ReturnResult rr = new ReturnResult();
            if (entity == null)
            {
                rr.SetFail("参数不能为空");
                return Ok(rr);
            }
            ProductionRcvRptApiVM vm = Wtm.CreateVM<ProductionRcvRptApiVM>();
            vm.CreateRcvRpt(entity);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
