using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs;
using WMS.ViewModel.InventoryManagement.InventoryAdjustLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustVM : BaseCRUDVM<InventoryAdjust>
    {
        
        public List<string> InventoryManagementInventoryAdjustFTempSelected { get; set; }
        public InventoryAdjustLineInvAdjustDetailListVM InventoryAdjustLineInvAdjustList { get; set; }

        public InventoryAdjustVM()
        {
            
            SetInclude(x => x.StockTaking);
            InventoryAdjustLineInvAdjustList = new InventoryAdjustLineInvAdjustDetailListVM();
            InventoryAdjustLineInvAdjustList.DetailGridPrix = "Entity.InventoryAdjustLine_InvAdjust";

        }

        protected override void InitVM()
        {
            
            InventoryAdjustLineInvAdjustList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryAdjust> SetDuplicatedCheck()
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
