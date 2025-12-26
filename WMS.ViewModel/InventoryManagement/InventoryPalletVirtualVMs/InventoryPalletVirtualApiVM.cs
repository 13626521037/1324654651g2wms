using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;
using Microsoft.EntityFrameworkCore;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using Elsa;
using WMS.Model;
using EFCore.BulkExtensions;
using WMS.ViewModel;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualApiVM : BaseCRUDVM<InventoryPalletVirtual>
    {

        public InventoryPalletVirtualApiVM()
        {
            SetInclude(x => x.Location);
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
        /// 根据托盘码获取托盘信息
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public InventoryPalletVirtualReturn GetDataByCode(string code)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            InventoryPalletVirtual data = DC.Set<InventoryPalletVirtual>()
                .AsNoTracking()
                .Include(x => x.InventoryPalletVirtualLine_InventoryPallet)
                .ThenInclude(x => x.BaseInventory.ItemMaster)
                .Include(x => x.Location)
                .Where(x => x.Code == code && x.Location.WhArea.WareHouseId == whid)
                .FirstOrDefault();

            if (data == null)
            {
                MSD.AddModelError("", "托盘码在当前存储地点中无效");
                return null;
            }

            return new InventoryPalletVirtualReturn(data);
        }

        /// <summary>
        /// 保存托盘信息
        /// </summary>
        public void Save(InventoryPalletVirtual data)
        {
            if (!MSD.IsValid)
            {
                return;
            }
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            bool isNew = false;
            // data的ID和Code只能同时为空或同时有值，否则报错
            // 数据有效性验证
            if(data == null)
            {
                // 报此错误，优先排查ID是否为guid格式。
                MSD.AddModelError("", "参数转换错误");
                return;
            }
            if (data.ID == Guid.Empty)  // 新增
            {
                if (!string.IsNullOrEmpty(data.Code))
                {
                    MSD.AddModelError("", "当前为新增操作，参数Code错误");
                    return;
                }
                if (data.SysVersion != null)
                {
                    MSD.AddModelError("", "当前为新增操作，参数SysVersion状态错误");
                    return;
                }
                // 新增时，明细表不准为空
                if (data.InventoryPalletVirtualLine_InventoryPallet == null || data.InventoryPalletVirtualLine_InventoryPallet.Count < 2)
                {
                    MSD.AddModelError("", "当前为新增操作，明细表至少需要2条数据");
                    return;
                }
                isNew = true;
            }
            else    // 修改
            {
                if (string.IsNullOrEmpty(data.Code))
                {
                    MSD.AddModelError("", "当前为修改操作，参数Code缺失");
                    return;
                }
                if (data.SysVersion == null || data.SysVersion <= 0)
                {
                    MSD.AddModelError("", "当前为修改操作，参数SysVersion状态错误");
                    return;
                }

                // 事务版本核对
                var oldData = DC.Set<InventoryPalletVirtual>()
                   .AsNoTracking()
                   .Where(x => x.ID == data.ID && x.Location.WhArea.WareHouseId == whid)
                   .FirstOrDefault();
                if (oldData == null)
                {
                    MSD.AddModelError("", "当前为修改操作，操作的数据不存在");
                    return;
                }
                if (oldData.Code != data.Code)
                {
                    MSD.AddModelError("", "参数错误。ID与Code不匹配。请尝试重新操作");
                    return;
                }
                if (oldData.SysVersion != data.SysVersion)
                {
                    MSD.AddModelError("", "数据已被修改，请重新操作");
                    return;
                }
            }
            List<InventoryPalletVirtualLine> existLines = new List<InventoryPalletVirtualLine>();   // 已存在别的单据中的明细行（需要删除掉。即从原来的托盘中删除，新增到新托盘中）
            List<Guid> existPallets = new List<Guid>();   // 已存在别的单据中的托盘（更新事务版本号）
            // 明细行数据有效性验证
            if (data.InventoryPalletVirtualLine_InventoryPallet != null && data.InventoryPalletVirtualLine_InventoryPallet.Count > 0)
            {
                int i = 1;
                // 校验每行的库位、库存信息的冻结状态必须保持一致
                foreach (var line in data.InventoryPalletVirtualLine_InventoryPallet)
                {
                    line.DocLineNo = i; // 行号
                    BaseInventory inventory = DC.Set<BaseInventory>()
                        .Include(x => x.WhLocation.WhArea)
                       .Where(x => x.ID == line.BaseInventoryId && x.IsAbandoned == false && x.WhLocation.WhArea.WareHouseId == whid)
                       .AsNoTracking()
                       .FirstOrDefault();   // 库存信息
                    if (inventory == null)
                    {
                        MSD.AddModelError("", $"第{i}行，库存信息{line.BaseInventoryId}不存在，请检查");
                        return;
                    }
                    if (inventory.WhLocation.AreaType != Model.WhLocationEnum.Normal)
                    {
                        MSD.AddModelError("", $"第{i}行，库位类型错误。组托操作只能在正常库位进行");
                        return;
                    }
                    if (inventory.WhLocation.WhArea.AreaType != Model.WhAreaEnum.Normal)
                    {
                        MSD.AddModelError("", $"第{i}行，库区类型错误。组托操作只能在正常库区进行");
                        return;
                    }
                    if (inventory.WhLocation.Locked == true)
                    {
                        MSD.AddModelError("", $"库位{inventory.WhLocation.Code}已被盘点锁定，不允许进行组托操作");
                        return;
                    }
                    if (inventory.Qty == 0)
                    {
                        MSD.AddModelError("", $"第{i}行，条码的库存数量为0，不允许进行组托操作");
                        return;
                    }
                    if (i == 1) // 第一条记录，组托单表头的库位和托盘状态赋值
                    {
                        data.Status = inventory.FrozenStatus; // 托盘状态
                        data.LocationId = inventory.WhLocationId; // 库位ID
                    }
                    else    // 后续记录，库位和托盘状态必须一致（正常情况下，前端已经做过校验）
                    {
                        if (inventory.FrozenStatus != data.Status)
                        {
                            MSD.AddModelError("", $"第{i}行，库存信息的冻结状态与首行库存信息的冻结状态不一致，请检查");
                            return;
                        }
                        if (inventory.WhLocationId != data.LocationId)
                        {
                            MSD.AddModelError("", $"第{i}行，库位与首行库位不一致，请检查");
                            return;
                        }
                    }
                    // 查找库存信息是否存在别的组托单中的明细行
                    var existLine = DC.Set<InventoryPalletVirtualLine>()
                        .Where(x => x.ID != line.ID && x.BaseInventoryId == line.BaseInventoryId)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if(existLine != null)
                    {
                        existLines.Add(existLine);
                        if(!existPallets.Exists(x => x == existLine.InventoryPalletId))
                        {
                            existPallets.Add((Guid)existLine.InventoryPalletId);
                        }
                    }
                    i++;
                }
            }
            else
            {
                data.Status = null; // 托盘状态
                data.LocationId = null; // 库位ID
            }

            Entity = data;
            using (var trans = DC.BeginTransaction())
            {
                if (isNew)
                {
                    Entity.Code = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventoryPalletDocNoRule", trans);
                    if (!MSD.IsValid)
                    {
                        return;
                    }
                    // Entity.Code = GenerateCode();   // 生成托盘码
                    Entity.SysVersion = 1;
                    DoAdd();
                }
                else
                {
                    Entity.SysVersion++;  // 事务版本+1
                    DoEdit(true);
                }
                if (!MSD.IsValid)
                {
                    return;
                }
                if(existLines.Count > 0)
                {
                    DC.Set<InventoryPalletVirtualLine>().RemoveRange(existLines); // 删除原来的明细行
                }
                if(existPallets.Count > 0)
                {
                    DC.Set<InventoryPalletVirtual>().Where(x => existPallets.Contains(x.ID)).ExecuteUpdate(x => x.SetProperty(y => y.SysVersion, y => y.SysVersion + 1)); // 更新原来的托盘的事务版本号
                }
                DC.SaveChanges();
                if (MSD.IsValid)
                {
                    foreach (var line in data.InventoryPalletVirtualLine_InventoryPallet)
                    {
                        // 逐条创建库存流水日志
                        if (!this.CreateInvLog(OperationTypeEnum.InventoryPalletVirtualCreate, Entity.Code, line.BaseInventoryId, null, null, null, line.Memo))    // 组托单无目标库存数据、数量
                        {
                            return;
                        }
                    }
                    trans.Commit();
                }
            }
        }
    }

    /// <summary>
    /// 组托单返回信息
    /// </summary>
    public class InventoryPalletVirtualReturn
    {
        public Guid? ID { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public string LocationID { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }

        public int? SysVersion { get; set; }

        public string Memo { get; set; }

        public string CreateTime { get; set; }

        public List<InventoryPalletVirtualLineReturn> Lines { get; set; }

        public InventoryPalletVirtualReturn(InventoryPalletVirtual inventoryPallet, bool initLines = true)
        {
            ID = inventoryPallet?.ID;
            Code = inventoryPallet?.Code;
            Status = inventoryPallet?.Status.GetEnumDisplayName();
            LocationID = inventoryPallet?.LocationId.ToString();
            LocationCode = inventoryPallet?.Location?.Code;
            LocationName = inventoryPallet?.Location?.Name;
            SysVersion = inventoryPallet?.SysVersion;
            Memo = inventoryPallet?.Memo;
            CreateTime = inventoryPallet?.CreateTime?.ToString("yyyy-MM-dd HH:mm:ss");
            Lines = new List<InventoryPalletVirtualLineReturn>();
            if (initLines && inventoryPallet?.InventoryPalletVirtualLine_InventoryPallet != null && inventoryPallet.InventoryPalletVirtualLine_InventoryPallet.Count > 0)
            {
                foreach (var line in inventoryPallet.InventoryPalletVirtualLine_InventoryPallet)
                {
                    var lineReturn = new InventoryPalletVirtualLineReturn(line);
                    Lines.Add(lineReturn);
                }
            }
        }
    }

    /// <summary>
    /// 组托单明细行返回信息
    /// </summary>
    public class InventoryPalletVirtualLineReturn
    {
        public Guid? ID { get; set; }

        public int? DocLineNo { get; set; }

        public string BaseInventoryID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal? Qty { get; set; }

        public string SerialNumber { get; set; }

        public string Memo { get; set; }

        public InventoryPalletVirtualLineReturn(InventoryPalletVirtualLine inventoryPalletVirtualLine)
        {
            ID = inventoryPalletVirtualLine?.ID;
            DocLineNo = inventoryPalletVirtualLine?.DocLineNo;
            BaseInventoryID = inventoryPalletVirtualLine?.BaseInventoryId.ToString();
            ItemCode = inventoryPalletVirtualLine?.BaseInventory?.ItemMaster?.Code;
            ItemName = inventoryPalletVirtualLine?.BaseInventory?.ItemMaster?.Name;
            SPECS = inventoryPalletVirtualLine?.BaseInventory?.ItemMaster?.SPECS;
            Qty = inventoryPalletVirtualLine?.BaseInventory?.Qty;
            SerialNumber = inventoryPalletVirtualLine?.BaseInventory?.SerialNumber;
            Memo = inventoryPalletVirtualLine?.Memo;
        }
    }
}
