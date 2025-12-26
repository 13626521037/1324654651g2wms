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
    /// 刀具调出单
    /// </summary>
	[Table("KnifeTransferOuts")]

    [Display(Name = "_Model.KnifeTransferOut")]
    public class KnifeTransferOut : BasePoco
    {
        [Display(Name = "_Model._KnifeTransferOut._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "调入存储地点")]
        [Comment("调入存储地点")]
        public BaseWareHouse ToWh { get; set; }
        [Display(Name = "调入存储地点")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("调入存储地点")]
        public Guid? ToWhId { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._FromWH")]
        [Comment("调出存储地点")]
        public BaseWareHouse FromWH { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._FromWH")]
        [Comment("调出存储地点")]
        public Guid? FromWHId { get; set; }
        [Display(Name = "备注")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._KnifeTransferOut")]
        [InverseProperty("KnifeTransferOut")]
        public List<KnifeTransferOutLine> KnifeTransferOutLine_KnifeTransferOut { get; set; }

	}

}
