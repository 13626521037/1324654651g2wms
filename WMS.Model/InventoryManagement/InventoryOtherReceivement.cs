using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 其它入库单
    /// </summary>
	[Table("InventoryOtherReceivement")]

    [Display(Name = "_Model.InventoryOtherReceivement")]
    public class InventoryOtherReceivement : BasePoco
    {
        [Display(Name = "_Model._InventoryOtherReceivement._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._IsScrap")]
        [Comment("是否报废")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsScrap { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivement._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._InventoryOtherReceivement")]
        [InverseProperty("InventoryOtherReceivement")]
        public List<InventoryOtherReceivementLine> InventoryOtherReceivementLine_InventoryOtherReceivement { get; set; }

	}

}
