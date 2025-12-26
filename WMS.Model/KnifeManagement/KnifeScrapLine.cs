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
    /// 刀具报废单明细
    /// </summary>
	[Table("KnifeScrapLines")]

    [Display(Name = "_Model.KnifeScrapLine")]
    public class KnifeScrapLine : BasePoco
    {
        [Display(Name = "_Model._KnifeScrapLine._KnifeScrap")]
        [Comment("刀具报废单")]
        public KnifeScrap KnifeScrap { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._KnifeScrap")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具报废单")]
        public Guid? KnifeScrapId { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._IsAccident")]
        [Comment("意外报废")]
        public bool? IsAccident { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._FromWhLocation")]
        [Comment("报废前库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._FromWhLocation")]
        [Comment("报废前库位")]
        public Guid? FromWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._ToWhLocation")]
        [Comment("报废后库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._ToWhLocation")]
        [Comment("报废后库位")]
        public Guid? ToWhLocationId { get; set; }

    }

}
