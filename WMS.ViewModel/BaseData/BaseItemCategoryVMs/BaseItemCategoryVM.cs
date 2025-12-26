using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseAnalysisTypeVMs;
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
using WMS.DataAccess;
using EFCore.BulkExtensions;
using System.Linq.Expressions;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
namespace WMS.ViewModel.BaseData.BaseItemCategoryVMs
{
    public partial class BaseItemCategoryVM : BaseCRUDVM<BaseItemCategory>
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

        public List<string> BaseDataBaseItemCategoryFTempSelected { get; set; }

        public BaseItemCategoryVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.AnalysisType);
            SetInclude(x => x.Department);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseItemCategory> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.OrganizationId), SimpleField(x => x.Code));
            return rv;

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
        /// 同步数据
        /// </summary>
        public void SyncData(string code)
        {
            UpdateQty = 0;
            InsertQty = 0;
            DeleteQty = 0;
            DateTime? lastUpdateTime = null;
            BaseSysParaVM baseSysParaVM = Wtm.CreateVM<BaseSysParaVM>();
            if (string.IsNullOrEmpty(code))
            {
                // 全量同步时，以系统参数中的最后更新时间为准进行同步
                lastUpdateTime = baseSysParaVM.GetParaDateTimeValue("BaseItemCategoryLastUpdateTime");
                if (lastUpdateTime == null)
                {
                    MSD.AddModelError("SysParaError", "获取系统参数BaseItemCategoryLastUpdateTime失败，请联系系统管理员");
                    return;
                }
                // lastUpdateTime = new DateTime(2000, 1, 1);  // 参数暂时不参与业务逻辑。当前只有料品使用参数控制，否则U9删除的数据无法同步状态到WMS
            }
            else
            {
                lastUpdateTime = new DateTime(2000, 1, 1);  // 单条数据同步时，将最后更新时间设置为2000-01-01，保证此条数据被同步
            }
            Expression<Func<BaseItemCategory, bool>> predicate = x => (string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)); // && x.LastUpdateTime > lastUpdateTime;
            //DateTime? lastUpdateTime = DC.Set<BaseItemCategory>().Where(predicate).Max(x => x.LastUpdateTime);
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseItemCategory>> u9Return = apiHelper.GetItemCategorys(code, lastUpdateTime);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                var deptVM = Wtm.CreateVM<BaseDepartmentVM>();
                var analysisTypeVM = Wtm.CreateVM<BaseAnalysisTypeVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseItemCategory> originalCates = DC.Set<BaseItemCategory>().Where(predicate).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        List<BaseItemCategory> newDatas = new List<BaseItemCategory>();
                        List<BaseItemCategory> updateDatas = new List<BaseItemCategory>();
                        List<BaseItemCategory> deleteDatas = new List<BaseItemCategory>();
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncCate in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseItemCategory matchItemCategory = null;
                            if (originalCates != null && originalCates.Count > 0)
                            {
                                matchItemCategory = originalCates.Find(x => x.SourceSystemId == syncCate.SourceSystemId);
                            }
                            if (matchItemCategory == null)   // 原来不存在，新增
                            {
                                Entity = syncCate;
                                syncCate.IsValid = true;
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncCate.Org);
                                if (Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。大类【{syncCate.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                if (!string.IsNullOrEmpty(syncCate.DeptCode))
                                {
                                    Entity.DepartmentId = deptVM.GetIdByCodeFromCache(syncCate.DeptCode);
                                    if (Entity.DepartmentId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。大类【{syncCate.Code}】所在部门不存在。请先尝试同步部门数据");
                                        return;
                                    }
                                }
                                if(!string.IsNullOrEmpty(syncCate.AnalysisTypeCode))
                                {
                                    Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncCate.AnalysisTypeCode);
                                    if (Entity.AnalysisTypeId == null)
                                    {
                                        MSD.AddModelError("SyncResult", $"同步失败。大类【{syncCate.Code}】所属分析类别不存在。请先尝试同步分析类别数据");
                                        return;
                                    }
                                }
                                Entity.ID = Guid.NewGuid();
                                Entity.CreateBy = LoginUserInfo?.ITCode;
                                Entity.CreateTime = DateTime.Now;
                                newDatas.Add(Entity);
                                //DoAdd();  // 太慢了。改为批量添加
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchItemCategory.LastUpdateTime != syncCate.LastUpdateTime)
                                {
                                    // 部门的归属组织不可能发生变化，所以不需要更新OrganizationId
                                    Entity = matchItemCategory;
                                    Entity.Code = syncCate.Code;
                                    Entity.Name = syncCate.Name;
                                    Entity.LastUpdateTime = syncCate.LastUpdateTime;

                                    //FC.Clear();
                                    //FC.Add("Entity.Code", syncCate.Code);
                                    //FC.Add("Entity.Name", syncCate.Name);
                                    //FC.Add("Entity.LastUpdateTime", syncCate.LastUpdateTime);

                                    if (!string.IsNullOrEmpty(syncCate.DeptCode))
                                    {
                                        Entity.DepartmentId = deptVM.GetIdByCodeFromCache(syncCate.DeptCode);
                                        if (Entity.DepartmentId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。大类【{syncCate.Code}】所在部门不存在。请先尝试同步部门数据");
                                            return;
                                        }
                                        //FC.Add("Entity.DepartmentId", Entity.DepartmentId);
                                    }
                                    if (!string.IsNullOrEmpty(syncCate.AnalysisTypeCode))
                                    {
                                        Entity.AnalysisTypeId = analysisTypeVM.GetIdByCodeFromCache(syncCate.AnalysisTypeCode);
                                        if (Entity.AnalysisTypeId == null)
                                        {
                                            MSD.AddModelError("SyncResult", $"同步失败。大类【{syncCate.Code}】所属分析类别不存在。请先尝试同步分析类别数据");
                                            return;
                                        }
                                        //FC.Add("Entity.AnalysisTypeId", Entity.AnalysisTypeId);
                                    }
                                    // DoEdit();
                                    Entity.UpdateBy = LoginUserInfo?.ITCode;
                                    Entity.UpdateTime = DateTime.Now;
                                    updateDatas.Add(Entity);
                                    UpdateQty++;
                                }
                            }
                        }
                        if (newDatas.Count > 0)
                        {
                            // 批量添加新数据
                            //((DataContext)DC).AddRange(newDatas); // EF原生方法较慢
                            //DC.SaveChanges();
                            ((DataContext)DC).BulkInsert(newDatas); // BulkInsert插入数据较快
                        }
                        if (updateDatas.Count > 0)
                        {
                            // 批量更新数据
                            ((DataContext)DC).BulkUpdate(updateDatas);
                        }
                        // 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        //foreach (var originalCate in originalCates)
                        //{
                        //    if (!MSD.IsValid)
                        //    {
                        //        break;
                        //    }
                        //    BaseItemCategory matchCate = null;
                        //    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                        //    {
                        //        matchCate = u9Return.Entity.Find(x => x.SourceSystemId == originalCate.SourceSystemId);
                        //    }
                        //    if (matchCate == null)
                        //    {
                        //        Entity = originalCate;
                        //        Entity.IsValid = false;
                        //        //FC.Clear();
                        //        //FC.Add("Entity.IsValid", Entity.IsValid);
                        //        //DoEdit();
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
                            baseSysParaVM.SetParaValue("BaseItemCategoryLastUpdateTime", u9Return.Entity.Max(x => x.LastUpdateTime)?.ToString("yyyy-MM-dd HH:mm:ss.fff"));
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
            }
        }
    }
}
