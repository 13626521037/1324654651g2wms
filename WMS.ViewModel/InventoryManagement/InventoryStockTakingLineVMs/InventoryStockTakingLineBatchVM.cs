
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

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingLineVMs
{
    public partial class InventoryStockTakingLineBatchVM : BaseBatchVM<InventoryStockTakingLine, InventoryStockTakingLine_BatchEdit>
    {
        public InventoryStockTakingLineBatchVM()
        {
            ListVM = new InventoryStockTakingLineListVM();
            LinkedVM = new InventoryStockTakingLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTakingLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryStockTakingLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}