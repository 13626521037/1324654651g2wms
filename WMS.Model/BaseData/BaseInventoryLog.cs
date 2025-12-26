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
    /// 库存流水（日志）
    /// </summary>
	[Table("BaseInventoryLog")]

    [Display(Name = "_Model.BaseInventoryLog")]
    public class BaseInventoryLog : BasePoco
    {
        [Display(Name = "_Model._BaseInventoryLog._OperationType")]
        [Comment("业务操作")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public OperationTypeEnum? OperationType { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("业务单据")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        [Comment("来源库存信息")]
        public BaseInventory SourceInventory { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        [Comment("来源库存信息")]
        public Guid? SourceInventoryId { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        [Comment("目标库存信息")]
        public BaseInventory TargetInventory { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        [Comment("目标库存信息")]
        public Guid? TargetInventoryId { get; set; }
        [Display(Name = "来源数量变化")]
        [Comment("来源数量变化")]
        [Precision(18, 5)]
        public decimal? SourceDiffQty { get; set; }
        [Display(Name = "目标数量变化")]
        [Comment("目标数量变化")]
        [Precision(18, 5)]
        public decimal? TargetDiffQty { get; set; }
        [Display(Name = "_Model._BaseInventoryLog._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

	}

}
