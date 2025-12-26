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
    /// 刀具修磨入库单明细
    /// </summary>
	[Table("KnifeGrindInLines")]

    [Display(Name = "_Model.KnifeGrindInLine")]
    public class KnifeGrindInLine : BasePoco
    {
        [Display(Name = "_Model._KnifeGrindInLine._KnifeGrindIn")]
        [Comment("刀具修磨入库")]
        public KnifeGrindIn KnifeGrindIn { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._KnifeGrindIn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具修磨入库")]
        public Guid? KnifeGrindInId { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._ToWhLocation")]
        [Comment("入库库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._ToWhLocation")]
        [Comment("入库库位")]
        public Guid? ToWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._FromWhLocation")]
        [Comment("出库库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._FromWhLocation")]
        [Comment("出库库位")]
        public Guid? FromWhLocationId { get; set; }
    }

}
