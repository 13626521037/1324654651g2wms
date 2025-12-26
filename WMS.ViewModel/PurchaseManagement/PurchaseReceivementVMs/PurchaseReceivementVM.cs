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
using WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs;
using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementVM : BaseCRUDVM<PurchaseReceivement>
    {
        
        public List<string> PurchaseManagementPurchaseReceivementFTempSelected { get; set; }
        public PurchaseReceivementLinePurchaseReceivementDetailListVM PurchaseReceivementLinePurchaseReceivementList { get; set; }

        public PurchaseReceivementVM()
        {
            
            SetInclude(x => x.Organization);
            SetInclude(x => x.Supplier);
            PurchaseReceivementLinePurchaseReceivementList = new PurchaseReceivementLinePurchaseReceivementDetailListVM();
            PurchaseReceivementLinePurchaseReceivementList.DetailGridPrix = "Entity.PurchaseReceivementLine_PurchaseReceivement";

        }

        protected override void InitVM()
        {
            
            PurchaseReceivementLinePurchaseReceivementList.CopyContext(this);

        }

        public override DuplicatedInfo<PurchaseReceivement> SetDuplicatedCheck()
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

        public override void DoDelete()
        {
            if (Entity.Status != PurchaseReceivementStatusEnum.NotReceive)
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
