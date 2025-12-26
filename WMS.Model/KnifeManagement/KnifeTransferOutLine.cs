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
    /// 刀具调出单明细
    /// </summary>
	[Table("KnifeTransferOutLines")]

    [Display(Name = "_Model.KnifeTransferOutLine")]
    public class KnifeTransferOutLine : BasePoco
    {
        [Display(Name = "_Model._KnifeTransferOutLine._KnifeTransferOut")]
        [Comment("刀具调出单")]
        public KnifeTransferOut KnifeTransferOut { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._KnifeTransferOut")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具调出单")]
        public Guid? KnifeTransferOutId { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "行状态")]
        [Comment("行状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeOrderStatusEnum? Status { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._FromWhLocation")]
        [Comment("调出库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._FromWhLocation")]
        [Comment("调出库位")]
        public Guid? FromWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._ToWhLocation")]
        [Comment("调入库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._ToWhLocation")]
        [Comment("调入库位")]
        public Guid? ToWhLocationId { get; set; }
    }

}
