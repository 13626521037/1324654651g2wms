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
    /// 刀具修磨入库
    /// </summary>
	[Table("KnifeGrindIns")]

    [Display(Name = "_Model.KnifeGrindIn")]
    public class KnifeGrindIn : BasePoco
    {
        [Display(Name = "_Model._KnifeGrindIn._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._U9RcvDocNo")]
        [Comment("收货单号")]
        public string U9RcvDocNo { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }

        [Display(Name = "组合单")]
        [Comment("组合单")]
        public KnifeCombine KnifeCombine { get; set; }
        [Display(Name = "组合单")]
        [Comment("组合单")]
        public Guid? KnifeCombineId { get; set; }

        [Display(Name = "_Model._KnifeGrindInLine._KnifeGrindIn")]
        [InverseProperty("KnifeGrindIn")]
        public List<KnifeGrindInLine> KnifeGrindInLine_KnifeGrindIn { get; set; }

	}

}
