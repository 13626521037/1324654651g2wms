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
    /// 移库单行
    /// </summary>
	[Table("InventoryMoveLocationLine")]

    [Display(Name = "_Model.InventoryMoveLocationLine")]
    public class InventoryMoveLocationLine : BasePoco
    {
        [Display(Name = "_Model._InventoryMoveLocationLine._InventoryMoveLocation")]
        [Comment("移库单")]
        public InventoryMoveLocation InventoryMoveLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._InventoryMoveLocation")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("移库单")]
        public Guid? InventoryMoveLocationId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._BaseInventory")]
        [Comment("库存信息")]
        public BaseInventory BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._BaseInventory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存信息")]
        public Guid? BaseInventoryId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._OutWhLocation")]
        [Comment("出库库位")]
        public BaseWhLocation OutWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._OutWhLocation")]
        [Comment("出库库位")]
        public Guid? OutWhLocationId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._InWhLocation")]
        [Comment("入库库位")]
        public BaseWhLocation InWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._InWhLocation")]
        [Comment("入库库位")]
        public Guid? InWhLocationId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
