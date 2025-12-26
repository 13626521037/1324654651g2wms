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
    /// 刀具归还单
    /// </summary>
	[Table("KnifeCheckIns")]

    [Display(Name = "_Model.KnifeCheckIn")]
    public class KnifeCheckIn : BasePoco
    {
        [Display(Name = "_Model._KnifeCheckIn._DocType")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeCheckInTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        [Comment("归还人")]
        public BaseOperator CheckInBy { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("归还人")]
        public Guid? CheckInById { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._CombineKnifeNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("组合刀号")]
        public string CombineKnifeNo { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._KnifeCheckIn")]
        [InverseProperty("KnifeCheckIn")]
        public List<KnifeCheckInLine> KnifeCheckInLine_KnifeCheckIn { get; set; }

	}

}
