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
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs;
using WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs;

namespace WMS.Services
{
    public class ErpSyncTaskService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ErpSyncTaskService> _logger;
        private readonly IConfiguration _config;
        private readonly WTMContext _wtm;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="scopeFactory"></param>
        /// <param name="config"></param>
        public ErpSyncTaskService(ILogger<ErpSyncTaskService> logger, IServiceScopeFactory scopeFactory, IConfiguration config)
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

        #region 同步组织任务（无依赖）

        /// <summary>
        /// 同步组织任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncOrgTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncOrgAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步组织任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncOrgAsync()
        {
            // 这里是你的实际业务逻辑
            // 例如：
            // - 清理旧数据
            // - 发送每日报告
            // - 同步外部系统数据
            // - 生成每日统计
            await Task.Run(() =>
            {
                BaseOrganizationVM vm = _wtm.CreateVM<BaseOrganizationVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步部门任务（依赖组织）

        /// <summary>
        /// 同步部门任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncDeptTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncDeptAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步部门任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncDeptAsync()
        {
            await Task.Run(() =>
            {
                BaseDepartmentVM vm = _wtm.CreateVM<BaseDepartmentVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步存储地点任务（依赖组织）

        /// <summary>
        /// 同步存储地点任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncWhTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncWhAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步存储地点任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncWhAsync()
        {
            await Task.Run(() =>
            {
                BaseWareHouseVM vm = _wtm.CreateVM<BaseWareHouseVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步供应商任务（依赖组织）

        /// <summary>
        /// 同步供应商任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncSupplierTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncSupplierAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步供应商任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncSupplierAsync()
        {
            await Task.Run(() =>
            {
                BaseSupplierVM vm = _wtm.CreateVM<BaseSupplierVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步单位任务（无依赖）

        /// <summary>
        /// 同步单位任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncUnitTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncUnitAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步单位任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncUnitAsync()
        {
            await Task.Run(() =>
            {
                BaseUnitVM vm = _wtm.CreateVM<BaseUnitVM>();
                vm.SyncData("");
            });
        }
        #endregion

        #region 同步料品分析类别任务（无依赖）

        /// <summary>
        /// 同步料品分析类别任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncAnalysisTypeTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncAnalysisTypeAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步料品分析类别任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncAnalysisTypeAsync()
        {
            await Task.Run(() =>
            {
                BaseAnalysisTypeVM vm = _wtm.CreateVM<BaseAnalysisTypeVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步料品分类任务（依赖组织、部门、料品分析类别）
        /// <summary>
        /// 同步料品分类任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncItemCategoryTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncItemCategoryAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步料品分类任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncItemCategoryAsync()
        {
            await Task.Run(() =>
            {
                BaseItemCategoryVM vm = _wtm.CreateVM<BaseItemCategoryVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步业务员任务（依赖部门）

        /// <summary>
        /// 同步业务员任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncOperatorTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncOperatorAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步业务员任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncOperatorAsync()
        {
            await Task.Run(() =>
            {
                BaseOperatorVM vm = _wtm.CreateVM<BaseOperatorVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步调出单单据类型业务（依赖：组织）

        /// <summary>
        /// 同步调出单单据类型业务
        /// </summary>
        /// <returns></returns>
        public async Task SyncTransferOutTypeTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncTransferOutTypeTypeAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步调出单单据类型业务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncTransferOutTypeTypeAsync()
        {
            await Task.Run(() =>
            {
                InventoryTransferOutDirectDocTypeVM vm = _wtm.CreateVM<InventoryTransferOutDirectDocTypeVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步杂发单单据类型业务（依赖：组织）

        /// <summary>
        /// 同步杂发单单据类型业务
        /// </summary>
        /// <returns></returns>
        public async Task SyncOtherShipDocTypeTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncOtherShipDocTypeAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步杂发单单据类型业务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncOtherShipDocTypeAsync()
        {
            await Task.Run(() =>
            {
                InventoryOtherShipDocTypeVM vm = _wtm.CreateVM<InventoryOtherShipDocTypeVM>();
                vm.SyncData("");
            });
        }

        #endregion

        #region 同步料品任务（依赖组织、料品分类、单位、部门、存储地点、分析类别）

        /// <summary>
        /// 同步料品任务
        /// </summary>
        /// <returns></returns>
        public async Task SyncItemMasterTaskAsync()
        {
            try
            {
                _logger.LogInformation("任务开始执行 - {Time}", DateTime.Now);

                // 任务逻辑
                await DoSyncItemMasterAsync();

                _logger.LogInformation("任务执行完成 - {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "任务执行失败");
                throw;  // Hangfire会自动重试
            }
        }

        /// <summary>
        /// 实际执行同步料品任务的逻辑
        /// </summary>
        /// <returns></returns>
        private async Task DoSyncItemMasterAsync()
        {
            await Task.Run(() =>
            {
                BaseItemMasterVM vm = _wtm.CreateVM<BaseItemMasterVM>();
                vm.SyncDataByBatch();
            });
        }

        #endregion
    }
}
