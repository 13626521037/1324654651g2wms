using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.Model.BaseData;
using WMS.Model;
using WMS.Util;
namespace WMS.ViewModel.BaseData.BaseSupplierVMs
{
    public partial class BaseSupplierVM : BaseCRUDVM<BaseSupplier>
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

        public List<string> BaseDataBaseSupplierFTempSelected { get; set; }

        public BaseSupplierVM()
        {

            SetInclude(x => x.Organization);

        }

        protected override void InitVM()
        {


        }

        public override DuplicatedInfo<BaseSupplier> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.Code), SimpleField(x => x.OrganizationId));
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
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseSupplier>> u9Return = apiHelper.GetSuppliers(code);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseSupplier> originals = DC.Set<BaseSupplier>().Where(x => string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncEntity in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseSupplier matchSupplier = null;
                            if (originals != null && originals.Count > 0)
                            {
                                matchSupplier = originals.Find(x => x.SourceSystemId == syncEntity.SourceSystemId);
                            }
                            if (matchSupplier == null)   // 原来不存在，新增
                            {
                                Entity = syncEntity;
                                Entity.IsValid = true;
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncEntity.Organization.SourceSystemId);
                                //Entity.OrganizationId = orgVM.GetIdBySourceSystemId(syncDept.Org);
                                if (Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。部门【{syncEntity.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                DoAdd();
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchSupplier.LastUpdateTime != syncEntity.LastUpdateTime)
                                {
                                    // 部门的归属组织不可能发生变化，所以不需要更新OrganizationId
                                    Entity = matchSupplier;
                                    Entity.Code = syncEntity.Code;
                                    Entity.Name = syncEntity.Name;
                                    Entity.ShortName = syncEntity.ShortName;
                                    Entity.LastUpdateTime = syncEntity.LastUpdateTime;
                                    Entity.IsValid = true;
                                    Entity.IsEffective = syncEntity.IsEffective;

                                    FC.Clear();
                                    FC.Add("Entity.Code", syncEntity.Code);
                                    FC.Add("Entity.Name", syncEntity.Name);
                                    FC.Add("Entity.ShortName", syncEntity.ShortName);
                                    FC.Add("Entity.LastUpdateTime", syncEntity.LastUpdateTime);
                                    FC.Add("Entity.IsValid", Entity.IsValid);
                                    FC.Add("Entity.IsEffective", syncEntity.IsEffective);
                                    DoEdit();
                                    UpdateQty++;
                                }
                            }
                        }
                        // 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        foreach (var original in originals)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseSupplier matchSupplier = null;
                            if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                            {
                                matchSupplier = u9Return.Entity.Find(x => x.SourceSystemId == original.SourceSystemId);
                            }
                            if (matchSupplier == null)
                            {
                                Entity = original;
                                Entity.IsValid = false;
                                FC.Clear();
                                FC.Add("Entity.IsValid", Entity.IsValid);
                                DoEdit();
                                DeleteQty++;
                            }
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
