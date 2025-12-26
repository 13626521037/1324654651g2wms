
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
namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryMoveLocationSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._DocNo")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        public Guid? InWhLocationId { get; set; }
        public List<ComboSelectListItem> AllInWhLocations { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}