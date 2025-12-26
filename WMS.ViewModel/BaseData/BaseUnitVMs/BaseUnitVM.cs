using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.BaseData;
using WMS.Model;
using System.Linq.Expressions;
using WMS.DataAccess;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseAnalysisTypeVMs;
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
using EFCore.BulkExtensions;
namespace WMS.ViewModel.BaseData.BaseUnitVMs
{
    public partial class BaseUnitVM : BaseCRUDVM<BaseUnit>
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

        public List<string> BaseDataBaseUnitFTempSelected { get; set; }

        public BaseUnitVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<BaseUnit> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.Code));
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
                lastUpdateTime = baseSysParaVM.GetParaDateTimeValue("BaseUnitLastUpdateTime");
                if (lastUpdateTime == null)
                {
                    MSD.AddModelError("SysParaError", "获取系统参数BaseUnitLastUpdateTime失败，请联系系统管理员");
                    return;
                }
                //lastUpdateTime = new DateTime(2000, 1, 1);  // 参数暂时不参与业务逻辑。当前只有料品使用参数控制，否则U9删除的数据无法同步状态到WMS
            }
            else
            {
                lastUpdateTime = new DateTime(2000, 1, 1);  // 单条数据同步时，将最后更新时间设置为2000-01-01，保证此条数据被同步
            }
            Expression<Func<BaseUnit, bool>> predicate = x => (string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)); // && x.LastUpdateTime > lastUpdateTime;
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseUnit>> u9Return = apiHelper.GetUnits(code, lastUpdateTime);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseUnit> originalUnits = DC.Set<BaseUnit>().Where(predicate).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        List<BaseUnit> newDatas = new List<BaseUnit>();
                        List<BaseUnit> updateDatas = new List<BaseUnit>();
                        List<BaseUnit> deleteDatas = new List<BaseUnit>();
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncData in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseUnit matchUnit = null;
                            if (originalUnits != null && originalUnits.Count > 0)
                            {
                                matchUnit = originalUnits.Find(x => x.SourceSystemId == syncData.SourceSystemId);
                            }
                            if (matchUnit == null)   // 原来不存在，新增
                            {
                                Entity = syncData;
                                syncData.IsValid = true;
                                Entity.ID = Guid.NewGuid();
                                Entity.CreateBy = LoginUserInfo?.ITCode;
                                Entity.CreateTime = DateTime.Now;
                                newDatas.Add(Entity);
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchUnit.LastUpdateTime != syncData.LastUpdateTime)
                                {
                                    Entity = matchUnit;
                                    Entity.Code = syncData.Code;
                                    Entity.Name = syncData.Name;
                                    Entity.LastUpdateTime = syncData.LastUpdateTime;
                                    Entity.IsEffective = syncData.IsEffective;
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
                            ((DataContext)DC).BulkInsert(newDatas); // BulkInsert插入数据较快
                        }
                        if (updateDatas.Count > 0)
                        {
                            // 批量更新数据
                            ((DataContext)DC).BulkUpdate(updateDatas);
                        }
                        // 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        //foreach (var originalUnit in originalUnits)
                        //{
                        //    if (!MSD.IsValid)
                        //    {
                        //        break;
                        //    }
                        //    BaseUnit matchCate = null;
                        //    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                        //    {
                        //        matchCate = u9Return.Entity.Find(x => x.SourceSystemId == originalUnit.SourceSystemId);
                        //    }
                        //    if (matchCate == null)
                        //    {
                        //        Entity = originalUnit;
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
                            baseSysParaVM.SetParaValue("BaseUnitLastUpdateTime", u9Return.Entity.Max(x => x.LastUpdateTime)?.ToString("yyyy-MM-dd HH:mm:ss.fff"));
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
