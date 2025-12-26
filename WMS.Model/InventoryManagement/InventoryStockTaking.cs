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
    /// 库位盘点单
    /// </summary>
	[Table("InventoryStockTaking")]

    [Display(Name = "_Model.InventoryStockTaking")]
    public class InventoryStockTaking : BasePoco
    {
        [Display(Name = "_Model._InventoryStockTaking._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ErpDocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单号")]
        public string ErpDocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Dimension")]
        [Comment("盘点维度")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryStockTakingDimensionEnum? Dimension { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitTime")]
        [Comment("提交时间")]
        public DateTime? SubmitTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._SubmitUser")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("提交人")]
        public string SubmitUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveTime")]
        [Comment("审核时间")]
        public DateTime? ApproveTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._ApproveUser")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("审核人")]
        public string ApproveUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseTime")]
        [Comment("关闭时间")]
        public DateTime? CloseTime { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._CloseUser")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("关闭人")]
        public string CloseUser { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryStockTakingStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Mode")]
        [Comment("盘点模式")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryStockTakingModeEnum? Mode { get; set; }
        [Display(Name = "_Model._InventoryStockTakingAreas._StockTaking")]
        [InverseProperty("StockTaking")]
        public List<InventoryStockTakingAreas> InventoryStockTakingAreas_StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLocations._StockTaking")]
        [InverseProperty("StockTaking")]
        public List<InventoryStockTakingLocations> InventoryStockTakingLocations_StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._StockTaking")]
        [InverseProperty("StockTaking")]
        public List<InventoryStockTakingLine> InventoryStockTakingLine_StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._StockTaking")]
        [InverseProperty("StockTaking")]
        public List<InventoryStockTakingErpDiffLine> InventoryStockTakingErpDiffLine_StockTaking { get; set; }
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        [InverseProperty("StockTaking")]
        public List<InventoryAdjust> InventoryAdjust_StockTaking { get; set; }
    }

}
