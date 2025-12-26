using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationApiBatchVM : BaseBatchVM<InventoryMoveLocation, InventoryMoveLocationApi_BatchEdit>
    {
        public InventoryMoveLocationApiBatchVM()
        {
            ListVM = new InventoryMoveLocationApiListVM();
            LinkedVM = new InventoryMoveLocationApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryMoveLocationApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
