using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeApiBatchVM : BaseBatchVM<InventoryUnfreeze, InventoryUnfreezeApi_BatchEdit>
    {
        public InventoryUnfreezeApiBatchVM()
        {
            ListVM = new InventoryUnfreezeApiListVM();
            LinkedVM = new InventoryUnfreezeApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryUnfreezeApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
