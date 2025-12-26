
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

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustBatchVM : BaseBatchVM<InventoryAdjust, InventoryAdjust_BatchEdit>
    {
        public InventoryAdjustBatchVM()
        {
            ListVM = new InventoryAdjustListVM();
            LinkedVM = new InventoryAdjust_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryAdjust_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryAdjustBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "_Model._InventoryAdjust._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjust._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}