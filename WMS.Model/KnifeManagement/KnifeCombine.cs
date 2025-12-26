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
    /// 刀具组合单
    /// </summary>
	[Table("KnifeCombines")]

    [Display(Name = "_Model.KnifeCombine")]
    public class KnifeCombine : BasePoco
    {
        [Display(Name = "_Model._KnifeCombine._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeCombine._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeCombine._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeCombine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeCombine._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._CloseTime")]
        [Comment("关闭时间")]
        public DateTime? CloseTime { get; set; }
        [Display(Name = "_Model._KnifeCombine._CombineKnifeNo")]
        [Comment("组合刀号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CombineKnifeNo { get; set; }
        [Display(Name = "_Model._KnifeCombine._CheckOutBy")]
        [Comment("领用人")]
        public BaseOperator CheckOutBy { get; set; }
        [Display(Name = "_Model._KnifeCombine._CheckOutBy")]
        [Comment("领用人")]
        public Guid? CheckOutById { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }

        [Display(Name = "_Model._KnifeCombineLine._KnifeCombine")]
        [InverseProperty("KnifeCombine")]
        public List<KnifeCombineLine> KnifeCombineLine_KnifeCombine { get; set; }

        [Display(Name = "组合单")]
        [InverseProperty("KnifeCombine")]
        public List<KnifeGrindRequest> KnifeGrindRequest_KnifeCombine { get; set; }
        [Display(Name = "组合单")]
        [InverseProperty("KnifeCombine")]
        public List<KnifeGrindOut> KnifeGrindOut_KnifeCombine { get; set; }
        [Display(Name = "组合单")]
        [InverseProperty("KnifeCombine")]
        public List<KnifeGrindIn> KnifeGrindIn_KnifeCombine { get; set; }

    }

}
