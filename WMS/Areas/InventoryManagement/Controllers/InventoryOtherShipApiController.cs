using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;
using Elsa;
using Newtonsoft.Json;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("其它出库单API")]
    [ApiController]
    [Route("api/InventoryOtherShip")]
	public partial class InventoryOtherShipApiController : BaseApiController
    {
        /// <summary>
        /// 获取其它出库单单据类型列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [ActionDescription("获取其它出库单单据类型列表")]
        [HttpPost("GetListType")]
        public ActionResult GetListType(string orgId)
        {
            ReturnResult<List<BaseReturn>> rr = new ReturnResult<List<BaseReturn>>();
            Guid id = Guid.Empty;
            if (string.IsNullOrEmpty(orgId))
            {
                if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    rr.SetFail("登录信息已过期，请重新登录");
                    return Ok(rr);
                }
                Guid whid;
                Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
                if (whid == Guid.Empty)
                {
                    rr.SetFail("登录信息已过期，请重新登录2");
                    return Ok(rr);
                }
                id = DC.Set<BaseWareHouse>().Where(x => x.ID == whid).Select(x => (Guid)x.OrganizationId).FirstOrDefault();
            }
            else
            {
                Guid.TryParse(orgId, out id);
            }
            if (id == Guid.Empty)
            {
                rr.SetFail("组织ID不正确");
                return Ok(rr);
            }
            else
            {
                rr.Entity = DC.Set<InventoryOtherShipDocType>()
                    .Where(x => x.OrganizationId == id && x.IsValid == true && x.IsEffective == Model.EffectiveEnum.Effective && !x.Name.Contains("刀具"))    // 过滤掉刀具相关的单据类型
                    .Select(x => new BaseReturn { ID = x.ID, Name = x.Name, Code = x.Code }).ToList();
            }

            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("[PDA] 创建WMS其它出库单")]
        [HttpPost("CreateInventoryOtherShip")]
        public ActionResult CreateInventoryOtherShip(InventoryOtherShipCreatePara para)
        {
            ReturnResult rr = new ReturnResult();
            InventoryOtherShipApiVM vm = Wtm.CreateVM<InventoryOtherShipApiVM>();
            vm.CreateDoc(para);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
