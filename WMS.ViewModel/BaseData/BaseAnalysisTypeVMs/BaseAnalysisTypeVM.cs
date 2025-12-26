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
using WMS.Util;
using Elsa.Models;
namespace WMS.ViewModel.BaseData.BaseAnalysisTypeVMs
{
    public partial class BaseAnalysisTypeVM : BaseCRUDVM<BaseAnalysisType>
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

        public List<string> BaseDataBaseAnalysisTypeFTempSelected { get; set; }

        public BaseAnalysisTypeVM()
        {

        }

        protected override void InitVM()
        {

        }

        public override DuplicatedInfo<BaseAnalysisType> SetDuplicatedCheck()
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
            string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
            string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
            U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode);
            U9Return<List<BaseAnalysisType>> u9Return = apiHelper.GetAnalysisTypes(code);
            if (u9Return.Success)
            {
                // 同步成功。将数据同步到WMS数据库
                if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                {
                    List<BaseAnalysisType> originalTypes = DC.Set<BaseAnalysisType>().Where(x => string.IsNullOrEmpty(code) || x.Code.Equals(code) || x.SourceSystemId.Equals(code)).AsNoTracking().ToList();
                    using (var trans = DC.BeginTransaction())
                    {
                        // 遍历U9返回的数据，将数据同步到WMS数据库（新增、修改）
                        foreach (var syncDept in u9Return.Entity)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseAnalysisType matchType = null;
                            if (originalTypes != null && originalTypes.Count > 0)
                            {
                                matchType = originalTypes.Find(x => x.SourceSystemId == syncDept.SourceSystemId);
                            }
                            if (matchType == null)   // 原来不存在，新增
                            {
                                Entity = syncDept;
                                Entity.IsValid = true;
                                DoAdd();
                                InsertQty++;
                            }
                            else    // 原来存在。如果最后更新时间更新，则更新
                            {
                                if (matchType.LastUpdateTime != syncDept.LastUpdateTime)
                                {
                                    Entity = matchType;
                                    Entity.Code = syncDept.Code;
                                    Entity.Name = syncDept.Name;
                                    Entity.LastUpdateTime = syncDept.LastUpdateTime;

                                    FC.Clear();
                                    FC.Add("Entity.Code", syncDept.Code);
                                    FC.Add("Entity.Name", syncDept.Name);
                                    FC.Add("Entity.LastUpdateTime", syncDept.LastUpdateTime);
                                    DoEdit();
                                    UpdateQty++;
                                }
                            }
                        }
                        // 遍历原来的数据，如果U9返回的数据中不存在，则设置为无效（不能真删除）
                        foreach (var originalType in originalTypes)
                        {
                            if (!MSD.IsValid)
                            {
                                break;
                            }
                            BaseAnalysisType matchType = null;
                            if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                            {
                                matchType = u9Return.Entity.Find(x => x.SourceSystemId == originalType.SourceSystemId);
                            }
                            if (matchType == null)
                            {
                                Entity = originalType;
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
