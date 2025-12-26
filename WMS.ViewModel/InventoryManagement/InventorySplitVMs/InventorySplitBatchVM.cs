
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

namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitBatchVM : BaseBatchVM<InventorySplit, InventorySplit_BatchEdit>
    {
        public InventorySplitBatchVM()
        {
            ListVM = new InventorySplitListVM();
            LinkedVM = new InventorySplit_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventorySplit_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventorySplitBTempSelected { get; set; }
        [Display(Name = "_Model._InventorySplit._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventorySplit._OldInv")]
        public Guid? OldInvId { get; set; }
        [Display(Name = "_Model._InventorySplit._NewInv")]
        public Guid? NewInvId { get; set; }
        [Display(Name = "_Model._InventorySplit._OrigQty")]
        public decimal? OrigQty { get; set; }
        [Display(Name = "_Model._InventorySplit._SplitQty")]
        public decimal? SplitQty { get; set; }
        [Display(Name = "_Model._InventorySplit._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}