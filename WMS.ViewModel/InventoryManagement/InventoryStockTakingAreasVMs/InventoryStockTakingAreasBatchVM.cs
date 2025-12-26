
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

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingAreasVMs
{
    public partial class InventoryStockTakingAreasBatchVM : BaseBatchVM<InventoryStockTakingAreas, InventoryStockTakingAreas_BatchEdit>
    {
        public InventoryStockTakingAreasBatchVM()
        {
            ListVM = new InventoryStockTakingAreasListVM();
            LinkedVM = new InventoryStockTakingAreas_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTakingAreas_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryStockTakingAreasBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}