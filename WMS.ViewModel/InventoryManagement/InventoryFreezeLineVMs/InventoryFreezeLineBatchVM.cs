
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryFreezeLineVMs
{
    public partial class InventoryFreezeLineBatchVM : BaseBatchVM<InventoryFreezeLine, InventoryFreezeLine_BatchEdit>
    {
        public InventoryFreezeLineBatchVM()
        {
            ListVM = new InventoryFreezeLineListVM();
            LinkedVM = new InventoryFreezeLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryFreezeLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryFreezeLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}