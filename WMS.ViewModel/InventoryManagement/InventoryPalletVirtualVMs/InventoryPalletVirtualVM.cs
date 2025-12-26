using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.BaseData.BaseWhLocationVMs;
using WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualVM : BaseCRUDVM<InventoryPalletVirtual>
    {
        
        public List<string> InventoryManagementInventoryPalletVirtualFTempSelected { get; set; }
        public InventoryPalletVirtualLineInventoryPalletDetailListVM InventoryPalletVirtualLineInventoryPalletList { get; set; }
        public InventoryPalletVirtualLineInventoryPalletDetailListVM1 InventoryPalletVirtualLineInventoryPalletList1 { get; set; }
        public InventoryPalletVirtualLineInventoryPalletDetailListVM2 InventoryPalletVirtualLineInventoryPalletList2 { get; set; }

        public InventoryPalletVirtualVM()
        {
            
            SetInclude(x => x.Location.WhArea.WareHouse);
            InventoryPalletVirtualLineInventoryPalletList = new InventoryPalletVirtualLineInventoryPalletDetailListVM();
            InventoryPalletVirtualLineInventoryPalletList.DetailGridPrix = "Entity.InventoryPalletVirtualLine_InventoryPallet";
            InventoryPalletVirtualLineInventoryPalletList1 = new InventoryPalletVirtualLineInventoryPalletDetailListVM1();
            InventoryPalletVirtualLineInventoryPalletList1.DetailGridPrix = "Entity.InventoryPalletVirtualLine_InventoryPallet";
            InventoryPalletVirtualLineInventoryPalletList2 = new InventoryPalletVirtualLineInventoryPalletDetailListVM2();
            InventoryPalletVirtualLineInventoryPalletList2.DetailGridPrix = "Entity.InventoryPalletVirtualLine_InventoryPallet";

        }

        protected override void InitVM()
        {
            
            InventoryPalletVirtualLineInventoryPalletList.CopyContext(this);
            InventoryPalletVirtualLineInventoryPalletList1.CopyContext(this);
            InventoryPalletVirtualLineInventoryPalletList2.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryPalletVirtual> SetDuplicatedCheck()
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
