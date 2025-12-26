using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// ERP对账行
    /// </summary>
	[Table("InventoryErpDiffLine")]

    [Display(Name = "_Model.InventoryErpDiffLine")]
    public class InventoryErpDiffLine : BasePoco
    {
        [Display(Name = "_Model._InventoryErpDiffLine._ErpDiff")]
        [Comment("ERP对账头")]
        public InventoryErpDiff ErpDiff { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._ErpDiff")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("ERP对账头")]
        public Guid? ErpDiffId { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._Inventory")]
        [Comment("库存信息")]
        public BaseInventory Inventory { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._Inventory")]
        [Comment("库存信息")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }

	}

}
