
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

namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeBatchVM : BaseBatchVM<InventoryUnfreeze, InventoryUnfreeze_BatchEdit>
    {
        public InventoryUnfreezeBatchVM()
        {
            ListVM = new InventoryUnfreezeListVM();
            LinkedVM = new InventoryUnfreeze_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryUnfreeze_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryUnfreezeBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryUnfreeze._Reason")]
        public string Reason { get; set; }
        [Display(Name = "_Model._InventoryUnfreeze._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}