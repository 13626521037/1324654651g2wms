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
    /// 刀具修磨申请单明细
    /// </summary>
	[Table("KnifeGrindRequestLines")]

    [Display(Name = "_Model.KnifeGrindRequestLine")]
    public class KnifeGrindRequestLine : BasePoco
    {
        [Display(Name = "_Model._KnifeGrindRequestLine._KnifeGrindRequest")]
        [Comment("刀具修磨申请单")]
        public KnifeGrindRequest KnifeGrindRequest { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._KnifeGrindRequest")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具修磨申请单")]
        public Guid? KnifeGrindRequestId { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._Status")]
        [Comment("行状态")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._FromWhLocation")]
        [Comment("申请库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._FromWhLocation")]
        [Comment("申请库位")]
        public Guid? FromWhLocationId { get; set; }
        [Display(Name = "U9采购单单号")]
        [StringLength(50)]
        [Comment("U9采购单单号")]
        public string U9PODocNo { get; set; }
        [Display(Name = "U9采购单行号")]
        [StringLength(50)]
        [Comment("U9采购单行号")]
        public string U9PODocLineNo { get; set; }

    }

}
