using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;
using WMS.Model;

namespace WMS.Model._Admin
{
    /// <summary>
    /// 外部数据基类
    /// </summary>
	[Table("BaseExternals")]

    [Display(Name = "_Model.BaseExternal")]
    public class BaseExternal : BasePoco,IPersistPoco
    {
        [Display(Name = "编码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code {  get; set; }

        [Display(Name = "名称")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }

        [Display(Name = "_Model._BaseExternal._SourceSystemId")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("来源系统主键")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SourceSystemId { get; set; }

        [Display(Name = "_Model._BaseExternal._LastUpdateTime")]
        [Comment("最后修改时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 此字段用于标记来源系统的数据是否已经被删除（false：已删除）
        /// </summary>
        [Display(Name = "_Model._BaseExternal._IsValid")]
        [Comment("是否有效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 数据库中此表（由继承对象决定）中所有的数据集合
        /// </summary>
        [NotMapped]
        public object AllDatas { get; set; }

        /// <summary>
        /// 获取编码和名称
        /// </summary>
        /// <returns></returns>
        public string CodeAndName
        {
            get
            {
                if (string.IsNullOrEmpty(Name))
                    return Code;
                else
                    return $"【{Code}】 {Name}";
            }
        }
	}

}
