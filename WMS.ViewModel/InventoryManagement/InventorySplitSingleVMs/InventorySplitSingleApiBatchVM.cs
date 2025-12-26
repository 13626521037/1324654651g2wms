using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleApiBatchVM : BaseBatchVM<InventorySplitSingle, InventorySplitSingleApi_BatchEdit>
    {
        public InventorySplitSingleApiBatchVM()
        {
            ListVM = new InventorySplitSingleApiListVM();
            LinkedVM = new InventorySplitSingleApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventorySplitSingleApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
