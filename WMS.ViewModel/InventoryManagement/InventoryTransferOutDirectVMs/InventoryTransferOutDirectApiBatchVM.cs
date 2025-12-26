using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectVMs
{
    public partial class InventoryTransferOutDirectApiBatchVM : BaseBatchVM<InventoryTransferOutDirect, InventoryTransferOutDirectApi_BatchEdit>
    {
        public InventoryTransferOutDirectApiBatchVM()
        {
            ListVM = new InventoryTransferOutDirectApiListVM();
            LinkedVM = new InventoryTransferOutDirectApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutDirectApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
