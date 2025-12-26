using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.KnifeManagement;
using WMS.Model.BaseData;

namespace WMS.Model.KnifeManagement
{
    /// <summary>
    /// 刀具操作
    /// </summary>
	[Table("KnifeOperations")]

    [Display(Name = "_Model.KnifeOperation")]
    public class KnifeOperation : BasePoco
    {
        [Display(Name = "_Model._KnifeOperation._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeOperation._Knife")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationType")]
        [Comment("刀具操作类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOperationTypeEnum? OperationType { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationTime")]
        [Comment("操作时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? OperationTime { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationBy")]
        [Comment("操作人")]
        public BaseOperator OperationBy { get; set; }
        [Display(Name = "_Model._KnifeOperation._OperationBy")]
        [Comment("操作人")]
        public Guid? OperationById { get; set; }
        [Display(Name = "_Model._KnifeOperation._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeOperation._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeOperation._UsedDays")]
        [Comment("使用天数")]
        public decimal? UsedDays { get; set; }
        [Display(Name = "_Model._KnifeOperation._RemainingDays")]
        [Comment("剩余天数")]
        public decimal? RemainingDays { get; set; }
        
        [Display(Name = "_Model._KnifeOperation._WhLocation")]
        [Comment("库位")]
        public BaseWhLocation WhLocation { get; set; }
        [Display(Name = "_Model._KnifeOperation._WhLocation")]
        [Comment("库位")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "_Model._KnifeOperation._GrindNum")]
        [Comment("修磨次数")]
        public decimal? GrindNum { get; set; }
        
        [Display(Name = "_Model._KnifeOperation._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("操作单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }

        [Display(Name = "U9来源行id")]
        [Comment("U9来源行id")]
        public string U9SourceLineID { get; set; }
        [Display(Name = "累计使用天数")]
        [Comment("累计使用天数")]
        public decimal? TotalUsedDays { get; set; }

        [Display(Name = "意外报废")]
        [Comment("意外报废")]
        public bool? IsAccident { get; set; }
        [Display(Name = "当前寿命")]
        [Comment("当前寿命")]
        public decimal? CurrentLife { get; set; }
        [Display(Name = "前状态")]
        [Comment("前状态")]
        public KnifeStatusEnum? BeforeStatus { get; set; }
        [Display(Name = "后状态")]
        [Comment("后状态")]
        public KnifeStatusEnum? AfterStatus { get; set; }
        [Display(Name = "经办人姓名")]
        [Comment("经办人姓名")]
        public string HandledByName { get; set; }

    }

}
