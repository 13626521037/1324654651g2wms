
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

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingErpDiffLineVMs
{
    public partial class InventoryStockTakingErpDiffLineBatchVM : BaseBatchVM<InventoryStockTakingErpDiffLine, InventoryStockTakingErpDiffLine_BatchEdit>
    {
        public InventoryStockTakingErpDiffLineBatchVM()
        {
            ListVM = new InventoryStockTakingErpDiffLineListVM();
            LinkedVM = new InventoryStockTakingErpDiffLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryStockTakingErpDiffLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryStockTakingErpDiffLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}