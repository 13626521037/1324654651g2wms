using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;

namespace WMS.Model.KnifeManagement
{
    /// <summary>
    /// 刀具领用单
    /// </summary>
	[Table("KnifeCheckOuts")]

    [Display(Name = "_Model.KnifeCheckOut")]
    public class KnifeCheckOut : BasePoco
    {
        [Display(Name = "_Model._KnifeCheckOut._DocType")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeCheckOutTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        [Comment("领用人")]
        public BaseOperator CheckOutBy { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("领用人")]
        public Guid? CheckOutById { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._KnifeCheckOut")]
        [InverseProperty("KnifeCheckOut")]
        public List<KnifeCheckOutLine> KnifeCheckOutLine_KnifeCheckOut { get; set; }

	}

}
