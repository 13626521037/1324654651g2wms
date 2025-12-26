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
namespace WMS.ViewModel.BaseData.BaseDepartmentVMs
{
    public partial class BaseDepartmentVM : BaseCRUDVM<BaseDepartment>
    {
        
        public List<string> BaseDataBaseDepartmentFTempSelected { get; set; }

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

        public BaseDepartmentVM()
        {
            
            SetInclude(x => x.Organization);

        }

        protected override void InitVM()
        {
            

        }

        public override DuplicatedInfo<BaseDepartment> SetDuplicatedCheck()
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
            U9Return<List<BaseDepartment>> u9Return = apiHelper.GetDepartments(code);
            if (u9Return.Success)
            {
                var orgVM = Wtm.CreateVM<BaseOrganizationVM>();
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseDepartment> originalDepts = DC.Set<BaseDepartment>().Where(x => string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncDept in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseDepartment matchDept = null;
                            if (originalDepts != null && originalDepts.Count > 0)
                            {
                                matchDept = originalDepts.Find(x => x.SourceSystemId == syncDept.SourceSystemId);
                            }
                            if (matchDept == null)   // 原来不存在，新增
                            {
                                Entity = syncDept;
                                Entity.IsValid = true;
                                Entity.OrganizationId = orgVM.GetIdBySourceSystemIdFromCache(syncDept.Org);
                                //Entity.OrganizationId = orgVM.GetIdBySourceSystemId(syncDept.Org);
                                if(Entity.OrganizationId == null)
                                {
                                    MSD.AddModelError("SyncResult", $"同步失败。部门【{syncDept.Code}】所在组织不存在。请先尝试同步组织数据");
                                    return;
                                }
                                DoAdd();
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchDept.LastUpdateTime != syncDept.LastUpdateTime)
                                {
                                    // 部门的归属组织不可能发生变化，所以不需要更新OrganizationId
                                    Entity = matchDept;
                                    Entity.Code = syncDept.Code;
                                    Entity.Name = syncDept.Name;
                                    Entity.IsMFG = syncDept.IsMFG;
                                    Entity.LastUpdateTime = syncDept.LastUpdateTime;
                                    Entity.IsValid = true;
                                    Entity.IsEffective = syncDept.IsEffective;

                                    FC.Clear();
                                    FC.Add("Entity.Code", syncDept.Code);
                                    FC.Add("Entity.Name", syncDept.Name);
                                    FC.Add("Entity.IsMFG", syncDept.IsMFG);
                                    FC.Add("Entity.LastUpdateTime", syncDept.LastUpdateTime);
                                    FC.Add("Entity.IsValid", Entity.IsValid);
                                    FC.Add("Entity.IsEffective", syncDept.IsEffective);
                                    DoEdit();
                                    UpdateQty++;
                                }
                            }
                        }
                        // 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        foreach (var originalDept in originalDepts)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseDepartment matchDept = null;
                            if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                            {
                                matchDept = u9Return.Entity.Find(x => x.SourceSystemId == originalDept.SourceSystemId);
                            }
                            if (matchDept == null)
                            {
                                Entity = originalDept;
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
