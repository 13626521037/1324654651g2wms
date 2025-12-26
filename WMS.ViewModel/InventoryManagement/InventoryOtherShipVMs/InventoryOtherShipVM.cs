using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryOtherShipDocTypeVMs;
using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseDepartmentVMs;
using WMS.ViewModel.BaseData.BaseOperatorVMs;
using WMS.ViewModel.BaseData.BaseWareHouseVMs;
using WMS.ViewModel.InventoryManagement.InventoryOtherShipLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipVM : BaseCRUDVM<InventoryOtherShip>
    {
        
        public List<string> InventoryManagementInventoryOtherShipFTempSelected { get; set; }
        public InventoryOtherShipLineInventoryOtherShipDetailListVM InventoryOtherShipLineInventoryOtherShipList { get; set; }

        public InventoryOtherShipVM()
        {
            
            SetInclude(x => x.DocType);
            SetInclude(x => x.BenefitOrganization);
            SetInclude(x => x.BenefitDepartment);
            SetInclude(x => x.BenefitPerson);
            SetInclude(x => x.Wh);
            SetInclude(x => x.OwnerOrganization);
            InventoryOtherShipLineInventoryOtherShipList = new InventoryOtherShipLineInventoryOtherShipDetailListVM();
            InventoryOtherShipLineInventoryOtherShipList.DetailGridPrix = "Entity.InventoryOtherShipLine_InventoryOtherShip";

        }

        protected override void InitVM()
        {
            
            InventoryOtherShipLineInventoryOtherShipList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryOtherShip> SetDuplicatedCheck()
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
