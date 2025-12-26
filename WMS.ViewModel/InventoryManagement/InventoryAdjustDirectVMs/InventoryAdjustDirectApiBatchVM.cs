using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectApiBatchVM : BaseBatchVM<InventoryAdjustDirect, InventoryAdjustDirectApi_BatchEdit>
    {
        public InventoryAdjustDirectApiBatchVM()
        {
            ListVM = new InventoryAdjustDirectApiListVM();
            LinkedVM = new InventoryAdjustDirectApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryAdjustDirectApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
