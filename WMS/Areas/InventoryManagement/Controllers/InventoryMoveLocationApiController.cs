using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("移库单API")]
    [ApiController]
    [Route("api/InventoryMoveLocation")]
    public partial class InventoryMoveLocationApiController : BaseApiController
    {
        [ActionDescription("[PDA] 保存库存移库单")]
        [HttpPost("Save")]
        public ActionResult Save(InventoryMoveLocationSavePara para)
        {
            ReturnResult rr = new ReturnResult();
            var vm = Wtm.CreateVM<InventoryMoveLocationApiVM>();
            vm.Save(para);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
