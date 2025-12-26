using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.ViewModel.InventoryManagement.InventoryErpDiffLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
using EFCore.BulkExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WMS.Util;
using WMS.DataAccess;
using DotLiquid.Util;
using NetTopologySuite.Index.HPRtree;
using Elsa.Models;
namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs
{
    public partial class InventoryErpDiffVM : BaseCRUDVM<InventoryErpDiff>
    {

        public List<string> InventoryManagementInventoryErpDiffFTempSelected { get; set; }
        public InventoryErpDiffLineErpDiffDetailListVM InventoryErpDiffLineErpDiffList { get; set; }

        private DateTime Now { get; set; }

        public InventoryErpDiffVM()
        {

            SetInclude(x => x.Wh);
            SetInclude(x => x.Item);
            InventoryErpDiffLineErpDiffList = new InventoryErpDiffLineErpDiffDetailListVM();
            InventoryErpDiffLineErpDiffList.DetailGridPrix = "Entity.InventoryErpDiffLine_ErpDiff";

        }

        protected override void InitVM()
        {

            InventoryErpDiffLineErpDiffList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryErpDiff> SetDuplicatedCheck()
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
        /// ERP对账
        /// </summary>
        public void Analysis()
        {
            if (Entity == null || Entity.WhId == null || Entity.WhId == Guid.Empty)
            {
                MSD.AddModelError("", "请先选择存储地点");
                return;
            }
            BaseWareHouse wh = DC.Set<BaseWareHouse>().Find(Entity.WhId);
            if (wh == null)
            {
                MSD.AddModelError("", "选择的存储地点不存在");
                return;
            }
            try
            {
                Now = DateTime.Now;
                // 删除选择存储地点的原有ERP对账数据
                DC.Set<InventoryErpDiff>().Where(x => x.WhId == Entity.WhId).ExecuteDelete();
                DC.SaveChanges();   // 不用加入事务。下面就算失败，也不再显示原有数据
                                    // 获取WMS库存
                List<InventoryErpDiff> entities = GetWmsInventory((Guid)Entity.WhId);
                // 获取ERP对比数据
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", LoginUserInfo.Name);
                U9Return<List<InventoryErpDiff>> u9Return = apiHelper.StockDataMatch(long.Parse(wh.SourceSystemId), entities);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
                else
                {
                    if (u9Return.Entity != null && u9Return.Entity.Count > 0)
                    {
                        List<InventoryErpDiffLine> lines = new List<InventoryErpDiffLine>();
                        // 匹配行数据
                        var originalWmsEntities = u9Return.Entity.FindAll(x => x.ItemId != null && x.ItemId != Guid.Empty);
                        var newErpEntities = u9Return.Entity.FindAll(x => x.ItemId == null || x.ItemId == Guid.Empty);
                        foreach (var item in originalWmsEntities)
                        {
                            var entity = entities.Find(x => x.ItemId == item.ItemId && x.Seiban == item.Seiban);    // 这里可能会慢，可以考虑优化（比如上面先转为字典）
                            if (entity == null)  // 不会出现
                            {
                                MSD.AddModelError("", "数据传输异常");
                                return;
                            }
                            // item.InventoryErpDiffLine_ErpDiff = entity.InventoryErpDiffLine_ErpDiff;    // 将行库存数据反写回返回的实体中
                            lines.AddRange(entity.InventoryErpDiffLine_ErpDiff);
                            item.ID = entity.ID;
                            item.WhId = wh.ID;
                            item.CreateBy = entity.CreateBy;
                            item.CreateTime = entity.CreateTime;
                        }
                        // string error = "";
                        //BaseItemMasterVM itemVM = Wtm.CreateVM<BaseItemMasterVM>();
                        // 新ERP数据料品ID转换
                        foreach (var item in newErpEntities)
                        {
                            item.ID = Guid.NewGuid();
                            item.WhId = wh.ID;
                            item.CreateBy = LoginUserInfo.ITCode;
                            item.CreateTime = Now;
                            item.ItemId = null;
                            //if(item.ItemId == null)
                            //{
                            //    error += item.SyncItem + ",";
                            //}
                            //string r = this.SSIdAttrToId(item, item.Item, "Item");   // 转换物料
                            //if (!string.IsNullOrEmpty(r)) { error += r + ","; }
                        }
                        //if (!string.IsNullOrEmpty(error))
                        //{
                        //    error = error.TrimEnd(',');
                        //    MSD.AddModelError("", error); 
                        //    return;
                        //}
                        ((DataContext)DC).BulkInsert(u9Return.Entity);
                        ((DataContext)DC).BulkInsert(lines);

                        // 单条转换物料ID过慢，增加物料表的ERPID索引，然后使用sql批量更新
                        string sql = "update doc set doc.ItemId=item.ID " +
                            " from InventoryErpDiff doc " +
                            " inner join BaseItemMaster item on item.SourceSystemId=doc.SyncItem " +
                            " where doc.WhId='" + wh.ID + "'";
                        DC.RunSQL(sql);
                        DC.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", ex.Message);
                return;
            }
        }

        /// <summary>
        /// 获取WMS库存
        /// </summary>
        /// <param name="whid"></param>
        private List<InventoryErpDiff> GetWmsInventory(Guid whid)
        {
            var inventorys = DC.Set<BaseInventory>().Include(x => x.ItemMaster).Where(x => x.WhLocation.WhArea.WareHouseId == whid).AsNoTracking().OrderBy(x => x.ItemMaster.Code).ThenBy(x => x.Seiban).ToList();
            List<InventoryErpDiff> entities = new List<InventoryErpDiff>();
            // 已经排序过，按照ItemMasterId和Seiban分组统计库存
            for (int i = 0; i < inventorys.Count; i++)
            {
                if (i > 0 && inventorys[i].ItemMasterId == inventorys[i - 1].ItemMasterId && inventorys[i].Seiban == inventorys[i - 1].Seiban)
                {
                    entities[entities.Count - 1].WmsQty += inventorys[i].Qty;
                    entities[entities.Count - 1].InventoryErpDiffLine_ErpDiff.Add(new InventoryErpDiffLine
                    {
                        ID = Guid.NewGuid(),
                        InventoryId = inventorys[i].ID,
                        Qty = inventorys[i].Qty,
                        ErpDiffId = entities[entities.Count - 1].ID,
                        CreateBy = LoginUserInfo.ITCode,
                        CreateTime = Now,
                    });
                }
                else
                {
                    var entity = new InventoryErpDiff();
                    entity.ID = Guid.NewGuid();
                    entity.CreateBy = LoginUserInfo.ITCode;
                    entity.CreateTime = Now;
                    entity.WhId = whid;
                    entity.ItemId = inventorys[i].ItemMasterId;
                    entity.SyncItem = inventorys[i].ItemMaster.SourceSystemId;
                    entity.Seiban = inventorys[i].Seiban;
                    entity.WmsQty = inventorys[i].Qty;
                    entity.ErpQty = 0;
                    entity.InventoryErpDiffLine_ErpDiff = new List<InventoryErpDiffLine>
                    {
                        new InventoryErpDiffLine
                        {
                            ID = Guid.NewGuid(),
                            InventoryId = inventorys[i].ID,
                            Qty = inventorys[i].Qty,
                            ErpDiffId = entity.ID,
                            CreateBy = LoginUserInfo.ITCode,
                            CreateTime = Now,
                        }
                    };
                    entities.Add(entity);
                }
            }
            return entities;
        }
    }
}
