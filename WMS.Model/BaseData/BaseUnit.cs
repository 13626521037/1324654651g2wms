using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 单位
    /// </summary>
	[Table("BaseUnit")]

    [Display(Name = "_Model.BaseUnit")]
    public class BaseUnit : BaseExternal
    {
        //[Display(Name = "_Model._BaseUnit._Code")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("编码")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Code { get; set; }
        //[Display(Name = "_Model._BaseUnit._Name")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("名称")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Name { get; set; }
        [Display(Name = "_Model._BaseUnit._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseUnit._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseItemMaster._StockUnit")]
        [InverseProperty("StockUnit")]
        public List<BaseItemMaster> BaseItemMaster_StockUnit { get; set; }

	}

}
