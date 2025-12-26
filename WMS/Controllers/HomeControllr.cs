using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Auth;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;

namespace WMS.Controllers
{
    public class HomeController : BaseController
    {
        [AllRights]
        public IActionResult Index()
        {
            ViewData["title"] = "WMS";
            return View();
        }

        [AllowAnonymous]
        public IActionResult PIndex()
        {
            return View();
        }

        [AllRights]
        [ActionDescription("FrontPage")]
        public IActionResult FrontPage()
        {
            return PartialView();
        }

        [AllRights]
        [ActionDescription("帮助")]
        public IActionResult Help()
        {
            List<Help> data = JsonConvert.DeserializeObject<List<Help>>(TempData["help"] as string);
            return PartialView(data);
        }

        public IActionResult GetActionChart()
        {
            var areas = GlobaInfo.AllModule.Select(x => x.Area).Distinct();
            var data = new List<ChartData>();

            foreach (var area in areas)
            {
                var controllers = GlobaInfo.AllModule.Where(x => x.Area == area);
                data.Add(new ChartData
                {
                    Category = "Controllers",
                    Value = controllers.Count(),
                    Series = area?.AreaName ?? "Default"
                });
                data.Add(new ChartData
                {
                    Category = "Actions",
                    Value = controllers.SelectMany(x => x.Actions).Count(),
                    Series = area?.AreaName ?? "Default"
                });
            }
            var rv = data.ToChartData();
            return Json(rv);
        }

        public IActionResult GetModelChart()
        {
            var models = new List<Type>();

            var pros = Wtm.ConfigInfo.Connections.SelectMany(x => x.DcConstructor.DeclaringType.GetProperties(BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance));
            if (pros != null)
            {
                foreach (var pro in pros)
                {
                    if (pro.PropertyType.IsGeneric(typeof(DbSet<>)))
                    {
                        models.Add(pro.PropertyType.GetGenericArguments()[0]);
                    }
                }
            }
            var data = new List<ChartData>();

            foreach (var m in models)
            {
                data.Add(new ChartData
                {
                    Value = m.GetProperties().Count(),
                    Category = m.GetPropertyDisplayName(),
                    Series = "Model"
                });
            }
            var rv = data.ToChartData();
            return Json(rv);
        }

        public IActionResult GetSampleChart()
        {
            var data = new List<ChartData>();
            Random r = new Random();
            int maxi = r.Next(3, 10);
            int maxy = r.Next(3, 10);
            for (int i = 0; i < maxi; i++)
            {
                for (int j = 0; j < maxy; j++)
                {
                    data.Add(new ChartData
                    {
                        Category = "x" + i,
                        Value = r.Next(100, 1000),
                        ValueX = r.Next(200, 2000),
                        Series = "y" + j,
                        Addition = r.Next(100, 1000),

                    });
                }
            }
            var rv = data.ToChartData();
            return Json(rv);
        }

        [AllRights]
        [ActionDescription("Layout")]
        public IActionResult Layout()
        {
            ViewData["debug"] = Wtm.ConfigInfo.IsQuickDebug;
            return PartialView();
        }

        [AllRights]
        public IActionResult UserInfo()
        {
            if (HttpContext.Request.Cookies.TryGetValue(CookieAuthenticationDefaults.CookiePrefix + AuthConstants.CookieAuthName, out string cookieValue))
            {
                var protectedData = Base64UrlTextEncoder.Decode(cookieValue);
                var dataProtectionProvider = HttpContext.RequestServices.GetRequiredService<IDataProtectionProvider>();
                var _dataProtector = dataProtectionProvider
                                        .CreateProtector(
                                            "Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationMiddleware",
                                            CookieAuthenticationDefaults.AuthenticationScheme,
                                            "v2");
                var unprotectedData = _dataProtector.Unprotect(protectedData);

                string cookieData = Encoding.UTF8.GetString(unprotectedData);
                return JsonMore(cookieData);
            }
            else
                return JsonMore("No Data");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 3600)]
        public github GetGithubInfo()
        {
            var rv = Wtm.ReadFromCache<github>("githubinfo", () =>
            {
                var s = Wtm.CallAPI<github>("github", "repos/dotnetcore/wtm", 60).Result;
                return s.Data;
            }, 1800);

            return rv;
        }

