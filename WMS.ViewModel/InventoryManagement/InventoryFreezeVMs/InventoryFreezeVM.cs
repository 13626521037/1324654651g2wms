using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryFreezeLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeVM : BaseCRUDVM<InventoryFreeze>
    {
        
        public List<string> InventoryManagementInventoryFreezeFTempSelected { get; set; }
        public InventoryFreezeLineInventoryFreezeDetailListVM InventoryFreezeLineInventoryFreezeList { get; set; }
        public InventoryFreezeLineInventoryFreezeDetailListVM1 InventoryFreezeLineInventoryFreezeList1 { get; set; }
        public InventoryFreezeLineInventoryFreezeDetailListVM2 InventoryFreezeLineInventoryFreezeList2 { get; set; }

        public InventoryFreezeVM()
        {
            
            InventoryFreezeLineInventoryFreezeList = new InventoryFreezeLineInventoryFreezeDetailListVM();
            InventoryFreezeLineInventoryFreezeList.DetailGridPrix = "Entity.InventoryFreezeLine_InventoryFreeze";
            InventoryFreezeLineInventoryFreezeList1 = new InventoryFreezeLineInventoryFreezeDetailListVM1();
            InventoryFreezeLineInventoryFreezeList1.DetailGridPrix = "Entity.InventoryFreezeLine_InventoryFreeze";
            InventoryFreezeLineInventoryFreezeList2 = new InventoryFreezeLineInventoryFreezeDetailListVM2();
            InventoryFreezeLineInventoryFreezeList2.DetailGridPrix = "Entity.InventoryFreezeLine_InventoryFreeze";

        }

        protected override void InitVM()
        {
            
            InventoryFreezeLineInventoryFreezeList.CopyContext(this);
            InventoryFreezeLineInventoryFreezeList1.CopyContext(this);
            InventoryFreezeLineInventoryFreezeList2.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryFreeze> SetDuplicatedCheck()
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
