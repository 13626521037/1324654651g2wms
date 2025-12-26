
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

namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleBatchVM : BaseBatchVM<InventorySplitSingle, InventorySplitSingle_BatchEdit>
    {
        public InventorySplitSingleBatchVM()
        {
            ListVM = new InventorySplitSingleListVM();
            LinkedVM = new InventorySplitSingle_BatchEdit();
        }

        public override bool DoBatchEdit()
        {
            
            return base.DoBatchEdit();
        }
    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class InventorySplitSingle_BatchEdit : BaseVM
    {

        
        public List<string> InventoryManagementInventorySplitSingleBTempSelected { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        public Guid? OriginalInvId { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalQty")]
        public decimal? OriginalQty { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._Memo")]
        public string Memo { get; set; }

        protected override void InitVM()
        {
           
        }
    }

}