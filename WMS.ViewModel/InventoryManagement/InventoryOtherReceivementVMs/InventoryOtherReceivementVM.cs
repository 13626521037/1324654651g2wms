using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryOtherReceivementLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementVM : BaseCRUDVM<InventoryOtherReceivement>
    {
        
        public List<string> InventoryManagementInventoryOtherReceivementFTempSelected { get; set; }
        public InventoryOtherReceivementLineInventoryOtherReceivementDetailListVM InventoryOtherReceivementLineInventoryOtherReceivementList { get; set; }

        public InventoryOtherReceivementVM()
        {
            
            InventoryOtherReceivementLineInventoryOtherReceivementList = new InventoryOtherReceivementLineInventoryOtherReceivementDetailListVM();
            InventoryOtherReceivementLineInventoryOtherReceivementList.DetailGridPrix = "Entity.InventoryOtherReceivementLine_InventoryOtherReceivement";

        }

        protected override void InitVM()
        {
            
            InventoryOtherReceivementLineInventoryOtherReceivementList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryOtherReceivement> SetDuplicatedCheck()
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
