using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model;
using WMS.Model.KnifeManagement;

namespace WMS.Services
{
    public class KnifeOperationMonthlyService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<KnifeOperationMonthlyService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // 检查间隔
        private readonly TimeSpan _executionWindowStart = new TimeSpan(22, 0, 0); // 22:00 (晚上10点)
        private readonly TimeSpan _executionWindowEnd = new TimeSpan(23, 0, 0); // 23:00 (晚上11点)

       
        public KnifeOperationMonthlyService( IServiceScopeFactory scopeFactory,ILogger<KnifeOperationMonthlyService> logger,IConfiguration config)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("刀具操作记录月初自动新增归还领用检测服务启动");

            // 启动时立即检查一次
            await CheckAndExecuteIfNeededAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = GetNextRunTime(now);

                // 计算等待时间（不超过检查间隔）
                var delay = nextRunTime - now;
                if (delay > _checkInterval)
                {
                    delay = _checkInterval;
                }

                _logger.LogInformation("下次计划执行时间：{Time}，等待 {minute} 分钟", nextRunTime, delay.TotalMinutes.ToString("F2"));

                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (!stoppingToken.IsCancellationRequested)
                {
                    await CheckAndExecuteIfNeededAsync();
                }
            }

            _logger.LogInformation("刀具操作记录月初自动新增归还领用检测服务停止");
        }

        private DateTime GetNextRunTime(DateTime currentTime)
        {
            // 获取当月最后一天
            var lastDayOfMonth = new DateTime(currentTime.Year, currentTime.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            // 计算本月最后一天的执行窗口开始时间
            var thisMonthExecutionTime = lastDayOfMonth.Date.Add(_executionWindowStart);

            // 如果当前时间已过本月执行窗口
            if (currentTime > lastDayOfMonth.Date.Add(_executionWindowEnd))
            {
                // 计算下个月最后一天的执行窗口开始时间
                var nextMonth = currentTime.Month == 12 ? 1 : currentTime.Month + 1;
                var nextYear = currentTime.Month == 12 ? currentTime.Year + 1 : currentTime.Year;
                var nextMonthLastDay = new DateTime(nextYear, nextMonth, 1).AddMonths(1).AddDays(-1);
                return nextMonthLastDay.Date.Add(_executionWindowStart);
            }

            // 如果当前时间在执行窗口之前
            if (currentTime < thisMonthExecutionTime)
            {
                return thisMonthExecutionTime;
            }
            //下一分钟
            var nextMinute = currentTime.AddMinutes(1).AddSeconds(-currentTime.Second);
            // 如果下一分钟超出执行窗口，则返回下个月
            if (nextMinute > lastDayOfMonth.Date.Add(_executionWindowEnd))
            {
                // 计算下个月最后一天的执行窗口开始时间
                var nextMonth = currentTime.Month == 12 ? 1 : currentTime.Month + 1;
                var nextYear = currentTime.Month == 12 ? currentTime.Year + 1 : currentTime.Year;
                var nextMonthLastDay = new DateTime(nextYear, nextMonth, 1).AddMonths(1).AddDays(-1);
                return nextMonthLastDay.Date.Add(_executionWindowStart);
            }
            return   nextMinute;

            /*// 当前时间在执行窗口内，计算下一个5分钟检查点
            var nextCheckInWindow = currentTime.AddMinutes(5 - (currentTime.Minute % 5));
            
            // 如果下一个检查点超出执行窗口，则返回下个月
            if (nextCheckInWindow > lastDayOfMonth.Date.Add(_executionWindowEnd))
            {
                var nextMonth = currentTime.Month == 12 ? 1 : currentTime.Month + 1;
                var nextYear = currentTime.Month == 12 ? currentTime.Year + 1 : currentTime.Year;
                var nextMonthLastDay = new DateTime(nextYear, nextMonth, 1).AddMonths(1).AddDays(-1);
                return nextMonthLastDay.Date.Add(_executionWindowStart);
            }

            return nextCheckInWindow;*/

        }

        private async Task CheckAndExecuteIfNeededAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var wtm = scope.ServiceProvider.GetRequiredService<WTMContext>();

            var today = DateTime.Now.Date;
            var currentMonth = DateTime.Now.ToString("yyyy-MM");

            // 获取当月最后一天
            var lastDayOfMonth = new DateTime(today.Year, today.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            // 计算本月最后一天的执行窗口开始时间
            var thisMonthExecutionTime = lastDayOfMonth.Date.Add(_executionWindowStart);

            // 本月最后一天的执行窗口结束时间
            var thisMonthEndTime = lastDayOfMonth.Date.Add(_executionWindowEnd);

            // 检查本月是否已成功执行过
            var lastSuccess = await wtm.DC.Set<KnifeTaskLog>()
            .Where(x => x.TaskName == "MonthlyKnifeOperation" )
            .Where(k => k.OperationTime.Year == today.Year)
            .Where(k => k.OperationTime.Month == today.Month)
            .FirstOrDefaultAsync();

            // 如果本月未执行过，且在时间窗口内
            if (lastSuccess == null &&
                DateTime.Now >= thisMonthExecutionTime &&
                DateTime.Now <= thisMonthEndTime)
            {
                try
                {
                    _logger.LogInformation("开始执行月度刀具更新履历任务");

                    // 执行实际业务逻辑
                    var result = await MonthlyCheckOutAndInKnifes(wtm);

                    // 记录成功日志
                    wtm.DC.AddEntity(new KnifeTaskLog
                    {
                        ID = Guid.NewGuid(),
                        TaskName = "MonthlyKnifeOperation",
                        OperationTime = DateTime.Now,
                        Num = result.Count,
                        
                    });

                    await wtm.DC.SaveChangesAsync();
                    _logger.LogInformation("月度刀具更新履历任务完成，处理了 {Count} 把刀", result.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "月度刀具更新履历任务失败");
                }
            }
        }

        private async Task<(int Count, string Message)> MonthlyCheckOutAndInKnifes(WTMContext wtm)
        {
            int processedCount = 0;

            // 统计刀数量
            var knifes = await wtm.DC.Set<Knife>()
                .Include(x=>x.CurrentCheckOutBy)
                .Include(x=>x.WhLocation)
                .Where(x => x.Status ==KnifeStatusEnum.CheckOut)
                .ToListAsync();


            if (!knifes.Any())
            {
                return (0, "没有刀具被领用 月初不需要自动归还领用");
            }

            // 分批处理
            const int batchSize = 100;
            for (int i = 0; i < knifes.Count; i += batchSize)
            {
                var batch = knifes.Skip(i).Take(batchSize).ToList();

                foreach (var knife in batch)
                {
                    var usedDays = (DateTime.Now.Date - knife.LastOperationDate.Date).Days + 1; // 使用天数包含当天
                    //处理逻辑  月末自动归还 月初自动领用  刀具状态变化
                    wtm.DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId=knife.ID,
                        DocNo = "月末自动归还",
                        OperationType = KnifeOperationTypeEnum.MonthlyCheckIn,
                        OperationTime = DateTime.Now,
                        OperationById = knife.CurrentCheckOutById,
                        HandledById = knife.HandledById,
                        UsedDays = usedDays,
                        RemainingDays = knife.RemainingUsedDays- usedDays,
                        WhLocationId = knife.WhLocationId,
                        CreateBy = "系统服务自动生成",
                        CreateTime = DateTime.Now,

                    });
                    wtm.DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId = knife.ID,
                        DocNo = "月初自动领用",
                        OperationType = KnifeOperationTypeEnum.MonthlyCheckOut,
                        OperationTime = DateTime.Now.AddHours(2),//(10-11) +2 =  (0-1)
                        OperationById = knife.CurrentCheckOutById,
                        HandledById = knife.HandledById,
                        WhLocationId = knife.WhLocationId,
                        CreateBy = "系统服务自动生成",
                        CreateTime = DateTime.Now,
                    });
                    knife.LastOperationDate = DateTime.Now.AddHours(2);
                    knife.TotalUsedDays = knife.TotalUsedDays + usedDays;
                    knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数

                }

                await wtm.DC.SaveChangesAsync();

                // 短暂延迟减轻数据库压力
                await Task.Delay(200);
            }
            processedCount = knifes.Count;
            return (processedCount, $"成功处理了{processedCount} 把刀");
        }
    }

   
}