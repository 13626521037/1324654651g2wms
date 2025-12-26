using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using WMS.Controllers;
using WalkingTec.Mvvm.Core;
using WMS.DataAccess;
using WMS.BaseData.Controllers;
using WMS.ViewModel.BaseData.BaseSupplierVMs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.BaseData.BaseUnitVMs;
using WMS.ViewModel.BaseData.BaseAnalysisTypeVMs;
using WMS.ViewModel.BaseData.BaseItemCategoryVMs;
using WMS.ViewModel.BaseData.BaseOperatorVMs;
using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.ViewModel.KnifeManagement.KnifeOperationVMs;

namespace WMS.Services
{
    public class KnifeMonthlyService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<KnifeMonthlyService> _logger;
        private readonly IConfiguration _config;
        private readonly WTMContext _wtm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeFactory"></param>
        /// <param name="config"></param>
        public KnifeMonthlyService(ILogger<KnifeMonthlyService> logger, IServiceScopeFactory scopeFactory, IConfiguration config)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _config = config;

            using var scope = _scopeFactory.CreateScope();
            _wtm = scope.ServiceProvider.GetRequiredService<WTMContext>();
            _wtm.LoginUserInfo = new LoginUserInfo
            {
                ITCode = "admin",
                Name = "系统管理员"
            };
        }


        public async Task MonthlyCheckInAndOutTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoMonthlyCheckInAndOutTaskAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoMonthlyCheckInAndOutTaskAsync()
        {

            KnifeOperationVM vm = _wtm.CreateVM<KnifeOperationVM>();
            await vm.MonthlyCheckInAndOutTask();
            /*await Task.Run(() =>
            {
                KnifeOperationVM vm = _wtm.CreateVM<KnifeOperationVM>();
                await vm.MonthlyCheckInAndOutTask();

            });*/
        }


    }
}
