using Esprima.Ast;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Namotion.Reflection;
using NetBox.Extensions;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WalkingTec.Mvvm.Core;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using WMS.ViewModel.BaseData.BaseDocInventoryRelationVMs;
using WMS.ViewModel.BaseData.BaseInventoryLogVMs;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.ViewModel.BaseData.BaseSeibanCustomerRelationVMs;
using WMS.ViewModel.InventoryManagement.InventorySplitVMs;

namespace WMS.ViewModel
{
    public static class BaseCRUDVMExtensions
    {
        /// <summary>
        /// 用来存放额外的属性值（需手动释放，否则忘记的话，此对象会越来越大，不用此方法）
        /// </summary>
        //private static Dictionary<(Type, object), object> ExtensionPropertyValues = new Dictionary<(Type, object), object>();

        /// <summary>
        /// 根据来源系统ID获取ID
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static Guid? GetIdBySourceSystemId<T>(this BaseCRUDVM<T> vm, string sourceSystemId) where T : BaseExternal, new()
        {
            T entity = vm.DC.Set<T>().AsNoTracking().Where(x => x.SourceSystemId == sourceSystemId && x.IsValid).FirstOrDefault();
            return entity?.ID;
        }

        /// <summary>
        /// 根据来源系统ID获取ID
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static Guid? GetIdBySourceSystemId<T, X>(this BaseCRUDVM<T> vm, string sourceSystemId) where X : BaseExternal, new() where T : TopBasePoco, new()
        {
            X entity = vm.DC.Set<X>().AsNoTracking().Where(x => x.SourceSystemId == sourceSystemId && x.IsValid).FirstOrDefault();
            return entity?.ID;
        }

