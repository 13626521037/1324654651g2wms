using Elsa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseSysParaVMs;

namespace WMS.Areas.BaseData.Controllers
{
    [Area("BaseData")]
    [AuthorizeJwtWithCookie]
    [ActionDescription("基础信息API")]
    [ApiController]
    [Route("api/BaseData")]
    public class BaseDataApiController : BaseApiController
    {
        // 基础信息API类：获取存储地点、库区、库位等信息

        /// <summary>
        /// 获取存储地点列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetListWh")]
        [ActionDescription("获取存储地点列表")]
        public ActionResult GetListWh()
        {
            // 进行类型转换，是为了节省流量
            ReturnResult<List<BaseWareHoustReturn>> rr = new ReturnResult<List<BaseWareHoustReturn>>();
            List<BaseWareHouse> whs = DC.Set<BaseWareHouse>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && x.IsValid)
                .AsNoTracking()
                .Select(x => new BaseWareHouse
                {
                    ID = x.ID,
                    Name = x.Name,
                    Code = x.Code,
                    OrganizationId = x.OrganizationId,
                })
                .ToList();
            List<BaseWareHoustReturn> whrs = new List<BaseWareHoustReturn>();
            foreach (var wh in whs)
            {
                BaseWareHoustReturn whr = new BaseWareHoustReturn();
                whr.ID = wh.ID;
                whr.Name = wh.Name;
                whr.Code = wh.Code;
                whr.OrgId = wh.OrganizationId;
                whrs.Add(whr);
            }
            rr.Entity = whrs;
            return Ok(rr);
        }

