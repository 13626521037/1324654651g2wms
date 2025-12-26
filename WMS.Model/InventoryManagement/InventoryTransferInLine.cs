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
    /// 调入单行
    /// </summary>
	[Table("InventoryTransferInLine")]

    [Display(Name = "_Model.InventoryTransferInLine")]
    public class InventoryTransferInLine : BasePoco
    {
        [Display(Name = "_Model._InventoryTransferInLine._InventoryTransferIn")]
        [Comment("调入单")]
        public InventoryTransferIn InventoryTransferIn { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._InventoryTransferIn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("调入单")]
        public Guid? InventoryTransferInId { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP行ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._ItemMaster")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._SerialNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string SerialNumber { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._TransferOutLine")]
        [Comment("调出单行")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public Guid? TransferOutLine { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryTransferInLineStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
