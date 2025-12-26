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
    /// 直接调出单行
    /// </summary>
	[Table("InventoryTransferOutDirectLine")]

    [Display(Name = "_Model.InventoryTransferOutDirectLine")]
    public class InventoryTransferOutDirectLine : BasePoco
    {
        [Display(Name = "_Model._InventoryTransferOutDirectLine._InventoryTransferOutDirect")]
        [Comment("直接调出单")]
        public InventoryTransferOutDirect InventoryTransferOutDirect { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._InventoryTransferOutDirect")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("直接调出单")]
        public Guid? InventoryTransferOutDirectId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP行ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._SerialNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._TransInQty")]
        [Comment("调入数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? TransInQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

        /// <summary>
        /// 关联的库存信息，提交数据到ERP时需要用到
        /// </summary>
        [NotMapped]
        public BaseInventory Inventory { get; set; }
	}

}
