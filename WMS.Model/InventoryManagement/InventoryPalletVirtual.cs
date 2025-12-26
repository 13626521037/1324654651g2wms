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
    /// 组托单
    /// </summary>
	[Table("InventoryPalletVirtual")]

    [Display(Name = "_Model.InventoryPalletVirtual")]
    public class InventoryPalletVirtual : BasePoco
    {
        [Display(Name = "_Model._InventoryPalletVirtual._Code")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("托盘码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Status")]
        [Comment("托盘状态")]
        public FrozenStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        [Comment("库位")]
        public BaseWhLocation Location { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        [Comment("库位")]
        public Guid? LocationId { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._SysVersion")]
        [Comment("事务版本")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? SysVersion { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtualLine._InventoryPallet")]
        [InverseProperty("InventoryPallet")]
        public List<InventoryPalletVirtualLine> InventoryPalletVirtualLine_InventoryPallet { get; set; }

	}

}
