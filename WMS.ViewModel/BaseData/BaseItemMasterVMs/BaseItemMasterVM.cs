using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseItemCategoryVMs;
using WMS.ViewModel.BaseData.BaseUnitVMs;
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.BaseData.BaseAnalysisTypeVMs;
using WMS.Model.BaseData;
using WMS.Model;
using System.Linq.Expressions;
using WMS.DataAccess;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
using EFCore.BulkExtensions;
using NPOI.SS.Formula.Functions;
using System.Data;
namespace WMS.ViewModel.BaseData.BaseItemMasterVMs
{
    public partial class BaseItemMasterVM : BaseCRUDVM<BaseItemMaster>
    {
        /// <summary>
        /// 同步更新数量
        /// </summary>
        public int UpdateQty { get; set; }

        /// <summary>
        /// 同步新增数量
        /// </summary>
        public int InsertQty { get; set; }

        /// <summary>
        /// 同步删除数量
        /// </summary>
        public int DeleteQty { get; set; }

        /// <summary>
        /// 需要更新的料品ID集合
        /// </summary>
        public List<Guid> UpdateIds { get; set; }

        public List<string> BaseDataBaseItemMasterFTempSelected { get; set; }

        public BaseItemMasterVM()
        {

            SetInclude(x => x.Organization);
            SetInclude(x => x.ItemCategory);
            SetInclude(x => x.StockUnit);
            SetInclude(x => x.ProductionOrg);
            SetInclude(x => x.ProductionDept);
            SetInclude(x => x.Wh);
            SetInclude(x => x.AnalysisType);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseItemMaster> SetDuplicatedCheck()
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
        /// 分批同步数据
        /// </summary>
        public void SyncDataByBatch()
        {
            int blockSize = 5000;    // 设置分批同步的块大小
            UpdateQty = 0;
            InsertQty = 0;
            DeleteQty = 0;
            UpdateIds = new List<Guid>();
            DateTime? lastUpdateTime = null;
            BaseSysParaVM baseSysParaVM = Wtm.CreateVM<BaseSysParaVM>();
            List<string> codes = new List<string>();

            // 全量同步时，以系统参数中的最后更新时间为准进行同步
            lastUpdateTime = baseSysParaVM.GetParaDateTimeValue("BaseItemMasterLastUpdateTime");
            if (lastUpdateTime == null)
            {
                MSD.AddModelError("SysParaError", "获取系统参数BaseItemMasterLastUpdateTime失败，请联系系统管理员");
                return;
            }

            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            GetErpLastUpdateTime(apiHelper);
            if (!MSD.IsValid)
            {
                return;
            }
            // 注意：每个块成功时，直接提交数据库。只有所有块都成功时，才修改系统参数中的最后更新时间。
            List<string> addList = GetAddOrUpdateIds(blockSize, false);     // 新增块列表。耗时：5"，52*5000数据量
            List<string> updateList = GetAddOrUpdateIds(blockSize, true);   // 修改块列表。耗时：4", 48*5000数据量
            List<string> all = addList.Concat(updateList).ToList(); // 所有块列表（新增+修改）

            //using (var trans = DC.BeginTransaction()) // 取消事务。让同步到一半被中断时，可以继续同步。（★注意：最后更新时间，必须要在全部同步成功后才修改）
            //{

            DateTime updateTime = new DateTime(2000, 1, 1); // 最后更新时间

            var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
            var cateVM = Wtm.CreateVM<BaseItemCategoryVM>();
            var unitVM = Wtm.CreateVM<BaseUnitVM>();
            var deptVM = Wtm.CreateVM<BaseDepartmentVM>();
            var whVM = Wtm.CreateVM<BaseWareHouseVM>();
            var analysisTypeVM = Wtm.CreateVM<BaseAnalysisTypeVM>();
            List<BaseItemMaster> originalItems = new List<BaseItemMaster>();
            if (updateList.Count > 0)   // 要改成直接用数据库查询
                //originalItems = DC.Set<BaseItemMaster>().Where(x => UpdateIds.Contains(x.ID)).AsNoTracking().ToList();
                // 耗时：16"
                originalItems = DC.Set<BaseItemMaster>().FromSql($"select item.* from (select ID from (SELECT temp.U9ID,item.ID,temp.ModifiedOn,item.LastUpdateTime FROM BaseItemMasterTemp temp left join BaseItemMaster item on item.SourceSystemId=temp.U9ID) a where id is not null and ModifiedOn>LastUpdateTime) b left join BaseItemMaster item on b.ID=item.ID").ToList();

            for (int m = 0; m < all.Count; m++)
            {
                bool isAdd = true;
                // 如果索引m超过了addList的长度，则说明是修改块，否则是新增块
                if (m >= addList.Count)
                {
                    isAdd = false;
                }
                U9Return<List<BaseItemMaster>> u9Return = apiHelper.GetItems("", lastUpdateTime, ids: all[m]);  // 耗时：1-2"，5000数据量（注意：循环内，需加n倍）
                if (u9Return.Success)
                {
                    // 同步成功。将数据同步到WMS数据库
                    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                    {
                        List<BaseItemMaster> newDatas = new List<BaseItemMaster>();
                        List<BaseItemMaster> updateDatas = new List<BaseItemMaster>();
                        List<BaseItemMaster> deleteDatas = new List<BaseItemMaster>();
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        int i = 0;
                        foreach (var syncEntity in u9Return.Entity) // 转换过程：第一次较慢，需加载缓存数据，后续较快。非首次耗时：新增：4"，5000数据量；修改：12"，5000数据量
                        {
                            i++;
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseItemMaster matchItem = null;
                            if (!isAdd)
                            {
                                if (originalItems != null && originalItems.Count > 0)
                                {
                                    matchItem = originalItems.Find(x => x.SourceSystemId == syncEntity.SourceSystemId);
                                    if (matchItem == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步时修改原料品失败。料品【{syncEntity.Code}】不存在。");   // 不会出现。前面是先判断有才进入更新列表的
                                        return;
                                    }
                                }
                            }
                            // if (matchItem == null)   // 原来不存在，新增
                            if (isAdd)
                            {
                                Entity = syncEntity;
                                syncEntity.IsValid = true;
                                // 料品组织
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncOrg);
                                if (Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                // 料品分类
                                Entity.ItemCategoryId = cateVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncCategory);
                                if (Entity.ItemCategoryId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分类不存在。请先尝试同步料品分类数据");
                                    return;
                                }
                                // 计量单位
                                Entity.StockUnitId = unitVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncUnit);
                                if (Entity.StockUnitId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的计量单位不存在。请先尝试同步计量单位数据");
                                    return;
                                }
                                // 生产组织
                                if (!string.IsNullOrEmpty(syncEntity.SyncPOrg))
                                {
                                    Entity.ProductionOrgId = orgVM.GetIdByCodeFromCache(syncEntity.SyncPOrg);   // 根据编码查找
                                    if (Entity.ProductionOrgId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产组织不存在。请先尝试同步组织数据");
                                        return;
                                    }
                                }
                                // 生产部门
                                if (!string.IsNullOrEmpty(syncEntity.SyncPDept))
                                {
                                    Entity.ProductionDeptId = deptVM.GetIdByCodeFromCache(syncEntity.SyncPDept);   // 根据编码查找
                                    if (Entity.ProductionDeptId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产部门不存在。请先尝试同步部门数据");
                                        return;
                                    }
                                }
                                // 默认存储地点
                                if (!string.IsNullOrEmpty(syncEntity.SyncWh))
                                {
                                    Entity.WhId = whVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncWh);
                                    if (Entity.WhId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的存储地点不存在。请先尝试同步存储地点数据");
                                        return;
                                    }
                                }
                                // 料品分析类别
                                if (!string.IsNullOrEmpty(syncEntity.SyncAnalysisType))
                                {
                                    Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncEntity.SyncAnalysisType);   // 根据编码查找
                                    if (Entity.AnalysisTypeId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分析类别不存在。请先尝试同步料品分析类别数据");
                                        return;
                                    }
                                }
                                Entity.ID = Guid.NewGuid();
                                Entity.CreateBy = LoginUserInfo?.ITCode;
                                Entity.CreateTime = DateTime.Now;
                                newDatas.Add(Entity);
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                // 归属组织不可能发生变化，所以不需要更新OrganizationId
                                Entity = matchItem;
                                Entity.Code = syncEntity.Code;
                                Entity.Name = syncEntity.Name;
                                Entity.LastUpdateTime = syncEntity.LastUpdateTime;
                                Entity.IsEffective = syncEntity.IsEffective;
                                Entity.UpdateBy = LoginUserInfo?.ITCode;
                                Entity.UpdateTime = DateTime.Now;
                                // 料品分类
                                Entity.ItemCategoryId = cateVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncCategory);
                                if (Entity.ItemCategoryId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分类不存在。请先尝试同步料品分类数据");
                                    return;
                                }
                                // 计量单位
                                Entity.StockUnitId = unitVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncUnit);
                                if (Entity.StockUnitId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的计量单位不存在。请先尝试同步计量单位数据");
                                    return;
                                }
                                // 生产组织
                                if (!string.IsNullOrEmpty(syncEntity.SyncPOrg))
                                {
                                    Entity.ProductionOrgId = orgVM.GetIdByCodeFromCache(syncEntity.SyncPOrg);   // 根据编码查找
                                    if (Entity.ProductionOrgId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产组织不存在。请先尝试同步组织数据");
                                        return;
                                    }
                                }
                                // 生产部门
                                if (!string.IsNullOrEmpty(syncEntity.SyncPDept))
                                {
                                    Entity.ProductionDeptId = deptVM.GetIdByCodeFromCache(syncEntity.SyncPDept);   // 根据编码查找
                                    if (Entity.ProductionDeptId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产部门不存在。请先尝试同步部门数据");
                                        return;
                                    }
                                }
                                // 默认存储地点
                                if (!string.IsNullOrEmpty(syncEntity.SyncWh))
                                {
                                    Entity.WhId = whVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncWh);
                                    if (Entity.WhId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的存储地点不存在。请先尝试同步存储地点数据");
                                        return;
                                    }
                                }
                                // 料品分析类别
                                if (!string.IsNullOrEmpty(syncEntity.SyncAnalysisType))
                                {
                                    Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncEntity.SyncAnalysisType);   // 根据编码查找
                                    if (Entity.AnalysisTypeId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分析类别不存在。请先尝试同步料品分析类别数据");
                                        return;
                                    }
                                }
                                updateDatas.Add(Entity);
                                UpdateQty++;
                            }
                        }
                        if (newDatas.Count > 0)
                        {
                            // 批量添加新数据
                            ((DataContext)DC).BulkInsert(newDatas); // BulkInsert插入数据较快。耗时：2"，5000数据量
                        }
                        if (updateDatas.Count > 0)
                        {
                            // 批量更新数据
                            ((DataContext)DC).BulkUpdate(updateDatas);  // 耗时：1-2"，5000数据量
                        }
                        var date1 = (DateTime)u9Return.Entity.Max(x => x.LastUpdateTime);
                        if (date1 > updateTime)
                        {
                            updateTime = date1;
                        }
                    }
                }
                else
                {
                    MSD.AddModelError("SyncResult", "同步失败。" + u9Return.Msg);
                    return;
                }
            }
            // 更新系统参数，记录最后同步时间
            if (MSD.IsValid)
            {
                baseSysParaVM.SetParaValue("BaseItemMasterLastUpdateTime", updateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
            //if (MSD.IsValid)
            //{
            //    trans.Commit();
            //}
            //}
        }

        /// <summary>
        /// 同步数据（此接口更改为只支持按编码同步，不支持按最后更新时间同步（效率太低））
        /// </summary>
        /// <param name="code">此方法的code支持多个编码，以逗号分隔</param>
        public void SyncData(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                MSD.AddModelError("SyncResult", "同步失败。料品编码不能为空");
                return;
            }
            UpdateQty = 0;
            InsertQty = 0;
            DeleteQty = 0;
            DateTime? lastUpdateTime = null;
            BaseSysParaVM baseSysParaVM = Wtm.CreateVM<BaseSysParaVM>();
            List<string> codes = new List<string>();
            if (string.IsNullOrEmpty(code))
            {
                // 全量同步时，以系统参数中的最后更新时间为准进行同步
                lastUpdateTime = baseSysParaVM.GetParaDateTimeValue("BaseItemMasterLastUpdateTime");
                if (lastUpdateTime == null)
                {
                    MSD.AddModelError("SysParaError", "获取系统参数BaseItemMasterLastUpdateTime失败，请联系系统管理员");
                    return;
                }
            }
            else
            {
                // 注意：此方法的code支持多个编码，以逗号分隔
                codes = code.Split(',').ToList();
                lastUpdateTime = new DateTime(2000, 1, 1);  // 单条数据同步时，将最后更新时间设置为2000-01-01，保证此条数据被同步
            }
            Expression<Func<BaseItemMaster, bool>> predicate = x => (codes.Count == 0 || codes.Contains(x.Code) || codes.Contains(x.SourceSystemId));   // && x.LastUpdateTime > lastUpdateTime;
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseItemMaster>> u9Return = apiHelper.GetItems(code, lastUpdateTime);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                var cateVM = Wtm.CreateVM<BaseItemCategoryVM>();
                var unitVM = Wtm.CreateVM<BaseUnitVM>();
                var deptVM = Wtm.CreateVM<BaseDepartmentVM>();
                var whVM = Wtm.CreateVM<BaseWareHouseVM>();
                var analysisTypeVM = Wtm.CreateVM<BaseAnalysisTypeVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseItemMaster> originalItems = DC.Set<BaseItemMaster>().Where(predicate).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        List<BaseItemMaster> newDatas = new List<BaseItemMaster>();
                        List<BaseItemMaster> updateDatas = new List<BaseItemMaster>();
                        List<BaseItemMaster> deleteDatas = new List<BaseItemMaster>();
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        int i = 0;
                        foreach (var syncEntity in u9Return.Entity)
                        {
                            i++;
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseItemMaster matchItem = null;
                            if (originalItems != null && originalItems.Count > 0)
                            {
                                matchItem = originalItems.Find(x => x.SourceSystemId == syncEntity.SourceSystemId);
                            }
                            if (matchItem == null)   // 原来不存在，新增
                            {
                                Entity = syncEntity;
                                syncEntity.IsValid = true;
                                // 料品组织
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncOrg);
                                if (Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                // 料品分类
                                Entity.ItemCategoryId = cateVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncCategory);
                                if (Entity.ItemCategoryId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分类不存在。请先尝试同步料品分类数据");
                                    return;
                                }
                                // 计量单位
                                Entity.StockUnitId = unitVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncUnit);
                                if (Entity.StockUnitId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的计量单位不存在。请先尝试同步计量单位数据");
                                    return;
                                }
                                // 生产组织
                                if (!string.IsNullOrEmpty(syncEntity.SyncPOrg))
                                {
                                    Entity.ProductionOrgId = orgVM.GetIdByCodeFromCache(syncEntity.SyncPOrg);   // 根据编码查找
                                    if (Entity.ProductionOrgId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产组织不存在。请先尝试同步组织数据");
                                        return;
                                    }
                                }
                                // 生产部门
                                if (!string.IsNullOrEmpty(syncEntity.SyncPDept))
                                {
                                    Entity.ProductionDeptId = deptVM.GetIdByCodeFromCache(syncEntity.SyncPDept);   // 根据编码查找
                                    if (Entity.ProductionDeptId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产部门不存在。请先尝试同步部门数据");
                                        return;
                                    }
                                }
                                // 默认存储地点
                                if (!string.IsNullOrEmpty(syncEntity.SyncWh))
                                {
                                    Entity.WhId = whVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncWh);
                                    if (Entity.WhId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的存储地点不存在。请先尝试同步存储地点数据");
                                        return;
                                    }
                                }
                                // 料品分析类别
                                if (!string.IsNullOrEmpty(syncEntity.SyncAnalysisType))
                                {
                                    Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncEntity.SyncAnalysisType);   // 根据编码查找
                                    if (Entity.AnalysisTypeId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分析类别不存在。请先尝试同步料品分析类别数据");
                                        return;
                                    }
                                }
                                Entity.ID = Guid.NewGuid();
                                Entity.CreateBy = LoginUserInfo?.ITCode;
                                Entity.CreateTime = DateTime.Now;
                                newDatas.Add(Entity);
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchItem.LastUpdateTime != syncEntity.LastUpdateTime)
                                {
                                    // 归属组织不可能发生变化，所以不需要更新OrganizationId
                                    Entity = matchItem;
                                    Entity.Code = syncEntity.Code;
                                    Entity.Name = syncEntity.Name;
                                    Entity.LastUpdateTime = syncEntity.LastUpdateTime;
                                    Entity.IsEffective = syncEntity.IsEffective;
                                    Entity.UpdateBy = LoginUserInfo?.ITCode;
                                    Entity.UpdateTime = DateTime.Now;
                                    // 料品分类
                                    Entity.ItemCategoryId = cateVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncCategory);
                                    if (Entity.ItemCategoryId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分类不存在。请先尝试同步料品分类数据");
                                        return;
                                    }
                                    // 计量单位
                                    Entity.StockUnitId = unitVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncUnit);
                                    if (Entity.StockUnitId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的计量单位不存在。请先尝试同步计量单位数据");
                                        return;
                                    }
                                    // 生产组织
                                    if (!string.IsNullOrEmpty(syncEntity.SyncPOrg))
                                    {
                                        Entity.ProductionOrgId = orgVM.GetIdByCodeFromCache(syncEntity.SyncPOrg);   // 根据编码查找
                                        if (Entity.ProductionOrgId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产组织不存在。请先尝试同步组织数据");
                                            return;
                                        }
                                    }
                                    // 生产部门
                                    if (!string.IsNullOrEmpty(syncEntity.SyncPDept))
                                    {
                                        Entity.ProductionDeptId = deptVM.GetIdByCodeFromCache(syncEntity.SyncPDept);   // 根据编码查找
                                        if (Entity.ProductionDeptId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的生产部门不存在。请先尝试同步部门数据");
                                            return;
                                        }
                                    }
                                    // 默认存储地点
                                    if (!string.IsNullOrEmpty(syncEntity.SyncWh))
                                    {
                                        Entity.WhId = whVM.GetIdBySourceSystemIdFromCache(syncEntity.SyncWh);
                                        if (Entity.WhId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的存储地点不存在。请先尝试同步存储地点数据");
                                            return;
                                        }
                                    }
                                    // 料品分析类别
                                    if (!string.IsNullOrEmpty(syncEntity.SyncAnalysisType))
                                    {
                                        Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncEntity.SyncAnalysisType);   // 根据编码查找
                                        if (Entity.AnalysisTypeId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。料品【{syncEntity.Code}】的料品分析类别不存在。请先尝试同步料品分析类别数据");
                                            return;
                                        }
                                    }
                                    updateDatas.Add(Entity);
                                    UpdateQty++;
                                }
                            }
                        }
                        if (newDatas.Count > 0)
                        {
                            // 批量添加新数据
                            ((DataContext)DC).BulkInsert(newDatas); // BulkInsert插入数据较快
                        }
                        if (updateDatas.Count > 0)
                        {
                            // 批量更新数据
                            ((DataContext)DC).BulkUpdate(updateDatas);
                        }
                        //// 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        //foreach (var originalItem in originalItems)
                        //{
                        //    if (!MSD.IsValid)
                        //    {
                        //        break;
                        //    }
                        //    BaseItemMaster matchItem = null;
                        //    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                        //    {
                        //        matchItem = u9Return.Entity.Find(x => x.SourceSystemId == originalItem.SourceSystemId);
                        //    }
                        //    if (matchItem == null)
                        //    {
                        //        Entity = originalItem;
                        //        Entity.IsValid = false;
                        //        deleteDatas.Add(Entity);
                        //        DeleteQty++;
                        //    }
                        //}
                        //if (deleteDatas.Count > 0)
                        //{
                        //    // 批量失效（假删除）数据
                        //    ((DataContext)DC).BulkUpdate(deleteDatas);
                        //}
                        // 更新系统参数，记录最后同步时间。仅在code为空时更新（即：仅全量同步时更新。单个编码更新时不更新）
                        if (MSD.IsValid && string.IsNullOrEmpty(code))
                        {
                            baseSysParaVM.SetParaValue("BaseItemMasterLastUpdateTime", u9Return.Entity.Max(x => x.LastUpdateTime)?.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        }
                        if (MSD.IsValid)
                        {
                            trans.Commit();
                        }
                    }
                }
            }
            else
            {
                MSD.AddModelError("SyncResult", "同步失败。" + u9Return.Msg);
                return;
            }
        }

        /// <summary>
        /// 获取ERP最后更新时间
        /// </summary>
        /// <param name="u9Url"></param>
        /// <param name="entCode"></param>
        public void GetErpLastUpdateTime(U9ApiHelper apiHelper)
        {
            string sql = "DELETE FROM BaseItemMasterTemp";
            DC.RunSQL(sql); // 耗时：1"
            var rr = apiHelper.BeforeSyncGetItemTime(); // 耗时：2.5"，81万数据（全部生效的料品）
            if (!rr.Success)
            {
                MSD.AddModelError("SyncResult", "同步失败。" + rr.Msg);
                return;
            }
            DateTime now = DateTime.Now;
            string itcode = LoginUserInfo?.ITCode;
            foreach (var item in rr.Entity)
            {
                item.ID = Guid.NewGuid();
                item.CreateBy = itcode;
                item.CreateTime = now;
            }
            // 批量插入数据库
            ((DataContext)DC).BulkInsert(rr.Entity); // BulkInsert插入数据较快。耗时：20"，81万数据（全部生效的料品）
        }

        /// <summary>
        /// 获取要新增的ID，用于分批同步
        /// </summary>
        /// <param name="blockSize">单次同步限制的大小</param>
        /// <param name="isUpdate">true:更新，false:新增</param>
        /// <returns></returns>
        public List<string> GetAddOrUpdateIds(int blockSize, bool isUpdate)
        {
            if (blockSize < 100) // 分批同步，最小限制100
                blockSize = 100;
            List<string> blocks = new List<string>();
            string sql = "";
            if (isUpdate)
            {
                sql = @"select U9ID,ID from 
                    (
	                    SELECT temp.U9ID,item.ID,temp.ModifiedOn,item.LastUpdateTime
	                    FROM BaseItemMasterTemp temp
	                    left join BaseItemMaster item on item.SourceSystemId=temp.U9ID
                    ) a 
                    where ID is not null and ModifiedOn>LastUpdateTime";
            }
            else
            {
                sql = @"select U9ID from 
                    (
	                    SELECT temp.U9ID,item.ID
	                    FROM BaseItemMasterTemp temp
	                    left join BaseItemMaster item on item.SourceSystemId=temp.U9ID
                    ) a 
                    where ID is null";
            }
            DataTable dt = DC.RunSQL(sql);
            bool flag = dt != null && dt.Rows != null && dt.Rows.Count > 0;
            if (flag)
            {
                string ids = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i % blockSize == 0 && i > 0)
                    {
                        if (ids.Length > 0)
                        {
                            ids = ids.Substring(0, ids.Length - 1); // 去掉末尾的逗号
                            blocks.Add(ids);
                        }
                        ids = "";
                    }
                    ids += dt.Rows[i]["U9ID"].ToString() + ",";
                    if (isUpdate)
                        UpdateIds.Add(Guid.Parse(dt.Rows[i]["ID"].ToString()));
                }
                if (ids.Length > 0)
                {
                    ids = ids.Substring(0, ids.Length - 1); // 去掉末尾的逗号
                    blocks.Add(ids);
                }
            }
            return blocks;
        }

    }
}
