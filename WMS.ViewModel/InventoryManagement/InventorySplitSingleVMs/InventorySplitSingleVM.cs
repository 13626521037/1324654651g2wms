using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseInventoryVMs;
using WMS.ViewModel.InventoryManagement.InventorySplitSingleLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
using Elsa;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WMS.ViewModel.BaseData.BaseSequenceDefineVMs;
namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleVM : BaseCRUDVM<InventorySplitSingle>
    {

        public List<string> InventoryManagementInventorySplitSingleFTempSelected { get; set; }
        public InventorySplitSingleLineSplitSingleDetailListVM InventorySplitSingleLineSplitSingleList { get; set; }

        public InventorySplitSingleVM()
        {

            SetInclude(x => x.OriginalInv);
            InventorySplitSingleLineSplitSingleList = new InventorySplitSingleLineSplitSingleDetailListVM();
            InventorySplitSingleLineSplitSingleList.DetailGridPrix = "Entity.InventorySplitSingleLine_SplitSingle";

        }

        protected override void InitVM()
        {

            InventorySplitSingleLineSplitSingleList.CopyContext(this);

        }

        public override DuplicatedInfo<InventorySplitSingle> SetDuplicatedCheck()
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
        /// 保存拆零单据
        /// </summary>
        /// <param name="originalInvId">拆前库存信息ID</param>
        /// <param name="ignoreWhCheck">是否忽略当前登录存储地点检测</param>
        /// <param name="memo">备注信息</param>
        /// <param name="singleQty">每份拆零数量</param>
        /// <returns></returns>
        public List<BaseInventory> Save(Guid originalInvId, bool ignoreWhCheck = false, string memo = "", decimal singleQty = 1)
        {
            if (!MSD.IsValid)
            {
                return null;
            }
            Guid whid = Guid.Empty;
            if (!ignoreWhCheck)
            {
                if (!LoginUserInfo.Attributes.HasKey("WarehouseId") || LoginUserInfo.Attributes["WarehouseId"] == null)
                {
                    MSD.AddModelError("", "登录信息已过期，请重新登录");
                    return null;
                }
                Guid.TryParse(LoginUserInfo.Attributes["WarehouseId"].ToString(), out whid);
            }
            if(originalInvId == Guid.Empty)
            {
                MSD.AddModelError("", "原库存ID参数错误");
                return null;
            }

            var originalInv = DC.Set<BaseInventory>()
                //.Include(x => x.ItemMaster)
                .Include(x => x.WhLocation)
                .Where(x => x.ID == originalInvId && (ignoreWhCheck || x.WhLocation.WhArea.WareHouseId == whid) && x.IsAbandoned == false)
                .FirstOrDefault();
            if (originalInv == null)
            {
                MSD.AddModelError("", "库存信息不存在");
                return null;
            }
            if (originalInv.WhLocation.AreaType != WhLocationEnum.Normal)
            {
                MSD.AddModelError("", "只允许拆零正常库位的库存");
                return null;
            }
            if (originalInv.FrozenStatus == Model.FrozenStatusEnum.Freezed)
            {
                MSD.AddModelError("", "库存已冻结，无法进行拆零");
                return null;
            }
            if (this.IsInvInPallet(originalInv.ID))
            {
                MSD.AddModelError("", "在托盘中的库存无法进行拆零");
                return null;
            }
            if (originalInv.Qty < 2)
            {
                MSD.AddModelError("", "库存数量不足，无法进行拆零（数量至少为2才可以进行拆零）");
                return null;
            }
            if (singleQty >= originalInv.Qty)
            {
                MSD.AddModelError("", "拆零数量必须小于库存数量");
                return null;
            }
            if (originalInv.Qty != (int)originalInv.Qty)
            {
                MSD.AddModelError("", "库存拆零时，数量必须为整数");
                return null;
            }
            if (originalInv.WhLocation.Locked == true)
            {
                MSD.AddModelError("OldInvId", "库存信息所在库位已被盘点锁定，无法进行拆零");
                return null;
            }
            var itemMaster = DC.Set<BaseItemMaster>().Where(x => x.ID == originalInv.ItemMasterId).FirstOrDefault();
            if (itemMaster == null)
            {
                MSD.AddModelError("", "系统错误:未找到原库存的物料信息");   // 不会出现的情况
                return null;
            }
            using (var trans = DC.BeginTransaction())
            {
                try
                {
                    // 新建拆零后的库存信息
                    decimal originalQty = (decimal)originalInv.Qty;
                    var invVm = Wtm.CreateVM<BaseInventoryApiVM>();
                    List<BaseInventory> newInvs = new List<BaseInventory>();
                    while (originalInv.Qty > 0) // 全部拆完（原条码失效）
                    {
                        decimal newQty = originalInv.Qty >= singleQty ? singleQty : (decimal)originalInv.Qty;
                        originalInv.Qty -= newQty;
                        if (originalInv.Qty < 0)
                        {
                            MSD.AddModelError("", "系统错误:库存拆零逻辑错误");  // 不会出现的情况
                            return null;
                        }
                        invVm.Entity = new BaseInventory
                        {
                            ItemMaster = originalInv.ItemMaster,
                            ItemMasterId = originalInv.ItemMasterId,
                            WhLocation = originalInv.WhLocation,
                            WhLocationId = originalInv.WhLocationId,
                            BatchNumber = originalInv.BatchNumber,
                            SerialNumber = invVm.GetSerialNumber(),
                            Seiban = originalInv.Seiban,
                            SeibanRandom = originalInv.SeibanRandom,
                            Qty = newQty,
                            IsAbandoned = false,
                            ItemSourceType = originalInv.ItemSourceType,
                            FrozenStatus = Model.FrozenStatusEnum.Normal,
                        };
                        invVm.DoAdd();
                        if (!MSD.IsValid)
                        {
                            return null;
                        }
                        newInvs.Add(invVm.Entity);
                    }
                    // 更新原库存信息
                    originalInv.IsAbandoned = true;
                    originalInv.UpdateBy = LoginUserInfo.ITCode;
                    originalInv.UpdateTime = DateTime.Now;
                    DC.SaveChanges();
                    // 保存单据
                    InventorySplitSingle doc = new InventorySplitSingle();
                    doc.DocNo = Wtm.CreateVM<BaseSequenceDefineVM>().GetSequence("InventorySplitSingleDocNoRule", trans);    // 获取单据编号
                    if (!MSD.IsValid)
                    {
                        return null;
                    }
                    doc.OriginalInvId = originalInvId;
                    doc.OriginalQty = originalQty;
                    doc.Memo = memo;
                    doc.InventorySplitSingleLine_SplitSingle = new List<InventorySplitSingleLine>();
                    foreach (var inv in newInvs)
                    {
                        InventorySplitSingleLine line = new InventorySplitSingleLine();
                        line.NewInvId = inv.ID;
                        line.Qty = inv.Qty;
                        doc.InventorySplitSingleLine_SplitSingle.Add(line);
                    }
                    Entity = doc;
                    DoAdd();
                    // 更新条码表
                    var oldBarCode = this.SetBarCodeQty(itemMaster, originalInv.SerialNumber, (decimal)originalInv.Qty, (int)originalInv.ItemSourceType);
                    foreach (var inv in newInvs)
                    {
                        var newBarCode = this.SetBarCodeQty(itemMaster, inv.SerialNumber, (decimal)inv.Qty, (int)inv.ItemSourceType, docNo: oldBarCode.DocNo);
                    }
                    if (!MSD.IsValid)
                    {
                        return null;
                    }
                    // 创建库存流水
                    foreach (var inv in newInvs)
                    {
                        if (!this.CreateInvLog(OperationTypeEnum.InventorySplitSingleCreate, Entity.DocNo, Entity.OriginalInvId, inv.ID, -inv.Qty, inv.Qty, Entity.Memo))
                        {
                            return null;
                        }
                    }
                    trans.Commit();
                    return newInvs;
                }
                catch (Exception ex)
                {
                    MSD.AddModelError("", "保存失败，原因：" + ex.Message);
                    trans.Rollback();
                    return null;
                }
            }
        }
    }
}
