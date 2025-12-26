
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryAdjustSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        public Guid? StockTakingId { get; set; }
        public List<ComboSelectListItem> AllStockTakings { get; set; }
        [Display(Name = "_Model._InventoryAdjust._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjust._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryAdjust._CreateBy")]
        public string CreateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}