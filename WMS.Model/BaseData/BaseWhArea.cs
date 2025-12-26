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

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 库区
    /// </summary>
	[Table("BaseWhArea")]

    [Display(Name = "_Model.BaseWhArea")]
    public class BaseWhArea : BasePoco
    {
        [Display(Name = "_Model._BaseWhArea._Code")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseWhArea._Name")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._BaseWhArea._AreaType")]
        [Comment("库区类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public WhAreaEnum? AreaType { get; set; }
        [Display(Name = "_Model._BaseWhArea._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWhArea._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseWhLocation._WhArea")]
        [InverseProperty("WhArea")]
        public List<BaseWhLocation> BaseWhLocation_WhArea { get; set; }
        [Display(Name = "_Model._InventoryStockTakingAreas._Area")]
        [InverseProperty("Area")]
        public List<InventoryStockTakingAreas> InventoryStockTakingAreas_Area { get; set; }
    }

}
