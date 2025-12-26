using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("库存拆零单Api")]
    [ApiController]
    [Route("api/InventorySplitSingle")]
    public partial class InventorySplitSingleApiController : BaseApiController
    {
        [ActionDescription("[PDA] 保存库存拆零单")]
        [HttpPost("Save")]
        public ActionResult Save(Guid originalInvId, decimal singleQty)
        {
            ReturnResult<List<InventorySplitSingleSaveReturn>> rr = new ReturnResult<List<InventorySplitSingleSaveReturn>>();
            var vm = Wtm.CreateVM<InventorySplitSingleVM>();
            List<BaseInventory> invs = vm.Save(originalInvId, singleQty: singleQty);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            else
            {
                rr.Entity = new List<InventorySplitSingleSaveReturn>();
                if (invs != null && invs.Count > 0)
                {
                    foreach (var inv in invs)
                    {
                        rr.Entity.Add(new InventorySplitSingleSaveReturn()
                        {
                            BarCode = $"{(int)inv.ItemSourceType}@{inv.ItemMaster.Code}@{inv.Qty.TrimZero()}@{inv.SerialNumber}",
                            Qty = (decimal)inv.Qty.TrimZero(),
                        });
                    }
                }
            }
            return Ok(rr);
        }

    }
}
