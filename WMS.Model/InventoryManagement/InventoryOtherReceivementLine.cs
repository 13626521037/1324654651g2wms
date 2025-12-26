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
    /// 其它入库单行
    /// </summary>
	[Table("InventoryOtherReceivementLine")]

    [Display(Name = "_Model.InventoryOtherReceivementLine")]
    public class InventoryOtherReceivementLine : BasePoco
    {
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherReceivement")]
        [Comment("其它入库单")]
        public InventoryOtherReceivement InventoryOtherReceivement { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherReceivement")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("其它入库单")]
        public Guid? InventoryOtherReceivementId { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP行ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherShipLine")]
        [Comment("其它出库单行")]
        public InventoryOtherShipLine InventoryOtherShipLine { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherShipLine")]
        [Comment("其它出库单行")]
        public Guid? InventoryOtherShipLineId { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._SerialNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
