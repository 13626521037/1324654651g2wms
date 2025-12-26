
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
namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs
{
    public partial class InventoryErpDiffSearcher : BaseSearcher
    {
        
        public List<string> InventoryManagementInventoryErpDiffSTempSelected { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        public Guid? WhId { get; set; }
        public List<ComboSelectListItem> AllWhs { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        public Guid? ItemId { get; set; }
        [Display(Name = "料号")]
        public string ItemCode { get; set; }
        public List<ComboSelectListItem> AllItems { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Seiban")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._WmsQty")]
        public decimal? WmsQty { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._ErpQty")]
        public decimal? ErpQty { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._CreateTime")]
        public DateRange CreateTime { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._UpdateTime")]
        public DateRange UpdateTime { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._CreateBy")]
        public string CreateBy { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._UpdateBy")]
        public string UpdateBy { get; set; }

        protected override void InitVM()
        {
            

        }
    }

}