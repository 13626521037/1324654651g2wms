using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;

namespace WMS.Model._Admin
{
    /// <summary>
    /// 外部数据单据基类
    /// </summary>
	[Table("BaseDocExternals")]

    [Display(Name = "_Model.BaseDocExternal")]
    public abstract class BaseDocExternal : BasePoco
    {
        [Display(Name = "来源系统主键")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("来源系统主键")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SourceSystemId { get; set; }
        [Display(Name = "最后修改时间")]
        [Comment("最后修改时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? LastUpdateTime { get; set; }

	}

}
