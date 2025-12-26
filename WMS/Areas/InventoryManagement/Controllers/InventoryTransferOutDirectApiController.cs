using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using WMS.Util;
using Newtonsoft.Json;
using Elsa;


namespace WMS.Controllers
{
    [Area("InventoryManagement")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("直接调出单API")]
    [ApiController]
    [Route("api/InventoryTransferOutDirect")]
    public partial class InventoryTransferOutDirectApiController : BaseApiController
    {
        [ActionDescription("获取调出组织列表")]
        [HttpPost("GetListOrg")]
        public ActionResult GetListOrg()
        {
            ReturnResult<List<BaseReturn>> rr = new ReturnResult<List<BaseReturn>>();
            rr.Entity = DC.Set<BaseOrganization>()
                .Where(x => (x.IsProduction == true || x.IsSale == true) && x.IsValid == true && x.IsEffective == Model.EffectiveEnum.Effective)
                .Select(x => new BaseReturn { ID = x.ID, Name = x.Name, Code = x.Code }).ToList();
            return Ok(rr);
        }

        [ActionDescription("获取存储地点列表")]
        [HttpPost("GetListWh")]
        public ActionResult GetListWh(string orgId)
        {
            ReturnResult<List<BaseWareHoustReturn>> rr = new ReturnResult<List<BaseWareHoustReturn>>();
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(orgId))
            {
                Guid.TryParse(orgId, out id);
                if (id == Guid.Empty)
                {
                    rr.SetFail("组织ID不正确");
                    return Ok(rr);
                }
                else
                {
                    rr.Entity = DC.Set<BaseWareHouse>()
                        .Where(x => x.OrganizationId == id && x.IsValid == true && x.IsEffective == Model.EffectiveEnum.Effective)
                        .Select(x => new BaseWareHoustReturn { ID = x.ID, Name = x.Name, Code = x.Code }).ToList();
                }
            }
            else
            {
                rr.SetFail("组织参数不能为空");
            }
            
            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("获取单据类型列表")]
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
                rr.Entity = DC.Set<InventoryTransferOutDirectDocType>()
                    .Where(x => x.OrganizationId == id && x.IsValid == true && x.IsEffective == Model.EffectiveEnum.Effective)
                    .Select(x => new BaseReturn { ID = x.ID, Name = x.Name, Code = x.Code }).ToList();
            }

            return Ok(JsonConvert.SerializeObject(rr, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        [ActionDescription("[PDA] 创建WMS调出单")]
        [HttpPost("CreateTransferOut")]
        public ActionResult CreateTransferOut(InventoryTransferOutDirectCreatePara para)
        {
            ReturnResult rr = new ReturnResult();
            InventoryTransferOutDirectApiVM vm = Wtm.CreateVM<InventoryTransferOutDirectApiVM>();
            vm.CreateTransferOut(para);
            if (!ModelState.IsValid)
            {
                rr.SetFail(ModelState.GetErrorJson().GetFirstError());
            }
            return Ok(rr);
        }
    }
}
