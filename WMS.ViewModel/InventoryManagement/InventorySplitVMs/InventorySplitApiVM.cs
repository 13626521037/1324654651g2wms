using Elsa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Util;
using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;


namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitApiVM : BaseCRUDVM<InventorySplit>
    {
        public InventorySplitSaveReturn SplitBarcode { get; set; }

        public InventorySplitApiVM()
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

        public void Save(InventorySplit data, bool ignoreWhCheck = false, IDbContextTransaction transaction = null)
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
            bool isNew = false;
            // 数据有效性验证
            if (data == null)
            {
                // 报此错误，优先排查ID是否为guid格式。
                MSD.AddModelError("", "参数转换错误");
                return;
            }

            // 获取原库存信息
            var oldInv = DC.Set<BaseInventory>()
                //.Include(x => x.ItemMaster)
                //.Include(x => x.WhLocation)
                .Where(x => x.ID == data.OldInvId && (ignoreWhCheck || x.WhLocation.WhArea.WareHouseId == whid) && x.IsAbandoned == false)
                //.AsNoTracking()
                .FirstOrDefault();
            if (oldInv == null)
            {
                MSD.AddModelError("OldInvId", "库存信息不存在");
                return;
            }
            var location = DC.Set<BaseWhLocation>().Where(x => x.ID == oldInv.WhLocationId).AsNoTracking().FirstOrDefault();
            var item = DC.Set<BaseItemMaster>().Where(x => x.ID == oldInv.ItemMasterId).AsNoTracking().FirstOrDefault();
            if (location.AreaType != WhLocationEnum.Normal)
            {
                MSD.AddModelError("OldInvId", "只允许拆分正常库位的库存");
                return;
            }
            if (oldInv.FrozenStatus == Model.FrozenStatusEnum.Freezed)
            {
                MSD.AddModelError("OldInvId", "库存已冻结，无法进行拆分");
                return;
            }
            if (this.IsInvInPallet(oldInv.ID))
            {
                MSD.AddModelError("OldInvId", "在托盘中的库存无法进行拆分");
                return;
            }
            if (data.SplitQty == oldInv.Qty)
            {
                // 不能报错（下架自动拆分时，跳过）
                SplitBarcode = null;
                return;
            }
            if (oldInv.Qty <= 1)
            {
                MSD.AddModelError("OldInvId", "库存数量不足，无法进行拆分");
                return;
            }
            //if(oldInv.Qty != data.OrigQty)    // 作用不大，不判断了。如果生效，需要修改“下架库存拆分”功能，要给值
            //{
            //    MSD.AddModelError("OrigQty", "库存数量已发生变化，请重试");
            //    return;
            //}
            data.OrigQty = oldInv.Qty;
            if (data.SplitQty < 1)
            {
                MSD.AddModelError("SplitQty", "拆分数量必须大于0");
                return;
            }
            if(data.SplitQty > oldInv.Qty)
            {
                MSD.AddModelError("SplitQty", "拆分数量不能大于库存数量");
                return;
            }
            if (location.Locked == true)
            {
                MSD.AddModelError("OldInvId", "库存信息所在库位已被盘点锁定，无法进行拆分");
                return;
            }
            IDbContextTransaction trans;
            if (transaction == null)
            {
                trans = DC.Database.BeginTransaction();
            }
            else
            {
                trans = transaction;
            }
            try
            {
                // 新建拆分后的库存信息
                var invVm = Wtm.CreateVM<BaseInventoryApiVM>();
                invVm.Entity = new BaseInventory
                {
                    ItemMasterId = item.ID,
                    WhLocationId = location.ID,
                    BatchNumber = oldInv.BatchNumber,
                    SerialNumber = invVm.GetSerialNumber(),
                    Seiban = oldInv.Seiban,
                    SeibanRandom = oldInv.SeibanRandom,
                    Qty = data.SplitQty,
                    IsAbandoned = false,
                    ItemSourceType = oldInv.ItemSourceType,
                    FrozenStatus = Model.FrozenStatusEnum.Normal,
                };
                invVm.DoAdd();
                if (!MSD.IsValid)
                {
                    return;
                }
                // 原库存数量减少
                oldInv.Qty = oldInv.Qty - data.SplitQty;
                //DC.UpdateProperty(oldInv, x => x.Qty);
                DC.SaveChanges();
                // 保存单据
                data.DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventorySplitDocNoRule", trans);    // 获取单据编号
                if (!MSD.IsValid)
                {
                    return;
                }
                data.NewInvId = invVm.Entity.ID;
                Entity = data;
                DoAdd();
                // 更新条码表
                var oldBarCode = this.SetBarCodeQty(item, oldInv.SerialNumber, (decimal)oldInv.Qty, (int)oldInv.ItemSourceType);
                var newBarCode = this.SetBarCodeQty(item, invVm.Entity.SerialNumber, (decimal)data.SplitQty, (int)invVm.Entity.ItemSourceType, docNo: oldBarCode.DocNo);
                SplitBarcode = new InventorySplitSaveReturn
                {
                    OldBarCode = oldBarCode.Code,
                    NewBarCode = newBarCode.Code,
                    OldInvId = oldInv.ID,
                    NewInvId = invVm.Entity.ID,
                    OldQty = (decimal)oldInv.Qty.TrimZero(),
                    NewQty = (decimal)data.SplitQty.TrimZero(),
                };
                if (!MSD.IsValid)
                {
                    return;
                }
                // 创建库存流水
                if (!this.CreateInvLog(OperationTypeEnum.InventorySplitCreate, Entity.DocNo, Entity.OldInvId, Entity.NewInvId, - Entity.SplitQty, Entity.SplitQty, Entity.Memo))
                {
                    return;
                }
                if (transaction == null)
                    trans.Commit();
            }
            catch (Exception ex)
            {
                MSD.AddModelError("", "保存失败，原因：" + ex.Message);
                if (transaction == null)
                    trans.Rollback();
            }
        }
    }
}
