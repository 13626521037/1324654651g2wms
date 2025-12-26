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
    /// 库存冻结单行
    /// </summary>
	[Table("InventoryFreezeLine")]

    [Display(Name = "_Model.InventoryFreezeLine")]
    public class InventoryFreezeLine : BasePoco
    {
        [Display(Name = "_Model._InventoryFreezeLine._InventoryFreeze")]
        [Comment("库存冻结单")]
        public InventoryFreeze InventoryFreeze { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._InventoryFreeze")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存冻结单")]
        public Guid? InventoryFreezeId { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._BaseInventory")]
        [Comment("库存信息")]
        public BaseInventory BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._BaseInventory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存信息")]
        public Guid? BaseInventoryId { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
