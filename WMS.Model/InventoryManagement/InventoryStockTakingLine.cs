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
using WMS.Model.KnifeManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 库存盘点单行			
    /// </summary>
	[Table("InventoryStockTakingLine")]

    [Display(Name = "_Model.InventoryStockTakingLine")]
    public class InventoryStockTakingLine : BasePoco
    {
        [Display(Name = "_Model._InventoryStockTakingLine._StockTaking")]
        [Comment("库存盘点单")]
        public InventoryStockTaking StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._StockTaking")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存盘点单")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Inventory")]
        [Comment("库存信息")]
        public BaseInventory Inventory { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Inventory")]
        [Comment("库存信息")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "刀具")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "刀具")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._SerialNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        public string SerialNumber { get; set; }
        [Display(Name = "番号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "批号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("批号")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._ScanBarCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扫描条码")]
        public string ScanBarCode { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Qty")]
        [Comment("库存数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Location")]
        [Comment("实盘库位")]
        public BaseWhLocation Location { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Location")]
        [Comment("实盘库位")]
        public Guid? LocationId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._StockTakingQty")]
        [Comment("已盘数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? StockTakingQty { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._DiffQty")]
        [Comment("盈亏数量")]
        [Precision(18, 5)]
        public decimal? DiffQty { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._GainLossStatus")]
        [Comment("盈亏状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public GainLossStatusEnum? GainLossStatus { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._IsNew")]
        [Comment("是否新条码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsNew { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._IsKnifeLedger")]
        [Comment("是否刀具台账")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsKnifeLedger { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._OperatingUser")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("盘点人")]
        public string OperatingUser { get; set; }
        [Display(Name = "盘点时间")]
        [Comment("盘点时间")]
        public DateTime? StockTakingTime { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._StockTakingLine")]
        [InverseProperty("StockTakingLine")]
        public List<InventoryAdjustLine> InventoryAdjustLine_StockTakingLine { get; set; }
    }

}
