
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

namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeLineVMs
{
    public partial class InventoryUnfreezeLineBatchVM : BaseBatchVM<InventoryUnfreezeLine, InventoryUnfreezeLine_BatchEdit>
    {
        public InventoryUnfreezeLineBatchVM()
        {
            ListVM = new InventoryUnfreezeLineListVM();
            LinkedVM = new InventoryUnfreezeLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryUnfreezeLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryUnfreezeLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}