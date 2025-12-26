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
    /// 委外发料单行
    /// </summary>
	[Table("PurchaseOutsourcingIssueLine")]

    [Display(Name = "_Model.PurchaseOutsourcingIssueLine")]
    public class PurchaseOutsourcingIssueLine : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._OutsourcingIssue")]
        [Comment("委外发料单")]
        public PurchaseOutsourcingIssue OutsourcingIssue { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._OutsourcingIssue")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("委外发料单")]
        public Guid? OutsourcingIssueId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._Qty")]
        [Comment("数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ToBeOffQty")]
        [Comment("待下架数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeOffQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ToBeShipQty")]
        [Comment("待出货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeShipQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._OffQty")]
        [Comment("已下架数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OffQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ShippedQty")]
        [Comment("已出货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ShippedQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseOutsourcingIssueLineStatusEnum? Status { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }
    }

}
