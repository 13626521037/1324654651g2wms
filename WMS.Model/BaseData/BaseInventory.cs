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

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 库存信息
    /// </summary>
	[Table("BaseInventory")]

    [Display(Name = "_Model.BaseInventory")]
    public class BaseInventory : BasePoco
    {
        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        [Comment("库位")]
        public BaseWhLocation WhLocation { get; set; }
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库位")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("批号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._BaseInventory._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "番号随机码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._BaseInventory._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseInventory._IsAbandoned")]
        [Comment("作废标记")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsAbandoned { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemSourceType")]
        [Comment("料品来源类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ItemSourceTypeEnum? ItemSourceType { get; set; }
        [Display(Name = "_Model._BaseInventory._FrozenStatus")]
        [Comment("冻结状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public FrozenStatusEnum? FrozenStatus { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._BaseInventory")]
        [InverseProperty("BaseInventory")]
        public List<InventoryPalletVirtualLine> InventoryPalletVirtualLine_BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._BaseInventory")]
        [InverseProperty("BaseInventory")]
        public List<InventoryFreezeLine> InventoryFreezeLine_BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._BaseInventory")]
        [InverseProperty("BaseInventory")]
        public List<InventoryUnfreezeLine> InventoryUnfreezeLine_BaseInventory { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        [InverseProperty("SourceInventory")]
        public List<BaseInventoryLog> BaseInventoryLog_SourceInventory { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        [InverseProperty("TargetInventory")]
        public List<BaseInventoryLog> BaseInventoryLog_TargetInventory { get; set; }
        [Display(Name = "_Model._InventorySplit._OldInv")]
        [InverseProperty("OldInv")]
        public List<InventorySplit> InventorySplit_OldInv { get; set; }
        [Display(Name = "_Model._InventorySplit._NewInv")]
        [InverseProperty("NewInv")]
        public List<InventorySplit> InventorySplit_NewInv { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._OldInv")]
        [InverseProperty("OldInv")]
        public List<InventoryAdjustDirect> InventoryAdjustDirect_OldInv { get; set; }
        [Display(Name = "_Model._InventoryAdjustDirect._NewInv")]
        [InverseProperty("NewInv")]
        public List<InventoryAdjustDirect> InventoryAdjustDirect_NewInv { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        [InverseProperty("Inventory")]
        public List<BaseDocInventoryRelation> BaseDocInventoryRelation_Inventory { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._BaseInventory")]
        [InverseProperty("BaseInventory")]
        public List<InventoryMoveLocationLine> InventoryMoveLocationLine_BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._Inventory")]
        [InverseProperty("Inventory")]
        public List<InventoryOtherShipLine> InventoryOtherShipLine_Inventory { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Inventory")]
        [InverseProperty("Inventory")]
        public List<InventoryStockTakingLine> InventoryStockTakingLine_Inventory { get; set; }
        [Display(Name = "_Model._InventoryErpDiffLine._Inventory")]
        [InverseProperty("Inventory")]
        public List<InventoryErpDiffLine> InventoryErpDiffLine_Inventory { get; set; }
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        [InverseProperty("OriginalInv")]
        public List<InventorySplitSingle> InventorySplitSingle_OriginalInv { get; set; }
        [Display(Name = "_Model._InventorySplitSingleLine._NewInv")]
        [InverseProperty("NewInv")]
        public List<InventorySplitSingleLine> InventorySplitSingleLine_NewInv { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Inventory")]
        [InverseProperty("Inventory")]
        public List<InventoryAdjustLine> InventoryAdjustLine_Inventory { get; set; }
        [Display(Name = "料品")]
        [Comment("料品")]
        [NotMapped]
        public string? ItemMasterCode_Import { get; set; }

        [Display(Name = "库位")]
        [Comment("库位")]
        [NotMapped]
        public string? LocationCode_Import { get; set; }
    }

}
