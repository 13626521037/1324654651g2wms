using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualApiBatchVM : BaseBatchVM<InventoryPalletVirtual, InventoryPalletVirtualApi_BatchEdit>
    {
        public InventoryPalletVirtualApiBatchVM()
        {
            ListVM = new InventoryPalletVirtualApiListVM();
            LinkedVM = new InventoryPalletVirtualApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryPalletVirtualApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
