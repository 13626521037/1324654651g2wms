using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseOrganizationVMs;
using WMS.ViewModel.BaseData.BaseSupplierVMs;
using WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnLineVMs;
using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnVM : BaseCRUDVM<PurchaseOutsourcingReturn>
    {
        
        public List<string> PurchaseManagementPurchaseOutsourcingReturnFTempSelected { get; set; }
        public PurchaseOutsourcingReturnLineOutsourcingReturnDetailListVM PurchaseOutsourcingReturnLineOutsourcingReturnList { get; set; }

        public PurchaseOutsourcingReturnVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Supplier);
            PurchaseOutsourcingReturnLineOutsourcingReturnList = new PurchaseOutsourcingReturnLineOutsourcingReturnDetailListVM();
            PurchaseOutsourcingReturnLineOutsourcingReturnList.DetailGridPrix = "Entity.PurchaseOutsourcingReturnLine_OutsourcingReturn";

        }

        protected override void InitVM()
        {
            
            PurchaseOutsourcingReturnLineOutsourcingReturnList.CopyContext(this);

        }

        public override DuplicatedInfo<PurchaseOutsourcingReturn> SetDuplicatedCheck()
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

        public override void DoDelete()
        {
            if (Entity.Status != PurchaseOutsourcingReturnStatusEnum.NotReceive)
            {
                MSD.AddModelError("", $"当前单据状态“{Entity.Status.GetEnumDisplayName()}”不允许删除");
                return;
            }
            base.DoDelete();
        }

        public override async Task DoDeleteAsync()
        {
            await base.DoDeleteAsync();

        }
    }
}
