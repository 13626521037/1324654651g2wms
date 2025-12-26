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
    /// 快捷操作配置
    /// </summary>
	[Table("BaseShortcut")]

    [Display(Name = "_Model.BaseShortcut")]
    public class BaseShortcut : BasePoco
    {
        [Display(Name = "_Model._BaseShortcut._Menu")]
        [Comment("菜单")]
        public FrameworkMenu Menu { get; set; }
        [Display(Name = "_Model._BaseShortcut._Menu")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("菜单")]
        public Guid? MenuId { get; set; }
        [Display(Name = "_Model._BaseShortcut._User")]
        [Comment("用户")]
        [NotMapped]
        public FrameworkUser User { get; set; }
        [Display(Name = "_Model._BaseShortcut._User")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("用户")]
        public string UserId { get; set; }

	}

}
