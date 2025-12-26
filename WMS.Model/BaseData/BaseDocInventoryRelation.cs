using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 单据库存关联关系
    /// </summary>
	[Table("BaseDocInventoryRelation")]

    [Display(Name = "_Model.BaseDocInventoryRelation")]
    public class BaseDocInventoryRelation : BasePoco
    {
        [Display(Name = "_Model._BaseDocInventoryRelation._DocType")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DocTypeEnum? DocType { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        [Comment("库存信息")]
        public BaseInventory Inventory { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Inventory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存信息")]
        public Guid? InventoryId { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._BusinessId")]
        [Comment("业务实体ID")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public Guid? BusinessId { get; set; }
        [Display(Name = "业务实体行ID")]
        [Comment("业务实体行ID")]
        public Guid? BusinessLineId { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._BaseDocInventoryRelation._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
