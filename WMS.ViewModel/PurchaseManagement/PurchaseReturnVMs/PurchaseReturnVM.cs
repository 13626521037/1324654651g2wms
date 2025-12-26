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
using WMS.ViewModel.PurchaseManagement.PurchaseReturnLineVMs;
using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnVM : BaseCRUDVM<PurchaseReturn>
    {
        
        public List<string> PurchaseManagementPurchaseReturnFTempSelected { get; set; }
        public PurchaseReturnLinePurchaseReturnDetailListVM PurchaseReturnLinePurchaseReturnList { get; set; }

        public PurchaseReturnVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Supplier);
            PurchaseReturnLinePurchaseReturnList = new PurchaseReturnLinePurchaseReturnDetailListVM();
            PurchaseReturnLinePurchaseReturnList.DetailGridPrix = "Entity.PurchaseReturnLine_PurchaseReturn";

        }

        protected override void InitVM()
        {
            
            PurchaseReturnLinePurchaseReturnList.CopyContext(this);

        }

        public override DuplicatedInfo<PurchaseReturn> SetDuplicatedCheck()
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
