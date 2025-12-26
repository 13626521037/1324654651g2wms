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
    /// 库存拆分单
    /// </summary>
	[Table("InventorySplit")]

    [Display(Name = "_Model.InventorySplit")]
    public class InventorySplit : BasePoco
    {
        [Display(Name = "_Model._InventorySplit._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventorySplit._OldInv")]
        [Comment("原库存信息")]
        public BaseInventory OldInv { get; set; }
        [Display(Name = "_Model._InventorySplit._OldInv")]
        [Comment("原库存信息")]
        public Guid? OldInvId { get; set; }
        [Display(Name = "_Model._InventorySplit._NewInv")]
        [Comment("新库存信息")]
        public BaseInventory NewInv { get; set; }
        [Display(Name = "_Model._InventorySplit._NewInv")]
        [Comment("新库存信息")]
        public Guid? NewInvId { get; set; }
        [Display(Name = "_Model._InventorySplit._OrigQty")]
        [Comment("原库存数量")]
        [Precision(18, 5)]
        [Range(1,999999999,ErrorMessage="Validate.{0}range{1}")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OrigQty { get; set; }
        [Display(Name = "_Model._InventorySplit._SplitQty")]
        [Comment("拆分数量")]
        [Precision(18, 5)]
        [Range(1,999999999,ErrorMessage="Validate.{0}range{1}")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? SplitQty { get; set; }
        [Display(Name = "_Model._InventorySplit._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
