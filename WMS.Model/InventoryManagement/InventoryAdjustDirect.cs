using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// InventoryAdjustDirect
    /// </summary>
	[Table("InventoryAdjustDirect")]

    [Display(Name = "_Model.InventoryAdjustDirect")]
    public class InventoryAdjustDirect : BasePoco
    {
        [Display(Name = "_Model._InventoryAdjustDirect._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._OldInv")]
        [Comment("原库存信息")]
        public BaseInventory OldInv { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._OldInv")]
        [Comment("原库存信息")]
        public Guid? OldInvId { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._NewInv")]
        [Comment("新库存信息")]
        public BaseInventory NewInv { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._NewInv")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("新库存信息")]
        public Guid? NewInvId { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._DiffQty")]
        [Comment("差异数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? DiffQty { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
