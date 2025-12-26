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
    /// 批次规则流水明细
    /// </summary>
	[Table("BaseSequenceRecordsDetail")]

    [Display(Name = "_Model.BaseSequenceRecordsDetail")]
    public class BaseSequenceRecordsDetail : BasePoco
    {
        [Display(Name = "_Model._BaseSequenceRecordsDetail._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseSequenceRecordsDetail._BaseSequenceRecords")]
        [Comment("主表")]
        public BaseSequenceRecords BaseSequenceRecords { get; set; }
        [Display(Name = "_Model._BaseSequenceRecordsDetail._BaseSequenceRecords")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("主表")]
        public Guid? BaseSequenceRecordsId { get; set; }

	}

}
