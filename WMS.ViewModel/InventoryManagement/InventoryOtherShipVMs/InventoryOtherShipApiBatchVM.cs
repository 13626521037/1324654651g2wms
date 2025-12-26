using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherShipVMs
{
    public partial class InventoryOtherShipApiBatchVM : BaseBatchVM<InventoryOtherShip, InventoryOtherShipApi_BatchEdit>
    {
        public InventoryOtherShipApiBatchVM()
        {
            ListVM = new InventoryOtherShipApiListVM();
            LinkedVM = new InventoryOtherShipApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherShipApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
