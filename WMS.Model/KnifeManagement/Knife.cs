using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;
using WMS.Model.InventoryManagement;

namespace WMS.Model.KnifeManagement
{
    /// <summary>
    /// 刀具
    /// </summary>
	[Table("Knifes")]

    [Display(Name = "_Model.Knife")]
    public class Knife : BasePoco
    {
        [Display(Name = "_Model._Knife._CreatedDate")]
        [Comment("建档时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "序列号")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("序列号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string SerialNumber { get; set; }
        [Display(Name = "条码")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("条码")]
        public string BarCode { get; set; }
        [Display(Name = "_Model._Knife._Status")]
        [Comment("刀具状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public KnifeStatusEnum? Status { get; set; }
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        [Comment("当前领用人")]
        public BaseOperator CurrentCheckOutBy { get; set; }
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        [Comment("当前领用人")]
        public Guid? CurrentCheckOutById { get; set; }
        [Display(Name = "_Model._Knife._HandledBy")]
        [Comment("经办人")]
        [NotMapped]
        public FrameworkUser HandledBy { get; set; }
        [Display(Name = "_Model._Knife._HandledBy")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("经办人")]
        public string HandledById { get; set; }
        [Display(Name = "_Model._Knife._LastOperationDate")]
        [Comment("最近操作日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime LastOperationDate { get; set; }
        [Display(Name = "_Model._Knife._WhLocation")]
        [Comment("库位")]
        public BaseWhLocation WhLocation { get; set; }
        [Display(Name = "_Model._Knife._WhLocation")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库位")]
        public Guid? WhLocationId { get; set; }
        [Display(Name = "_Model._Knife._GrindCount")]
        [Comment("修磨次数")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? GrindCount { get; set; }
        [Display(Name = "_Model._Knife._InitialLife")]
        [Comment("初始寿命")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InitialLife { get; set; }
        [Display(Name = "_Model._Knife._CurrentLife")]
        [Comment("当前寿命")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? CurrentLife { get; set; }
        [Display(Name = "_Model._Knife._TotalUsedDays")]
        [Comment("累计使用天数")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? TotalUsedDays { get; set; }
        [Display(Name = "_Model._Knife._RemainingUsedDays")]
        [Comment("剩余使用天数")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? RemainingUsedDays { get; set; }
        
        [Display(Name = "_Model._Knife._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._Knife._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "MiscShipLineID")]
        [Comment("杂发行id")]
        public string MiscShipLineID { get; set; }
        [Display(Name = "在库状态")]
        [Comment("在库状态")]
        public KnifeInStockStatusEnum InStockStatus { get; set; }
        [Display(Name = "字段1")]
        [Comment("字段1")]
        public string Field1 { get; set; }
        [Display(Name = "字段2")]
        [Comment("字段2")]
        public string Field2 { get; set; }
        [Display(Name = "实际料号")]
        [Comment("实际料号")]
        public string ActualItemCode { get; set; }
        [Display(Name = "修磨刀具号")]
        [Comment("修磨刀具号")]
        public string GrindKnifeNO { get; set; }
        [Display(Name = "经办人姓名")]
        [Comment("经办人姓名")]
        public string HandledByName { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }

        [Display(Name = "_Model._KnifeOperation._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeOperation> KnifeOperation_Knife { get; set; }
        [Display(Name = "_Model._KnifeCheckOutLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeCheckOutLine> KnifeCheckOutLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeScrapLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeScrapLine> KnifeScrapLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeCheckInLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeCheckInLine> KnifeCheckInLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindRequestLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeGrindRequestLine> KnifeGrindRequestLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindInLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeGrindInLine> KnifeGrindInLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeCombineLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeCombineLine> KnifeCombineLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeGrindOutLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeGrindOutLine> KnifeGrindOutLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeTransferInLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeTransferInLine> KnifeTransferInLine_Knife { get; set; }
        [Display(Name = "_Model._KnifeTransferOutLine._Knife")]
        [InverseProperty("Knife")]
        public List<KnifeTransferOutLine> KnifeTransferOutLine_Knife { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._Knife")]
        [InverseProperty("Knife")]
        public List<InventoryStockTakingLine> InventoryStockTakingLine_Knife { get; set; }

        [Display(Name = "料品")]
        [Comment("料品")]
        [NotMapped]
        public string? ItemMasterCode_Import { get; set; }

    }

}
