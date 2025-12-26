
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

namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeBatchVM : BaseBatchVM<InventoryFreeze, InventoryFreeze_BatchEdit>
    {
        public InventoryFreezeBatchVM()
        {
            ListVM = new InventoryFreezeListVM();
            LinkedVM = new InventoryFreeze_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryFreeze_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryFreezeBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryFreeze._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryFreeze._Reason")]
        public string Reason { get; set; }
        [Display(Name = "_Model._InventoryFreeze._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}