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
    /// 批次规则定义
    /// </summary>
	[Table("BaseSequenceDefine")]

    [Display(Name = "_Model.BaseSequenceDefine")]
    public class BaseSequenceDefine : BasePoco
    {
        [Display(Name = "_Model._BaseSequenceDefine._Code")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._Name")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._DocType")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseSequenceDefine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseSequenceDefineLine._SequenceDefine")]
        [InverseProperty("SequenceDefine")]
        public List<BaseSequenceDefineLine> BaseSequenceDefineLine_SequenceDefine { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        [InverseProperty("SequenceDefine")]
        public List<BaseSequenceRecords> BaseSequenceRecords_SequenceDefine { get; set; }

    }

}
