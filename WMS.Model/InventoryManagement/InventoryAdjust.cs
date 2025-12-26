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
    /// 库存调整单
    /// </summary>
	[Table("InventoryAdjust")]

    [Display(Name = "_Model.InventoryAdjust")]
    public class InventoryAdjust : BasePoco
    {
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        [Comment("盘点单")]
        public InventoryStockTaking StockTaking { get; set; }
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        [Comment("盘点单")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "_Model._InventoryAdjust._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryAdjust._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._InvAdjust")]
        [InverseProperty("InvAdjust")]
        public List<InventoryAdjustLine> InventoryAdjustLine_InvAdjust { get; set; }

	}

}
