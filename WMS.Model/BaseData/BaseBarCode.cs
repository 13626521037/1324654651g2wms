using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 条码表
    /// </summary>
	[Table("BaseBarCode")]

    [Display(Name = "_Model.BaseBarCode")]
    public class BaseBarCode : BasePoco
    {
        [Display(Name = "_Model._BaseBarCode._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("来源单据号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseBarCode._Code")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("条码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "序列号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Sn { get; set; }
        [Display(Name = "_Model._BaseBarCode._Item")]
        [Comment("料品")]
        public BaseItemMaster Item { get; set; }
        [Display(Name = "_Model._BaseBarCode._Item")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("料品")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._BaseBarCode._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("客户编码")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerName")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("客户名称")]
        public string CustomerName { get; set; }
        [Display(Name = "_Model._BaseBarCode._CustomerNameFirstLetter")]
        [StringLength(200, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("客户首字母拼音")]
        public string CustomerNameFirstLetter { get; set; }
        [Display(Name = "_Model._BaseBarCode._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "番号随机码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "批号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("批号")]
        public string BatchNumber { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Display(Name = "(1)供应商编码")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("供应商编码")]
        public string ExtendedFields1 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields2")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段2")]
        public string ExtendedFields2 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields3")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段3")]
        public string ExtendedFields3 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields4")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段4")]
        public string ExtendedFields4 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields5")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段5")]
        public string ExtendedFields5 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields6")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段6")]
        public string ExtendedFields6 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields7")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段7")]
        public string ExtendedFields7 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields8")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段8")]
        public string ExtendedFields8 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields9")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段9")]
        public string ExtendedFields9 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields10")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段10")]
        public string ExtendedFields10 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields11")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段11")]
        public string ExtendedFields11 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields12")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段12")]
        public string ExtendedFields12 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields13")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段13")]
        public string ExtendedFields13 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields14")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段14")]
        public string ExtendedFields14 { get; set; }
        [Display(Name = "_Model._BaseBarCode._ExtendedFields15")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("扩展字段15")]
        public string ExtendedFields15 { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }

        [Display(Name = "料号")]
        [Comment("料号")]
        [NotMapped]
        public string ItemMasterCode_Import { get; set; }

        [Display(Name = "组织编码")]
        [Comment("组织编码")]
        [NotMapped]
        public string OrgCode_Import { get; set; }

        [Display(Name = "料品来源类型")]
        [Comment("料品来源类型")]
        [NotMapped]
        public ItemSourceTypeEnum? ItemSourceType_Import { get; set; }
    }

}
