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
using WMS.ViewModel.InventoryManagement.InventoryTransferOutManualLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualVM : BaseCRUDVM<InventoryTransferOutManual>
    {
        
        public List<string> InventoryManagementInventoryTransferOutManualFTempSelected { get; set; }
        public InventoryTransferOutManualLineInventoryTransferOutManualDetailListVM InventoryTransferOutManualLineInventoryTransferOutManualList { get; set; }

        public InventoryTransferOutManualVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.TransInOrganization);
            SetInclude(x => x.TransInWh);
            SetInclude(x => x.TransOutOrganization);
            SetInclude(x => x.TransOutWh);
            InventoryTransferOutManualLineInventoryTransferOutManualList = new InventoryTransferOutManualLineInventoryTransferOutManualDetailListVM();
            InventoryTransferOutManualLineInventoryTransferOutManualList.DetailGridPrix = "Entity.InventoryTransferOutManualLine_InventoryTransferOutManual";

        }

        protected override void InitVM()
        {
            
            InventoryTransferOutManualLineInventoryTransferOutManualList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryTransferOutManual> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(x => x.DocNo));
            return rv;

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
