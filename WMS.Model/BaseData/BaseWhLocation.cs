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
using WMS.Model.KnifeManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 库位
    /// </summary>
	[Table("BaseWhLocation")]

    [Display(Name = "_Model.BaseWhLocation")]
    public class BaseWhLocation : BasePoco
    {
        [Display(Name = "_Model._BaseWhLocation._Code")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Name")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhLocation._WhArea")]
        [Comment("库区")]
        public BaseWhArea WhArea { get; set; }
        [Display(Name = "_Model._BaseWhLocation._WhArea")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库区")]
        public Guid? WhAreaId { get; set; }
        [Display(Name = "_Model._BaseWhLocation._AreaType")]
        [Comment("库位类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public WhLocationEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Locked")]
        [Comment("盘点锁定")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? Locked { get; set; }
        [Display(Name = "_Model._BaseWhLocation._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWhLocation._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("描述")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        [InverseProperty("WhLocation")]
        public List<BaseInventory> BaseInventory_WhLocation { get; set; }
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        [InverseProperty("Location")]
        public List<InventoryPalletVirtual> InventoryPalletVirtual_Location { get; set; }
        [Display(Name = "_Model._Knife._WhLocation")]
        [InverseProperty("WhLocation")]
        public List<Knife> Knife_WhLocation { get; set; }
        [Display(Name = "_Model._KnifeOperation._WhLocation")]
        [InverseProperty("WhLocation")]
        public List<KnifeOperation> KnifeOperation_WhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeCheckOutLine> KnifeCheckOutLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeScrapLine> KnifeScrapLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeScrapLine> KnifeScrapLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeCheckInLine> KnifeCheckInLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeCheckInLine> KnifeCheckInLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeGrindRequestLine> KnifeGrindRequestLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeGrindInLine> KnifeGrindInLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeGrindInLine> KnifeGrindInLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeCombineLine> KnifeCombineLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeCombineLine> KnifeCombineLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeGrindOutLine> KnifeGrindOutLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeTransferInLine> KnifeTransferInLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeTransferInLine> KnifeTransferInLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._FromWhLocation")]
        [InverseProperty("FromWhLocation")]
        public List<KnifeTransferOutLine> KnifeTransferOutLine_FromWhLocation { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._ToWhLocation")]
        [InverseProperty("ToWhLocation")]
        public List<KnifeTransferOutLine> KnifeTransferOutLine_ToWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        [InverseProperty("InWhLocation")]
        public List<InventoryMoveLocation> InventoryMoveLocation_InWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._OutWhLocation")]
        [InverseProperty("OutWhLocation")]
        public List<InventoryMoveLocationLine> InventoryMoveLocationLine_OutWhLocation { get; set; }
        [Display(Name = "_Model._InventoryMoveLocationLine._InWhLocation")]
        [InverseProperty("InWhLocation")]
        public List<InventoryMoveLocationLine> InventoryMoveLocationLine_InWhLocation { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLocations._Location")]
        [InverseProperty("Location")]
        public List<InventoryStockTakingLocations> InventoryStockTakingLocations_Location { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Location")]
        [InverseProperty("Location")]
        public List<InventoryStockTakingLine> InventoryStockTakingLine_Location { get; set; }
        [Display(Name = "_Model._InventoryAdjustLine._Location")]
        [InverseProperty("Location")]
        public List<InventoryAdjustLine> InventoryAdjustLine_Location { get; set; }
    }

}
