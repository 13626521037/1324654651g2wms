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
using Microsoft.EntityFrameworkCore;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.Model;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectApiVM : BaseCRUDVM<InventoryAdjustDirect>
    {

        public InventoryAdjustDirectApiVM()
        {
            SetInclude(x => x.OldInv);
            SetInclude(x => x.NewInv);
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
        /// 保存库存调整单
        /// </summary>
        /// <param name="para"></param>
        public void Save(InventoryAdjustDirect para)
        {
            // 参数有效性验证
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
            if (para == null || para.NewInv == null || para.NewInv.ItemMaster == null || para.NewInv.Qty == null)
            {
                MSD.AddModelError("", "参数错误");
                return;
            }
            if (para.NewInv.Qty < 0)
            {
                MSD.AddModelError("", "调整数量不能为负数");
                return;
            }
            if (para.NewInv.WhLocationId == null)
            {
                MSD.AddModelError("", "请选择库位");
                return;
            }
            if (string.IsNullOrEmpty(para.NewInv.ItemMaster.Code))
            {
                MSD.AddModelError("", "请输入料号");
                return;
            }
            if (string.IsNullOrEmpty(para.NewInv.BatchNumber))
            {
                MSD.AddModelError("", "请输入批号");
                return;
            }
            BaseWhLocation location = DC.Set<BaseWhLocation>()
                .Include(x => x.WhArea.WareHouse)
                .Where(x => x.ID == para.NewInv.WhLocationId)
                .AsNoTracking()
                .FirstOrDefault();
            if (location == null)
            {
                MSD.AddModelError("", "库位不存在");
                return;
            }
            if (location.Locked == true)
            {
                MSD.AddModelError("", "库位已被盘点锁定，请先解锁");
                return;
            }
            if (location.IsEffective == Model.EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "库位已被禁用，请先启用");
                return;
            }
            BaseItemMaster item = DC.Set<BaseItemMaster>()
                .Where(x => x.Code == para.NewInv.ItemMaster.Code && x.OrganizationId == location.WhArea.WareHouse.OrganizationId)
                .AsNoTracking()
                .FirstOrDefault();
            if (item == null)
            {
                MSD.AddModelError("", "料号在当前库位所在的组织中不存在");
                return;
            }
            if (item.IsEffective == Model.EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "料号已被禁用");
                return;
            }
            using (var tran = DC.Database.BeginTransaction())
            {
                try
                {
                    decimal oldQty = 0;  // 原库存信息的数量
                    // 如果有原库存信息，将原库存信息数量清零，并作废
                    BaseInventory oldInv = null;  // 原库存信息
                    if (para.OldInvId != null)
                    {
                        oldInv = DC.Set<BaseInventory>().Where(x => x.ID == para.OldInvId).FirstOrDefault();  // 要修改，要跟踪
                        if (oldInv == null)
                        {
                            MSD.AddModelError("", "原库存信息不存在");
                            return;
                        }
                        if (oldInv.IsAbandoned == true)
                        {
                            MSD.AddModelError("", "原库存信息已作废，无法进行修改");
                            return;
                        }
                        if (oldInv.FrozenStatus == Model.FrozenStatusEnum.Freezed)
                        {
                            MSD.AddModelError("", "原库存信息已冻结，无法进行修改");
                            return;
                        }
                        oldQty = (decimal)oldInv.Qty;
                        // 在新库存信息选择的库位中，不允许出现未作废但序列号相同的库存信息
                        if (oldInv.WhLocationId != para.NewInv.WhLocationId)    // 不同库位操作，原库存信息数量清零，并作废
                        {
                            BaseInventory existInv = DC.Set<BaseInventory>()
                               .Where(x => x.WhLocationId == para.NewInv.WhLocationId && x.IsAbandoned == false && x.SerialNumber == oldInv.SerialNumber)
                               .AsNoTracking()
                               .FirstOrDefault();
                            if (existInv != null)
                            {
                                MSD.AddModelError("", "新库存信息已存在，请勿重复添加");
                                return;
                            }
                        }
                        if (oldInv.ItemMasterId != item.ID)
                        {
                            MSD.AddModelError("", "库存手动调整不允许修改料号");
                            return;
                        }
                        oldInv.Qty = 0;
                        oldInv.IsAbandoned = true;
                        oldInv.UpdateBy = LoginUserInfo.ITCode;
                        oldInv.UpdateTime = DateTime.Now;
                        DC.UpdateEntity(oldInv);
                        DC.SaveChanges();
                    }
                    // 新增库存信息
                    var invVm = Wtm.CreateVM<BaseInventoryApiVM>();
                    BaseInventory newInv = new BaseInventory
                    {
                        ItemMasterId = item.ID,
                        WhLocationId = para.NewInv.WhLocationId,
                        Qty = para.NewInv.Qty,
                        BatchNumber = para.NewInv.BatchNumber,
                        Seiban = para.NewInv.Seiban,
                        SeibanRandom = para.NewInv.SeibanRandom,
                        SerialNumber = oldInv == null ? invVm.GetSerialNumber() : oldInv.SerialNumber,  // 原库存信息存在时，序列号不变，否则生成新的序列号
                        CreateBy = LoginUserInfo.ITCode,
                        CreateTime = DateTime.Now,
                        IsAbandoned = false,
                        ItemSourceType = Model.ItemSourceTypeEnum.Make,
                        FrozenStatus = Model.FrozenStatusEnum.Normal,
                    };
                    invVm.Entity = newInv;
                    invVm.DoAdd();
                    if (!MSD.IsValid)
                    {
                        return;
                    }
                    // 新增库存调整单
                    Entity = new InventoryAdjustDirect
                    {
                        OldInvId = para.OldInvId,
                        NewInvId = newInv.ID,
                        DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventoryAdjustDirectDocNoRule", tran),    // 获取单据编号
                        DiffQty = para.NewInv.Qty - oldQty,
                        Memo = para.Memo,
                    };
                    DoAdd();
                    // 更新条码表
                    this.SetBarCodeQty(item, newInv.SerialNumber, (decimal)newInv.Qty, (int)newInv.ItemSourceType);
                    if (!MSD.IsValid)
                    {
                        return;
                    }
                    // 记录库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryAdjustDirectCreate, Entity.DocNo, Entity.OldInvId, Entity.NewInvId, Entity.OldInvId == null ? null : Entity.DiffQty - newInv.Qty, newInv.Qty, Entity.Memo))
                    {
                        return;
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MSD.AddModelError("", "保存失败，原因：" + ex.Message);
                    return;
                }
            }
        }
    }
}
