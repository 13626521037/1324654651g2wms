using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 移库单
    /// </summary>
	[Table("InventoryMoveLocation")]

    [Display(Name = "_Model.InventoryMoveLocation")]
    public class InventoryMoveLocation : BasePoco
    {
        [Display(Name = "_Model._InventoryMoveLocation._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        [Comment("入库库位")]
        public BaseWhLocation InWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        [Comment("入库库位")]
        public Guid? InWhLocationId { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._InventoryMoveLocation")]
        [InverseProperty("InventoryMoveLocation")]
        public List<InventoryMoveLocationLine> InventoryMoveLocationLine_InventoryMoveLocation { get; set; }

	}

}
