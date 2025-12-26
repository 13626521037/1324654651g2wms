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
    /// 批次流水记录表
    /// </summary>
	[Table("BaseSequenceRecords")]

    [Display(Name = "_Model.BaseSequenceRecords")]
    public class BaseSequenceRecords : BasePoco
    {
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        [Comment("批次规则定义主表")]
        public BaseSequenceDefine SequenceDefine { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("批次规则定义主表")]
        public Guid? SequenceDefineId { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SegmentFlag")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("段标识")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SegmentFlag { get; set; }
        [Display(Name = "_Model._BaseSequenceRecords._SerialValue")]
        [Comment("流水值")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? SerialValue { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }
        [Display(Name = "_Model._BaseSequenceRecordsDetail._BaseSequenceRecords")]
        [InverseProperty("BaseSequenceRecords")]
        public List<BaseSequenceRecordsDetail> BaseSequenceRecordsDetail_BaseSequenceRecords { get; set; }

	}

}
