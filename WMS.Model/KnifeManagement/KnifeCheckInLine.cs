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
    /// 刀具归还单明细
    /// </summary>
	[Table("KnifeCheckInLines")]

    [Display(Name = "_Model.KnifeCheckInLine")]
    public class KnifeCheckInLine : BasePoco
    {
        [Display(Name = "_Model._KnifeCheckInLine._KnifeCheckIn")]
        [Comment("刀具归还单")]
        public KnifeCheckIn KnifeCheckIn { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._KnifeCheckIn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具归还单")]
        public Guid? KnifeCheckInId { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._ToWhLocation")]
        [Comment("归还库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._ToWhLocation")]
        [Comment("归还库位")]
        public Guid? ToWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._FromWhLocation")]
        [Comment("领用库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._FromWhLocation")]
        [Comment("领用库位")]
        public Guid? FromWhLocationId { get; set; }

    }

}
