
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLocationsVMs
{
    public partial class InventoryStockTakingLocationsBatchVM : BaseBatchVM<InventoryStockTakingLocations, InventoryStockTakingLocations_BatchEdit>
    {
        public InventoryStockTakingLocationsBatchVM()
        {
            ListVM = new InventoryStockTakingLocationsListVM();
            LinkedVM = new InventoryStockTakingLocations_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTakingLocations_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryStockTakingLocationsBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}