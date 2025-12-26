
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

namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleLineVMs
{
    public partial class InventorySplitSingleLineBatchVM : BaseBatchVM<InventorySplitSingleLine, InventorySplitSingleLine_BatchEdit>
    {
        public InventorySplitSingleLineBatchVM()
        {
            ListVM = new InventorySplitSingleLineListVM();
            LinkedVM = new InventorySplitSingleLine_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventorySplitSingleLine_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventorySplitSingleLineBTempSelected { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}