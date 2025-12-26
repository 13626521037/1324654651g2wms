using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.InventoryManagement.InventoryTransferInLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
using NPOI.SS.Formula.Functions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WMS.Util;
using WMS.Model.BaseData;
namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInVM : BaseCRUDVM<InventoryTransferIn>
    {

        public List<string> InventoryManagementInventoryTransferInFTempSelected { get; set; }
        public InventoryTransferInLineInventoryTransferInDetailListVM InventoryTransferInLineInventoryTransferInList { get; set; }

        public InventoryTransferInVM()
        {

            SetInclude(x => x.TransInOrganization);
            SetInclude(x => x.TransInWh);
            InventoryTransferInLineInventoryTransferInList = new InventoryTransferInLineInventoryTransferInDetailListVM();
            InventoryTransferInLineInventoryTransferInList.DetailGridPrix = "Entity.InventoryTransferInLine_InventoryTransferIn";

        }

        protected override void InitVM()
        {

            InventoryTransferInLineInventoryTransferInList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryTransferIn> SetDuplicatedCheck()
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

        public override void DoDelete()
        {
            using (var tran = DC.BeginTransaction())
            {
                if (Entity == null || Entity.ID == Guid.Empty)
                {
                    MSD.AddModelError("", "当前记录不存在，请刷新后重试");
                    return;
                }
                if (Entity.Status == InventoryTransferInStatusEnum.AllInWh || Entity.Status == InventoryTransferInStatusEnum.PartInWh)
                {
                    MSD.AddModelError("", "单据已入库，无法删除");
                    return;
                }
                base.DoDelete();
                if (!MSD.IsValid)
                {
                    return;
                }

                // 查询单据库存关联关系
                var invRelations = DC.Set<BaseDocInventoryRelation>()
                    .Where(x => x.BusinessId == Entity.ID && x.DocType == DocTypeEnum.InventoryTransferIn).ToList();

                foreach (var relation in invRelations)
                {
                    var inventory = DC.Set<BaseInventory>().Where(y => y.ID == relation.InventoryId).FirstOrDefault();
                    if (inventory == null || inventory.IsAbandoned == true)
                    {
                        // 删除单据库存关联关系
                        DC.DeleteEntity(relation);
                        continue;
                    }
                    // 创建库存流水
                    if (!this.CreateInvLog(OperationTypeEnum.InventoryTransferInDelete, Entity.DocNo, relation.InventoryId, null, -inventory.Qty, null))
                    {
                        return;
                    }
                    // 待收库位的库存信息清零
                    inventory.Qty = 0;
                    inventory.IsAbandoned = true;
                    inventory.UpdateBy = LoginUserInfo.ITCode;
                    inventory.UpdateTime = DateTime.Now;
                    // 删除单据库存关联关系
                    DC.DeleteEntity(relation);
                }
                DC.SaveChanges();

                // 删除U9单据
                string u9Url = Wtm.ConfigInfo.AppSettings["U9Url"];
                string entCode = Wtm.ConfigInfo.AppSettings["EntCode"];
                U9ApiHelper apiHelper = new U9ApiHelper(u9Url, entCode, "0300", LoginUserInfo.Name);    // 这个接口的组织参数不重要，随便传一下
                U9Return u9Return = apiHelper.DeleteTransferIn(Entity.ErpID);
                if (!u9Return.Success)
                {
                    MSD.AddModelError("", u9Return.Msg);
                    return;
                }
                tran.Commit();
            }
        }
    }
}
