using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.PrintManagement
{
    /// <summary>
    /// 供应商标签打印
    /// </summary>
	[Table("PrintSupplier")]

    [Display(Name = "_Model.PrintSupplier")]
    public class PrintSupplier : BasePoco
    {
        [Display(Name = "_Model._PrintSupplier._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._DocLineNo")]
        [Comment("行号")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PrintSupplier._Item")]
        [Comment("料品")]
        public BaseItemMaster Item { get; set; }
        [Display(Name = "_Model._PrintSupplier._Item")]
        [Comment("料品")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._PrintSupplier._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintSupplier._ReceivedQty")]
        [Comment("已收数量")]
        [Precision(18, 5)]
        public decimal? ReceivedQty { get; set; }
        [Display(Name = "_Model._PrintSupplier._ValidQty")]
        [Comment("可操作数")]
        [Precision(18, 5)]
        public decimal? ValidQty { get; set; }
        [Display(Name = "_Model._PrintSupplier._BatchNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("批号")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._PrintSupplier._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintSupplier._SeibanRandom")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._PrintSupplier._Weight")]
        [Comment("重量")]
        [Precision(18, 5)]
        public decimal? Weight { get; set; }
        [Display(Name = "_Model._PrintSupplier._Supplier")]
        [Comment("供应商")]
        public BaseSupplier Supplier { get; set; }
        [Display(Name = "_Model._PrintSupplier._Supplier")]
        [Comment("供应商")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncSupplier")]
        [Comment("供应商同步字段")]
        [NotMapped]
        public string SyncSupplier { get; set; }
        [Display(Name = "_Model._PrintSupplier._SyncItem")]
        [Comment("料品同步字段")]
        [NotMapped]
        public string SyncItem { get; set; }

        [Display(Name = "打印总数")]
        [NotMapped]
        public decimal? PrintQty { get; set; }

        [Display(Name = "装箱数")]
        [NotMapped]
        public decimal? PackingQty { get; set; }

        [Display(Name = "自定义数量")]
        [NotMapped]
        public string CustomQty { get; set; }
    }

}
