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
    /// 批次规则定义明细行
    /// </summary>
	[Table("BaseSequenceDefineLine")]

    [Display(Name = "_Model.BaseSequenceDefineLine")]
    public class BaseSequenceDefineLine : BasePoco
    {
        [Display(Name = "_Model._BaseSequenceDefineLine._SequenceDefine")]
        [Comment("批次规则定义主表")]
        public BaseSequenceDefine SequenceDefine { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SequenceDefine")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("批次规则定义主表")]
        public Guid? SequenceDefineId { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SegmentOrder")]
        [Comment("段顺序")]
        [Range(1,999999999,ErrorMessage="Validate.{0}range{1}")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? SegmentOrder { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SegmentType")]
        [Comment("段类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SegmentTypeEnum? SegmentType { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SegmentValue")]
        [StringLength(20, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("段值")]
        public string SegmentValue { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SerialLength")]
        [Comment("流水号位数")]
        public int? SerialLength { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._PadChar")]
        [StringLength(1, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("补位字符")]
        public string PadChar { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._DateFormat")]
        [Comment("日期格式")]
        public DateFormatEnum? DateFormat { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
