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
    /// 盘点单ERP差异数据
    /// </summary>
	[Table("InventoryStockTakingErpDiffLine")]

    [Display(Name = "_Model.InventoryStockTakingErpDiffLine")]
    public class InventoryStockTakingErpDiffLine : BasePoco
    {
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._StockTaking")]
        [Comment("盘点单")]
        public InventoryStockTaking StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._StockTaking")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("盘点单")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._Item")]
        [Comment("料品")]
        public BaseItemMaster Item { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._Item")]
        [Comment("料品")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._WmsQty")]
        [Comment("本系统数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? WmsQty { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._ErpQty")]
        [Comment("ERP数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ErpQty { get; set; }

	}

}
