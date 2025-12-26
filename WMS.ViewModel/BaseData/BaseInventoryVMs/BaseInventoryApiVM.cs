using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using Microsoft.EntityFrameworkCore;
using WMS.ViewModel.BaseData.BaseWhLocationVMs;
using WMS.ViewModel.BaseData.BaseItemMasterVMs;
using WMS.Model;
using Elsa;
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs;
using System.IO;
using WMS.Model.InventoryManagement;
using WMS.Util;


namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryApiVM : BaseCRUDVM<BaseInventory>
    {

        public BaseInventoryApiVM()
        {
            SetInclude(x => x.ItemMaster);
            SetInclude(x => x.WhLocation);
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
        /// 根据条码获取库存数据
        /// </summary>
        /// <param name="sn">条码</param>
        /// <returns></returns>
        public BaseInventoryReturn GetDataBySn(string sn, bool includeBarCode = false)
        {
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            BaseInventory data = DC.Set<BaseInventory>()
                .AsNoTracking()
                .Where(x => x.WhLocation.WhArea.WareHouseId == whid && x.IsAbandoned == false)
                .Include(x => x.ItemMaster)
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .FirstOrDefault(x => x.SerialNumber == sn);
            if (data == null)
            {
                MSD.AddModelError("", "条码在当前存储地点中无效");
                return null;
            }
            BaseBarCode barCode = null;
            if (includeBarCode)
            {
                barCode = DC.Set<BaseBarCode>().AsNoTracking().Where(x => x.Sn == data.SerialNumber).FirstOrDefault();
            }

            return new BaseInventoryReturn(data, false, barCode);
        }

        /// <summary>
        /// 创建库存数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="locationCode"></param>
        /// <param name="qty"></param>
        /// <param name="isAbandoned"></param>
        /// <param name="itemSourceType"></param>
        /// <param name="frozenStatus"></param>
        public void Create(string itemCode, string locationCode, decimal qty, string seiban, bool isAbandoned, int itemSourceType, int frozenStatus)
        {
            BaseWhLocation location = DC.Set<BaseWhLocation>()
                .Where(x => x.Code == locationCode)
                .Include(x => x.WhArea.WareHouse.Organization)
                .AsNoTracking()
                .FirstOrDefault();
            if (location == null)
            {
                MSD.AddModelError("", "库位编码不正确");
                return;
            }
            BaseItemMaster item = DC.Set<BaseItemMaster>()
                .Where(x => x.OrganizationId == location.WhArea.WareHouse.OrganizationId && x.Code == itemCode)
                .AsNoTracking()
                .FirstOrDefault();
            if (item == null)
            {
                MSD.AddModelError("", "物料编码不正确");
                return;
            }
            if (itemSourceType != 1 && itemSourceType != 3)
            {
                MSD.AddModelError("", "物料来源类型不正确");
                return;
            }
            if (frozenStatus != 0 && frozenStatus != 1)
            {
                MSD.AddModelError("", "冻结状态不正确");
                return;
            }
            Entity = new BaseInventory
            {
                ItemMasterId = item.ID,
                WhLocationId = location.ID,
                BatchNumber = DateTime.Now.ToString("yyyyMMdd"),
                SerialNumber = GetSerialNumber(),
                Seiban = seiban,
                Qty = qty,
                IsAbandoned = isAbandoned,
                ItemSourceType = (ItemSourceTypeEnum)itemSourceType,
                FrozenStatus = (FrozenStatusEnum)frozenStatus,
            };
            DoAdd();
        }

        /// <summary>
        /// 获取库存或托盘信息通用接口
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type">0：混合（全部），1：库存条码（全部），2：库存条码（不含托盘内的库存条码），3：库存条码（只看托盘内的库存条码），4：托盘码，5：混合（不含托盘内的库存条码），6：混合（只看托盘内的库存条码）</param>
        /// <param name="locationType">库位类型（-1：不限，0：正常库位）</param>
        /// <param name="takingType">盘点类型（-1：不限，0：未盘点锁定， 1：已盘点锁定）</param>
        /// <returns></returns>
        public InvOrPalletReturn GetInvOrPalletData(string code, int type, int locationType, int takingType, bool isFreeze = false)
        {
            if (type < 0 || type > 6)
            {
                MSD.AddModelError("", "类型不正确");
                return null;
            }
            if (locationType != -1 && locationType != 0)
            {
                MSD.AddModelError("", "库位类型不正确");
                return null;
            }
            if (takingType != -1 && takingType != 0 && takingType != 1)
            {
                MSD.AddModelError("", "盘点类型不正确");
                return null;
            }
            // 重启服务时，其它信息正常，whid会为空（暂未找到合适的方法解决重启服务的问题）
            if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录");
                return null;
            }
            Guid whid;
            Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);

            // 判断料品条码
            if (type == 0 || type == 1 || type == 2 || type == 3 || type == 5 || type == 6)
            {
                BaseInventory data = DC.Set<BaseInventory>()
                    .AsNoTracking()
                    .Where(x => x.WhLocation.WhArea.WareHouseId == whid && x.IsAbandoned == false)
                    .Where(x => locationType == -1 || (locationType == 0 && x.WhLocation.AreaType == WhLocationEnum.Normal))
                    .Where(x => x.FrozenStatus == (isFreeze ? FrozenStatusEnum.Freezed : FrozenStatusEnum.Normal))
                    .Include(x => x.ItemMaster)
                    .Include(x => x.WhLocation.WhArea.WareHouse)
                    .FirstOrDefault(x => x.SerialNumber == code);
                if (data != null)
                {
                    int actualType;
                    // 查询条码是否在托盘内
                    InventoryPalletVirtualLine palletLine = DC.Set<InventoryPalletVirtualLine>()
                        .AsNoTracking()
                        .FirstOrDefault(x => x.BaseInventoryId == data.ID);
                    if (palletLine != null)
                    {
                        actualType = 3;
                    }
                    else
                    {
                        actualType = 2;
                    }
                    if ((type == 2 || type == 5) && actualType == 3)  // 请求为：库存条码（不含托盘内的库存条码）或库存条码（只看托盘内的库存条码）时，实际类型与请求类型必须一致
                    {
                        MSD.AddModelError("", "此条码在托盘内");
                        return null;
                    }
                    if ((type == 3 || type == 6) && actualType == 2)  // 请求为：库存条码（只看托盘内的库存条码）或库存条码（只看托盘内的库存条码）时，实际类型与请求类型必须一致
                    {
                        MSD.AddModelError("", "此条码不在托盘内");
                        return null;
                    }
                    return new InvOrPalletReturn() { RequestType = type, ActualType = actualType, Invs = new List<BaseInventoryReturn>() { new BaseInventoryReturn(data, false) } };
                }
            }
            // 判断托盘条码
            if (type == 0 || type == 4 || type == 5 || type == 6)
            {
                InventoryPalletVirtual data = DC.Set<InventoryPalletVirtual>()
                    .AsNoTracking()
                    .Include(x => x.InventoryPalletVirtualLine_InventoryPallet)
                    .ThenInclude(x => x.BaseInventory.ItemMaster)
                    .Include(x => x.InventoryPalletVirtualLine_InventoryPallet)
                    .ThenInclude(x => x.BaseInventory.WhLocation.WhArea.WareHouse)
                    .Include(x => x.Location)
                    .Where(x => x.Code == code && x.Location.WhArea.WareHouseId == whid)
                    .Where(x => locationType == -1 || (locationType == 0 && x.Location.AreaType == WhLocationEnum.Normal))
                    .Where(x => x.Status == (isFreeze ? FrozenStatusEnum.Freezed : FrozenStatusEnum.Normal))
                    .FirstOrDefault();

                if (data != null)
                {
                    //if (data.InventoryPalletVirtualLine_InventoryPallet.Count == 0)
                    //{
                    //    MSD.AddModelError("", "此托盘码为空（无关联库存条码）");
                    //    return null;
                    //}
                    //else
                    //{
                    InvOrPalletReturn result = new InvOrPalletReturn() { RequestType = type, ActualType = 4, Pallet = new InventoryPalletVirtualReturn(data, false), Invs = new List<BaseInventoryReturn>() };
                    foreach (var line in data.InventoryPalletVirtualLine_InventoryPallet)
                    {
                        if (line.BaseInventory.Qty > 0) // 只返回库存大于0的行
                            result.Invs.Add(new BaseInventoryReturn(line.BaseInventory, true));
                    }
                    return result;
                    //}
                }
            }

            if (type == 0 || type == 5 || type == 6)
            {
                MSD.AddModelError("", "此二维码无效");
            }
            else if (type == 1 || type == 2 || type == 3)
            {
                MSD.AddModelError("", "此条码无效");
            }
            else if (type == 4)
            {
                MSD.AddModelError("", "此托盘码无效");
            }
            return null;
        }

        /// <summary>
        /// 获取一个随机的13位序列号
        /// </summary>
        /// <returns></returns>
        public string GetSerialNumber()
        {
            string sn = Common.GetRandom13();
            while (DC.Set<BaseInventory>().Any(x => x.SerialNumber == sn))
            {
                sn = Common.GetRandom13();
            }
            return sn;
        }
    }

    /// <summary>
    /// 获取库存或托盘信息返回实体
    /// </summary>
    public class InvOrPalletReturn
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public int RequestType { get; set; }

        /// <summary>
        /// 实际类型（只返回2,3,4）
        /// </summary>
        public int ActualType { get; set; }

        /// <summary>
        /// 托盘信息
        /// </summary>
        public InventoryPalletVirtualReturn Pallet { get; set; }

        /// <summary>
        /// 库存信息（为保证格式统一，托盘的行信息也会在此）
        /// </summary>
        public List<BaseInventoryReturn> Invs { get; set; }
    }

    /// <summary>
    /// 库存信息返回实体
    /// </summary>
    public class BaseInventoryReturn
    {
        public Guid? ID { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string ItemSPECS { get; set; }

        public string ItemMateriaModel { get; set; }

        public decimal? Qty { get; set; }

        public string FrozenStatus { get; set; }

        public Guid? LocationID { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }

        public Guid? AreaID { get; set; }

        public string AreaCode { get; set; }

        public string AreaName { get; set; }

        public Guid? WhID { get; set; }

        public string WhCode { get; set; }

        public string WhName { get; set; }

        public string SerialNumber { get; set; }

        public string BatchNumber { get; set; }

        public string Seiban { get; set; }

        /// <summary>
        /// 是否扫描托盘（代表是否扫描托盘码，而非库存是否在托盘内）
        /// </summary>
        public bool IsScanPallet { get; set; }

        public BaseBarCode BarCode { get; set; }

        public BaseInventoryReturn(BaseInventory inventory, bool isScanPallet, BaseBarCode barCode = null)
        {
            ID = inventory?.ID;
            ItemID = inventory?.ItemMaster.ID;
            ItemCode = inventory?.ItemMaster.Code;
            ItemName = inventory?.ItemMaster.Name;
            ItemSPECS = inventory?.ItemMaster.SPECS;
            ItemMateriaModel = inventory?.ItemMaster.MateriaModel;
            Qty = inventory?.Qty;
            FrozenStatus = inventory?.FrozenStatus.GetEnumDisplayName();
            LocationID = inventory?.WhLocation.ID;
            LocationCode = inventory?.WhLocation.Code;
            LocationName = inventory?.WhLocation.Name;
            AreaID = inventory?.WhLocation.WhArea.ID;
            AreaCode = inventory?.WhLocation.WhArea.Code;
            AreaName = inventory?.WhLocation.WhArea.Name;
            WhID = inventory?.WhLocation.WhArea.WareHouse.ID;
            WhCode = inventory?.WhLocation.WhArea.WareHouse.Code;
            WhName = inventory?.WhLocation.WhArea.WareHouse.Name;
            SerialNumber = inventory?.SerialNumber;
            BatchNumber = inventory?.BatchNumber;
            Seiban = inventory?.Seiban;
            IsScanPallet = isScanPallet;
            BarCode = barCode;
        }
    }
}