        [AllRights]
        public IActionResult ShowBarCode(string code)
        {
            ViewBag.code = code;
            return PartialView();
        }

        [AllRights]
        [ActionDescription("Hangfire页面")]
        public IActionResult HangfirePage()
        {
            // 按照返回的ID，访问打印服务的地址，进行页面展示
            string url = "/hangfire/servers";
            string str = @"
                <div style='text-align:center;margin: 30px;'>
                    请在新窗口中查看。 如无新窗口弹出，请检查是否禁用了弹出窗口
                </div>
                <script>
                    window.open('" + url + @"', '_blank');
                </script>";
            return Content(str, "text/html");
        }

        [AllRights]
        [ActionDescription("获取首页的数值数据")]
        public IActionResult GetNums()
        {
            ReturnResult<List<int>> rr = new ReturnResult<List<int>>();
            var paras = new SqlParameter[]
            {
                new SqlParameter("@startdate", DateTime.Now.Date),
                new SqlParameter("@enddate", DateTime.Now.AddDays(1).Date)
            };
            try
            {
                DataTable dt = DC.RunSP("GetFrontPageNums", paras);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rr.Entity = [
                        int.Parse(dt.Rows[0][0].ToString()),
                        int.Parse(dt.Rows[0][1].ToString()),
                        int.Parse(dt.Rows[0][2].ToString()),
                        int.Parse(dt.Rows[0][3].ToString()),
                    ];
                }
                else
                {
                    rr.SetFail("获取数据失败");
                }
            }
            catch (Exception ex)
            {
                rr.SetFail("获取数据出错。" + ex.Message);
            }
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("获取按日统计上下架数据")]
        public IActionResult GetChartData1()
        {
            ReturnResult<IntChartData> rr = new ReturnResult<IntChartData>();
            rr.Entity = new IntChartData();
            rr.Entity.XAxis = new List<string>();
            rr.Entity.Values1 = new List<int>();
            rr.Entity.Values2 = new List<int>();
            DateTime statDate = DateTime.Now.AddDays(-6).Date;
            DateTime endDate = DateTime.Now.Date;
            var paras = new SqlParameter[]
            {
                new SqlParameter("@startdate", statDate),
                new SqlParameter("@enddate", endDate)
            };
#if DEBUG
            paras =
            [
                new SqlParameter("@startdate", new DateTime(2025, 8, 23).Date),
                new SqlParameter("@enddate", new DateTime(2025, 8, 30).Date)
            ];
#endif
            try
            {
                DataTable dt = DC.RunSP("GetFrontPageChartDatas1", paras);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var datas = dt.AsEnumerable();
                    rr.Entity.XAxis = datas.Select(x => Convert.ToDateTime(x[0]).ToString("MM-dd")).ToList();
                    rr.Entity.Values1 = datas.Select(x => int.Parse(x[1].ToString())).ToList();
                    rr.Entity.Values2 = datas.Select(x => int.Parse(x[2].ToString())).ToList();
                }
                else
                {
                    rr.SetFail("获取数据失败");
                }
            }
            catch (Exception ex)
            {
                rr.SetFail("获取数据出错。" + ex.Message);
            }
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("获取按日统计上下架数据")]
        public IActionResult GetChartData2()
        {
            ReturnResult<DecimalChartData> rr = new ReturnResult<DecimalChartData>();
            rr.Entity = new DecimalChartData();
            rr.Entity.XAxis = new List<string>();
            rr.Entity.Values1 = new List<decimal>();
            rr.Entity.Values2 = new List<decimal>();
            DateTime statDate = DateTime.Now.AddMonths(-5).Date;
            DateTime endDate = DateTime.Now.Date;
            var paras = new SqlParameter[]
            {
                new SqlParameter("@startdate", statDate),
                new SqlParameter("@enddate", endDate)
            };

            try
            {
                DataTable dt = DC.RunSP("GetFrontPageChartDatas2", paras);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var datas = dt.AsEnumerable();
                    rr.Entity.XAxis = datas.Select(x => Convert.ToDateTime(x[0]).ToString("yyyy-MM")).ToList();
                    rr.Entity.Values1 = datas.Select(x => (decimal)Math.Round(int.Parse(x[1].ToString()) / 10000.0, 1)).ToList();
                    rr.Entity.Values2 = datas.Select(x => (decimal)Math.Round(int.Parse(x[2].ToString()) / 10000.0, 1)).ToList();
                }
                else
                {
                    rr.SetFail("获取数据失败");
                }
            }
            catch (Exception ex)
            {
                rr.SetFail("获取数据出错。" + ex.Message);
            }
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("编辑快捷操作页面")]
        public IActionResult ShortcutEdit()
        {
            return View();
        }

        [AllRights]
        [ActionDescription("获取所有快捷操作列表")]
        public IActionResult GetAllShortcuts()
        {
            ReturnResult<ShortcutsReturn> rr = new ReturnResult<ShortcutsReturn>();
            //Wtm.LoginUserInfo.FunctionPrivileges[0].MenuItemId
            if (Wtm == null || Wtm.LoginUserInfo == null || Wtm.LoginUserInfo.FunctionPrivileges == null || Wtm.LoginUserInfo.FunctionPrivileges.Count == 0)
            {
                rr.SetFail("登录过期或当前账号未获取任何权限");
                return Ok(rr);
            }
            List<Guid> menuIds = Wtm.LoginUserInfo.FunctionPrivileges.Where(x => x.Allowed == true).Select(x => x.MenuItemId).ToList(); // 已授权的菜单
            List<FrameworkMenu> allmenus = DC.Set<FrameworkMenu>().Where(x => x.ShowOnMenu == true && x.Url != null && menuIds.Contains(x.ID)).ToList();    // 筛选前端显示的菜单
            menuIds = allmenus.Select(x => x.ID).ToList();
            List<BaseShortcut> shortcuts = DC.Set<BaseShortcut>().Where(x => x.UserId == Wtm.LoginUserInfo.ITCode && menuIds.Contains((Guid)x.MenuId)).ToList();    // 已配置的快捷操作菜单
            rr.Entity = new ShortcutsReturn
            {
                AllMenus = allmenus.Select(x => new ShortcutsMenuReturn { value = x.ID.ToString(), title = x.ModuleName }).ToList(),
                Shortcuts = shortcuts.OrderBy(x => x.CreateTime).Select(x => x.MenuId.ToString()).ToList()
            };
            // 将AllMenus中与Shortcuts匹配的元素排到最前面，且顺序与Shortcuts中的顺序保持一致
            List<ShortcutsMenuReturn> newAllMenus = new List<ShortcutsMenuReturn>();
            foreach (var item in rr.Entity.Shortcuts)
            {
                var menu = allmenus.FirstOrDefault(x => x.ID == Guid.Parse(item));
                if (menu != null)
                {
                    newAllMenus.Add(new ShortcutsMenuReturn { value = menu.ID.ToString(), title = menu.ModuleName });
                }
            }
            foreach (var item in rr.Entity.AllMenus)
            {
                if (!rr.Entity.Shortcuts.Contains(item.value))
                {
                    newAllMenus.Add(item);
                }
            }
            rr.Entity.AllMenus = newAllMenus;
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("获取已配置的快捷操作")]
        public IActionResult GetShortcuts()
        {
            ReturnResult<List<ShortcutDetail>> rr = new ReturnResult<List<ShortcutDetail>>();
            List<Guid> menuIds = Wtm.LoginUserInfo.FunctionPrivileges.Where(x => x.Allowed == true).Select(x => x.MenuItemId).ToList(); // 已授权的菜单
            List<BaseShortcut> shortcuts = DC.Set<BaseShortcut>().Include(x => x.Menu).AsNoTracking().Where(x => x.UserId == Wtm.LoginUserInfo.ITCode && menuIds.Contains((Guid)x.MenuId)).OrderBy(x => x.CreateTime).ToList();    // 已配置的快捷操作菜单
            rr.Entity = shortcuts.Select(x => new ShortcutDetail
            {
                Icon = x.Menu.Icon,
                Url = x.Menu.Url,
                Name = x.Menu.ModuleName
            }).ToList();
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("保存快捷操作")]
        public IActionResult SaveShortcuts(List<ShortcutsMenuReturn> Data)
        {
            ReturnResult rr = new ReturnResult();
            if (Data == null || Data.Count == 0)
            {
                rr.SetFail("没有选择任何菜单");
            }
            else
            {
                using (var trans = DC.BeginTransaction())
                {
                    // 删除原来的快捷操作
                    DC.Set<BaseShortcut>().Where(x => x.UserId == Wtm.LoginUserInfo.ITCode).ExecuteDelete();
                    // 保存新的快捷操作
                    List<BaseShortcut> shortcuts = new List<BaseShortcut>();
                    foreach (var item in Data)
                    {
                        if (Guid.TryParse(item.value, out Guid menuId))
                        {
                            shortcuts.Add(new BaseShortcut
                            {
                                ID = Guid.NewGuid(),
                                UserId = Wtm.LoginUserInfo.ITCode,
                                MenuId = menuId,
                                CreateTime = DateTime.Now,
                                CreateBy = Wtm.LoginUserInfo.ITCode
                            });
                        }
                    }
                    DC.Set<BaseShortcut>().AddRange(shortcuts);
                    DC.SaveChanges();
                    trans.Commit();
                }
            }
            return Ok(rr);
        }

        [AllRights]
        [ActionDescription("获取公告")]
        public IActionResult GetNotice()
        {
            ReturnResult<List<BaseSysNotice>> rr = new ReturnResult<List<BaseSysNotice>>();
            List<BaseSysNotice> notices = DC.Set<BaseSysNotice>().AsNoTracking().OrderByDescending(x => x.CreateTime).ToList();
            rr.Entity = notices;
            return Ok(rr);
        }

        public class github
        {
            public int stargazers_count { get; set; }
            public int forks_count { get; set; }
            public int subscribers_count { get; set; }
            public int open_issues_count { get; set; }
        }

        public class ShortcutsReturn
        {
            public List<ShortcutsMenuReturn> AllMenus { get; set; }
            public List<string> Shortcuts { get; set; }
        }

        public class ShortcutsMenuReturn
        {
            public string value { get; set; }

            public string title { get; set; }
        }

        public class ShortcutDetail
        {
            public string Icon { get; set; }

            public string Url { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// 标准的图表数据格式（整数）
        /// </summary>
        public class IntChartData
        {
            /// <summary>
            /// X轴标签
            /// </summary>
            public List<string> XAxis { get; set; }

            /// <summary>
            /// 数据1
            /// </summary>
            [JsonNumberHandling(JsonNumberHandling.Strict)] // json序列化时，强制转为数字类型（否则会转为字符串）
            public List<int> Values1 { get; set; }

            /// <summary>
            /// 数据2
            /// </summary>
            [JsonNumberHandling(JsonNumberHandling.Strict)]
            public List<int> Values2 { get; set; }
        }

        /// <summary>
        /// 标准的图表数据格式（小数）
        /// </summary>
        public class DecimalChartData
        {
            /// <summary>
            /// X轴标签
            /// </summary>
            public List<string> XAxis { get; set; }

            /// <summary>
            /// 数据1
            /// </summary>
            [JsonNumberHandling(JsonNumberHandling.Strict)] // json序列化时，强制转为数字类型（否则会转为字符串）
            public List<decimal> Values1 { get; set; }

            /// <summary>
            /// 数据2
            /// </summary>
            [JsonNumberHandling(JsonNumberHandling.Strict)]
            public List<decimal> Values2 { get; set; }
        }
    }

}
