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
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
using WMS.Model;


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeApiVM : BaseCRUDVM<InventoryUnfreeze>
    {

        public InventoryUnfreezeApiVM()
        {
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
        /// 获取所有解冻原因
        /// </summary>
        /// <returns></returns>
        public List<string> GetReasons()
        {
            var para = DC.Set<BaseSysPara>().Where(x => x.Code == "InventoryUnfreezeReason").FirstOrDefault();
            if (para == null)
            {
                MSD.AddModelError("", "解冻原因未设置，请联系管理员");
                return null;
            }
            return para.Value.Split(',').ToList();
        }

        /// <summary>
        /// 保存解冻单
        /// </summary>
        /// <param name="para"></param>
        public void Save(InventoryFreezeSavePara para)
        {
            // 因业务数据一致，此方法参数共享冻结单的参数。

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
            if (para == null || para.InventoryFreezeLines == null || para.InventoryFreezeLines.Count == 0)
            {
                MSD.AddModelError("", "参数错误");
                return;
            }
            if (string.IsNullOrEmpty(para.Reason))
            {
                MSD.AddModelError("", "解冻原因不能为空");
                return;
            }

            List<InventoryPalletVirtual> pallets = new List<InventoryPalletVirtual>();  // 本单涉及的托盘
            // 行校验
            foreach (var line in para.InventoryFreezeLines)
            {
                if (line.InventoryId == Guid.Empty)
                {
                    MSD.AddModelError("", "库存信息ID不能为空");
                    return;
                }
                // 库存在托盘内：验证库存信息ID、数量、托盘信息ID、托盘版本、托盘中的行数
                if (line.PalletId != null && line.PalletId != Guid.Empty)
                {
                    var pallet = pallets.Find(x => x.ID == line.PalletId);
                    if (pallet == null)
                    {
                        pallet = DC.Set<InventoryPalletVirtual>()
                            .Include(x => x.InventoryPalletVirtualLine_InventoryPallet)
                            .ThenInclude(x => x.BaseInventory)
                            .ThenInclude(x => x.ItemMaster)
                            .AsNoTracking()
                            .Where(x => x.ID == line.PalletId && x.Location.WhArea.WareHouseId == whid)
                            .FirstOrDefault();
                        if (pallet != null)
                        {
                            pallets.Add(pallet);
                        }
                        else    // 托盘ID验证
                        {
                            MSD.AddModelError("", $"托盘信息({line.PalletId})不存在");
                            return;
                        }
                    }
                    if (pallet.SysVersion != line.PalletVersion)    // 托盘事务版本验证
                    {
                        MSD.AddModelError("", "托盘信息已发生变更，请重新操作");
                        return;
                    }
                    var palletLine = pallet.InventoryPalletVirtualLine_InventoryPallet.Find(x => x.BaseInventoryId == line.InventoryId);
                    if (palletLine == null) // 库存信息ID验证
                    {
                        MSD.AddModelError("", $"托盘({line.PalletId})中未找到库存({line.InventoryId})");
                        return;
                    }
                    if (palletLine.BaseInventory.Qty != line.Qty)   // 数量验证
                    {
                        MSD.AddModelError("", $"托盘({pallet.Code})中库存({palletLine.BaseInventory.ItemMaster.Code})数量({palletLine.BaseInventory.Qty})与解冻数量({line.Qty})不一致。请重新操作");
                        return;
                    }
                    if (palletLine.BaseInventory.FrozenStatus == Model.FrozenStatusEnum.Normal)
                    {
                        MSD.AddModelError("", $"库存({palletLine.BaseInventory.ItemMaster.Code})未被冻结，无法解冻");
                        return;
                    }
                    if (palletLine.BaseInventory.IsAbandoned == true)
                    {
                        MSD.AddModelError("", $"库存({palletLine.BaseInventory.ItemMaster.Code})已作废，无法解冻");
                        return;
                    }
                }
                else    // 库存不在托盘内：验证库存信息ID、数量
                {
                    var inventory = DC.Set<BaseInventory>()
                       .AsNoTracking()
                       .Include(x => x.ItemMaster)
                       .Where(x => x.ID == line.InventoryId && x.WhLocation.WhArea.WareHouseId == whid)
                       .FirstOrDefault();
                    // 判断库存信息是否在托盘内
                    var palletLine = DC.Set<InventoryPalletVirtualLine>()
                        .Include(x => x.InventoryPallet)
                        .Where(x => x.BaseInventoryId == line.InventoryId)
                        .AsNoTracking()
                        .FirstOrDefault();
                    if (palletLine != null)
                    {
                        MSD.AddModelError("", $"库存({inventory.ItemMaster.Code})在托盘({palletLine.InventoryPallet.Code})中。但未传托盘信息，参数错误。请重新操作或联系信息部");
                        return;
                    }
                    if (inventory == null)    // 库存信息ID验证
                    {
                        MSD.AddModelError("", $"库存信息({line.InventoryId})不存在");
                        return;
                    }
                    if (inventory.Qty != line.Qty)   // 数量验证
                    {
                        MSD.AddModelError("", $"库存({inventory.ItemMaster.Code})数量({inventory.Qty})与解冻数量({line.Qty})不一致。请重新操作");
                        return;
                    }
                    if (inventory.FrozenStatus == Model.FrozenStatusEnum.Normal)
                    {
                        MSD.AddModelError("", $"库存({inventory.ItemMaster.Code})未被冻结，无法解冻");
                        return;
                    }
                    if (inventory.IsAbandoned == true)
                    {
                        MSD.AddModelError("", $"库存({inventory.ItemMaster.Code})已作废，无法解冻");
                        return;
                    }
                }
            }
            // 托盘内的数量反向验证（到这一步，事务版本已经验证通过。如果下面再报错，说明前端构造的参数有问题。或者托盘的事务版本控制在某些业务中有问题。）
            foreach (var pallet in pallets)
            {
                var palletLineNum = pallet.InventoryPalletVirtualLine_InventoryPallet.Count();  // 托盘中的行数
                var paraLineNum = para.InventoryFreezeLines.Count(x => x.PalletId == pallet.ID);    // 参数中此托盘的行数
                if (palletLineNum != paraLineNum)
                {
                    MSD.AddModelError("", $"参数错误，托盘({pallet.ID})中库存数量与解冻数量不一致。请重新操作");
                    return;
                }
            }

            // 验证全部通过，开始构造单据
            using (var tran = DC.BeginTransaction())
            {
                List<Guid> invs = new List<Guid>();
                InventoryUnfreeze unfreeze = new InventoryUnfreeze();
                unfreeze.Reason = para.Reason;
                unfreeze.Memo = para.Memo;
                unfreeze.InventoryUnfreezeLine_InventoryUnfreeze = new List<InventoryUnfreezeLine>();
                foreach (var line in para.InventoryFreezeLines)
                {
                    InventoryUnfreezeLine unfreezeLine = new InventoryUnfreezeLine();
                    unfreezeLine.BaseInventoryId = line.InventoryId;
                    unfreezeLine.Qty = line.Qty;
                    unfreezeLine.Memo = line.Memo;
                    unfreeze.InventoryUnfreezeLine_InventoryUnfreeze.Add(unfreezeLine);
                    invs.Add(line.InventoryId);
                }
                var vm = Wtm.CreateVM<BaseSequenceDefineVM>();
                unfreeze.DocNo = vm.GetSequence("InventoryUnfreezeDocNoRule", tran);
                if (!MSD.IsValid)
                {
                    return;
                }
                Entity = unfreeze;
                DoAdd();
                if (!MSD.IsValid)
                {
                    return;
                }
                // 将所有库存信息进行解冻
                DC.Set<BaseInventory>().Where(x => invs.Contains(x.ID)).ExecuteUpdate(x => x.SetProperty(y => y.FrozenStatus, Model.FrozenStatusEnum.Normal));
                // 将所有托盘进行解冻
                DC.Set<InventoryPalletVirtual>()
                   .Where(x => pallets.Select(y => y.ID).Contains(x.ID))
                   .ExecuteUpdate(x => x.SetProperty(y => y.Status, Model.FrozenStatusEnum.Normal).SetProperty(y => y.SysVersion, y => y.SysVersion + 1));
                DC.SaveChanges();
                foreach (var line in unfreeze.InventoryUnfreezeLine_InventoryUnfreeze)
                {
                    // 逐条创建库存流水日志
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryUnfreezeCreate, Entity.DocNo, line.BaseInventoryId, null, null, null, line.Memo))   // 冻结、解冻不涉及数量变更，不传数量
                    {
                        return;
                    }
                }
                tran.Commit();
            }
        }
    }
}
