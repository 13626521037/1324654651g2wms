
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

namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualLineVMs
{
    public partial class InventoryPalletVirtualLineBatchVM : BaseBatchVM<InventoryPalletVirtualLine, InventoryPalletVirtualLine_BatchEdit>
    {
        public InventoryPalletVirtualLineBatchVM()
        {
            ListVM = new InventoryPalletVirtualLineListVM();
            LinkedVM = new InventoryPalletVirtualLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryPalletVirtualLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryPalletVirtualLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}