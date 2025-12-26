using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 采购退料单行
    /// </summary>
	[Table("PurchaseReturnLine")]

    [Display(Name = "_Model.PurchaseReturnLine")]
    public class PurchaseReturnLine : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseReturnLine._PurchaseReturn")]
        [Comment("采购退料单")]
        public PurchaseReturn PurchaseReturn { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._PurchaseReturn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("采购退料单")]
        public Guid? PurchaseReturnId { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ToBeOffQty")]
        [Comment("待下架数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeOffQty { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ToBeShipQty")]
        [Comment("待出货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeShipQty { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._OffQty")]
        [Comment("已下架数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OffQty { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ShippedQty")]
        [Comment("已出货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ShippedQty { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReturnLineStatusEnum? Status { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }
    }

}
