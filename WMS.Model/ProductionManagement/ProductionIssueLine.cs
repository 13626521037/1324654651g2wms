using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;

namespace WMS.Model.ProductionManagement
{
    /// <summary>
    /// 生产领料单行
    /// </summary>
	[Table("ProductionIssueLine")]

    [Display(Name = "_Model.ProductionIssueLine")]
    public class ProductionIssueLine : BaseDocExternal
    {
        [Display(Name = "_Model._ProductionIssueLine._ProductionIssue")]
        [Comment("生产领料单")]
        public ProductionIssue ProductionIssue { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ProductionIssue")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("生产领料单")]
        public Guid? ProductionIssueId { get; set; }
        [Display(Name = "生产订单")]
        [Comment("生产订单")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string MODocNo { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._PickListLineNo")]
        [Comment("备料表行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? PickListLineNo { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Qty")]
        [Comment("数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ToBeOffQty")]
        [Comment("待下架数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeOffQty { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ToBeShipQty")]
        [Comment("待出货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeShipQty { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._OffQty")]
        [Comment("已下架数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OffQty { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ShippedQty")]
        [Comment("已出货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ShippedQty { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ProductionIssueLineStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWh { get; set; }
    }

}
