using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;

namespace WMS.Model.KnifeManagement
{
    /// <summary>
    /// KnifeTaskLog
    /// </summary>
	[Table("KnifeTaskLogs")]

    [Display(Name = "_Model.KnifeTaskLog")]
    public class KnifeTaskLog : BasePoco
    {
        [Display(Name = "_Model._KnifeTaskLog._TaskName")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("任务名称")]
        public string TaskName { get; set; }
        [Display(Name = "_Model._KnifeTaskLog._OperationTime")]
        [Comment("执行时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime OperationTime { get; set; }
        [Display(Name = "数量")]
        [Comment("数量")]
        public int Num { get; set; }
        [Display(Name = "备注")]
        [Comment("备注")]
        public string Memo { get; set; }

}

}
