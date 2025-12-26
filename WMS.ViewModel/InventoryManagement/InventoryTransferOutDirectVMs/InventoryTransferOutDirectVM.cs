using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectDocTypeVMs;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectVM : BaseCRUDVM<InventoryTransferOutDirect>
    {

        public List<string> InventoryManagementInventoryTransferOutDirectFTempSelected { get; set; }
        public InventoryTransferOutDirectLineInventoryTransferOutDirectDetailListVM InventoryTransferOutDirectLineInventoryTransferOutDirectList { get; set; }

        public InventoryTransferOutDirectVM()
        {

            SetInclude(x => x.DocType);
            SetInclude(x => x.TransInOrganization);
            SetInclude(x => x.TransInWh);
            SetInclude(x => x.TransOutWh);
            SetInclude(x => x.TransOutOrganization);
            InventoryTransferOutDirectLineInventoryTransferOutDirectList = new InventoryTransferOutDirectLineInventoryTransferOutDirectDetailListVM();
            InventoryTransferOutDirectLineInventoryTransferOutDirectList.DetailGridPrix = "Entity.InventoryTransferOutDirectLine_InventoryTransferOutDirect";

        }

        protected override void InitVM()
        {

            InventoryTransferOutDirectLineInventoryTransferOutDirectList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryTransferOutDirect> SetDuplicatedCheck()
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
    }
}
