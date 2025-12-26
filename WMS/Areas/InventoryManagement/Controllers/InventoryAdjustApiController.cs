using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryAdjustVMs;
using WMS.Model.InventoryManagement;
using WMS.Util;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("盘点调整单")]
    [ApiController]
    [Route("api/InventoryAdjust")]
    public partial class InventoryAdjustApiController : BaseApiController
    {
        [ActionDescription("生成盘点（库存）调整单")]
        [HttpPost("Create")]
        public ActionResult Create(string erpDocNo)
        {
            ReturnResult rr = new ReturnResult();
            var vm = Wtm.CreateVM<InventoryStockTakingVM>();
            vm.Adjust(erpDocNo);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
