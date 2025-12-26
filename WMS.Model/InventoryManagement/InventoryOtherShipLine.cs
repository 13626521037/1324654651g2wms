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
    /// 其它出库单行
    /// </summary>
	[Table("InventoryOtherShipLine")]

    [Display(Name = "_Model.InventoryOtherShipLine")]
    public class InventoryOtherShipLine : BasePoco
    {
        [Display(Name = "_Model._InventoryOtherShipLine._InventoryOtherShip")]
        [Comment("其它出库单")]
        public InventoryOtherShip InventoryOtherShip { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._InventoryOtherShip")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("其它出库单")]
        public Guid? InventoryOtherShipId { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP行ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._Inventory")]
        [Comment("库存信息")]
        public BaseInventory Inventory { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._Inventory")]
        [Comment("库存信息")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._RcvQty")]
        [Comment("已入库数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? RcvQty { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherShipLine")]
        [InverseProperty("InventoryOtherShipLine")]
        public List<InventoryOtherReceivementLine> InventoryOtherReceivementLine_InventoryOtherShipLine { get; set; }
    }

}
