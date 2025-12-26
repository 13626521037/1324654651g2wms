
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
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectBatchVM : BaseBatchVM<InventoryAdjustDirect, InventoryAdjustDirect_BatchEdit>
    {
        public InventoryAdjustDirectBatchVM()
        {
            ListVM = new InventoryAdjustDirectListVM();
            LinkedVM = new InventoryAdjustDirect_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventoryAdjustDirect_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventoryAdjustDirectBTempSelected { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._OldInv")]
        public Guid? OldInvId { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._NewInv")]
        public Guid? NewInvId { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._DiffQty")]
        public decimal? DiffQty { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}