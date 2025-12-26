using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// ERP对账
    /// </summary>
	[Table("InventoryErpDiff")]

    [Display(Name = "_Model.InventoryErpDiff")]
    public class InventoryErpDiff : BasePoco
    {
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        [Comment("料品")]
        public BaseItemMaster Item { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        [Comment("料品")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._WmsQty")]
        [Comment("WMS数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? WmsQty { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._ErpQty")]
        [Comment("ERP数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ErpQty { get; set; }
        [Display(Name = "ERP料品ID")]
        [Comment("ERP料品ID")]
        public string SyncItem { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._ErpDiff")]
        [InverseProperty("ErpDiff")]
        public List<InventoryErpDiffLine> InventoryErpDiffLine_ErpDiff { get; set; }

        
	}

}