        /// <summary>
        /// 获取库区列表
        /// </summary>
        /// <param name="whid">存储地点ID</param>
        /// <returns></returns>
        [HttpPost("GetListWhArea")]
        [ActionDescription("获取库区列表")]
        public ActionResult GetListWhArea(string whId)
        {
            // 进行类型转换，是为了节省流量
            ReturnResult<List<BaseWhAreaReturn>> rr = new ReturnResult<List<BaseWhAreaReturn>>();
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(whId))
            {
                Guid.TryParse(whId, out id);
                if (id == Guid.Empty)
                {
                    rr.SetFail("存储地点ID不正确");
                    return Ok(rr);
                }
            }
            List<BaseWhArea> areas = DC.Set<BaseWhArea>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && (id == Guid.Empty || x.WareHouseId == id))
                .AsNoTracking()
                .Select(x => new BaseWhArea
                 {
                     ID = x.ID,
                     Name = x.Name,
                     Code = x.Code,
                     WareHouseId = x.WareHouseId
                 })
                .ToList();
            List<BaseWhAreaReturn> arears = new List<BaseWhAreaReturn>();
            foreach (var area in areas)
            {
                BaseWhAreaReturn arear = new BaseWhAreaReturn();
                arear.ID = area.ID;
                arear.Name = area.Name;
                arear.Code = area.Code;
                arear.WareHouseId = area.WareHouseId;
                arears.Add(arear);
            }
            rr.Entity = arears;
            return Ok(rr);
        }

        /// <summary>
        /// 获取库位列表
        /// </summary>
        /// <param name="areaId">库区ID</param>
        /// <returns></returns>
        [HttpPost("GetListWhLocation")]
        [ActionDescription("获取库位列表")]
        public ActionResult GetListWhLocation(string areaId)
        {
            // 进行类型转换，是为了节省流量
            ReturnResult<List<BaseWhLocationReturn>> rr = new ReturnResult<List<BaseWhLocationReturn>>();
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(areaId))
            {
                Guid.TryParse(areaId, out id);
                if (id == Guid.Empty)
                {
                    rr.SetFail("库区ID不正确");
                    return Ok(rr);
                }
            }
            List<BaseWhLocation> locations = DC.Set<BaseWhLocation>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && (id == Guid.Empty || x.WhAreaId == id))
                .AsNoTracking()
                .Select(x => new BaseWhLocation
                 {
                     ID = x.ID,
                     Name = x.Name,
                     Code = x.Code,
                     WhAreaId = x.WhAreaId
                 })
                .ToList();
            List<BaseWhLocationReturn> locs = new List<BaseWhLocationReturn>();
            foreach (var location in locations)
            {
                BaseWhLocationReturn loc = new BaseWhLocationReturn();
                loc.ID = location.ID;
                loc.Name = location.Name;
                loc.Code = location.Code;
                loc.WhAreaId = location.WhAreaId;
                locs.Add(loc);
            }
            rr.Entity = locs;
            return Ok(rr);
        }

        /// <summary>
        /// 通过编码获取库位信息
        /// </summary>
        /// <param name="code">库位编码</param>
        /// <returns></returns>
        [HttpPost("GetLocationByCode")]
        [ActionDescription("通过编码获取库位信息")]
        public ActionResult GetLocationByCode(string code)
        {
            ReturnResult<BaseReturn> rr = new ReturnResult<BaseReturn>();
            if (!Wtm.LoginUserInfo.Attributes.HasKey("WarehouseId") || Wtm.LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                rr.SetFail("登录信息已过期，请重新登录");
                return Ok(rr);
            }
            Guid whid;
            Guid.TryParse(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

            if (string.IsNullOrEmpty(code))
            {
                rr.SetFail("库位编码不能为空");
                return Ok(rr);
            }
            BaseWhLocation location = DC.Set<BaseWhLocation>()
                .Include(x => x.WhArea.WareHouse)
                .Where(x => x.Code.ToLower().Equals(code.ToLower()))
                .AsNoTracking()
                .FirstOrDefault();
            if (location == null)
            {
                rr.SetFail("库位不存在");
                return Ok(rr);
            }
            if (location.IsEffective != Model.EffectiveEnum.Effective)
            {
                rr.SetFail("库位已失效");
            }
            else if (location.WhArea.WareHouse.ID != whid)
            {
                rr.SetFail("库位不属于当前登录仓库");
            }
            else if (location.AreaType != Model.WhLocationEnum.Normal)
            {
                rr.SetFail("只允许扫描正式库位");
            }
            else
            {
                rr.Entity = new BaseReturn { ID = location.ID, Name = location.Name, Code = location.Code };
            }
            return Ok(rr);
        }

        /// <summary>
        /// 获取APP配置信息
        /// </summary>
        /// <returns></returns>
        [Public]
        [HttpPost("GetAppConfig")]
        [ActionDescription("获取APP配置信息")]
        public ActionResult GetAppConfig()
        {
            ReturnResult<object> rr = new ReturnResult<object>();
            var vm = Wtm.CreateVM<BaseSysParaVM>();
            string Address = vm.GetParaValue("AppAddress");
            string Version = vm.GetParaValue("AppVersion");
            if (string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Version))
            {
                rr.SetFail("系统配置信息错误。请联系管理员检查参数AppAddress、AppVersion");
            }
            else
            {
                rr.Entity = new { Address, Version };
            }
            return Ok(rr);
        }
        
        /// <summary>
        /// 获取组织列表（参数type：0-全部，1-生产组织，2-销售组织，3-生产+销售）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("GetListOrg")]
        [ActionDescription("获取组织列表")]
        public ActionResult GetListOrg(int type = 0)
        {
            ReturnResult<List<BaseOrganizationReturn>> rr = new ReturnResult<List<BaseOrganizationReturn>>();
            List<BaseOrganization> orgs = DC.Set<BaseOrganization>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && x.IsValid)
                .Where(x => type == 0 || (type == 1 && x.IsProduction == true) || (type == 2 && x.IsSale == true) || (type == 3 && (x.IsProduction == true || x.IsSale == true)))
                .AsNoTracking()
                .Select(x => new BaseOrganization
                {
                    ID = x.ID,
                    Name = x.Name,
                    Code = x.Code,
                })
                .ToList();
            List<BaseOrganizationReturn> orgrs = new List<BaseOrganizationReturn>();
            foreach (var org in orgs)
            {
                BaseOrganizationReturn orgr = new BaseOrganizationReturn();
                orgr.ID = org.ID;
                orgr.Name = org.Name;
                orgr.Code = org.Code;
                orgrs.Add(orgr);
            }
            rr.Entity = orgrs;
            return Ok(rr);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        [HttpPost("GetListDept")]
        [ActionDescription("获取部门列表")]
        public ActionResult GetListDept(string orgId)
        {
            // 进行类型转换，是为了节省流量
            ReturnResult<List<BaseDepartmentReturn>> rr = new ReturnResult<List<BaseDepartmentReturn>>();
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(orgId))
            {
                Guid.TryParse(orgId, out id);
                if (id == Guid.Empty)
                {
                    rr.SetFail("组织ID格式不正确");
                    return Ok(rr);
                }
            }
            List<BaseDepartment> depts = DC.Set<BaseDepartment>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && x.IsValid == true && (id == Guid.Empty || x.OrganizationId == id))
                .AsNoTracking()
                .Select(x => new BaseDepartment
                {
                    ID = x.ID,
                    Name = x.Name,
                    Code = x.Code,
                    OrganizationId = x.OrganizationId
                })
                .ToList();
            List<BaseDepartmentReturn> deptrs = new List<BaseDepartmentReturn>();
            foreach (var dept in depts)
            {
                BaseDepartmentReturn deptr = new BaseDepartmentReturn();
                deptr.ID = dept.ID;
                deptr.Name = dept.Name;
                deptr.Code = dept.Code;
                deptr.OrgId = dept.OrganizationId;
                deptrs.Add(deptr);
            }
            rr.Entity = deptrs;
            // 过滤筛选：只保留末级部门
            if (rr.Entity != null && rr.Entity.Count > 0)   // 2025-09-16 zhouxin 增加过滤，只显示末级的部门
            {
                rr.Entity = rr.Entity.Where(
                    x => !rr.Entity.Any(y => y.Code != x.Code
                    && y.Code.StartsWith(x.Code)
                    && y.Code.Length == x.Code.Length + 2)).ToList();
            }
            
            return Ok(rr);
        }

        /// <summary>
        /// 获取业务员列表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpPost("GetListOperator")]
        [ActionDescription("获取业务员列表")]
        public ActionResult GetListOperator(string deptId)
        {
            // 进行类型转换，是为了节省流量
            ReturnResult<List<BaseOperatorReturn>> rr = new ReturnResult<List<BaseOperatorReturn>>();
            Guid id = Guid.Empty;
            if (!string.IsNullOrEmpty(deptId))
            {
                Guid.TryParse(deptId, out id);
                if (id == Guid.Empty)
                {
                    rr.SetFail("部门ID格式不正确");
                    return Ok(rr);
                }
            }
            List<BaseOperator> operators = DC.Set<BaseOperator>()
                .Where(x => x.IsEffective == Model.EffectiveEnum.Effective && x.IsValid == true && (id == Guid.Empty || x.DepartmentId == id))
                .AsNoTracking()
                .Select(x => new BaseOperator
                {
                    ID = x.ID,
                    Name = x.Name,
                    Code = x.Code,
                    DepartmentId = x.DepartmentId
                })
                .ToList();
            List<BaseOperatorReturn> oprs = new List<BaseOperatorReturn>();
            foreach (var operator1 in operators)
            {
                BaseOperatorReturn opr = new BaseOperatorReturn();
                opr.ID = operator1.ID;
                opr.Name = operator1.Name;
                opr.Code = operator1.Code;
                opr.DeptId = operator1.DepartmentId;
                oprs.Add(opr);
            }
            rr.Entity = oprs;
            return Ok(rr);
        }

    }
}
