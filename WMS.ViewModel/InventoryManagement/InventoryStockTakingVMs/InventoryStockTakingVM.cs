using Aliyun.OSS;
using EFCore.BulkExtensions;
using Elsa.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Org.BouncyCastle.Crypto;
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
using WMS.Model.InventoryManagement;
using WMS.Model.InventoryManagement.Return;
using WMS.Model.KnifeManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.ViewModel.BaseData.BaseSysParaVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingLineVMs;
using WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs;
using WMS.ViewModel.KnifeManagement.KnifeVMs;
namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingVM : BaseCRUDVM<InventoryStockTaking>
    {

        public List<string> InventoryManagementInventoryStockTakingFTempSelected { get; set; }
        public InventoryStockTakingLineStockTakingDetailListVM InventoryStockTakingLineStockTakingList { get; set; }
        public InventoryStockTakingLocationsStockTakingDetailListVM InventoryStockTakingLocationsStockTakingList { get; set; }
        public InventoryStockTakingErpDiffLineStockTakingDetailListVM InventoryStockTakingErpDiffLineStockTakingList { get; set; }

        public InventoryStockTakingVM()
        {

            SetInclude(x => x.Wh);
            InventoryStockTakingLineStockTakingList = new InventoryStockTakingLineStockTakingDetailListVM();
            InventoryStockTakingLineStockTakingList.DetailGridPrix = "Entity.InventoryStockTakingLine_StockTaking";
            InventoryStockTakingLocationsStockTakingList = new InventoryStockTakingLocationsStockTakingDetailListVM();
            InventoryStockTakingLocationsStockTakingList.DetailGridPrix = "Entity.InventoryStockTakingLocations_StockTaking";
            InventoryStockTakingErpDiffLineStockTakingList = new InventoryStockTakingErpDiffLineStockTakingDetailListVM();
            InventoryStockTakingErpDiffLineStockTakingList.DetailGridPrix = "Entity.InventoryStockTakingErpDiffLine_StockTaking";
        }

        protected override void InitVM()
        {

            InventoryStockTakingLineStockTakingList.CopyContext(this);
            InventoryStockTakingLocationsStockTakingList.CopyContext(this);
            InventoryStockTakingErpDiffLineStockTakingList.CopyContext(this);
        }

        public override DuplicatedInfo<InventoryStockTaking> SetDuplicatedCheck()
        {
            return null;

        }

        public override async Task DoAddAsync()
        {
            if (Entity.ID != Guid.Empty)
            {
                MSD.AddModelError("", "方法调用错误，当前为修改状态");
                return;
            }
            if (Entity.WhId == null || Entity.WhId == Guid.Empty)
            {
                MSD.AddModelError("", "存储地点不能为空");
                return;
            }
            using (var trans = DC.BeginTransaction())
            {
                // 生成单号
                Entity.DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventoryStockTakingDocNoRule", trans);
                await base.DoAddAsync();
                trans.Commit();
            }
        }

        public override async Task DoEditAsync(bool updateAllFields = false)
        {
            await Task.Run(() =>
            {
                // 只修改备注字段
                Entity.UpdateTime = DateTime.Now;
                Entity.UpdateBy = LoginUserInfo.ITCode;
                DC.UpdateProperty(Entity, x => x.Memo);
                DC.UpdateProperty(Entity, x => x.UpdateBy);
                DC.UpdateProperty(Entity, x => x.UpdateTime);
                // 只有超级管理员可以修改盘点模式。只有开立和审核中状态的单据才能修改盘点模式
                if (Entity.Mode != null
                    && LoginUserInfo != null && LoginUserInfo.Roles.Any(x => (x.RoleName == "超级管理员" || x.RoleName == "信息部管理员") && x.RoleCode == "001")
                    && Entity.Status == InventoryStockTakingStatusEnum.Opened || Entity.Status == InventoryStockTakingStatusEnum.Approving)
                {
                    DC.UpdateProperty(Entity, x => x.Mode);
                }
                DC.SaveChanges();
            });
            //await base.DoEditAsync();

        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }

        /// <summary>
        /// 删除盘点单
        /// </summary>
        public override void DoDelete()
        {
            if (Entity.Status != InventoryStockTakingStatusEnum.Opened)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            List<BaseWhLocation> locations = DC.Set<InventoryStockTakingLocations>()
                .Include(x => x.Location).AsNoTracking()
                .Where(x => x.StockTakingId == Entity.ID).Select(x => x.Location).ToList();
            using (var trans = DC.BeginTransaction())
            {
                foreach (var location in locations)
                {
                    if (location.Locked == true)    // 库位解锁
                        location.Locked = false;
                }
                // 批量更新数据（数量大时，必须批量更新）
                ((DataContext)DC).BulkUpdate(locations);
                base.DoDelete();
                if (MSD.IsValid)
                    trans.Commit();
            }
        }

        /// <summary>
        /// 添加选中库位
        /// </summary>
        public void AddSelectedLocations(string[] IDs, string docId)
        {
            if (IDs == null || IDs.Length == 0)
            {
                MSD.AddModelError("", "请至少选择一个库位");
                return;
            }

            if (string.IsNullOrEmpty(docId))
            {
                MSD.AddModelError("", "盘点单ID无效");
                return;
            }

            Guid docGuid = Guid.Empty;
            if (!Guid.TryParse(docId, out docGuid))
            {
                MSD.AddModelError("", "盘点单ID格式错误");
                return;
            }

            // ID类型转换
            List<Guid> ids = new List<Guid>();
            foreach (var id in IDs)
            {
                if (Guid.TryParse(id, out Guid g))
                {
                    ids.Add(g);
                }
                else
                {
                    MSD.AddModelError("", $"库位ID:{id}错误");
                    return;
                }
            }

            // 获取盘点单
            InventoryStockTaking stockTaking = DC.Set<InventoryStockTaking>()
                .AsNoTracking()
                .Include(x => x.InventoryStockTakingLocations_StockTaking).FirstOrDefault(x => x.ID == docGuid);
            if (stockTaking == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            if (stockTaking.Status != InventoryStockTakingStatusEnum.Opened)
            {
                MSD.AddModelError("", "盘点单非开立状态，不允许维护（添加、删除）盘点库位");
                return;
            }
            int maxLineNum = stockTaking.InventoryStockTakingLocations_StockTaking.Max(x => x.LineNum) ?? 0;

            // 定义盘点库位行（后面批量添加到盘点单的库位行中）
            List<InventoryStockTakingLocations> lines = new List<InventoryStockTakingLocations>();

            // 获取库位信息
            List<BaseWhLocation> locations = DC.Set<BaseWhLocation>().Where(x => ids.ToList().Contains(x.ID)).ToList();
            using (var trans = DC.BeginTransaction())
            {
                foreach (var location in locations)
                {
                    maxLineNum++;
                    if (location.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"库位:{location.Code}的类型不是正常库位");
                        return;
                    }
                    if (location.IsEffective == EffectiveEnum.Ineffective)
                    {
                        MSD.AddModelError("", $"库位:{location.Code}已被禁用");
                        return;
                    }
                    if (location.Locked == true)
                    {
                        MSD.AddModelError("", $"库位:{location.Code}已被其它盘点单锁定");
                        return;
                    }
                    if (stockTaking.InventoryStockTakingLocations_StockTaking.Any(x => x.LocationId == location.ID))
                    {
                        MSD.AddModelError("", $"库位:{location.Code}已存在于盘点单。请尝试重新进入盘点库位选择页面");
                        return;
                    }
                    location.Locked = true;

                    // 定义库位行
                    InventoryStockTakingLocations line = new InventoryStockTakingLocations();
                    line.ID = Guid.NewGuid();
                    line.StockTakingId = stockTaking.ID;
                    line.LocationId = location.ID;
                    line.LineNum = maxLineNum;
                    line.CreateBy = LoginUserInfo.ITCode;
                    line.CreateTime = DateTime.Now;
                    lines.Add(line);
                }
                // 批量添加库位行
                ((DataContext)DC).BulkInsert(lines);
                DC.SaveChanges();
                if (MSD.IsValid)
                    trans.Commit();
            }
        }

        /// <summary>
        /// 批量删除选中库位
        /// </summary>
        /// <param name="IDs"></param>
        public void DeleteSelectedLocations(string[] IDs)
        {
            if (IDs == null || IDs.Length == 0)
            {
                MSD.AddModelError("", "请至少选择一个库位");
                return;
            }

            // ID类型转换
            List<Guid> ids = new List<Guid>();
            foreach (var id in IDs)
            {
                if (Guid.TryParse(id, out Guid g))
                {
                    ids.Add(g);
                }
                else
                {
                    MSD.AddModelError("", $"库位ID:“{id}”错误");
                    return;
                }
            }

            List<InventoryStockTakingLocations> lines = DC.Set<InventoryStockTakingLocations>()
                .Include(x => x.Location)
                .Include(x => x.StockTaking)
                .Where(x => ids.Contains(x.ID)).ToList();
            List<BaseWhLocation> locations = lines.Select(x => x.Location).ToList();
            if (lines.Count == 0)
            {
                return;
            }
            if (lines[0].StockTaking.Status != InventoryStockTakingStatusEnum.Opened)
            {
                MSD.AddModelError("", "盘点单非开立状态，不允许维护（添加、删除）盘点库位");
                return;
            }
            using (var trans = DC.BeginTransaction())
            {
                foreach (var line in lines)
                {
                    if (line.Location.Locked == true)    // 库位解锁
                        line.Location.Locked = false;
                    //DC.DeleteEntity(line);
                }
                // 批量更新数据（数量大时，必须批量更新）
                ((DataContext)DC).BulkUpdate(locations);    // 更新库位
                ((DataContext)DC).BulkDelete(lines);    // 删除库位行
                DC.SaveChanges();
                if (MSD.IsValid)
                    trans.Commit();
            }
        }

        /// <summary>
        /// 提交盘点单
        /// </summary>
        public void Submit()
        {
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            // 盘点单状态必须为“开立”，才可以提交
            if (Entity.Status != InventoryStockTakingStatusEnum.Opened)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许提交");
                return;
            }
            // 至少需要包含一个库位，才可以进行提交盘点单
            List<InventoryStockTakingLocations> locations = DC.Set<InventoryStockTakingLocations>()
                .Where(x => x.StockTakingId == Entity.ID).AsNoTracking().ToList();
            if (locations == null || locations.Count == 0)
            {
                MSD.AddModelError("", "盘点单至少需要包含一个库位");
                return;
            }
            List<Guid?> locationIds = locations.Select(x => (Guid?)x.LocationId).ToList();
            // 构造盘点单明细行——库存信息
            List<InventoryStockTakingLine> lines = new List<InventoryStockTakingLine>();
            int i = 1;
            List<BaseInventory> invs = DC.Set<BaseInventory>()
                .AsNoTracking()
                .Where(x => locationIds.Contains(x.WhLocationId) && x.IsAbandoned == false && x.Qty > 0)
                .ToList();
            if (invs != null && invs.Count > 0)
            {
                var invTemp = invs.Find(x => x.FrozenStatus == FrozenStatusEnum.Freezed);
                if (invTemp != null)
                {
                    MSD.AddModelError("", $"库存序列号：{invTemp.SerialNumber}已被冻结，不能进行盘点");
                    return;
                }
                foreach (var inv in invs)
                {
                    InventoryStockTakingLine line = new InventoryStockTakingLine();
                    line.ID = Guid.NewGuid();
                    line.StockTakingId = Entity.ID;
                    line.DocLineNo = i++;
                    line.InventoryId = inv.ID;
                    line.KnifeId = null;
                    line.ItemMasterId = inv.ItemMasterId;
                    line.SerialNumber = inv.SerialNumber;
                    line.Seiban = inv.Seiban;
                    line.BatchNumber = inv.BatchNumber;
                    line.ScanBarCode = "";  // 扫码以后再更新
                    line.Qty = inv.Qty;
                    line.LocationId = null; //inv.WhLocationId;
                    line.StockTakingQty = 0;
                    line.DiffQty = null;
                    line.GainLossStatus = GainLossStatusEnum.NotStart;
                    line.IsNew = false;     // 此字段无法修改。创建时可赋值
                    line.IsKnifeLedger = false;
                    line.OperatingUser = null;
                    line.Memo = "";
                    line.CreateBy = LoginUserInfo.ITCode;
                    line.CreateTime = DateTime.Now;
                    lines.Add(line);
                }
            }
            // 构造盘点单明细行——刀具信息
            List<Knife> knives = DC.Set<Knife>()
                .AsNoTracking()
                .Where(x => locationIds.Contains(x.WhLocationId) && (x.Status == KnifeStatusEnum.InStock))  //  || x.Status == KnifeStatusEnum.GrindRequested
                .ToList();
            if (knives != null && knives.Count > 0)
            {
                foreach (var knife in knives)
                {
                    InventoryStockTakingLine line = new InventoryStockTakingLine();
                    line.ID = Guid.NewGuid();
                    line.StockTakingId = Entity.ID;
                    line.DocLineNo = i++;
                    line.InventoryId = null;
                    line.KnifeId = knife.ID;
                    line.ItemMasterId = knife.ItemMasterId;
                    line.SerialNumber = knife.SerialNumber;
                    line.ScanBarCode = "";  // 扫码以后再更新
                    line.Qty = 1;   // 刀具台账无数量概念。盘到就算1
                    line.LocationId = null; // knife.WhLocationId;
                    line.StockTakingQty = 0;
                    line.DiffQty = null;
                    line.GainLossStatus = GainLossStatusEnum.NotStart;
                    line.IsNew = false;     // 此字段无法修改。创建时可赋值
                    line.IsKnifeLedger = true;
                    line.OperatingUser = null;
                    line.Memo = "";
                    line.CreateBy = LoginUserInfo.ITCode;
                    line.CreateTime = DateTime.Now;
                    lines.Add(line);
                }
            }

            // 允许行为空。如：将空的库位中盘盈一些库存进去
            //if (lines.Count == 0)
            //{
            //    MSD.AddModelError("", "盘点单选中的库位中无库存或刀具");
            //    return;
            //}

            using (var trans = DC.BeginTransaction())
            {
                // 批量添加盘点单明细行
                ((DataContext)DC).BulkInsert(lines);
                Entity.Status = InventoryStockTakingStatusEnum.Approving;
                Entity.SubmitUser = LoginUserInfo.ITCode;
                Entity.SubmitTime = DateTime.Now;
                DC.UpdateEntity(Entity);
                DC.SaveChanges();
                if (!MSD.IsValid)
                {
                    return;
                }
                //// 过账U9，进行现有库存匹配（只过账库存信息相关的行。刀具台账在U9中无账）  浩敏：如果库存不匹配，属于系统问题，不应归属盘点单的必要操作。单独做一个功能（或报表）用来监控库存一致性。
                //BaseSysParaVM baseSysParaVM = Wtm.CreateVM<BaseSysParaVM>();
                //var forceMatch = baseSysParaVM.GetParaIntValue("InventoryStockTakingERPForceMatch");
                //if (forceMatch == 1)
                //{

                //}
                if (MSD.IsValid)
                {
                    trans.Commit();
                }
            }
        }

        /// <summary>
        /// 终止盘点单
        /// </summary>
        public void ForceClose()
        {
            if (Entity == null || Entity.ID == Guid.Empty)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            // 盘点单状态必须为“开立”，才可以提交
            if (Entity.Status == InventoryStockTakingStatusEnum.Opened
                || Entity.Status == InventoryStockTakingStatusEnum.Closed
                || Entity.Status == InventoryStockTakingStatusEnum.ForceClosed)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许终止");
                return;
            }

            if (Entity.CreateBy != LoginUserInfo.ITCode)
            {
                MSD.AddModelError("", "只有创建人才能终止盘点单");
                return;
            }

            // 已审核的盘点单。要判断U9是否已删除盘点单。如果已经删除，允许终止。否则，不允许终止。（标准操作：由信息部管理员弃审并删除U9的盘点单，再由库管员或管理员在G2WMS中进行终止操作）
            if (Entity.Status == InventoryStockTakingStatusEnum.Approved && Entity.ErpDocNo != null && Entity.Mode == InventoryStockTakingModeEnum.ErpWms)
            {
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", LoginUserInfo.Name);
                U9Return<int> u9Return = apiHelper.IsInventorySheetExist(Entity.ErpDocNo);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", $"U9接口调用失败：{u9Return.Msg}");
                    return;
                }
                if (u9Return.Entity == 1)
                {
                    MSD.AddModelError("", $"请先删除U9关联的盘点单{Entity.ErpDocNo}");
                    return;
                }
            }

            // 修改盘点单状态为“终止”，并将关联的库位解锁
            using (var trans = DC.BeginTransaction())
            {
                List<InventoryStockTakingLocations> locations = DC.Set<InventoryStockTakingLocations>()
                    .Include(x => x.Location)
                    .Where(x => x.StockTakingId == Entity.ID).AsNoTracking().ToList();
                if (locations != null && locations.Count > 0)
                {
                    foreach (var location in locations)
                    {
                        if (location.Location.Locked == true)    // 库位解锁
                            location.Location.Locked = false;
                    }
                    // 批量更新数据（数量大时，必须批量更新）
                    ((DataContext)DC).BulkUpdate(locations.Select(x => x.Location));
                }
                Entity.Status = InventoryStockTakingStatusEnum.ForceClosed;
                Entity.CloseUser = LoginUserInfo.ITCode;
                Entity.CloseTime = DateTime.Now;
                Entity.UpdateBy = LoginUserInfo.ITCode;
                Entity.UpdateTime = Entity.CloseTime;
                DC.UpdateEntity(Entity);
                DC.SaveChanges();

                trans.Commit();
            }
        }

        /// <summary>
        /// 审核盘点单
        /// </summary>
        public void Approve(string docId)
        {
            if (string.IsNullOrEmpty(docId))
            {
                MSD.AddModelError("", "盘点单ID无效");
                return;
            }

            Guid docGuid = Guid.Empty;
            if (!Guid.TryParse(docId, out docGuid))
            {
                MSD.AddModelError("", "盘点单ID格式错误");
                return;
            }

            // 获取盘点单
            using (var trans = DC.BeginTransaction())
            {
                try
                {
                    InventoryStockTaking stockTaking = DC.Set<InventoryStockTaking>()
                        .Include(x => x.Wh.Organization)
                        .Include(x => x.InventoryStockTakingLocations_StockTaking).ThenInclude(x => x.Location)
                        .Include(x => x.InventoryStockTakingLine_StockTaking).ThenInclude(x => x.ItemMaster)
                        .Include(x => x.InventoryStockTakingLine_StockTaking).ThenInclude(x => x.Inventory)
                        .Include(x => x.InventoryStockTakingLine_StockTaking).ThenInclude(x => x.Knife)
                        .FirstOrDefault(x => x.ID == docGuid);
                    if (stockTaking == null)
                    {
                        MSD.AddModelError("", "盘点单不存在");
                        return;
                    }
                    if (stockTaking.Status != InventoryStockTakingStatusEnum.Approving)
                    {
                        MSD.AddModelError("", $"盘点单当前状态{stockTaking.Status.GetEnumDisplayName()}无法进行审核");
                        return;
                    }

                    // 统一空番号的格式（避免group by时统计为不同的2行）
                    foreach (var line in stockTaking.InventoryStockTakingLine_StockTaking)
                    {
                        if (line.Seiban == null)
                        {
                            line.Seiban = "";
                        }
                    }
                    var errorGainKnife = stockTaking.InventoryStockTakingLine_StockTaking?.Find(x => x.GainLossStatus == GainLossStatusEnum.Gain
                        && x.IsKnifeLedger == true && x.Knife != null && x.Knife.Status != KnifeStatusEnum.inventoryLoss);
                    if (errorGainKnife != null)
                    {
                        MSD.AddModelError("", $"盘点单存在非盘亏状态的刀具盘盈，不允许进行审核。序列号：{errorGainKnife.SerialNumber}");
                        return;
                    }

                    // 获取盘点单差异（按料品+番号统计汇总）
                    CreateInventorySheetPara para = GetStockTakingDiff(stockTaking);
                    if (para != null)   // 存在差异时，过账U9
                    {
                        stockTaking.Status = InventoryStockTakingStatusEnum.Approved;
                        string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                        string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                        U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, stockTaking.Wh.Organization.Code, LoginUserInfo.Name);
                        U9Return u9Return = apiHelper.CreateInventorySheet(para);
                        if (!u9Return.Success)
                        {
                            MSD.AddModelError("", $"U9过账失败：{u9Return.Msg}");
                            return;
                        }
                        stockTaking.ErpDocNo = u9Return.Msg;    // ERP单号绑定
                    }
                    else    // 无需过账U9。直接生成库存调整单。盘点单状态直接变为关闭（无论是否需要进行调整，均生成调整单。无需调整时，调整单行为空）
                    {
                        stockTaking.Status = InventoryStockTakingStatusEnum.Approved;   // 不能直接改为关闭。要由差异单生成事件来改（为了和U9审核差异单生成调整单统一处理）
                        Adjust(stockTaking, trans);
                    }

                    if (MSD.IsValid)
                    {
                        DC.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MSD.AddModelError("", $"系统异常，请联系管理员。{ex.Message}");
                    return;
                }
            }
        }

        /// <summary>
        /// 获取盘点差异（构造U9盘点单参数）
        /// </summary>
        /// <param name="stockTaking">盘点单实体</param>
        private CreateInventorySheetPara GetStockTakingDiff(InventoryStockTaking stockTaking)
        {
            CreateInventorySheetPara para = null;
            // 删除所有标记为新行，且盘点数为0的行
            List<InventoryStockTakingLine> lines = stockTaking.InventoryStockTakingLine_StockTaking.Where(x => x.IsNew == true && x.StockTakingQty == 0).ToList();
            foreach (var line in lines)
            {
                DC.DeleteEntity(line);
            }
            // 所有状态为“未盘”的行均标记为盘亏
            lines = stockTaking.InventoryStockTakingLine_StockTaking.Where(x => x.GainLossStatus == GainLossStatusEnum.NotStart).ToList();
            foreach (var line in lines)
            {
                line.GainLossStatus = GainLossStatusEnum.Loss;
                line.OperatingUser = "";
                line.StockTakingQty = 0;
                line.DiffQty = -line.Qty;
            }
            DC.SaveChanges();
            if (stockTaking.Mode == InventoryStockTakingModeEnum.Wms)    // 盘点只调整WMS库存，无需构造U9参数
            {
                return null;
            }
            // 按料品+番号统计汇总
            var groupLines = stockTaking.InventoryStockTakingLine_StockTaking
                .Where(x => x.IsKnifeLedger == false)   // 过滤刀具台账行
                .GroupBy(x => new { x.ItemMaster.Code, x.Seiban })
                .Select(x => new { x.Key.Code, x.Key.Seiban, DiffQty = x.Sum(y => y.DiffQty) })
                .ToList();
            if (groupLines != null && groupLines.Count > 0)
            {
                para = new CreateInventorySheetPara();
                para.org = stockTaking.Wh.Organization.Code;
                para.inventorySheetDocType = "InvSheet001";
                para.businessDate = DateTime.Now.ToString("yyyy-MM-dd");
                para.wh = stockTaking.Wh.Code;
                para.allSeinban = 0;    // 此参数没用，固定写死0即可
                para.inventSheetLine = new List<CreateInventorySheetLinePara>();
                para.businessId = stockTaking.ID;
                foreach (var line in groupLines)
                {
                    if (line.DiffQty != 0)
                    {
                        CreateInventorySheetLinePara linePara = new CreateInventorySheetLinePara();
                        linePara.ItemInfo_ItemCode = line.Code;
                        linePara.Seiban = line.Seiban;
                        linePara.CheckingQtyCU = (decimal)line.DiffQty;
                        para.inventSheetLine.Add(linePara);
                    }
                }
                if (para.inventSheetLine.Count == 0)
                {
                    para = null;
                }
            }
            return para;
        }

        /// <summary>
        /// 库存调账
        /// </summary>
        public void Adjust(string u9DocNo)
        {
            InventoryStockTaking stockTaking = DC.Set<InventoryStockTaking>()
                .Include(x => x.Wh.Organization)
                .Include(x => x.InventoryStockTakingLocations_StockTaking).ThenInclude(x => x.Location)
                .Include(x => x.InventoryStockTakingLine_StockTaking).ThenInclude(x => x.ItemMaster)
                .Include(x => x.InventoryStockTakingLine_StockTaking).ThenInclude(x => x.Inventory)
                .Where(x => x.ErpDocNo == u9DocNo).FirstOrDefault();
            if (stockTaking == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            if (stockTaking.Status != InventoryStockTakingStatusEnum.Approved)
            {
                MSD.AddModelError("", $"盘点单当前状态“{stockTaking.Status.GetEnumDisplayName()}”无法进行库存调账");
                return;
            }
            Adjust(stockTaking);
        }

        /// <summary>
        /// 库存调账（生成库存调整单）
        /// </summary>
        /// <param name="stockTaking">盘点单</param>
        /// <param name="transaction">事务</param>
        public void Adjust(InventoryStockTaking stockTaking, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = null)
        {
            var tran = transaction ?? DC.Database.BeginTransaction();
            if (stockTaking == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            if (stockTaking.Status != InventoryStockTakingStatusEnum.Approved)
            {
                MSD.AddModelError("", "盘点单当前状态无法进行库存调账");
                return;
            }
            // 解锁库位（必须放在前面处理，放后面会导致未修改。可能是其它保存操作导致跟踪实体失效）
            foreach (var line in stockTaking.InventoryStockTakingLocations_StockTaking)
            {
                line.Location.Locked = false;
            }
            stockTaking.Status = InventoryStockTakingStatusEnum.Closed; // 修改盘点单状态为“关闭”
            BaseData.BaseInventoryVMs.BaseInventoryVM baseInvVM = Wtm.CreateVM<BaseData.BaseInventoryVMs.BaseInventoryVM>();
            InventoryAdjustVMs.InventoryAdjustVM invAdjustVM = Wtm.CreateVM<InventoryAdjustVMs.InventoryAdjustVM>();
            // 创建库存调整单
            invAdjustVM.Entity = new InventoryAdjust
            {
                StockTakingId = stockTaking.ID,
                DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventoryAdjustDocNoRule", tran),
                InventoryAdjustLine_InvAdjust = new List<InventoryAdjustLine>(),
            };
            // 刀具盘盈盘亏集合
            List<(List<Guid> knifeids_surplus, Guid whLocationId)> knifeInfos_surplus = new List<(List<Guid> knifeids, Guid whLocationId)>();
            List<Guid> knifeids_loss = new List<Guid>();
            // 循环盘点单行
            foreach (var line in stockTaking.InventoryStockTakingLine_StockTaking)
            {
                // 刀具台账
                if (line.IsKnifeLedger == true)
                {
                    if (line.KnifeId == null)
                    {
                        MSD.AddModelError("", $"数据异常。序列号：{line.SerialNumber}为刀具台账，但刀具ID为空");
                        return;
                    }
                    
                    if(line.GainLossStatus == GainLossStatusEnum.Gain)  // 盘盈
                    {
                        if (line.LocationId == null)
                        {
                            MSD.AddModelError("", $"数据异常。序列号：{line.SerialNumber}状态为盘盈，但库位为空");
                            return;
                        }
                        if (knifeInfos_surplus.Any(x => x.whLocationId == line.LocationId)) // 盘盈库位在集合中已存在
                        {
                            (List<Guid> knifeids_surplus, Guid whLocationId) item = knifeInfos_surplus.Find(x => x.whLocationId == line.LocationId);
                            item.knifeids_surplus.Add((Guid)line.KnifeId);
                        }
                        else    // 盘盈库位在集合中不存在
                        {
                            (List<Guid> knifeids_surplus, Guid whLocationId) item = (new List<Guid>() { (Guid)line.KnifeId }, (Guid)line.LocationId);
                            knifeInfos_surplus.Add(item);
                        }
                    }
                    else if(line.GainLossStatus == GainLossStatusEnum.Loss) // 盘亏
                    {
                        knifeids_loss.Add((Guid)line.KnifeId);
                    }
                }
                // 库存账
                if (line.DiffQty != 0 && line.IsKnifeLedger == false)   // 刀具台账不调账
                {
                    var targetInv = line.Inventory;
                    InventoryAdjustLine adjustLine = new InventoryAdjustLine
                    {
                        StockTakingLineId = line.ID,
                        InventoryId = line.InventoryId,
                        Inventory = line.Inventory, // 下面都用adjustLine，所以给一下值
                        LocationId = line.LocationId,
                        Qty = line.Inventory?.Qty ?? 0,
                        StockTakingQty = line.StockTakingQty,
                        DiffQty = line.DiffQty,
                        GainLossStatus = line.GainLossStatus,
                        Memo = line.Memo,
                    };
                    invAdjustVM.Entity.InventoryAdjustLine_InvAdjust.Add(adjustLine);

                    // 更新原库存信息
                    if (adjustLine.InventoryId != null)
                    {
                        adjustLine.Inventory.Qty += adjustLine.DiffQty;
                        if (adjustLine.Inventory.Qty < 0)
                        {
                            MSD.AddModelError("", $"条码：{line.ScanBarCode}库存数量不足，无法完成库存调账。");
                            return;
                        }
                        if (adjustLine.Inventory.Qty == 0)
                        {
                            adjustLine.Inventory.IsAbandoned = true;
                        }
                    }
                    else    // 创建库存信息
                    {
                        // 查找条码表，找到对应信息
                        var barcode = DC.Set<BaseBarCode>()
                           .Include(x => x.Item)
                           .AsNoTracking()
                           .FirstOrDefault(x => x.Sn == line.SerialNumber);
                        if (barcode == null)    // 正常不会出现
                        {
                            MSD.AddModelError("", $"序列号：{line.SerialNumber}在条码表中不存在，无法完成库存调账。");
                            return;
                        }
                        int scanType = int.Parse(line.ScanBarCode.Substring(0, 1));
                        baseInvVM.Entity = new BaseInventory
                        {
                            ItemMasterId = line.ItemMasterId,
                            WhLocationId = line.LocationId,
                            BatchNumber = barcode.BatchNumber ?? "",
                            SerialNumber = barcode.Sn,
                            Seiban = barcode.Seiban,
                            SeibanRandom = barcode.SeibanRandom,
                            Qty = adjustLine.StockTakingQty,
                            ItemSourceType = (ItemSourceTypeEnum)scanType,
                            FrozenStatus = FrozenStatusEnum.Normal,
                            IsAbandoned = false,
                        };
                        baseInvVM.DoAdd();
                        if (!MSD.IsValid)
                        {
                            return;
                        }
                        targetInv = baseInvVM.Entity;
                    }

                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryAdjustCreate, invAdjustVM.Entity.DocNo, null, targetInv.ID, null, adjustLine.DiffQty, invAdjustVM.Entity.Memo))
                    {
                        return;
                    }
                }
            }
            invAdjustVM.DoAdd();
            if (!MSD.IsValid)
            {
                return;
            }
            // 刀具台账调整
            KnifeVM knifeVM = Wtm.CreateVM<KnifeVM>();
            knifeVM.DoInventoryAdjust_Knifes(knifeInfos_surplus, knifeids_loss, stockTaking.DocNo);
            if (!MSD.IsValid)
            {
                return;
            }


            if (MSD.IsValid && transaction == null)
            {
                tran.Commit();
            }
        }
    }
}
