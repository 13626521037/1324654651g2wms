using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using Microsoft.EntityFrameworkCore;

namespace WMS.Model.BaseData
{
    [Table("BaseSysPara")]

    [Display(Name = "系统参数设置")]
    public class BaseSysPara : BasePoco
    {
        [Display(Name = "编码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }

        [Display(Name = "名称")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }

        [Display(Name = "参数值")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("参数值")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Value { get; set; }

        [Display(Name = "备注")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
    }
}
