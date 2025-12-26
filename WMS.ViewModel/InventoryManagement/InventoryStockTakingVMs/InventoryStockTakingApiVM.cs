using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using Elsa;
using WMS.Model;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using WMS.Model.KnifeManagement;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using System.Linq.Expressions;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingApiVM : BaseCRUDVM<InventoryStockTaking>
    {

        public InventoryStockTakingApiVM()
        {
            SetInclude(x => x.Wh);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        /// <summary>
        /// 获取单据列表
        /// </summary>
        /// <returns></returns>
        public List<InventoryStockTakingReturn> GetList()
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            if (whid == Guid.Empty)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录2");
                return null;
            }
            List<InventoryStockTakingReturn> list = new List<InventoryStockTakingReturn>();
            Expression<Func<InventoryStockTaking, bool>> queryable = x => x.Status == InventoryStockTakingStatusEnum.Approving
                && x.WhId == whid;
            list = DC.Set<InventoryStockTaking>()
               .Where(queryable)
               .AsNoTracking()
               .Select(x => new InventoryStockTakingReturn
               {
                   ID = x.ID,
                   DocNo = x.DocNo,
                   Status = x.Status.GetEnumDisplayName(),
                   StatusValue = (int)x.Status,
                   WhId = x.WhId,
                   WhName = x.Wh.Name,
                   WhCode = x.Wh.Code,
                   CreateTime = x.CreateTime,
               }).OrderByDescending(x => x.CreateTime).ToList();
            if (list.Count > 50)
            {
                // 只返回前50条
                list = list.Take(50).ToList();
            }
            return list;
        }

        /// <summary>
        /// 扫码获取盘点单
        /// </summary>
        /// <param name="docNo">单据编号</param>
        public InventoryStockTakingReturn GetDoc(string docNo)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            var whVm = Wtm.CreateVM<BaseWareHouseVM>();
            BaseWareHouse loginWh = whVm.GetEntityById(whid);
            if (loginWh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return null;
            }

            InventoryStockTaking doc = DC.Set<InventoryStockTaking>()
                .Where(x => x.DocNo == docNo)
                .Include(x => x.Wh)
                .AsNoTracking().FirstOrDefault();
            if (doc != null)    // 单据在本系统中存在
            {
                if (doc.WhId != loginWh.ID)
                {
                    MSD.AddModelError("", "单据存储地点与登录用户的存储地点不一致，请尝试重新登录");
                    return null;
                }
                // 非审核中状态下只能查看，不能进行盘点
                //if (doc.Status != InventoryStockTakingStatusEnum.Approving) // 只有审核中状态下才能进行盘点
                //{
                //    MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”无法进行盘点");
                //    return null;
                //}
                return new InventoryStockTakingReturn(doc);
            }
            else
            {
                MSD.AddModelError("", "单据不存在");
                return null;
            }
        }

        /// <summary>
        /// 扫码获取库位
        /// </summary>
        /// <param name="docId">盘点单ID</param>
        /// <param name="code">库位编码</param>
        /// <returns></returns>
        public BaseReturn GetLocation(Guid? docId, string code)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            var whVm = Wtm.CreateVM<BaseWareHouseVM>();
            BaseWareHouse loginWh = whVm.GetEntityById(whid);
            if (loginWh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return null;
            }

            BaseWhLocation location = DC.Set<BaseWhLocation>()
                .Include(x => x.WhArea)
                .Where(x => x.Code.ToLower().Equals(code.ToLower()))
                .AsNoTracking()
                .FirstOrDefault();
            if (location == null)
            {
                MSD.AddModelError("", "库位不存在");
                return null;
            }
            if (location.IsEffective != Model.EffectiveEnum.Effective)
            {
                MSD.AddModelError("", "库位已失效");
                return null;
            }
            if (location.WhArea.WareHouseId != loginWh.ID)
            {
                MSD.AddModelError("", "扫描库位不属于当前登录仓库");
                return null;
            }
            // 盘点单状态为审核中才能进行盘点
            var doc = DC.Set<InventoryStockTaking>().Where(x => x.ID == docId).AsNoTracking().FirstOrDefault();
            if (doc == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return null;
            }
            if (doc.Status != InventoryStockTakingStatusEnum.Approving)
            {
                MSD.AddModelError("", $"当前单据状态“{doc.Status.GetEnumDisplayName()}”无法进行盘点");
                return null;
            }
            // 判断扫描库位是否属于本盘点单的库位范围
            if (!DC.Set<InventoryStockTakingLocations>().Any(x => x.StockTakingId == docId && x.LocationId == location.ID))
            {
                MSD.AddModelError("", $"库位{code}不属于本盘点单的盘点范围");
                return null;
            }
            return new BaseReturn { ID = location.ID, Code = location.Code, Name = location.Name };
        }

        /// <summary>
        /// 盘点扫码
        /// </summary>
        /// <param name="sn">序料号</param>
        /// <param name="barCode">条码</param>
        /// <param name="locationId">库位ID</param>
        /// <returns></returns>
        public StockTakeScanReturn StockTakeScan(string sn, string barCode, Guid? locationId)
        {
            /**
             * 0. 暂定不支持托盘码
             * 1. 从盘点行中查找是否已经有该条码，如果有，则直接返回行信息
             * 2. 在库存中查询条码对应的库存，如果库存不存在，再从刀具台账中查询（优先级1）
             * 3. 如果刀具台账不存在，则查询G2条码表（优先级2）
             * 4. G2条码表不存在，则查询链溯条码表（优先级3）
             * 5. 链溯条码表不存在，则返回错误信息（优先级4）
             * */
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            var whVm = Wtm.CreateVM<BaseWareHouseVM>();
            BaseWareHouse loginWh = whVm.GetEntityById(whid);
            if (loginWh == null)
            {
                MSD.AddModelError("", "存储地点信息无效，请尝试重新登录");
                return null;
            }

            BaseWhLocation location = DC.Set<BaseWhLocation>()
                .Include(x => x.WhArea)
                .AsNoTracking()
                .FirstOrDefault(x => x.ID == locationId);
            if (location == null)
            {
                MSD.AddModelError("", "库位不存在");
                return null;
            }
            if (location.IsEffective != Model.EffectiveEnum.Effective)
            {
                MSD.AddModelError("", "库位已失效");
                return null;
            }
            if (location.WhArea.WareHouseId != loginWh.ID)
            {
                MSD.AddModelError("", $"盘点库位不属于当前登录仓库{loginWh.Code}");
                return null;
            }

            // 获取盘点单
            InventoryStockTaking doc = DC.Set<InventoryStockTaking>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Status == InventoryStockTakingStatusEnum.Approving && x.InventoryStockTakingLocations_StockTaking.Any(y => y.LocationId == locationId));
            if (doc == null)
            {
                MSD.AddModelError("", $"当前扫描库位（{location.Code}）未关联任何进行中的盘点单");
                return null;
            }

            // 1. 从盘点行中查找是否已经有该条码
            InventoryStockTakingLine line = DC.Set<InventoryStockTakingLine>()  // 必须要跟踪（要修改）
                .Include(x => x.Knife.WhLocation.WhArea.WareHouse)
                .Include(x => x.Inventory.WhLocation.WhArea.WareHouse)
                .Include(x => x.ItemMaster)
                .FirstOrDefault(x => x.SerialNumber == sn && x.StockTakingId == doc.ID);
            if (line != null)
            {
                if (line.Knife != null && line.Knife.WhLocationId != null)
                {
                    if (line.Knife.WhLocationId != locationId)
                    {
                        MSD.AddModelError("", $"条码{sn}所在库位（{line.Knife.WhLocation?.Code}）与当前盘点库位({location.Code})不一致");
                        return null;
                    }
                }
                if (line.Inventory != null && line.Inventory.WhLocationId != null)
                {
                    if (line.Inventory.WhLocationId != locationId)
                    {
                        MSD.AddModelError("", $"条码{sn}所在库位（{line.Inventory.WhLocation?.Code}）与当前盘点库位({location.Code})不一致");
                        return null;
                    }
                }
                bool isRescan = true;
                if (line.GainLossStatus == GainLossStatusEnum.NotStart)
                {
                    line.ScanBarCode = barCode;
                    line.StockTakingQty = line.Qty;
                    line.DiffQty = 0;
                    line.GainLossStatus = GainLossStatusEnum.Equal; // 扫到即盘平；未扫到后面盈亏分析时变为盘亏。原库存信息有的不存在盘盈的情况。原库存信息没有的才能盘盈（只有这一种盘盈）
                    line.OperatingUser = LoginUserInfo.ITCode;
                    line.StockTakingTime = DateTime.Now;
                    line.LocationId = locationId;
                    isRescan = false;
                    DC.SaveChanges();
                }
                return new StockTakeScanReturn
                {
                    Type = line.InventoryId == null ? (line.KnifeId == null ? StockTakeScanTypeEnum.BarCode : StockTakeScanTypeEnum.Knife) : StockTakeScanTypeEnum.Inventory,
                    ID = line.InventoryId ?? line.KnifeId ?? null,
                    StockTakeLineID = line.ID,
                    LocationID = line.Inventory?.WhLocation?.ID ?? line.Knife?.WhLocation?.ID,
                    LocationCode = line.Inventory?.WhLocation?.Code ?? line.Knife?.WhLocation?.Code,
                    LocationName = line.Inventory?.WhLocation?.Name ?? line.Knife?.WhLocation?.Name,
                    ItemID = line.ItemMaster.ID,
                    ItemCode = line.ItemMaster.Code,
                    ItemName = line.ItemMaster.Name,
                    ItemSpec = line.ItemMaster.SPECS,
                    Batch = line.BatchNumber,
                    Seiban = line.Seiban,
                    SerialNumber = line.SerialNumber,
                    Qty = (decimal)line.Qty,
                    StockTakingQty = (decimal)line.StockTakingQty,
                    IsNew = (bool)line.IsNew,
                    IsRescan = isRescan,
                };
            }
            // 2. 从库存中查询条码对应的库存（此种情况匹配到只会报错，不会匹配成功。因为库存信息中存在，且库位正确，则在盘点单提交时已经添加到盘点行中）
            BaseInventory data = DC.Set<BaseInventory>()
                .AsNoTracking()
                .Where(x => x.IsAbandoned == false && x.Qty > 0) // x.WhLocation.WhArea.WareHouseId == whid && 无需判定原库位是否属于当前登录仓库
                                                                 // .Where(x => x.WhLocation.AreaType == WhLocationEnum.Normal)
                                                                 // .Where(x => x.FrozenStatus == FrozenStatusEnum.Normal)
                .Include(x => x.ItemMaster)
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .FirstOrDefault(x => x.SerialNumber == sn);
            if (data != null)
            {
                if (data.WhLocation.WhArea.WareHouseId != loginWh.ID)
                {
                    MSD.AddModelError("", $"当前条码不属于当前登录存储地点{loginWh.Code}（对应库存信息存在于{data.WhLocation.WhArea.WareHouse.Code}中）");
                    return null;
                }
                if (data.WhLocationId != locationId)
                {
                    MSD.AddModelError("", $"当前条码所在库位（{data.WhLocation.Code}）不属于当前盘点库位({location.Code})");
                    return null;
                }
                if (data.WhLocation.AreaType != WhLocationEnum.Normal)
                {
                    MSD.AddModelError("", $"条码所属库位({data.WhLocation.AreaType.GetEnumDisplayName()})不属于正常库位，无法进行盘点");
                    return null;
                }
                if (data.FrozenStatus == FrozenStatusEnum.Freezed)
                {
                    MSD.AddModelError("", "该库存已冻结");
                    return null;
                }
                MSD.AddModelError("", "盘点过程中可能有库存异动。请将当前条码提供给管理员核实。");
                return null;
            }
            else
            {
                // 3. 从刀具台账中查询
                Knife knife = DC.Set<Knife>()
                   .AsNoTracking()
                   .Where(x => x.Status != KnifeStatusEnum.DefectiveReturned)   // 不良退回的会返回到库存中，不属于台账。其余状态均可接受
                   .Include(x => x.ItemMaster)
                   .Include(x => x.WhLocation.WhArea.WareHouse)
                   .FirstOrDefault(x => x.SerialNumber == sn);
                if (knife != null)
                {
                    if (knife.WhLocation.WhArea.WareHouseId != loginWh.ID)
                    {
                        MSD.AddModelError("", "库位不属于当前登录仓库");
                        return null;
                    }
                    if (knife.WhLocation.AreaType != WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"条码所属库位({knife.WhLocation.AreaType.GetEnumDisplayName()})不属于正常库位，无法进行盘点");
                        return null;
                    }
                    if (knife.Status == KnifeStatusEnum.InStock) // 盘点单创建时，已锁库存。所有在库的刀具应该已经在盘点行中，不应该在此处出现。
                    {
                        MSD.AddModelError("", "盘点过程中可能有刀具台账异动。请将当前条码提供给管理员核实。");
                        return null;
                    }

                    // 添加一行盘点行记录
                    Guid lineId = AddStockTakeLine(barCode, locationId, doc, knife: knife);
                    if (!MSD.IsValid)
                    {
                        return null;
                    }
                    return new StockTakeScanReturn
                    {
                        Type = StockTakeScanTypeEnum.Knife,
                        ID = knife.ID,
                        StockTakeLineID = lineId,
                        LocationID = knife.WhLocation?.ID,
                        LocationCode = knife.WhLocation?.Code,
                        LocationName = knife.WhLocation?.Name,
                        ItemID = knife.ItemMaster?.ID,
                        ItemCode = knife.ItemMaster?.Code,
                        ItemName = knife.ItemMaster?.Name,
                        ItemSpec = knife.ItemMaster?.SPECS,
                        Batch = "",
                        Seiban = "",
                        SerialNumber = knife.SerialNumber,
                        Qty = 1,
                        IsNew = true,
                        IsRescan = false
                    };
                }
                else
                {
                    // 4. 从G2条码表中查询
                    BaseBarCodeVM barCodeVM = new BaseBarCodeVM();
                    BaseBarCode barCode2 = DC.Set<BaseBarCode>().Include(x => x.Item).AsNoTracking().FirstOrDefault(x => x.Sn == sn);
                    if (barCode2 != null)
                    {
                        // 添加一行盘点行记录
                        Guid lineId = AddStockTakeLine(barCode, locationId, doc, barCode: barCode2);
                        if (!MSD.IsValid)
                        {
                            return null;
                        }
                        return new StockTakeScanReturn
                        {
                            Type = StockTakeScanTypeEnum.BarCode,
                            ID = null,  //barCode2.ID,（条码表的ID没有用处。库存、刀具台账的ID后面会做流水记录用）
                            StockTakeLineID = lineId,
                            LocationID = null,  // 条码表中无库位信息
                            LocationCode = null,
                            LocationName = null,
                            ItemID = barCode2.Item?.ID,
                            ItemCode = barCode2.Item?.Code,
                            ItemName = barCode2.Item?.Name,
                            ItemSpec = barCode2.Item?.SPECS,
                            Batch = "",
                            Seiban = barCode2.Seiban,
                            SerialNumber = barCode2.Sn,
                            Qty = (decimal)barCode2.Qty,
                            IsNew = true,
                            IsRescan = false,
                        };
                    }
                    else
                    {
                        // 5. 从链溯条码表中查询
                        var result = LsWmsHelper.GetBarCode(sn);
                        if (result == null)
                        {
                            MSD.AddModelError("", "此条码无效");
                            return null;
                        }
                        string[] arr = result.BarCode.Split('@');
                        if (arr.Length != 4)
                        {
                            MSD.AddModelError("", "本系统不存在此条码。链溯WMS中的条码格式错误");
                            return null;
                        }
                        decimal qty = 0;
                        decimal.TryParse(arr[2], out qty);
                        if (qty < 0)
                        {
                            MSD.AddModelError("", "数量信息错误或条码格式错误");
                            return null;
                        }
                        string itemCode = arr[1];
                        if (string.IsNullOrEmpty(itemCode))
                        {
                            MSD.AddModelError("", "料号信息错误或条码格式错误");
                            return null;
                        }
                        BaseOrganization org = DC.Set<BaseOrganization>().AsNoTracking().Where(x => x.Code == result.OrgCode).FirstOrDefault();
                        if (org == null)
                        {
                            MSD.AddModelError("", $"组织编码“{result.OrgCode}”不存在");
                            return null;
                        }
                        if (org.IsEffective == EffectiveEnum.Ineffective || org.IsValid == false)
                        {
                            MSD.AddModelError("", $"组织“{org.Name}”已失效");
                            return null;
                        }
                        BaseItemMaster item = DC.Set<BaseItemMaster>().AsNoTracking().Where(x => x.Code == itemCode && x.Organization.Code == org.Code).FirstOrDefault();
                        if (item == null)
                        {
                            MSD.AddModelError("", $"料号“{itemCode}”不存在，请尝试同步");
                            return null;
                        }
                        if (item.IsEffective == EffectiveEnum.Ineffective || item.IsValid == false)
                        {
                            MSD.AddModelError("", $"料号“{item.Name}”已失效");
                            return null;
                        }
                        // 构建条码信息
                        barCodeVM.Entity = new BaseBarCode
                        {
                            DocNo = result.DocNo,
                            Code = result.BarCode,
                            Sn = arr[3],
                            Qty = qty,
                            ItemId = item.ID,
                            // 从链溯系统返回的各种字段信息.
                        };
                        barCodeVM.DoAdd();
                        barCode2 = DC.Set<BaseBarCode>().Include(x => x.Item).AsNoTracking().Where(x => x.Sn == arr[3]).FirstOrDefault();
                        if (barCode2 == null)
                        {
                            MSD.AddModelError("", "从链溯获取的条码信息保存失败");
                            return null;
                        }
                        // 添加一行盘点行记录
                        Guid lineId = AddStockTakeLine(barCode, locationId, doc, barCode: barCode2);
                        if (!MSD.IsValid)
                        {
                            return null;
                        }
                        return new StockTakeScanReturn
                        {
                            Type = StockTakeScanTypeEnum.BarCode,
                            ID = null,  //barCode2.ID,
                            StockTakeLineID = lineId,
                            LocationID = null,  // 条码表中无库位信息
                            LocationCode = null,
                            LocationName = null,
                            ItemID = barCode2.Item?.ID,
                            ItemCode = barCode2.Item?.Code,
                            ItemName = barCode2.Item?.Name,
                            ItemSpec = barCode2.Item?.SPECS,
                            Batch = "",
                            Seiban = barCode2.Seiban,
                            SerialNumber = barCode2.Sn,
                            Qty = qty,
                            IsNew = true,
                            IsRescan = false,
                        };
                    }
                }
            }
        }

        /// <summary>
        /// 添加一行盘点行记录
        /// </summary>
        /// <param name="knife"></param>
        /// <param name="inventory"></param>
        /// <param name="barCode"></param>
        /// <param name="scanBarCode"></param>
        /// <param name="locationId"></param>
        /// <param name="doc"></param>
        private Guid AddStockTakeLine(string scanBarCode, Guid? locationId, InventoryStockTaking doc, Knife knife = null, BaseInventory inventory = null, BaseBarCode barCode = null)
        {
            if (inventory == null && knife == null && barCode == null)
            {
                MSD.AddModelError("", "AddStockTakeLine方法调用错误。库存实体、刀具实体、条码实体至少需要指定一个");
                return Guid.Empty;
            }
            // 获取盘点行的最大行号
            int maxLineNo = DC.Set<InventoryStockTakingLine>()
                .AsNoTracking()
                .Where(x => x.StockTakingId == doc.ID)
                .Max(x => x.DocLineNo) ?? 0;
            // 将新行添加到盘点单行中
            InventoryStockTakingLine newLine = new InventoryStockTakingLine
            {
                ID = Guid.NewGuid(),
                StockTakingId = doc.ID,
                DocLineNo = maxLineNo + 1,
                InventoryId = inventory?.ID,
                KnifeId = knife?.ID,
                ItemMasterId = inventory?.ItemMasterId ?? knife?.ItemMasterId ?? barCode?.ItemId,
                SerialNumber = inventory?.SerialNumber ?? knife?.SerialNumber ?? barCode?.Sn,
                Seiban = inventory?.Seiban ?? barCode?.Seiban ?? "",
                BatchNumber = inventory?.BatchNumber ?? barCode?.BatchNumber ?? "",
                ScanBarCode = scanBarCode,
                Qty = 0,
                LocationId = locationId,
                StockTakingQty = inventory?.Qty ?? barCode?.Qty ?? 1,
                DiffQty = inventory?.Qty ?? barCode?.Qty ?? 1,
                GainLossStatus = GainLossStatusEnum.Gain,   // 新增行均为盘盈
                IsNew = true,
                IsKnifeLedger = knife == null ? false : true,
                OperatingUser = LoginUserInfo.ITCode,
                StockTakingTime = DateTime.Now,
                CreateBy = LoginUserInfo.ITCode,
                CreateTime = DateTime.Now,
            };
            DC.Set<InventoryStockTakingLine>().Add(newLine);
            DC.SaveChanges();
            return newLine.ID;
        }

        /// <summary>
        /// 清除单条盘点数据
        /// </summary>
        /// <param name="lineId">盘点行ID</param>
        public void StockTakeClear(Guid? lineId)
        {
            if (lineId == null)
            {
                MSD.AddModelError("", "盘点行ID不能为空");
                return;
            }
            InventoryStockTakingLine line = DC.Set<InventoryStockTakingLine>().Include(x => x.StockTaking).Where(x => x.ID == lineId).FirstOrDefault();
            if (line == null)
            {
                MSD.AddModelError("", "盘点行不存在");
                return;
            }
            if (line.GainLossStatus == GainLossStatusEnum.NotStart) // 正常不会出现。前端按钮显示了，说明已经盘过了
            {
                MSD.AddModelError("", "当前条码尚未盘点，操作失败");
                return;
            }
            if (line.StockTaking.Status != InventoryStockTakingStatusEnum.Approving)
            {
                MSD.AddModelError("", $"当前盘点单状态({line.StockTaking.Status.GetEnumDisplayName()})不允许操作");
                return;
            }
            // 如果是新行，则删除。如果是旧行，则清除盘点数据
            if (line.IsNew == true)
            {
                DC.Set<InventoryStockTakingLine>().Remove(line);
                DC.SaveChanges();
            }
            else
            {
                // 盘点操作相关的数据清空
                line.ScanBarCode = "";
                line.StockTakingQty = 0;
                line.DiffQty = null;
                line.GainLossStatus = GainLossStatusEnum.NotStart;
                line.OperatingUser = null;
                line.LocationId = null;
                DC.SaveChanges();
            }
        }

        /// <summary>
        /// 清除库位盘点数据
        /// </summary>
        /// <param name="docId">盘点单ID</param>
        /// <param name="locationId">库位ID</param>
        public void LocationStockTakeClear(Guid? docId, Guid? locationId)
        {
            if (docId == null || locationId == null)
            {
                MSD.AddModelError("", "库位盘点数据清除失败。参数不能为空");
                return;
            }
            InventoryStockTaking doc = DC.Set<InventoryStockTaking>()
                .Include(x => x.InventoryStockTakingLine_StockTaking)
                .Include(x => x.InventoryStockTakingLocations_StockTaking)
                .Where(x => x.ID == docId)
                .FirstOrDefault();
            if (doc == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return;
            }
            if (doc.Status != InventoryStockTakingStatusEnum.Approving)
            {
                MSD.AddModelError("", $"盘点单当前状态({doc.Status.GetEnumDisplayName()})不允许执行清除盘点数据操作");
                return;
            }
            var location = doc.InventoryStockTakingLocations_StockTaking.FirstOrDefault(x => x.LocationId == locationId);
            if (location == null)
            {
                MSD.AddModelError("", "此库位不属于当前盘点单");   // 正常不会出现
                return;
            }

            // 盘点行数据清空
            var lines = doc.InventoryStockTakingLine_StockTaking.Where(x => x.LocationId == locationId).ToList();
            if (lines == null || lines.Count == 0)
            {
                return;
            }
            List<InventoryStockTakingLine> removeLines = new List<InventoryStockTakingLine>();
            foreach (var line in lines)
            {
                // 如果是新行，则删除。如果是旧行，则清除盘点数据
                if (line.IsNew == true)
                {
                    removeLines.Add(line);
                }
                else
                {
                    // 盘点操作相关的数据清空
                    line.ScanBarCode = "";
                    line.StockTakingQty = 0;
                    line.DiffQty = null;
                    line.GainLossStatus = GainLossStatusEnum.NotStart;
                    line.OperatingUser = null;
                    line.LocationId = null;
                }
            }
            DC.Set<InventoryStockTakingLine>().RemoveRange(removeLines);
            DC.SaveChanges();
        }

        /// <summary>
        /// 获取盘点单行数据
        /// </summary>
        /// <param name="docId">单据ID</param>
        /// <param name="locationId">库位ID</param>
        /// <param name="status">盘点状态</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        public List<InventoryStockTakingLineReturn> GetLines(Guid? docId, Guid? locationId, GainLossStatusEnum? status, int pageNum, int pageSize)
        {
            List<InventoryStockTakingLineReturn> ret = new List<InventoryStockTakingLineReturn>();
            if (docId == null || locationId == null)
            {
                MSD.AddModelError("", "库位盘点数据清除失败。参数不能为空");
                return ret;
            }
            if (pageNum <= 0 || pageSize <= 0)
            {
                MSD.AddModelError("", "页码或每页数量不能小于等于0");
                return ret;
            }
            InventoryStockTaking doc = DC.Set<InventoryStockTaking>()
                .Include(x => x.InventoryStockTakingLine_StockTaking)
                .ThenInclude(x => x.ItemMaster)
                .Include(x => x.InventoryStockTakingLine_StockTaking)
                .ThenInclude(x => x.Location)
                .Include(x => x.InventoryStockTakingLine_StockTaking)
                .ThenInclude(x => x.Inventory)
                .ThenInclude(x => x.WhLocation)
                .Include(x => x.InventoryStockTakingLine_StockTaking)
                .ThenInclude(x => x.Knife)
                .ThenInclude(x => x.WhLocation)
                .AsNoTracking()
                .Where(x => x.ID == docId)
                .FirstOrDefault();
            if (doc == null)
            {
                MSD.AddModelError("", "盘点单不存在");
                return ret;
            }
            if (doc.InventoryStockTakingLine_StockTaking == null || doc.InventoryStockTakingLine_StockTaking.Count == 0)
            {
                return ret;
            }

            List<InventoryStockTakingLine> lines = doc.InventoryStockTakingLine_StockTaking
                .FindAll(x => (x.LocationId == locationId || (x.Inventory != null && x.Inventory.WhLocationId == locationId) || (x.Knife != null && x.Knife.WhLocationId == locationId)) && (status == null || x.GainLossStatus == status))
                .OrderByDescending(x => x.StockTakingTime).ThenBy(x => x.DocLineNo)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();  // 分页
            if (lines == null || lines.Count == 0)
            {
                return ret;
            }
            foreach (var line in lines)
            {
                InventoryStockTakingLineReturn lineRet = new InventoryStockTakingLineReturn
                {
                    ID = line.ID,
                    ItemCode = line.ItemMaster.Code,
                    ItemName = line.ItemMaster.Name,
                    ItemSPECS = line.ItemMaster.SPECS,
                    SerialNumber = line.SerialNumber,
                    BatchNumber = line.BatchNumber,
                    Seiban = line.Seiban,
                    LocationCode = line.Location?.Code ?? line.Inventory?.WhLocation?.Code ?? line.Knife?.WhLocation?.Code,
                    IsNew = line.IsNew,
                    Qty = line.Qty,
                    StockTakingQty = line.StockTakingQty,
                    IsKnifeLedger = line.IsKnifeLedger,
                    Status = (int)line.GainLossStatus,
                    StatusName = line.GainLossStatus.GetEnumDisplayName(),
                };
                ret.Add(lineRet);
            }

            return ret;
        }
    }
}
