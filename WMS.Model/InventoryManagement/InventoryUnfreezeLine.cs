using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 库存解冻单行
    /// </summary>
	[Table("InventoryUnfreezeLine")]

    [Display(Name = "_Model.InventoryUnfreezeLine")]
    public class InventoryUnfreezeLine : BasePoco
    {
        [Display(Name = "_Model._InventoryUnfreezeLine._InventoryUnfreeze")]
        [Comment("库存解冻单")]
        public InventoryUnfreeze InventoryUnfreeze { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._InventoryUnfreeze")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存解冻单")]
        public Guid? InventoryUnfreezeId { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._BaseInventory")]
        [Comment("库存信息")]
        public BaseInventory BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._BaseInventory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存信息")]
        public Guid? BaseInventoryId { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
