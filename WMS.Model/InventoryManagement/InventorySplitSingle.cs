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
    /// 库存拆零单
    /// </summary>
	[Table("InventorySplitSingle")]

    [Display(Name = "_Model.InventorySplitSingle")]
    public class InventorySplitSingle : BasePoco
    {
        [Display(Name = "_Model._InventorySplitSingle._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        [Comment("原库存信息")]
        public BaseInventory OriginalInv { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("原库存信息")]
        public Guid? OriginalInvId { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalQty")]
        [Comment("原数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OriginalQty { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._SplitSingle")]
        [InverseProperty("SplitSingle")]
        public List<InventorySplitSingleLine> InventorySplitSingleLine_SplitSingle { get; set; }

	}

}
