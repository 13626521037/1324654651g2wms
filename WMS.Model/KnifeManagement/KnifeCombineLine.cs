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
    /// 刀具组合单明细
    /// </summary>
	[Table("KnifeCombineLines")]

    [Display(Name = "_Model.KnifeCombineLine")]
    public class KnifeCombineLine : BasePoco
    {
        [Display(Name = "_Model._KnifeCombineLine._KnifeCombine")]
        [Comment("刀具组合单")]
        public KnifeCombine KnifeCombine { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._KnifeCombine")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具组合单")]
        public Guid? KnifeCombineId { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._FromWhLocation")]
        [Comment("领用库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._FromWhLocation")]
        [Comment("领用库位")]
        public Guid? FromWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._ToWhLocation")]
        [Comment("归还库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._ToWhLocation")]
        [Comment("归还库位")]
        public Guid? ToWhLocationId { get; set; }


    }

}
