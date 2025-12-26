using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.DataAccess;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
namespace WMS.ViewModel.BaseData.BaseCustomerVMs
{
    public partial class BaseCustomerVM : BaseCRUDVM<BaseCustomer>
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
        /// 需要更新的ID集合
        /// </summary>
        public List<Guid> UpdateIds { get; set; }

        public List<string> BaseDataBaseCustomerFTempSelected { get; set; }

        public BaseCustomerVM()
        {

            SetInclude(x => x.Organization);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseCustomer> SetDuplicatedCheck()
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
        /// 同步数据
        /// </summary>
        public void SyncData(string code)
        {
            UpdateQty = 0;
            InsertQty = 0;
            DeleteQty = 0;
            DateTime? lastUpdateTime = null;
            BaseSysParaVM baseSysParaVM = Wtm.CreateVM<BaseSysParaVM>();

            // 全量同步时，以系统参数中的最后更新时间为准进行同步
            lastUpdateTime = baseSysParaVM.GetParaDateTimeValue("BaseCustomerLastUpdateTime");
            if (lastUpdateTime == null)
            {
                MSD.AddModelError("SysParaError", "获取系统参数BaseCustomerLastUpdateTime失败，请联系系统管理员");
                return;
            }

            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseCustomer>> u9Return = apiHelper.GetCustomers(code, lastUpdateTime);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseCustomer> originals = DC.Set<BaseCustomer>().Where(x => string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        List<BaseCustomer> newDatas = new List<BaseCustomer>();
                        List<BaseCustomer> updateDatas = new List<BaseCustomer>();
                        List<BaseCustomer> deleteDatas = new List<BaseCustomer>();
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncEntity in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseCustomer matchCustomer = null;
                            if (originals != null && originals.Count > 0)
                            {
                                matchCustomer = originals.Find(x => x.SourceSystemId == syncEntity.SourceSystemId);
                            }
                            if (matchCustomer == null)   // 原来不存在，新增
                            {
                                Entity = syncEntity;
                                Entity.IsValid = true;
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncEntity.Organization.SourceSystemId);
                                //Entity.OrganizationId = orgVM.GetIdBySourceSystemId(syncDept.Org);
                                if (Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。客户【{syncEntity.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                // DoAdd();
                                Entity.ID = Guid.NewGuid();
                                Entity.CreateBy = LoginUserInfo?.ITCode;
                                Entity.CreateTime = DateTime.Now;
                                newDatas.Add(Entity);
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchCustomer.LastUpdateTime != syncEntity.LastUpdateTime)
                                {
                                    // 部门的归属组织不可能发生变化，所以不需要更新OrganizationId
                                    Entity = matchCustomer;
                                    Entity.Code = syncEntity.Code;
                                    Entity.Name = syncEntity.Name;
                                    Entity.ShortName = syncEntity.ShortName;
                                    Entity.LastUpdateTime = syncEntity.LastUpdateTime;
                                    Entity.IsValid = true;
                                    Entity.IsEffective = syncEntity.IsEffective;
                                    Entity.EnglishShortName = syncEntity.EnglishShortName;

                                    FC.Clear();
                                    FC.Add("Entity.Code", syncEntity.Code);
                                    FC.Add("Entity.Name", syncEntity.Name);
                                    FC.Add("Entity.ShortName", syncEntity.ShortName);
                                    FC.Add("Entity.LastUpdateTime", syncEntity.LastUpdateTime);
                                    FC.Add("Entity.IsValid", Entity.IsValid);
                                    FC.Add("Entity.IsEffective", syncEntity.IsEffective);
                                    FC.Add("Entity.EnglishShortName", syncEntity.EnglishShortName);
                                    // DoEdit();
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
                        //foreach (var original in originals)
                        //{
                        //    if (!MSD.IsValid)
                        //    {
                        //        break;
                        //    }
                        //    BaseCustomer matchCustomer = null;
                        //    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                        //    {
                        //        matchCustomer = u9Return.Entity.Find(x => x.SourceSystemId == original.SourceSystemId);
                        //    }
                        //    if (matchCustomer == null)
                        //    {
                        //        Entity = original;
                        //        Entity.IsValid = false;
                        //        FC.Clear();
                        //        FC.Add("Entity.IsValid", Entity.IsValid);
                        //        DoEdit();
                        //        DeleteQty++;
                        //    }
                        //}
                        // 更新系统参数，记录最后同步时间。仅在code为空时更新（即：仅全量同步时更新。单个编码更新时不更新）
                        if (MSD.IsValid && string.IsNullOrEmpty(code))
                        {
                            baseSysParaVM.SetParaValue("BaseCustomerLastUpdateTime", u9Return.Entity.Max(x => x.LastUpdateTime)?.ToString("yyyy-MM-dd HH:mm:ss.fff"));
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
