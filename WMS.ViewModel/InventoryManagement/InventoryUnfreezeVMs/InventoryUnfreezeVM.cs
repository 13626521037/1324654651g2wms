using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;

using WMS.ViewModel.InventoryManagement.InventoryUnfreezeLineVMs;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeVM : BaseCRUDVM<InventoryUnfreeze>
    {
        
        public List<string> InventoryManagementInventoryUnfreezeFTempSelected { get; set; }
        public InventoryUnfreezeLineInventoryUnfreezeDetailListVM InventoryUnfreezeLineInventoryUnfreezeList { get; set; }
        public InventoryUnfreezeLineInventoryUnfreezeDetailListVM1 InventoryUnfreezeLineInventoryUnfreezeList1 { get; set; }
        public InventoryUnfreezeLineInventoryUnfreezeDetailListVM2 InventoryUnfreezeLineInventoryUnfreezeList2 { get; set; }

        public InventoryUnfreezeVM()
        {
            
            InventoryUnfreezeLineInventoryUnfreezeList = new InventoryUnfreezeLineInventoryUnfreezeDetailListVM();
            InventoryUnfreezeLineInventoryUnfreezeList.DetailGridPrix = "Entity.InventoryUnfreezeLine_InventoryUnfreeze";
            InventoryUnfreezeLineInventoryUnfreezeList1 = new InventoryUnfreezeLineInventoryUnfreezeDetailListVM1();
            InventoryUnfreezeLineInventoryUnfreezeList1.DetailGridPrix = "Entity.InventoryUnfreezeLine_InventoryUnfreeze";
            InventoryUnfreezeLineInventoryUnfreezeList2 = new InventoryUnfreezeLineInventoryUnfreezeDetailListVM2();
            InventoryUnfreezeLineInventoryUnfreezeList2.DetailGridPrix = "Entity.InventoryUnfreezeLine_InventoryUnfreeze";

        }

        protected override void InitVM()
        {
            
            InventoryUnfreezeLineInventoryUnfreezeList.CopyContext(this);
            InventoryUnfreezeLineInventoryUnfreezeList1.CopyContext(this);
            InventoryUnfreezeLineInventoryUnfreezeList2.CopyContext(this);

        }

        public override DuplicatedInfo<InventoryUnfreeze> SetDuplicatedCheck()
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
