using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseCustomerVMs;
using WMS.ViewModel.SalesManagement.SalesShipLineVMs;
using WMS.Model.SalesManagement;
using WMS.Model;
namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipVM : BaseCRUDVM<SalesShip>
    {
        
        public List<string> SalesManagementSalesShipFTempSelected { get; set; }
        public SalesShipLineShipDetailListVM SalesShipLineShipList { get; set; }

        public SalesShipVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Customer);
            SalesShipLineShipList = new SalesShipLineShipDetailListVM();
            SalesShipLineShipList.DetailGridPrix = "Entity.SalesShipLine_Ship";

        }

        public override void DoDelete()
        {
            if (Entity.Status != SalesShipStatusEnum.InWh)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }

        protected override void InitVM()
        {
            
            SalesShipLineShipList.CopyContext(this);

        }

        public override DuplicatedInfo<SalesShip> SetDuplicatedCheck()
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
