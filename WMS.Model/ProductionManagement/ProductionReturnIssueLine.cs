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
    /// 生产退料单行
    /// </summary>
	[Table("ProductionReturnIssueLine")]

    [Display(Name = "_Model.ProductionReturnIssueLine")]
    public class ProductionReturnIssueLine : BaseDocExternal
    {
        [Display(Name = "_Model._ProductionReturnIssueLine._ProductionReturnIssue")]
        [Comment("生产退料单")]
        public ProductionReturnIssue ProductionReturnIssue { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ProductionReturnIssue")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("生产退料单")]
        public Guid? ProductionReturnIssueId { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._MODocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("生产订单")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string MODocNo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._PickListLineNo")]
        [Comment("备料表行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? PickListLineNo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Qty")]
        [Comment("数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ReceiveQty")]
        [Comment("已收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._InWhQty")]
        [Comment("已入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ProductionReturnIssueLineStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWh { get; set; }
    }

}
