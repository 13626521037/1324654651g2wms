using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 系统公告
    /// </summary>
	[Table("BaseSysNotice")]

    [Display(Name = "_Model.BaseSysNotice")]
    public class BaseSysNotice : BasePoco
    {
        [Display(Name = "_Model._BaseSysNotice._Title")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("标题")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Title { get; set; }
        [Display(Name = "_Model._BaseSysNotice._Content")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("正文")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Content { get; set; }

	}

}
