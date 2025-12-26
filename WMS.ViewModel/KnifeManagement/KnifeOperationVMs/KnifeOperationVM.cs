using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
using Microsoft.AspNetCore.Http;
using NPOI.SS.Formula.Functions;
using Jint.Native;
namespace WMS.ViewModel.KnifeManagement.KnifeOperationVMs
{
    public partial class KnifeOperationVM : BaseCRUDVM<KnifeOperation>
    {
        
        public List<string> KnifeManagementKnifeOperationFTempSelected { get; set; }

        public KnifeOperationVM()
        {

            SetInclude(x => x.Knife);
            SetInclude(x => x.OperationBy);
            SetInclude(x => x.HandledBy);
            SetInclude(x => x.WhLocation);

        }


        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<KnifeOperation> SetDuplicatedCheck()
        {
            return null;

        }

        public override async Task DoAddAsync()        
        {
            
            await base.DoAddAsync();

        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            
            await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }

        /// <summary>
        /// 获取刀具履历记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public List<KnifeOperationsReturn> getKnifeOperations(GetKnifeOperationsInputInfo info)
        {
            var result = new List<KnifeOperationsReturn>();
            try
            {
                var users = DC.Set<FrameworkUser>().ToList();
                DateTime begin= new DateTime();
                DateTime end = new DateTime();
                string knifeNo = info.KnifeNo;

                if(string.IsNullOrEmpty(info.Begin)!= string.IsNullOrEmpty(info.End))//两个都填或者都不填
                {
                    MSD.AddModelError("", "时间参数缺失");
                    return null;
                }
                if (!string.IsNullOrEmpty(info.Begin)&&!DateTime.TryParse(info.Begin, out begin))//填了要校验
                {
                    MSD.AddModelError("", "开始时间格式无效");
                    return null;
                }
                if (!string.IsNullOrEmpty(info.End) && !DateTime.TryParse(info.End, out end))
                {
                    MSD.AddModelError("", "结束时间格式无效");
                    return null;
                }
                if (!string.IsNullOrEmpty(info.KnifeNo) &&!DC.Set<Knife>().Any(x => x.SerialNumber == info.KnifeNo))
                {
                    MSD.AddModelError("", "无效刀号");
                    return null;
                }


                if (string.IsNullOrEmpty(knifeNo) && begin == DateTime.MinValue && end == DateTime.MinValue)
                {
                    MSD.AddModelError("", "请输入参数" );
                    return null;
                }
                if (!string.IsNullOrEmpty(knifeNo) && begin == DateTime.MinValue && end == DateTime.MinValue)
                {
                    result = DC.Set<KnifeOperation>()
                    .Include(x => x.Knife).ThenInclude(x => x.ItemMaster)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                    .Include(x => x.OperationBy).ThenInclude(x => x.Department)
                    .Where(x => x.Knife.SerialNumber == knifeNo)
                    .Select(x => new KnifeOperationsReturn()
                    {
                        Id = x.ID.ToString(),
                        KnifeNo = x.Knife.SerialNumber,
                        OperationType = (KnifeOperationTypeEnum)x.OperationType,
                        OperationType_Trl = x.OperationType.GetEnumDisplayName(),
                        OperationTime = ((DateTime)x.OperationTime).ToString("yyyy-MM-dd HH:mm:ss.fff"),
                        OperationBy = x.OperationBy.Name,
                        HandledBy = x.HandledById,
                        UsedDays = x.UsedDays,
                        TotalUsedDays = x.TotalUsedDays,
                        RemainingDays = x.RemainingDays,
                        CurrentLife = x.CurrentLife,
                        WhLocation = x.WhLocation == null ? "" : x.WhLocation.Name,
                        WareHouseId = x.WhLocation.WhArea.WareHouse.SourceSystemId,
                        WareHouseCode = x.WhLocation.WhArea.WareHouse.Code,
                        WareHouseName = x.WhLocation.WhArea.WareHouse.Name,
                        GrindNum = x.GrindNum == null ? 0 : (decimal)x.GrindNum,//
                        U9SourceLineID = x.OperationType == KnifeOperationTypeEnum.Created ? x.Knife.MiscShipLineID : x.U9SourceLineID,//建档的给杂发行id
                        U9SourceLineType = (x.OperationType == KnifeOperationTypeEnum.GrindRequest ? "请购" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.GrindingIn ? "收货" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.Created ? "杂发" : null)//?? ((x.OperationType == KnifeOperationTypeEnum.Created && !string.IsNullOrEmpty(x.U9SourceLineID)) ? "收货" : null)//本来是建档而且有收货行id放收货 现在改为一定会有的杂发行id
                                            ?? "其他",
                        MsicShipLineID = x.Knife.MiscShipLineID,
                        ItemCode = x.Knife.ItemMaster.Code,
                        ItemId = x.Knife.ItemMaster.SourceSystemId,
                        DocNo = x.DocNo,
                        OperationDeptId = x.OperationBy.Department.SourceSystemId,
                        OperationDeptCode = x.OperationBy.Department.Code,
                        OperationDeptName = x.OperationBy.Department.Name,
                        Remake = (x.OperationType == KnifeOperationTypeEnum.CheckIn && x.UsedDays == 0) ? "错领归还":"",

                    })
                    .AsNoTracking()
                    .ToList();
                }
                if (string.IsNullOrEmpty(knifeNo) && begin != DateTime.MinValue && end != DateTime.MinValue)
                {
                    result = DC.Set<KnifeOperation>()
                   .Include(x => x.Knife).ThenInclude(x => x.ItemMaster)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                   .Include(x=>x.OperationBy).ThenInclude(x=>x.Department)
                   .Where(x => x.OperationTime>=begin && x.OperationTime <= end)
                   .Select(x => new KnifeOperationsReturn()
                   {
                       Id = x.ID.ToString(),
                       KnifeNo = x.Knife.SerialNumber,
                       OperationType = (KnifeOperationTypeEnum)x.OperationType,
                       OperationType_Trl = x.OperationType.GetEnumDisplayName(),
                       OperationTime = ((DateTime)x.OperationTime).ToString("yyyy-MM-dd HH:mm:ss.fff"),
                       OperationBy = x.OperationBy.Name,
                       HandledBy = x.HandledById,
                       TotalUsedDays = x.TotalUsedDays,
                       RemainingDays = x.RemainingDays,

                       CurrentLife = x.CurrentLife,
                       UsedDays = x.UsedDays,
                       WhLocation = x.WhLocation == null ? "" : x.WhLocation.Name,
                       WareHouseId = x.WhLocation.WhArea.WareHouse.SourceSystemId,
                       WareHouseCode = x.WhLocation.WhArea.WareHouse.Code,
                       WareHouseName = x.WhLocation.WhArea.WareHouse.Name,
                       GrindNum = x.GrindNum == null ? 0 : (decimal)x.GrindNum,//
                       U9SourceLineID = x.OperationType == KnifeOperationTypeEnum.Created ? x.Knife.MiscShipLineID : x.U9SourceLineID,//建档的给杂发行id
                       U9SourceLineType = (x.OperationType == KnifeOperationTypeEnum.GrindRequest ? "请购" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.GrindingIn ? "收货" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.Created ? "杂发" : null)//?? ((x.OperationType == KnifeOperationTypeEnum.Created && !string.IsNullOrEmpty(x.U9SourceLineID)) ? "收货" : null)//本来是建档而且有收货行id放收货 现在改为一定会有的杂发行id
                                            ?? "其他",
                       MsicShipLineID = x.Knife.MiscShipLineID,
                       ItemCode = x.Knife.ItemMaster.Code,
                       ItemId = x.Knife.ItemMaster.SourceSystemId,
                       DocNo = x.DocNo,
                       OperationDeptId = x.OperationBy.Department.SourceSystemId,
                       OperationDeptCode = x.OperationBy.Department.Code,
                       OperationDeptName = x.OperationBy.Department.Name,
                       Remake = (x.OperationType == KnifeOperationTypeEnum.CheckIn && x.UsedDays == 0) ? "错领归还":"",


                   })
                    .ToList();
                }
                if (!string.IsNullOrEmpty(knifeNo) && begin != DateTime.MinValue && end != DateTime.MinValue)
                {
                    result = DC.Set<KnifeOperation>()
                   .Include(x => x.Knife).ThenInclude(x => x.ItemMaster)
                    .Include(x => x.WhLocation).ThenInclude(x => x.WhArea).ThenInclude(x => x.WareHouse)
                   .Include(x => x.OperationBy).ThenInclude(x => x.Department)

                   .Where(x => x.Knife.SerialNumber == knifeNo)
                   .Where(x => x.OperationTime >= begin && x.OperationTime <= end)
                   .Select(x => new KnifeOperationsReturn()
                   {
                       Id = x.ID.ToString(),
                       KnifeNo = x.Knife.SerialNumber,
                       OperationType = (KnifeOperationTypeEnum)x.OperationType,
                       OperationType_Trl = x.OperationType.GetEnumDisplayName(),
                       OperationTime = ((DateTime)x.OperationTime).ToString("yyyy-MM-dd HH:mm:ss.fff"),
                       OperationBy = x.OperationBy.Name,
                       HandledBy = x.HandledById,
                       UsedDays = x.UsedDays,
                       TotalUsedDays = x.TotalUsedDays,
                       RemainingDays = x.RemainingDays,
                       CurrentLife = x.CurrentLife,
                       WhLocation = x.WhLocation == null ? "" : x.WhLocation.Name,
                       WareHouseId = x.WhLocation.WhArea.WareHouse.SourceSystemId,
                       WareHouseCode = x.WhLocation.WhArea.WareHouse.Code,
                       WareHouseName = x.WhLocation.WhArea.WareHouse.Name,
                       GrindNum = x.GrindNum == null ? 0 : (decimal)x.GrindNum,//
                       U9SourceLineID = x.OperationType == KnifeOperationTypeEnum.Created ? x.Knife.MiscShipLineID : x.U9SourceLineID,//建档的给杂发行id
                       U9SourceLineType = (x.OperationType == KnifeOperationTypeEnum.GrindRequest ? "请购" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.GrindingIn ? "收货" : null)
                                            ?? (x.OperationType == KnifeOperationTypeEnum.Created ? "杂发" : null)//?? ((x.OperationType == KnifeOperationTypeEnum.Created && !string.IsNullOrEmpty(x.U9SourceLineID)) ? "收货" : null)//本来是建档而且有收货行id放收货 现在改为一定会有的杂发行id
                                            ?? "其他",
                       MsicShipLineID = x.Knife.MiscShipLineID,
                       ItemCode = x.Knife.ItemMaster.Code,
                       ItemId = x.Knife.ItemMaster.SourceSystemId,
                       DocNo = x.DocNo,
                       OperationDeptId = x.OperationBy.Department.SourceSystemId,
                       OperationDeptCode = x.OperationBy.Department.Code,
                       OperationDeptName = x.OperationBy.Department.Name,
                       Remake = (x.OperationType == KnifeOperationTypeEnum.CheckIn && x.UsedDays == 0) ? "错领归还":"",

                   })
                    .ToList();
                }
                foreach (var l in result)
                {
                    var tempId = l.HandledBy;
                    var tempName = users.Where(x => x.ID.ToString().ToUpper() == tempId.ToUpper()).Select(x => x.Name).FirstOrDefault();
                    l.HandledBy = tempName;
                }


            }
            catch (Exception e)
            {
                MSD.AddModelError("", "异常" + e.Message);
                return null;
            }
            return result;//

        }

