using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualVMs
{
    public partial class InventoryTransferOutManualApiBatchVM : BaseBatchVM<InventoryTransferOutManual, InventoryTransferOutManualApi_BatchEdit>
    {
        public InventoryTransferOutManualApiBatchVM()
        {
            ListVM = new InventoryTransferOutManualApiListVM();
            LinkedVM = new InventoryTransferOutManualApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutManualApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
