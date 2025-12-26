using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;

namespace WMS.Model.PrintManagement
{
    /// <summary>
    /// 单据标签打印
    /// </summary>
	[Table("PrintDocument")]

    [Display(Name = "_Model.PrintDocument")]
    public class PrintDocument : BasePoco
    {
        [Display(Name = "_Model._PrintDocument._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintDocument._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemID")]
        [Comment("料品ID")]
        public long? ItemID { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("料号")]
        public string ItemCode { get; set; }
        [Display(Name = "_Model._PrintDocument._ItemName")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("品名")]
        public string ItemName { get; set; }
        [Display(Name = "_Model._PrintDocument._SPECS")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("规格")]
        public string SPECS { get; set; }
        [Display(Name = "_Model._PrintDocument._Description")]
        [StringLength(2000, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("描述")]
        public string Description { get; set; }
        [Display(Name = "_Model._PrintDocument._UnitName")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单位")]
        public string UnitName { get; set; }
        [Display(Name = "_Model._PrintDocument._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintDocument._ReceivedQty")]
        [Comment("已收数量")]
        [Precision(18, 5)]
        public decimal? ReceivedQty { get; set; }
        [Display(Name = "_Model._PrintDocument._ValidQty")]
        [Comment("可操作数")]
        [Precision(18, 5)]
        public decimal? ValidQty { get; set; }
        [Display(Name = "_Model._PrintDocument._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintDocument._SeibanRandom")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "组织编码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("组织编码")]
        public string OrgCode { get; set; }
        [Display(Name = "供应商编码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("供应商编码")]
        public string SupplierCode { get; set; }
        [Display(Name = "打印总数")]
        [NotMapped]
        public decimal? PrintQty { get; set; }

        [Display(Name = "装箱数")]
        [NotMapped]
        public decimal? PackingQty { get; set; }
	}

}
