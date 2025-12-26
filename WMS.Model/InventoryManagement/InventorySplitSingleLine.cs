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
    /// 库存拆零单行
    /// </summary>
	[Table("InventorySplitSingleLine")]

    [Display(Name = "_Model.InventorySplitSingleLine")]
    public class InventorySplitSingleLine : BasePoco
    {
        [Display(Name = "_Model._InventorySplitSingleLine._SplitSingle")]
        [Comment("库存拆零单")]
        public InventorySplitSingle SplitSingle { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._SplitSingle")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存拆零单")]
        public Guid? SplitSingleId { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._NewInv")]
        [Comment("新库存信息")]
        public BaseInventory NewInv { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._NewInv")]
        [Comment("新库存信息")]
        public Guid? NewInvId { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
