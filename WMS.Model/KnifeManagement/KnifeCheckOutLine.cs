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
    /// 刀具领用单明细
    /// </summary>
	[Table("KnifeCheckOutLines")]

    [Display(Name = "_Model.KnifeCheckOutLine")]
    public class KnifeCheckOutLine : BasePoco
    {
        [Display(Name = "_Model._KnifeCheckOutLine._KnifeCheckOut")]
        [Comment("刀具领用单")]
        public KnifeCheckOut KnifeCheckOut { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._KnifeCheckOut")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具领用单")]
        public Guid? KnifeCheckOutId { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._FromWhLocation")]
        [Comment("领用库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._FromWhLocation")]
        [Comment("领用库位")]
        public Guid? FromWhLocationId { get; set; }

        [Display(Name = "是新刀")]
        [Comment("是新刀")]
        public string IsNewLine { get; set; }//"1"  表示新刀 可以打印


    }

}
