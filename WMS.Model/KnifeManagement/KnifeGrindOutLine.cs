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
    /// 修磨出库单明细
    /// </summary>
	[Table("KnifeGrindOutLines")]

    [Display(Name = "_Model.KnifeGrindOutLine")]
    public class KnifeGrindOutLine : BasePoco
    {
        [Display(Name = "_Model._KnifeGrindOutLine._KnifeGrindOut")]
        [Comment("刀具修磨出库单")]
        public KnifeGrindOut KnifeGrindOut { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._KnifeGrindOut")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具修磨出库单")]
        public Guid? KnifeGrindOutId { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._FromWhLocation")]
        [Comment("出库库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._FromWhLocation")]
        [Comment("出库库位")]
        public Guid? FromWhLocationId { get; set; }
        [Display(Name = "行状态")]
        [Comment("行状态")]
        public KnifeOrderStatusEnum? Status { get; set; }

    }

}
