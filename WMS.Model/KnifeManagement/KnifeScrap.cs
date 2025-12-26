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
    /// 刀具报废单
    /// </summary>
	[Table("KnifeScraps")]

    [Display(Name = "_Model.KnifeScrap")]
    public class KnifeScrap : BasePoco
    {
        [Display(Name = "_Model._KnifeScrap._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeScrap._DocType")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeScrapTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._KnifeScrap._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum Status { get; set; }
        [Display(Name = "_Model._KnifeScrap._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeScrap._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeScrap._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "关闭时间")]
        [Comment("关闭时间")]
        public DateTime? CloseTime { get; set; }
        [Display(Name = "_Model._KnifeScrap._U9MiscRcvDocNo")]
        [Comment("U9杂收单单号")]
        public string U9MiscRcvDocNo { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }


        [Display(Name = "_Model._KnifeScrapLine._KnifeScrap")]
        [InverseProperty("KnifeScrap")]
        public List<KnifeScrapLine> KnifeScrapLine_KnifeScrap { get; set; }

	}

}