        /// <summary>
        /// 根据来源系统ID获取ID（从缓存中取，适合批量操作时调用。为了避免与数据库做过多的交互影响效率）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="sourceSystemId"></param>
        /// <returns></returns>
        public static Guid? GetIdBySourceSystemIdFromCache<T>(this BaseCRUDVM<T> vm, string sourceSystemId) where T : BaseExternal, new()
        {
            if (vm.Entity.AllDatas == null)
            {
                vm.Entity.AllDatas = vm.DC.Set<T>().AsNoTracking().Where(x => x.IsValid).ToList();
            }
            if (vm.Entity.AllDatas != null)
            {
                try
                {
                    List<T> datas = (List<T>)vm.Entity.AllDatas;
                    return datas.Find(x => x.SourceSystemId == sourceSystemId)?.ID;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据ID获取来源系统ID
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static string GetSourceSystemIdById<T>(this BaseCRUDVM<T> vm, Guid id) where T : BaseExternal, new()
        {
            T entity = vm.DC.Set<T>().AsNoTracking().Where(x => x.ID == id && x.IsValid).FirstOrDefault();
            return entity?.SourceSystemId;
        }

        /// <summary>
        /// 根据来源系统ID获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="sourceSystemId">来源系统ID</param>
        /// <returns></returns>
        public static T GetEntityBySourceSystemId<T>(this BaseCRUDVM<T> vm, string sourceSystemId) where T : BaseExternal, new()
        {
            T entity = vm.DC.Set<T>().AsNoTracking().Where(x => x.SourceSystemId == sourceSystemId && x.IsValid).FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// 根据来源系统ID获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="sourceSystemId">来源系统ID</param>
        /// <returns></returns>
        public static X GetEntityBySourceSystemId<T, X>(this BaseCRUDVM<T> vm, string sourceSystemId) where X : BaseExternal, new() where T : TopBasePoco, new()
        {
            X entity = vm.DC.Set<X>().AsNoTracking().Where(x => x.SourceSystemId == sourceSystemId && x.IsValid).FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static T GetEntityById<T>(this BaseCRUDVM<T> vm, Guid id) where T : BaseExternal, new()
        {
            T entity = vm.DC.Set<T>().AsNoTracking().Where(x => x.ID == id && x.IsValid).FirstOrDefault();

            return entity;
        }

        /// <summary>
        /// 根据Code获取ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Guid? GetIdByCode<T>(this BaseCRUDVM<T> vm, string code) where T : BaseExternal, new()
        {
            T entity = vm.DC.Set<T>().AsNoTracking().Where(x => x.Code.ToString() == code && x.IsValid).FirstOrDefault();
            return entity?.ID;
        }

        /// <summary>
        /// 根据Code获取ID（从缓存中取，适合批量操作时调用。为了避免与数据库做过多的交互影响效率）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Guid? GetIdByCodeFromCache<T>(this BaseCRUDVM<T> vm, string code) where T : BaseExternal, new()
        {
            if (vm.Entity.AllDatas == null)
            {
                vm.Entity.AllDatas = vm.DC.Set<T>().AsNoTracking().Where(x => x.IsValid).ToList();
            }
            if (vm.Entity.AllDatas != null)
            {
                try
                {
                    List<T> datas = (List<T>)vm.Entity.AllDatas;
                    return datas.Find(x => x.Code.ToString() == code)?.ID;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 将来源系统ID属性值转化为本系统ID属性
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="vm"></param>
        /// <param name="entity">实体</param>
        /// <param name="attr">实体属性（如：OrgId-本系统ID, Org-本系统实体, SyncOrg-来源系统ID。3个属性必须满足这个格式（以Org为核心，增加前缀和后缀）。此实例参数传entity.Org即可）</param>
        /// <param name="attrName">属性名称（如：OrgId-本系统ID, Org-本系统实体, SyncOrg-来源系统ID。3个属性必须满足这个格式（以Org为核心，增加前缀和后缀）。Org即可）</param>
        /// <returns></returns>
        public static string SSIdAttrToId<T1, T2, T3>(this BaseCRUDVM<T1> vm, T2 entity, T3 attr, string attrName) where T1 : BasePoco, new() where T3 : BaseExternal, new()
        {
            try
            {
                // ERP系统ID
                var syncProperty = entity.GetType().GetProperty("Sync" + attrName);
                if (syncProperty == null)
                {
                    return $"数据转换出错，未找到Sync{attrName}属性";
                }
                var syncValue = (string)syncProperty.GetValue(entity);

                // 获取实体
                var thisEntity = vm.DC.Set<T3>().AsNoTracking().Where(x => x.SourceSystemId == syncValue && x.IsValid).FirstOrDefault();
                if (thisEntity == null)
                {
                    return $"数据转换出错，未找到{syncValue}实体({typeof(T3).GetPropertyDisplayName()})";
                }

                // 本系统ID
                var thisProperty = entity.GetType().GetProperty(attrName + "Id");
                if (thisProperty == null)
                {
                    return $"数据转换出错，未找到{attrName}Id属性";
                }
                thisProperty.SetValue(entity, thisEntity.ID);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 创建一条库存流水记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="operationType">业务操作类型</param>
        /// <param name="DocNo">业务单据号</param>
        /// <param name="sourceInventoryId">来源库存信息ID</param>
        /// <param name="targetInventoryId">目标库存信息ID</param>
        /// <param name="sourceDiffQty">来源数量变化。正数增加，负数减少</param>
        /// <param name="targetDiffQty">目标数量变化。正数增加，负数减少</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        public static bool CreateInvLog<T>(this BaseCRUDVM<T> vm, OperationTypeEnum operationType, string DocNo, Guid? sourceInventoryId, Guid? targetInventoryId, decimal? sourceDiffQty, decimal? targetDiffQty, string memo = "") where T : TopBasePoco, new()
        {
            if (!vm.MSD.IsValid)
            {
                return false;
            }
            if (string.IsNullOrEmpty(DocNo))
            {
                vm.MSD.AddModelError("", "库存流水记录创建失败，单据号不能为空");
                return false;
            }
            if (sourceInventoryId == null && targetInventoryId == null)
            {
                vm.MSD.AddModelError("", "库存流水记录创建失败，来源库存信息和目标库存信息不能同时为空");
                return false;
            }
            BaseInventoryLog log = new BaseInventoryLog();
            log.OperationType = operationType;
            log.DocNo = DocNo;
            log.SourceInventoryId = sourceInventoryId;
            log.TargetInventoryId = targetInventoryId;
            log.SourceDiffQty = sourceDiffQty;
            log.TargetDiffQty = targetDiffQty;
            log.Memo = memo;
            BaseInventoryLogVM logVM = vm.Wtm.CreateVM<BaseInventoryLogVM>();
            logVM.Entity = log;
            logVM.DoAdd();
            if (vm.MSD.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 创建一条单据库存关联关系
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="docType">单据类型</param>
        /// <param name="invId">库存信息ID</param>
        /// <param name="businessId">业务实体ID</param>
        /// <param name="Qty">数量</param>
        /// <param name="memo">备注</param>
        /// <returns></returns>
        public static bool CreateInvRelation<T>(this BaseCRUDVM<T> vm, DocTypeEnum docType, Guid invId, Guid businessId, Guid? businessLineId, decimal? Qty, string memo = "") where T : TopBasePoco, new()
        {
            if (!vm.MSD.IsValid)
            {
                return false;
            }
            if (invId == Guid.Empty)
            {
                vm.MSD.AddModelError("", "单据库存关联关系创建失败，库存信息ID不能为空");
                return false;
            }
            if (businessId == Guid.Empty)
            {
                vm.MSD.AddModelError("", "单据库存关联关系创建失败，业务实体ID不能为空");
                return false;
            }
            if (Qty == 0)
            {
                vm.MSD.AddModelError("", "单据库存关联关系创建失败，数量不能为0");
                return false;
            }
            BaseDocInventoryRelation relation = new BaseDocInventoryRelation();
            relation.DocType = docType;
            relation.InventoryId = invId;
            relation.BusinessId = businessId;
            relation.BusinessLineId = businessLineId;
            relation.Qty = Qty;
            relation.Memo = memo;
            BaseDocInventoryRelationVM relationVM = vm.Wtm.CreateVM<BaseDocInventoryRelationVM>();
            relationVM.Entity = relation;
            relationVM.DoAdd();
            if (vm.MSD.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断库存是否在托盘中
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public static bool IsInvInPallet<T>(this BaseCRUDVM<T> vm, Guid? inventoryId) where T : TopBasePoco, new()
        {
            if (inventoryId == null)
            {
                return false;
            }
            InventoryPalletVirtualLine palletLine = vm.DC.Set<InventoryPalletVirtualLine>().AsNoTracking().Where(x => x.BaseInventoryId == inventoryId).FirstOrDefault();
            if (palletLine == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 复制一条库存信息（主要用于将临时库位的库存复制到正式库位）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="sourceInventory"></param>
        /// <returns></returns>
        public static BaseInventory CopyInventory<T>(this BaseCRUDVM<T> vm, BaseInventory sourceInventory, Guid LocationId) where T : TopBasePoco, new()
        {
            if (sourceInventory == null)
            {
                return null;
            }
            BaseInventory targetInventory = new BaseInventory
            {
                ID = Guid.NewGuid(),
                ItemMasterId = sourceInventory.ItemMasterId,
                WhLocationId = LocationId,
                BatchNumber = sourceInventory.BatchNumber,
                SerialNumber = sourceInventory.SerialNumber,
                Seiban = sourceInventory.Seiban,
                SeibanRandom = sourceInventory.SeibanRandom,
                Qty = sourceInventory.Qty,
                IsAbandoned = false,
                ItemSourceType = sourceInventory.ItemSourceType,
                FrozenStatus = sourceInventory.FrozenStatus,
                CreateTime = DateTime.Now,
                CreateBy = vm.LoginUserInfo.ITCode,
            };

            return targetInventory;
        }

        /// <summary>
        /// 获取指定物料的库存信息（有效可使用，越早越靠前）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="itemMasterId"></param>
        /// <returns></returns>
        public static List<BaseInventory> GetItemInventory<T>(this BaseCRUDVM<T> vm, Guid? itemId, Guid? WhId) where T : TopBasePoco, new()
        {
            if (itemId == null || WhId == null || itemId == Guid.Empty || WhId == Guid.Empty)
            {
                return new List<BaseInventory>();
            }
            List<BaseInventory> invs = vm.DC.Set<BaseInventory>()
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .AsNoTracking()
                .Where(x => x.WhLocation.WhArea.WareHouseId == WhId
                    && x.WhLocation.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.WareHouse.IsEffective == EffectiveEnum.Effective
                    && x.IsAbandoned == false
                    && x.Qty > 0
                    && x.FrozenStatus == FrozenStatusEnum.Normal
                    && x.WhLocation.AreaType == WhLocationEnum.Normal   // 只看正常库位
                    && x.ItemMasterId == itemId)
                .OrderBy(x => x.CreateTime).ToList();
            return invs;
        }

        /// <summary>
        /// 获取指定物料的库存信息(考虑番号)（有效可使用，越早越靠前）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="itemMasterId"></param>
        /// <returns></returns>
        public static List<BaseInventory> GetItemInventory<T>(this BaseCRUDVM<T> vm, Guid? itemId, Guid? WhId, string seiban) where T : TopBasePoco, new()
        {
            if (string.IsNullOrEmpty(seiban))
            {
                return GetItemInventory(vm, itemId, WhId);
            }
            if (itemId == null || WhId == null || itemId == Guid.Empty || WhId == Guid.Empty)
            {
                return new List<BaseInventory>();
            }
            // 取番号相同的库存信息
            List<BaseInventory> invs1 = vm.DC.Set<BaseInventory>()
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .AsNoTracking()
                .Where(x => x.WhLocation.WhArea.WareHouseId == WhId
                    && x.WhLocation.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.WareHouse.IsEffective == EffectiveEnum.Effective
                    && x.IsAbandoned == false
                    && x.Seiban == seiban
                    && x.Qty > 0
                    && x.FrozenStatus == FrozenStatusEnum.Normal
                    && x.WhLocation.AreaType == WhLocationEnum.Normal   // 只看正常库位
                    && x.ItemMasterId == itemId)
                .OrderBy(x => x.CreateTime).ToList();
            // 取番号不同、客户相同的库存信息
            List<BaseInventory> invs2;
            var seibanVm = vm.Wtm.CreateVM<BaseSeibanCustomerRelationVM>();
            var relation = seibanVm.GetSeibanInfo(seiban);
            if (relation != null)
            {
                invs2 = vm.DC.Set<BaseInventory>()
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .AsNoTracking()
                .Where(x => x.WhLocation.WhArea.WareHouseId == WhId
                    && x.WhLocation.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.WareHouse.IsEffective == EffectiveEnum.Effective
                    && x.IsAbandoned == false
                    && x.Seiban != seiban
                    && vm.DC.Set<BaseSeibanCustomerRelation>().Where(y => y.Code == x.Seiban).FirstOrDefault() != null 
                    && relation.CustomerId == vm.DC.Set<BaseSeibanCustomerRelation>().Where(y => y.Code == x.Seiban).FirstOrDefault().CustomerId    // 匹配客户
                    && x.Qty > 0
                    && x.FrozenStatus == FrozenStatusEnum.Normal
                    && x.WhLocation.AreaType == WhLocationEnum.Normal   // 只看正常库位
                    && x.ItemMasterId == itemId)
                .OrderBy(x => x.CreateTime).ToList();
            }
            else
            {
                invs2 = new List<BaseInventory>();
                vm.MSD.Clear(); // 获取番号客户对照关系时，可能会有错误提示信息，需要清除
            }
            // 取番号不同、客户不同的库存信息
            List<BaseInventory> invs3 = vm.DC.Set<BaseInventory>()
                .Include(x => x.WhLocation.WhArea.WareHouse)
                .AsNoTracking()
                .Where(x => x.WhLocation.WhArea.WareHouseId == WhId
                    && x.WhLocation.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.IsEffective == EffectiveEnum.Effective
                    && x.WhLocation.WhArea.WareHouse.IsEffective == EffectiveEnum.Effective
                    && x.IsAbandoned == false
                    && x.Seiban != seiban
                    && !invs2.Contains(x)   // 排除已经取过的库存信息
                    && x.Qty > 0
                    && x.FrozenStatus == FrozenStatusEnum.Normal
                    && x.WhLocation.AreaType == WhLocationEnum.Normal   // 只看正常库位
                    && x.ItemMasterId == itemId)
                .OrderBy(x => x.CreateTime).ToList();
            return invs1.Union(invs2).Union(invs3).ToList();
        }

        /// <summary>
        /// 判断序列号是否为全新的序列号（如果仅存在已失效的库存信息也算使用过）
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static bool IsSNNeverUsed<T>(this BaseCRUDVM<T> vm, string serialNumber) where T : TopBasePoco, new()
        {
            if (vm.DC.Set<BaseInventory>().Any(x => x.SerialNumber == serialNumber))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断序列号是否未使用（如果仅存在已失效的库存信息算未使用过）。物料收入“待收库位”时需要调用此方法
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static bool IsSNNotUsed<T>(this BaseCRUDVM<T> vm, string serialNumber) where T : TopBasePoco, new()
        {
            if (vm.DC.Set<BaseInventory>().Any(x => x.SerialNumber == serialNumber && x.IsAbandoned == false))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 修改条码表中条码的数量（条码存在时，修改；条码不存在时，新增）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="vm"></param>
        /// <param name="serialNumber"></param>
        /// <param name="qty"></param>
        public static BaseBarCode SetBarCodeQty<T>(this BaseCRUDVM<T> vm, BaseItemMaster item, string serialNumber, decimal qty, int type = 3, string docNo = "") where T : TopBasePoco, new()
        {
            if (string.IsNullOrEmpty(serialNumber) || item == null)
            {
                vm.MSD.AddModelError("", "条码表操作失败，条码或物料编码不能为空");
                return null;
            }
            BaseBarCode entity = vm.DC.Set<BaseBarCode>().Include(x => x.Item).Where(x => x.Sn == serialNumber.Trim()).FirstOrDefault();
            if (entity == null)
            {
                entity = new BaseBarCode
                {
                    ID = Guid.NewGuid(),
                    ItemId = item.ID,
                    Code = $"{type}@{item.Code}@{qty.TrimZero()}@{serialNumber.Trim()}",
                    Sn = serialNumber.Trim(),
                    Qty = qty,
                    DocNo = docNo,
                    CreateTime = DateTime.Now,
                    CreateBy = vm.LoginUserInfo.ITCode,
                };
                vm.DC.Set<BaseBarCode>().Add(entity);
            }
            else
            {
                if (entity.Item.Code != item.Code)
                {
                    vm.MSD.AddModelError("", "条码表操作失败，已存在的条码与物料编码不匹配");
                    return null;
                }
                entity.Code = $"{entity.Code.Substring(0, 1)}@{item.Code}@{qty.TrimZero()}@{serialNumber.Trim()}";
                entity.Qty = qty;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = vm.LoginUserInfo.ITCode;
                vm.DC.Set<BaseBarCode>().Update(entity);
            }
            return entity;
        }

        /// <summary>
        /// 下架库存拆分
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="PickType"></typeparam>
        /// <param name="vm"></param>
        /// <param name="pickParas"></param>
        /// <returns></returns>
        public static List<InventorySplitSaveReturn> InvSplit<T, PickType>(this BaseCRUDVM<T> vm, List<PickType> pickParas, string memo, IDbContextTransaction trans) where T : TopBasePoco, new() where PickType : BasePickPara
        {
            List<InventorySplitSaveReturn> ret = new List<InventorySplitSaveReturn>();
            InventorySplitApiVM splitApiVM = vm.Wtm.CreateVM<InventorySplitApiVM>();
            foreach (var para in pickParas)
            {
                splitApiVM.SplitBarcode = null;
                splitApiVM.Save(new InventorySplit
                {
                    OldInvId = para.InvID,
                    SplitQty = para.OffQty,
                    Memo = memo,
                }, transaction: trans);
                if (!vm.MSD.IsValid)
                {
                    return null;
                }
                if (splitApiVM.SplitBarcode != null)
                    ret.Add(splitApiVM.SplitBarcode);
            }

            return ret;
        }
    }
}
