using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeApiBatchVM : BaseBatchVM<InventoryFreeze, InventoryFreezeApi_BatchEdit>
    {
        public InventoryFreezeApiBatchVM()
        {
            ListVM = new InventoryFreezeApiListVM();
            LinkedVM = new InventoryFreezeApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryFreezeApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
