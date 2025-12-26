using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.PurchaseManagement;
using WMS.Model;
namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs
{
    public partial class PurchaseReceivementLineVM : BaseCRUDVM<PurchaseReceivementLine>
    {
        
        public List<string> PurchaseManagementPurchaseReceivementLineFTempSelected { get; set; }

        public PurchaseReceivementLineVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<PurchaseReceivementLine> SetDuplicatedCheck()
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
