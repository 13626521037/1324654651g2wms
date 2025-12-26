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
    /// 库存调整单行
    /// </summary>
	[Table("InventoryAdjustLine")]

    [Display(Name = "_Model.InventoryAdjustLine")]
    public class InventoryAdjustLine : BasePoco
    {
        [Display(Name = "_Model._InventoryAdjustLine._InvAdjust")]
        [Comment("库存调整单")]
        public InventoryAdjust InvAdjust { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._InvAdjust")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存调整单")]
        public Guid? InvAdjustId { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._StockTakingLine")]
        [Comment("盘点单行")]
        public InventoryStockTakingLine StockTakingLine { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._StockTakingLine")]
        [Comment("盘点单行")]
        public Guid? StockTakingLineId { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Inventory")]
        [Comment("库存信息")]
        public BaseInventory Inventory { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Inventory")]
        [Comment("库存信息")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Location")]
        [Comment("库位")]
        public BaseWhLocation Location { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Location")]
        [Comment("库位")]
        public Guid? LocationId { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Qty")]
        [Comment("原库存数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._StockTakingQty")]
        [Comment("盘点数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? StockTakingQty { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._DiffQty")]
        [Comment("盈亏数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? DiffQty { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._GainLossStatus")]
        [Comment("盈亏状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public GainLossStatusEnum? GainLossStatus { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
