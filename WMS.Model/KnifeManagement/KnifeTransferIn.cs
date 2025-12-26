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
    /// 刀具调入单
    /// </summary>
	[Table("KnifeTransferIns")]

    [Display(Name = "_Model.KnifeTransferIn")]
    public class KnifeTransferIn : BasePoco
    {
        [Display(Name = "_Model._KnifeTransferIn._DocNo")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ApprovedTime")]
        [Comment("审核时间")]
        public DateTime? ApprovedTime { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        
        [Display(Name = "备注")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "调出单单号")]
        [Comment("调出单单号")]
        public string TransferOutDocNo { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._FromWH")]
        [Comment("调出存储地点")]
        public BaseWareHouse FromWH { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._FromWH")]
        [Comment("调出存储地点")]
        public Guid? FromWHId { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ToWH")]
        [Comment("调入存储地点")]
        public BaseWareHouse ToWH { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ToWH")]
        [Comment("调入存储地点")]
        public Guid? ToWHId { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "存储地点")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }

        [Display(Name = "_Model._KnifeTransferInLine._KnifeTransferIn")]
        [InverseProperty("KnifeTransferIn")]
        public List<KnifeTransferInLine> KnifeTransferInLine_KnifeTransferIn { get; set; }

	}

}
