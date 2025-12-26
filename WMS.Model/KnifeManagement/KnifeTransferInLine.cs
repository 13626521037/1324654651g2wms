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
    /// 刀具调入单明细
    /// </summary>
	[Table("KnifeTransferInLines")]

    [Display(Name = "_Model.KnifeTransferInLine")]
    public class KnifeTransferInLine : BasePoco
    {
        [Display(Name = "_Model._KnifeTransferInLine._KnifeTransferIn")]
        [Comment("刀具调入单")]
        public KnifeTransferIn KnifeTransferIn { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._KnifeTransferIn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("刀具调入单")]
        public Guid? KnifeTransferInId { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._Knife")]
        [Comment("刀具")]
        public Knife Knife { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._Knife")]
        [Comment("刀具")]
        public Guid? KnifeId { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._ToWhLocation")]
        [Comment("调入库位")]
        public BaseWhLocation ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._ToWhLocation")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("调入库位")]
        public Guid? ToWhLocationId { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._FromWhLocation")]
        [Comment("调出库位")]
        public BaseWhLocation FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._FromWhLocation")]
        [Comment("调出库位")]
        public Guid? FromWhLocationId { get; set; }
    }

}
