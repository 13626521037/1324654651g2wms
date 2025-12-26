using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustApiBatchVM : BaseBatchVM<InventoryAdjust, InventoryAdjustApi_BatchEdit>
    {
        public InventoryAdjustApiBatchVM()
        {
            ListVM = new InventoryAdjustApiListVM();
            LinkedVM = new InventoryAdjustApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryAdjustApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
