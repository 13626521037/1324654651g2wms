using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseWhLocationVMs;
using WMS.ViewModel.InventoryManagement.InventoryMoveLocationLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationVM : BaseCRUDVM<InventoryMoveLocation>
    {
        
        public List<string> InventoryManagementInventoryMoveLocationFTempSelected { get; set; }
        public InventoryMoveLocationLineInventoryMoveLocationDetailListVM InventoryMoveLocationLineInventoryMoveLocationList { get; set; }

        public InventoryMoveLocationVM()
        {
            
            SetInclude(x => x.InWhLocation);
            InventoryMoveLocationLineInventoryMoveLocationList = new InventoryMoveLocationLineInventoryMoveLocationDetailListVM();
            InventoryMoveLocationLineInventoryMoveLocationList.DetailGridPrix = "Entity.InventoryMoveLocationLine_InventoryMoveLocation";

        }

        protected override void InitVM()
        {
            
            InventoryMoveLocationLineInventoryMoveLocationList.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryMoveLocation> SetDuplicatedCheck()
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
