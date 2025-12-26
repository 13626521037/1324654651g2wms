
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutManualLineVMs
{
    public partial class InventoryTransferOutManualLineBatchVM : BaseBatchVM<InventoryTransferOutManualLine, InventoryTransferOutManualLine_BatchEdit>
    {
        public InventoryTransferOutManualLineBatchVM()
        {
            ListVM = new InventoryTransferOutManualLineListVM();
            LinkedVM = new InventoryTransferOutManualLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutManualLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferOutManualLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}