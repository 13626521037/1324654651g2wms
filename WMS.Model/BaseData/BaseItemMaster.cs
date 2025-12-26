using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WalkingTec.Mvvm.Core;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Model.KnifeManagement;
using WMS.Model.PrintManagement;
using WMS.Model.ProductionManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 料品
    /// </summary>
	[Table("BaseItemMaster")]

    [Display(Name = "_Model.BaseItemMaster")]
    public class BaseItemMaster : BaseExternal
    {
        [Display(Name = "_Model._BaseItemMaster._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ItemCategory")]
        [Comment("分类")]
        public BaseItemCategory ItemCategory { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ItemCategory")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("分类")]
        public Guid? ItemCategoryId { get; set; }
        //[Display(Name = "_Model._BaseItemMaster._Code")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("物料编码")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Code { get; set; }
        //[Display(Name = "_Model._BaseItemMaster._Name")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("物料名称")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Name { get; set; }
        [Display(Name = "_Model._BaseItemMaster._SPECS")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("规格")]
        public string SPECS { get; set; }
        [Display(Name = "_Model._BaseItemMaster._MateriaModel")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("型号")]
        public string MateriaModel { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Description")]
        [Comment("描述")]
        public string Description { get; set; }
        [Display(Name = "_Model._BaseItemMaster._StockUnit")]
        [Comment("库存单位")]
        public BaseUnit StockUnit { get; set; }
        [Display(Name = "_Model._BaseItemMaster._StockUnit")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存单位")]
        public Guid? StockUnitId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionOrg")]
        [Comment("默认生产组织")]
        public BaseOrganization ProductionOrg { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionOrg")]
        [Comment("默认生产组织")]
        public Guid? ProductionOrgId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionDept")]
        [Comment("默认生产部门")]
        public BaseDepartment ProductionDept { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionDept")]
        [Comment("默认生产部门")]
        public Guid? ProductionDeptId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Wh")]
        [Comment("默认存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Wh")]
        [Comment("默认存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._FormAttribute")]
        [Comment("形态属性")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ItemFormAttributeEnum? FormAttribute { get; set; }
        [Display(Name = "_Model._BaseItemMaster._MateriaAttribute")]
        [Comment("属性")]
        public string MateriaAttribute { get; set; }
        [Display(Name = "_Model._BaseItemMaster._GearRatio")]
        [Comment("速比")]
        public decimal? GearRatio { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Power")]
        [Comment("功率")]
        public decimal? Power { get; set; }
        [Display(Name = "_Model._BaseItemMaster._SafetyStockQty")]
        [Comment("安全库存")]
        public decimal? SafetyStockQty { get; set; }
        [Display(Name = "_Model._BaseItemMaster._FixedLT")]
        [Comment("生产周期(天)")]
        public decimal? FixedLT { get; set; }
        [Display(Name = "_Model._BaseItemMaster._BuildBatch")]
        [Comment("生产批量")]
        public int? BuildBatch { get; set; }
        [Display(Name = "_Model._BaseItemMaster._NotAnalysisQty")]
        [Comment("自动判定散单数量")]
        public int? NotAnalysisQty { get; set; }
        [Display(Name = "_Model._BaseItemMaster._AnalysisType")]
        [Comment("分析类别")]
        public BaseAnalysisType AnalysisType { get; set; }
        [Display(Name = "_Model._BaseItemMaster._AnalysisType")]
        [Comment("分析类别")]
        public Guid? AnalysisTypeId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<BaseInventory> BaseInventory_ItemMaster { get; set; }
        [InverseProperty("ItemMaster")]
        public List<PurchaseReceivementLine> PurchaseReceivementLine_ItemMaster { get; set; }
        [Display(Name = "_Model._BaseBarCode._Item")]
        [InverseProperty("Item")]
        public List<BaseBarCode> BaseBarCode_Item { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<PurchaseReturnLine> PurchaseReturnLine_ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<InventoryTransferOutDirectLine> InventoryTransferOutDirectLine_ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<InventoryTransferOutManualLine> InventoryTransferOutManualLine_ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<InventoryTransferInLine> InventoryTransferInLine_ItemMaster { get; set; }
        [Display(Name = "_Model._KnifeLifes._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<KnifeLifes> KnifeLifes_ItemMaster { get; set; }
        [Display(Name = "_Model._PrintSupplier._Item")]
        [InverseProperty("Item")]
        public List<PrintSupplier> PrintSupplier_Item { get; set; }
        [Display(Name = "_Model._PrintMO._Item")]
        [InverseProperty("Item")]
        public List<PrintMO> PrintMO_Item { get; set; }
        [Display(Name = "_Model._InventoryOtherReceivementLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<InventoryOtherReceivementLine> InventoryOtherReceivementLine_ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<InventoryStockTakingLine> InventoryStockTakingLine_ItemMaster { get; set; }
        [Display(Name = "_Model._InventoryStockTakingErpDiffLine._Item")]
        [InverseProperty("Item")]
        public List<InventoryStockTakingErpDiffLine> InventoryStockTakingErpDiffLine_Item { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        [InverseProperty("Item")]
        public List<InventoryErpDiff> InventoryErpDiff_Item { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<PurchaseOutsourcingIssueLine> PurchaseOutsourcingIssueLine_ItemMaster { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<PurchaseOutsourcingReturnLine> PurchaseOutsourcingReturnLine_ItemMaster { get; set; }
        [Display(Name = "_Model._SalesShipLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<SalesShipLine> SalesShipLine_ItemMaster { get; set; }
        [Display(Name = "_Model._SalesRMALine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<SalesRMALine> SalesRMALine_ItemMaster { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<SalesReturnReceivementLine> SalesReturnReceivementLine_ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<ProductionIssueLine> ProductionIssueLine_ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<ProductionReturnIssueLine> ProductionReturnIssueLine_ItemMaster { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ItemMaster")]
        [InverseProperty("ItemMaster")]
        public List<ProductionRcvRptLine> ProductionRcvRptLine_ItemMaster { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        [NotMapped]
        public string SyncOrg { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [NotMapped]
        public string SyncCategory { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [NotMapped]
        public string SyncUnit { get; set; }

        /// <summary>
        /// 生产组织
        /// </summary>
        [NotMapped]
        public string SyncPOrg { get; set; }

        /// <summary>
        /// 生产部门
        /// </summary>
        [NotMapped]
        public string SyncPDept { get; set; }

        /// <summary>
        /// 默认存储地点
        /// </summary>
        [NotMapped]
        public string SyncWh { get; set; }

        /// <summary>
        /// 分析类别编码
        /// </summary>
        [NotMapped]
        public string SyncAnalysisType { get; set; }
    }

}
