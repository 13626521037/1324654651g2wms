
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

namespace WMS.ViewModel.InventoryManagement.InventoryTransferOutDirectLineVMs
{
    public partial class InventoryTransferOutDirectLineBatchVM : BaseBatchVM<InventoryTransferOutDirectLine, InventoryTransferOutDirectLine_BatchEdit>
    {
        public InventoryTransferOutDirectLineBatchVM()
        {
            ListVM = new InventoryTransferOutDirectLineListVM();
            LinkedVM = new InventoryTransferOutDirectLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryTransferOutDirectLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryTransferOutDirectLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}