using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementApiBatchVM : BaseBatchVM<InventoryOtherReceivement, InventoryOtherReceivementApi_BatchEdit>
    {
        public InventoryOtherReceivementApiBatchVM()
        {
            ListVM = new InventoryOtherReceivementApiListVM();
            LinkedVM = new InventoryOtherReceivementApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryOtherReceivementApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
