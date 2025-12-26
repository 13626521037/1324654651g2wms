using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitApiBatchVM : BaseBatchVM<InventorySplit, InventorySplitApi_BatchEdit>
    {
        public InventorySplitApiBatchVM()
        {
            ListVM = new InventorySplitApiListVM();
            LinkedVM = new InventorySplitApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventorySplitApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
