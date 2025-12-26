
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;
namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventorySplitSingleSTempSelected { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        public Guid? OriginalInvId { get; set; }
        public List<ComboSelectListItem> AllOriginalInvs { get; set; }
        [Display(Name = "来源序列号")]
        public string OriginalInvSN { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalQty")]
        public decimal? OriginalQty { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._CreateBy")]
        public string CreateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}