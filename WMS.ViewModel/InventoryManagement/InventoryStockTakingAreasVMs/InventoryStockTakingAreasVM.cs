using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingAreasVMs
{
    public partial class InventoryStockTakingAreasVM : BaseCRUDVM<InventoryStockTakingAreas>
    {
        
        public List<string> InventoryManagementInventoryStockTakingAreasFTempSelected { get; set; }

        public InventoryStockTakingAreasVM()
        {
            
        }

        protected override void InitVM()
        {
            
        }

        public override DuplicatedInfo<InventoryStockTakingAreas> SetDuplicatedCheck()
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
