// WTM默认页面 Wtm buidin page
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Elsa.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Core.Support.Json;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Mvc.Admin.ViewModels.FrameworkUserVms;
using WMS.Model.BaseData;
using WMS.Util;

namespace WalkingTec.Mvvm.Admin.Api
{
    [AuthorizeJwtWithCookie]
    [ApiController]
    [Route("api/_[controller]")]
    [ActionDescription("_Admin.LoginApi")]
    [AllRights]
    public class AccountController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] string account, [FromForm] string password, [FromForm] string tenant = null, [FromForm] bool rememberLogin = false)
        {

            var user = Wtm.DoLogin(account, password, tenant);
            if (user == null)
            {
                return BadRequest(Localizer["Sys.LoginFailed"].Value);
            }

            //其他属性可以通过user.Attributes["aaa"] = "bbb"方式赋值

            Wtm.LoginUserInfo = user;

            AuthenticationProperties properties = null;
            if (rememberLogin)
            {
                properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
                };
            }

            var principal = Wtm.LoginUserInfo.CreatePrincipal();
            await Wtm.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
            return CheckUserInfo();
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginJwt(SimpleLogin loginInfo)
        {
            Guid whid;
            BaseWareHouse wh;
            if (string.IsNullOrEmpty(loginInfo.WhId))
            {
                ModelState.AddModelError("whId", "不能为空");
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                // 验证存储地点是否存在、是否禁用
                try
                {
                    Guid.TryParse(loginInfo.WhId, out whid);
                    wh = DC.Set<BaseWareHouse>().Include(x => x.Organization).Where(x => x.ID == whid).AsNoTracking().FirstOrDefault();

                    if (wh == null)
                    {
                        ModelState.AddModelError("", $"存储地点{loginInfo.WhId}不存在");
                        return BadRequest(ModelState.GetErrorJson());
                    }
                    if (!wh.IsValid || wh.IsEffective == WMS.Model.EffectiveEnum.Ineffective)
                    {
                        ModelState.AddModelError("", $"存储地点{loginInfo.WhId}已禁用");
                        return BadRequest(ModelState.GetErrorJson());
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return BadRequest(ModelState.GetErrorJson());
                }
            }
            var user = Wtm.DoLogin(loginInfo.Account, loginInfo.Password, loginInfo.Tenant);
            if (user == null)
            {
                ModelState.AddModelError("", Localizer["Sys.LoginFailed"]);
                return BadRequest(ModelState.GetErrorJson());
            }
            // 用户、存储地点配置验证
            var userwh = DC.Set<BaseUserWhRelation>().Where(x => x.UserId == user.ITCode && x.WhId == wh.ID).AsNoTracking().FirstOrDefault();
            if (userwh == null)
            {
                ModelState.AddModelError("", $"用户{user.ITCode}无权限登录存储地点{wh.Code}");
                return BadRequest(ModelState.GetErrorJson());
            }
            var userEntity = DC.Set<FrameworkUser>().Where(x => x.ITCode == user.ITCode).FirstOrDefault();
            if (userEntity != null)
            {
                userEntity.WarehouseId = wh.ID; // 建立实体字段是为了防止web服务重启后，值丢失。但这样做需要满足同一用户只能在一个设备登录，否则会导致先登录的用户存储地点与其实际登录的存储地点不一致。
                DC.SaveChanges();
                // await Wtm.RemoveUserCache(userEntity.ITCode);
            }

            //其他属性可以通过user.Attributes["aaa"] = "bbb"方式赋值
            user.Attributes["WarehouseId"] = loginInfo.WhId;    // 因为user对象是先取的，再改变了数据库中的存储地点。所以需要在这里再赋值一次存储地点
            Wtm.LoginUserInfo = user;
            var Privileges = Common.GetPrivileges(user.FunctionPrivileges, Wtm);   // 权限
            var authService = HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;
            var token = await authService.IssueTokenAsync(Wtm.LoginUserInfo);
            return Content(JsonSerializer.Serialize(new { Token = token, WhName = wh.Name, OrgName = wh.Organization.Name, UserName = userEntity.Name, Privileges = Privileges }), "application/json");
        }

        [AllowAnonymous]
        [HttpPost("GetCurrentLoginWh")]
        public IActionResult GetCurrentLoginWh()
        {
            if (Wtm != null && Wtm.LoginUserInfo != null && Wtm.LoginUserInfo.Attributes != null && Wtm.LoginUserInfo.Attributes["WarehouseId"] != null)
            {
                return Ok(Wtm.LoginUserInfo.Attributes["WarehouseId"].ToString());
            }
            else
            {
                return Ok("无");
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginU9(SimpleLogin loginInfo)
        {
            var user = Wtm.DoLogin(loginInfo.Account, loginInfo.Password, loginInfo.Tenant);
            if (user == null)
            {
                ModelState.AddModelError(" ", Localizer["Sys.LoginFailed"]);
                return BadRequest(ModelState.GetErrorJson());
            }

            //其他属性可以通过user.Attributes["aaa"] = "bbb"方式赋值

            Wtm.LoginUserInfo = user;
            var authService = HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;
            var token = await authService.IssueTokenAsync(Wtm.LoginUserInfo);
            return Content(JsonSerializer.Serialize(token), "application/json");
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult GetUserWh(string code)
        {
            // 获取用户所有可以登录的存储地点
            var userwhs = DC.Set<BaseUserWhRelation>()
                .CheckEqual(code, x => x.UserId)
                .AsNoTracking()
                .Select(p => new { ID = p.Wh.ID, Code = p.Wh.Code, Name = p.Wh.Name, OrgName = p.Wh.Organization.Name })
                .ToList();
            return Ok(userwhs);
        }

        [Public]
        [HttpGet("[action]")]
        public async Task<IActionResult> LoginRemote([FromQuery] string _remotetoken)
        {
            if (Wtm?.LoginUserInfo != null)
            {
                var principal = Wtm.LoginUserInfo.CreatePrincipal();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, null);
            }
            return CheckUserInfo();
        }


        [AllRights]
        [HttpGet("[action]")]
        public IActionResult SetTenant([FromQuery] string tenant)
        {
            bool rv = Wtm.SetCurrentTenant(tenant == "" ? null : tenant);
            return Ok(rv);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Reg(SimpleReg regInfo)
        {
            var exist = DC.Set<FrameworkUser>().Any(x => x.ITCode.ToLower() == regInfo.ITCode.ToLower());

            if (exist == true)
            {
                ModelState.AddModelError("ITCode", Localizer["Login.ItcodeDuplicate"]);
                return BadRequest(ModelState.GetErrorJson());
            }

            var hasuserrole = DC.Set<FrameworkRole>().Where(x => x.RoleCode == "002").FirstOrDefault();
            FrameworkUser user = new FrameworkUser
            {
                ITCode = regInfo.ITCode,
                Name = regInfo.Name,
                Password = Utils.GetMD5String(regInfo.Password),
                IsValid = true,
                PhotoId = regInfo.PhotoId,
            };
            if (hasuserrole != null)
            {
                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "002"
                };
                DC.Set<FrameworkUserRole>().Add(userrole);
            }
            DC.Set<FrameworkUser>().Add(user);
            DC.SaveChanges();
            return Ok();
        }


        [HttpPost("[action]")]
        [AllRights]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        public IActionResult RefreshToken(string refreshToken)
        {
            var rv = Wtm.RefreshToken();
            if (rv == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(rv);
            }
        }

        [AllRights]
        [HttpGet("[action]")]
        public IActionResult CheckUserInfo(bool IsApi = true)
        {
            if (Wtm.LoginUserInfo == null)
            {
                return BadRequest();
            }
            else
            {
                var forapi = Wtm.LoginUserInfo;
                if (IsApi)
                {
                    forapi.SetAttributesForApi(Wtm);
                }
                forapi.DataPrivileges = null;
                forapi.FunctionPrivileges = null;
                if (forapi.Attributes == null)
                {
                    forapi.Attributes = new Dictionary<string, object>();
                }
                if (forapi.Attributes.ContainsKey("IsMainHost"))
                {
                    forapi.Attributes.Remove("IsMainHost");
                }
                if (ConfigInfo.HasMainHost && string.IsNullOrEmpty(Wtm.LoginUserInfo.TenantCode) == true)
                {
                    forapi.Attributes.Add("IsMainHost", true);
                }
                else
                {
                    forapi.Attributes.Add("IsMainHost", false);
                }
                return Ok(forapi);
            }
        }


        [AllRights]
        [HttpPost("[action]")]
        public IActionResult ChangePassword(ChangePasswordVM vm)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm).Result;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoChange();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok();
                }
            }

        }

        [Public]
        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                await Wtm.CallAPI<string>("mainhost", "/api/_account/logout", HttpMethodEnum.GET, new { }, 10);
                return Ok(ConfigInfo.MainHost);
            }
            else
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("/");
            }
        }

        [HttpGet("GetFrameworkRoles")]
        [ActionDescription("GetRoles")]
        [AllRights]
        public IActionResult GetFrameworkRoles()
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetFrameworkRoles").Result;
            }
            return Ok(DC.Set<FrameworkRole>().GetSelectListItems(Wtm, x => x.RoleName, x => x.RoleCode));
        }

        [HttpGet("GetFrameworkGroups")]
        [ActionDescription("GetGroups")]
        [AllRights]
        public IActionResult GetFrameworkGroups()
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetFrameworkGroups").Result;
            }
            return Ok(DC.Set<FrameworkGroup>().GetSelectListItems(Wtm, x => x.GroupName, x => x.GroupCode));
        }

        [HttpGet("GetFrameworkGroupsTree")]
        [ActionDescription("GetGroupsTree")]
        [AllRights]
        public IActionResult GetFrameworkGroupsTree()
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetFrameworkGroupsTree").Result;
            }
            return Ok(DC.Set<FrameworkGroup>().GetTreeSelectListItems(Wtm, x => x.GroupName, x => x.GroupCode));
        }


        [HttpGet("GetUserById")]
        [AllRights]
        public IActionResult GetUserById(string keywords)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetUserById").Result;
            }
            var users = DC.Set<FrameworkUser>().Where(x => x.ITCode.ToLower().StartsWith(keywords.ToLower())).GetSelectListItems(Wtm, x => x.Name + "(" + x.ITCode + ")", x => x.ITCode);
            return Ok(users);
        }

        [HttpGet("GetUserByGroup")]
        [AllRights]
        public IActionResult GetUserByGroup(string keywords)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetUserByGroup").Result;
            }
            var users = DC.Set<FrameworkUserGroup>().Where(x => x.GroupCode == keywords).Select(x => x.UserCode).ToList();
            return Ok(users);
        }

        [HttpGet("GetUserByRole")]
        [AllRights]
        public IActionResult GetUserByRole(string keywords)
        {
            if (ConfigInfo.HasMainHost && Wtm.LoginUserInfo?.CurrentTenant == null)
            {
                return Request.RedirectCall(Wtm, "/api/_account/GetUserByRole").Result;
            }
            var users = DC.Set<FrameworkUserRole>().Where(x => x.RoleCode == keywords).Select(x => x.UserCode).ToList();
            return Ok(users);
        }

    }

    public class SimpleLogin
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Tenant { get; set; }

        public string RemoteToken { get; set; }

        // 自定义参数
        public string WhId { get; set; }    // 存储地点ID
    }
    public class SimpleReg
    {
        [Display(Name = "_Admin.Account")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string ITCode { get; set; }

        [Display(Name = "_Admin.Name")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Name { get; set; }

        [Display(Name = "Login.Password")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        public string Password { get; set; }

        [Display(Name = "_Admin.Photo")]
        public Guid? PhotoId { get; set; }
    }

}
