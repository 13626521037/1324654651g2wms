using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingVMs
{
    public partial class InventoryStockTakingApiBatchVM : BaseBatchVM<InventoryStockTaking, InventoryStockTakingApi_BatchEdit>
    {
        public InventoryStockTakingApiBatchVM()
        {
            ListVM = new InventoryStockTakingApiListVM();
            LinkedVM = new InventoryStockTakingApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTakingApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