        /// <summary>
        /// 根据当前履历信息计算所有料品的寿命信息
        /// </summary>
        /// <returns></returns>
        public List<KnifeItemLivesReturn> GetKnifeItemLives()
        {
            try
            {
                // 1. 获取所有刀具操作记录
                var allOperations = DC.Set<KnifeOperation>()
                    .Include(o => o.Knife).ThenInclude(x=>x.ItemMaster)
                    .ToList();

                var result =  allOperations
                    .GroupBy(o => o.Knife.ItemMaster.Code)
                    .Where(g => g.Key != null)
                    .Select(itemGroup => CalculateMaterialLife(itemGroup.Key, itemGroup))
                    .ToList();

                return result;

            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "异常:"+ ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 计算料品的初始/一次修磨...五次修磨寿命
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemGroup"></param>
        /// <returns></returns>
        private KnifeItemLivesReturn CalculateMaterialLife(string itemCode, IGrouping<string, KnifeOperation> itemGroup)
        {
            var knifeGroups = itemGroup.GroupBy(o => o.Knife.SerialNumber);
            var result = new KnifeItemLivesReturn { ItemCode = itemCode };
            var initialLives = new List<decimal>();
            var grindingLifeDict = new Dictionary<int, List<decimal>>();

            foreach (var knifeGroup in knifeGroups)
            {
                var operations = knifeGroup.OrderBy(o => o.OperationTime).ToList();//排序 不排好像也行

               
                // 4. 计算初始寿命
                var initialLifeRecord = operations.FirstOrDefault(o =>
                    (o.GrindNum == 0 && o.OperationType == KnifeOperationTypeEnum.Scrap && o.IsAccident == false) || 
                    (o.GrindNum == 1 && o.OperationType == KnifeOperationTypeEnum.GrindingIn));

                if (initialLifeRecord != null)
                {
                    initialLives.Add((decimal)initialLifeRecord.TotalUsedDays);
                }

                // 5. 计算各次修磨寿命
                for (int i = 1; i <= 100; i++) // 最大100次修磨
                {
                    var grindingIn = operations.FirstOrDefault(o =>
                        o.GrindNum == (i) && o.OperationType == KnifeOperationTypeEnum.GrindingIn);

                    var grindingEnd = operations.FirstOrDefault(o =>
                        (o.GrindNum == (i ) && o.OperationType == KnifeOperationTypeEnum.Scrap && o.IsAccident == false) ||
                        (o.GrindNum == (i + 1) && o.OperationType == KnifeOperationTypeEnum.GrindingIn));

                    if (grindingIn != null && grindingEnd != null)//&& grindingEnd.TotalUsedDays - grindingIn.TotalUsedDays!=0
                    {
                        if (!grindingLifeDict.ContainsKey(i))
                        {
                            grindingLifeDict[i] = new List<decimal>();
                        }
                        grindingLifeDict[i].Add((decimal)(grindingEnd.TotalUsedDays - grindingIn.TotalUsedDays));
                    }
                }
            }

            // 6. 设置返回结果
            result.InitialLife = initialLives.Any() ? Math.Round(initialLives.Average(), 2).ToString("0.00") : "0.00";
            result.GrindLivePairs = grindingLifeDict
                .OrderBy(kv => kv.Key)
                .Where(kv => kv.Value.Any())
                .Select(kv => new GrindLifePair(
                    grindNum: $"{kv.Key}",
                    //grindNum: $"第{kv.Key}次修磨",
                    grindLife: Math.Round((decimal)kv.Value.Average(), 2) // 保留2位小数
                ))
                .ToList();
            
            return result;
        }

        /// <summary>
        /// 月末自动处理领用状态刀具任务执行
        /// </summary>
        /// <returns></returns>
        public async Task MonthlyCheckInAndOutTask()
        {

            //判断月末
            var currentTime = DateTime.Now;
            var lastDayOfMonth = new DateTime(currentTime.Year, currentTime.Month, 1).AddMonths(1).AddDays(-1);
            if(lastDayOfMonth!= currentTime.Date)
            {
                return;
            }
            //判断已执行
            var lastSuccess =await DC.Set<KnifeTaskLog>()
                .Where(x => x.TaskName == "MonthlyKnifeOperation")
                .Where(k => k.OperationTime.Year == DateTime.Now.Date.Year)
                .Where(k => k.OperationTime.Month == DateTime.Now.Date.Month)
                .FirstOrDefaultAsync();
            if (lastSuccess != null)
            {
                return;
            }
            //开始归还与领用
            try
            {
                // 执行实际业务逻辑
                var result = await MonthlyCheckOutAndInKnifes();

                // 记录成功日志
                DC.AddEntity(new KnifeTaskLog
                {
                    ID = Guid.NewGuid(),
                    TaskName = "MonthlyKnifeOperation",
                    OperationTime = DateTime.Now,
                    Num = result.Count,

                });

                await DC.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "异常:"+ ex.Message);
            }
        }

        /// <summary>
        /// 月末自动处理领用状态刀具(新增归还和领用)
        /// </summary>
        /// <returns></returns>
        private async Task<(int Count, string Message)> MonthlyCheckOutAndInKnifes()
        {
            int processedCount = 0;
            //当前操作人
            var handledBy = DC.Set<FrameworkUser>().FirstOrDefault(x => x.ITCode == LoginUserInfo.ITCode);
            // 统计刀数量
            var knifes = await DC.Set<Knife>()
                .Include(x => x.CurrentCheckOutBy)
                .Include(x => x.WhLocation)
                .Where(x => x.Status == KnifeStatusEnum.CheckOut)
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
                    knife.LastOperationDate = DateTime.Now.AddHours(2);
                    knife.TotalUsedDays = knife.TotalUsedDays + usedDays;
                    knife.RemainingUsedDays = Math.Max(0, (int)(knife.RemainingUsedDays - usedDays));//剩余使用天数
                    //处理逻辑  月末自动归还 月初自动领用  刀具状态变化
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId = knife.ID,
                        DocNo = "月末自动归还",
                        OperationType = KnifeOperationTypeEnum.MonthlyCheckIn,
                        OperationTime = DateTime.Now,
                        OperationById = knife.CurrentCheckOutById,
                        HandledById = knife.HandledById,
                        HandledBy = handledBy,
                        HandledByName = handledBy.Name,
                        UsedDays = usedDays,
                        RemainingDays = knife.RemainingUsedDays,
                        TotalUsedDays = knife.TotalUsedDays ,
                        CurrentLife = knife.CurrentLife,
                        WhLocationId = knife.WhLocationId,
                        CreateBy = "系统服务自动生成",
                        CreateTime = DateTime.Now,

                    });
                    DC.Set<KnifeOperation>().Add(new KnifeOperation
                    {
                        KnifeId = knife.ID,
                        DocNo = "月初自动领用",
                        OperationType = KnifeOperationTypeEnum.MonthlyCheckOut,
                        OperationTime = DateTime.Now.AddHours(2),//(10-11) +2 =  (0-1)
                        OperationById = knife.CurrentCheckOutById,
                        HandledById = knife.HandledById,
                        HandledBy = handledBy,
                        HandledByName = handledBy.Name,
                        UsedDays = 0,
                        TotalUsedDays = knife.TotalUsedDays,
                        RemainingDays = knife.RemainingUsedDays,
                        CurrentLife = knife.CurrentLife,
                        WhLocationId = knife.WhLocationId,
                        CreateBy = "系统服务自动生成",
                        CreateTime = DateTime.Now,
                    });
                   

                }

                await DC.SaveChangesAsync();

                // 短暂延迟减轻数据库压力
                await Task.Delay(200);
            }
            processedCount = knifes.Count;
            return (processedCount, $"成功处理了{processedCount} 把刀");
        }


    }
}
