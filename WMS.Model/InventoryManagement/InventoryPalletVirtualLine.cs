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
    /// 组托单行
    /// </summary>
	[Table("InventoryPalletVirtualLine")]

    [Display(Name = "_Model.InventoryPalletVirtualLine")]
    public class InventoryPalletVirtualLine : BasePoco
    {
        [Display(Name = "_Model._InventoryPalletVirtualLine._InventoryPallet")]
        [Comment("组托单")]
        public InventoryPalletVirtual InventoryPallet { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._InventoryPallet")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组托单")]
        public Guid? InventoryPalletId { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._BaseInventory")]
        [Comment("库存信息")]
        public BaseInventory BaseInventory { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._BaseInventory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存信息")]
        public Guid? BaseInventoryId { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }
    }

}
