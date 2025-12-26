using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferInVMs
{
    public partial class InventoryTransferInApiBatchVM : BaseBatchVM<InventoryTransferIn, InventoryTransferInApi_BatchEdit>
    {
        public InventoryTransferInApiBatchVM()
        {
            ListVM = new InventoryTransferInApiListVM();
            LinkedVM = new InventoryTransferInApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferInApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
