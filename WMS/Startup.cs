using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Core.Support.FileHandlers;
using WalkingTec.Mvvm.Mvc;
using System;
using Hangfire;
using WMS.Services;
using Hangfire.Dashboard;

namespace WMS
{
    public class Startup
    {
        public IConfiguration ConfigRoot { get; }

        public Startup(IWebHostEnvironment env, IConfiguration config)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ConfigRoot = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWtmWorkflow(ConfigRoot);
            services.AddDistributedMemoryCache();
            services.AddWtmSession(3600, ConfigRoot);
            services.AddWtmCrossDomain(ConfigRoot);
            services.AddWtmAuthentication(ConfigRoot);
            services.AddWtmHttpClient(ConfigRoot);
            services.AddWtmSwagger(true);
            services.AddWtmMultiLanguages(ConfigRoot);


            services.AddMvc(options =>
            {
                options.UseWtmMvcOptions();
            })
            .AddJsonOptions(options =>
            {
                options.UseWtmJsonOptions(); // 使用系统默认的json序列化配置时，会忽略空值属性。下面对忽略的进行重写。这句不能直接注销掉，会影响大小写，影响系统功能。添加下面的代码是否有其它影响未知
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
            })

            .ConfigureApiBehaviorOptions(options =>
            {
                options.UseWtmApiOptions();
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddWtmDataAnnotationsLocalization(typeof(Program));

            services.AddWtmContext(ConfigRoot, (options) =>
            {
                options.DataPrivileges = DataPrivilegeSettings();
                options.CsSelector = CSSelector;
                options.FileSubDirSelector = SubDirSelector;
                options.ReloadUserFunc = ReloadUser;
            });


            //services.AddHostedService<KnifeOperationMonthlyService>();
            services.AddTransient<KnifeMonthlyService>();

            #region Hangfire 任务调度服务

            string hangfireConnection = ConfigRoot.GetConnectionString("HangfireConnection");
            services.AddHangfire(config => config.UseSqlServerStorage(hangfireConnection));
            // 添加Hangfire服务器
            services.AddHangfireServer();
            services.AddTransient<ErpSyncTaskService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptionsMonitor<Configs> configs)
        {
            IconFontsHelper.GenerateIconFont("wwwroot/layui", "wwwroot/font-awesome");

            app.UseExceptionHandler(configs.CurrentValue.ErrorHandler);
            app.UseStaticFiles();
            app.UseWtmStaticFiles();
            app.UseRouting();
            app.UseWtmMultiLanguages();
            app.UseWtmCrossDomain();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseWtmSwagger();
            app.UseWtm();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areaRoute",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseWtmContext();

            // 启用Hangfire仪表板（可选）
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            #region 在此处添加Hangfire定时任务（添加或更新任务）

            // 同步组织（每小时00分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-Organization-Hour-0-Task", x => x.SyncOrgTaskAsync(), "0 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步部门（每小时01分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-Department-Hour-1-Task", x => x.SyncDeptTaskAsync(), "1 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步存储地点（每小时02分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-WareHouse-Hour-2-Task", x => x.SyncWhTaskAsync(), "2 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步供应商（每小时03分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-Supplier-Hour-3-Task", x => x.SyncSupplierTaskAsync(), "3 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步单位（每小时04分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-Unit-Hour-4-Task", x => x.SyncUnitTaskAsync(), "4 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步料品分析类别（每小时05分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-AnalysisType-Hour-5-Task", x => x.SyncAnalysisTypeTaskAsync(), "5 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步料品分类（每小时06分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-ItemCategory-Hour-6-Task", x => x.SyncItemCategoryTaskAsync(), "6 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步业务员（每小时07分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-Operator-Hour-7-Task", x => x.SyncOperatorTaskAsync(), "7 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步调出单单据类型（每小时08分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-TransferOutType-Hour-8-Task", x => x.SyncTransferOutTypeTaskAsync(), "8 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步杂发单单据类型（每小时09分）
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-TransferOutType-Hour-9-Task", x => x.SyncOtherShipDocTypeTaskAsync(), "9 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });

            // 同步料品（每小时10分）。料品比较慢，放在最后
            RecurringJob.AddOrUpdate<ErpSyncTaskService>("ErpSync-ItemMaster-Hour-10-Task", x => x.SyncItemMasterTaskAsync(), "10 * * * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });
            // 刀具操作每月末归还于与领用（）
            RecurringJob.AddOrUpdate<KnifeMonthlyService>("KnifeMonthlyService-Task", x => x.MonthlyCheckInAndOutTaskAsync(), "0/5 22 28-31 * *", new RecurringJobOptions()
            {
                TimeZone = TimeZoneInfo.Local,
                MisfireHandling = MisfireHandlingMode.Ignorable
            });
            #endregion
        }

        /// <summary>
        /// Wtm will call this function to dynamiclly set connection string
        /// 框架会调用这个函数来动态设定每次访问需要链接的数据库
        /// </summary>
        /// <param name="context">ActionContext</param>
        /// <returns>Connection string key name</returns>
        public string CSSelector(ActionExecutingContext context)
        {
            //To override the default logic of choosing connection string,
            //change this function to return different connection string key
            //根据context返回不同的连接字符串的名称
            return null;
        }

        /// <summary>
        /// Set data privileges that system supports
        /// 设置系统支持的数据权限
        /// </summary>
        /// <returns>data privileges list</returns>
        public List<IDataPrivilege> DataPrivilegeSettings()
        {
            List<IDataPrivilege> pris = new List<IDataPrivilege>();
            //Add data privilege to specific type
            //指定哪些模型需要数据权限

            return pris;
        }

        /// <summary>
        /// Set sub directory of uploaded files
        /// 动态设置上传文件的子目录
        /// </summary>
        /// <param name="fh">IWtmFileHandler</param>
        /// <returns>subdir name</returns>
        public string SubDirSelector(IWtmFileHandler fh)
        {
            return null;
        }

        /// <summary>
        /// Custom Reload user process when cache is not available
        /// 设置自定义的方法重新读取用户信息，这个方法会在用户缓存失效的时候调用
        /// </summary>
        /// <param name="context"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public LoginUserInfo ReloadUser(WTMContext context, string account)
        {
            return null;
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // 这里可以添加你的授权逻辑
            List<string> users = new List<string>() { "周鑫", "林杰", "陈嗣医", "陈浩敏" };
            var httpContext = context.GetHttpContext();
            bool ret = httpContext.User.Identity.IsAuthenticated
                   && users.Contains(httpContext.User.Identity.Name);
            return ret;
        }
    }
}
