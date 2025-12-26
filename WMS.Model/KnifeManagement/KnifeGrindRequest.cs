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
    /// 刀具修磨申请单
    /// </summary>
	[Table("KnifeGrindRequests")]

    [Display(Name = "_Model.KnifeGrindRequest")]
    public class KnifeGrindRequest : BasePoco
    {
        [Display(Name = "_Model._KnifeGrindRequest._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._U9PRDocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("U9请购单单号")]
        public string U9PRDocNo { get; set; }
        [Display(Name = "上次U9采购单单号")]
        [StringLength(50)]
        [Comment("上次U9采购单单号")]
        public string LastU9PODocNo { get; set; }
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

        [Display(Name = "_Model._KnifeGrindRequestLine._KnifeGrindRequest")]
        [InverseProperty("KnifeGrindRequest")]
        public List<KnifeGrindRequestLine> KnifeGrindRequestLine_KnifeGrindRequest { get; set; }

	}

}
