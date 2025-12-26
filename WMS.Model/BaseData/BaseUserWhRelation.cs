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
    /// 用户存储地点对照关系
    /// </summary>
	[Table("BaseUserWhRelation")]

    [Display(Name = "_Model.BaseUserWhRelation")]
    public class BaseUserWhRelation : BasePoco
    {
        [Display(Name = "_Model._BaseUserWhRelation._User")]
        [Comment("用户")]
        [NotMapped]
        public FrameworkUser User { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._User")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("用户")]
        public string UserId { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Wh")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
