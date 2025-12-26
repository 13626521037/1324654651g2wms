
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryOtherReceivementSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._ErpID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._BusinessDate")]
        public DateRange BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._IsScrap")]
        public bool? IsScrap { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._CreateBy")]
        public string CreateBy { get; set; }

        protected override void InitVM()
        {
            
        }
    }

}