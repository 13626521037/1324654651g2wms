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
using WMS.Model;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using Elsa.Models;


namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationApiVM : BaseCRUDVM<InventoryMoveLocation>
    {

        public InventoryMoveLocationApiVM()
        {
            SetInclude(x => x.InWhLocation);
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

        public void Save(InventoryMoveLocationSavePara data, bool ignoreWhCheck = false)
        {
            if (!MSD.IsValid)
            {
                return;
            }
            Guid whid = Guid.Empty;
            if (!ignoreWhCheck)
            {
                if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return;
                }
                Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            }
            if (whid == Guid.Empty)
            {
                MSD.AddModelError("", "登录信息已过期，请重新登录2");
                return;
            }

            // 数据有效性验证
            if (data == null)
            {
                // 报此错误，优先排查ID是否为guid格式。
                MSD.AddModelError("", "参数错误");
                return;
            }
            if (data.Lines == null || data.Lines.Count == 0)
            {
                MSD.AddModelError("", "移库单行参数不能为空");
                return;
            }

            #region 获取调入库位，并判断合法性

            var moveInLoc = DC.Set<BaseWhLocation>().AsNoTracking().Include(x => x.WhArea).Where(x => x.ID == data.MoveInLocationId).FirstOrDefault();
            if (moveInLoc == null)
            {
                MSD.AddModelError("", "入库库位不存在");
                return;
            }
            if (moveInLoc.WhArea.WareHouseId != whid)
            {
                MSD.AddModelError("", "入库库位不属于当前登录仓库");
                return;
            }
            if (moveInLoc.IsEffective == EffectiveEnum.Ineffective)
            {
                MSD.AddModelError("", "入库库位已失效");
                return;
            }
            if (moveInLoc.Locked == true)
            {
                MSD.AddModelError("", "入库库位已被盘点锁定");
                return;
            }
            if (moveInLoc.AreaType != WhLocationEnum.Normal)
            {
                MSD.AddModelError("", "只允许移入正常库位");
                return;
            }

            #endregion

            List<BaseInventory> invs = new List<BaseInventory>();

            #region 获取行上的实体，并判断合法性

            foreach (var line in data.Lines)
            {
                var inv = DC.Set<BaseInventory>().Include(x => x.WhLocation.WhArea).Include(x => x.ItemMaster).Where(x => x.ID == line.InventoryId).FirstOrDefault();
                if (inv == null)
                {
                    MSD.AddModelError("", "库存信息不存在");
                    return;
                }
                if (inv.IsAbandoned == true)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}已作废");
                    return;
                }
                if (inv.WhLocation.WhArea.WareHouseId != whid)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}不属于当前登录仓库");
                    return;
                }
                if (inv.WhLocation.AreaType != WhLocationEnum.Normal)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}所在库位不属于正常库位");
                    return;
                }
                if (inv.WhLocation.Locked == true)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}所在库位{inv.WhLocation.Code}已被盘点锁定");
                }
                if (inv.FrozenStatus == FrozenStatusEnum.Freezed)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}已冻结");
                    return;
                }
                if (this.IsInvInPallet(inv.ID))
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}在托盘中，无法进行移库");
                    return;
                }
                if (inv.Qty <= 0)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}数量为{inv.Qty}");
                }
                if (inv.Qty != line.Qty)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}数量已发生变化，请重试");
                    return;
                }
                if (inv.WhLocationId == moveInLoc.ID)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}已在入库库位{moveInLoc.Code}，无需再移库");
                    return;
                }
                if (inv.WhLocation.IsEffective == EffectiveEnum.Ineffective)
                {
                    MSD.AddModelError("", $"库存{inv.SerialNumber}所在库位已失效");
                    return;
                }
                invs.Add(inv);
            }

            #endregion

            #region 创建单据

            using (var trans = DC.BeginTransaction())
            {
                try
                {
                    Entity = new InventoryMoveLocation();
                    Entity.InWhLocationId = moveInLoc.ID;
                    Entity.Memo = data.Memo;
                    Entity.InventoryMoveLocationLine_InventoryMoveLocation = new List<InventoryMoveLocationLine>();
                    Entity.DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventoryMoveLocationDocNoRule", trans);    // 获取单据编号
                    if (!MSD.IsValid)
                    {
                        return;
                    }
                    foreach (var inv in invs)
                    {
                        Entity.InventoryMoveLocationLine_InventoryMoveLocation.Add(new InventoryMoveLocationLine()
                        {
                            BaseInventoryId = inv.ID,
                            OutWhLocationId = inv.WhLocationId,
                            InWhLocationId = moveInLoc.ID,
                            Qty = inv.Qty,
                        });
                        // 新建移库后的库存信息
                        var invVm = Wtm.CreateVM<BaseInventoryApiVM>();
                        invVm.Entity = this.CopyInventory(inv, moveInLoc.ID);

                        // 原库存数量减少
                        inv.Qty = 0;
                        inv.IsAbandoned = true;
                        DC.SaveChanges();

                        invVm.DoAdd();
                        if (!MSD.IsValid)
                        {
                            return;
                        }
                        
                        
                        // 更新条码表（理论上讲无需更新。因为条码的数量等信息均未发生变化）
                        this.SetBarCodeQty(inv.ItemMaster, inv.SerialNumber, (decimal)invVm.Entity.Qty, (int)inv.ItemSourceType);
                        if (!MSD.IsValid)
                        {
                            return;
                        }
                        // 创建库存流水
                        if (!this.CreateInvLog(OperationTypeEnum.InventoryMoveLocation, Entity.DocNo, inv.ID, invVm.Entity.ID, -invVm.Entity.Qty, invVm.Entity.Qty, data.Memo))
                        {
                            return;
                        }
                    }
                    DC.SaveChanges();
                    DoAdd();    // 创建单据
                    if (MSD.IsValid)
                        trans.Commit();
                }
                catch (Exception ex)
                {
                    MSD.AddModelError("", "保存失败，原因：" + ex.Message);
                    trans.Rollback();
                }
            }

            #endregion
        }
    }
}
