using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 手动调出单行
    /// </summary>
	[Table("InventoryTransferOutManualLine")]

    [Display(Name = "_Model.InventoryTransferOutManualLine")]
    public class InventoryTransferOutManualLine : BaseDocExternal
    {
        [Display(Name = "_Model._InventoryTransferOutManualLine._InventoryTransferOutManual")]
        [Comment("手动调出单")]
        public InventoryTransferOutManual InventoryTransferOutManual { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._InventoryTransferOutManual")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("手动调出单")]
        public Guid? InventoryTransferOutManualId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._NewDocLineNo")]
        [Comment("新行号")]
        public int? NewDocLineNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ItemMaster")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ToBeOffQty")]
        [Comment("待下架数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeOffQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ToBeShipQty")]
        [Comment("待出货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeShipQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._OffQty")]
        [Comment("已下架数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OffQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ShippedQty")]
        [Comment("已出货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ShippedQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._TransInQty")]
        [Comment("调入数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? TransInQty { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryTransferOutManualLineStatusEnum? Status { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

    }

}
