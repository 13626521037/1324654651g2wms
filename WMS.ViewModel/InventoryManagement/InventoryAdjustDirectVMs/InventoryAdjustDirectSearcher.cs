
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryAdjustDirectSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._CreateBy")]
        public string CreateBy { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}