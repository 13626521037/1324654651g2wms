using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;

namespace WMS.Model.ProductionManagement
{
    /// <summary>
    /// 生产报工单行
    /// </summary>
	[Table("ProductionRcvRptLine")]

    [Display(Name = "_Model.ProductionRcvRptLine")]
    public class ProductionRcvRptLine : BasePoco
    {
        [Display(Name = "_Model._ProductionRcvRptLine._ProductionRcvRpt")]
        [Comment("生产报工单")]
        public ProductionRcvRpt ProductionRcvRpt { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ProductionRcvRpt")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("生产报工单")]
        public Guid? ProductionRcvRptId { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._MODocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("生产订单")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string MODocNo { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP行ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._Qty")]
        [Comment("数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ReceiveQty")]
        [Comment("已收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._InWhQty")]
        [Comment("已入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ProductionRcvRptLineStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

        /// <summary>
        /// 扫码明细
        /// </summary>
        [NotMapped]
        public List<ReceivingSubLinePara> ReceivingSubLine { get; set; }
	}

}
