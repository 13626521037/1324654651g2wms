
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferInLineVMs
{
    public partial class InventoryTransferInLineBatchVM : BaseBatchVM<InventoryTransferInLine, InventoryTransferInLine_BatchEdit>
    {
        public InventoryTransferInLineBatchVM()
        {
            ListVM = new InventoryTransferInLineListVM();
            LinkedVM = new InventoryTransferInLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferInLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferInLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}