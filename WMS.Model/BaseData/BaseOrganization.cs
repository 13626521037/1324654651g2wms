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
using WMS.Model.PrintManagement;
using WMS.Model.ProductionManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 组织
    /// </summary>
	[Table("BaseOrganization")]

    [Display(Name = "_Model.BaseOrganization")]
    public class BaseOrganization : BaseExternal
    {
        [Display(Name = "_Model._BaseOrganization._IsProduction")]
        [Comment("是否生产组织")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsProduction { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsSale")]
        [Comment("是否销售组织")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsSale { get; set; }
        [Display(Name = "_Model._BaseOrganization._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseOrganization._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseDepartment._Organization")]
        [InverseProperty("Organization")]
        public List<BaseDepartment> BaseDepartment_Organization { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Organization")]
        [InverseProperty("Organization")]
        public List<BaseWareHouse> BaseWareHouse_Organization { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Organization")]
        [InverseProperty("Organization")]
        public List<BaseItemCategory> BaseItemCategory_Organization { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Organization")]
        [InverseProperty("Organization")]
        public List<BaseItemMaster> BaseItemMaster_Organization { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionOrg")]
        [InverseProperty("ProductionOrg")]
        public List<BaseItemMaster> BaseItemMaster_ProductionOrg { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        [InverseProperty("Organization")]
        public List<PurchaseReceivement> PurchaseReceivement_Organization { get; set; }
        [Display(Name = "_Model._BaseSupplier._Organization")]
        [InverseProperty("Organization")]
        public List<BaseSupplier> BaseSupplier_Organization { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        [InverseProperty("Organization")]
        public List<PurchaseReturn> PurchaseReturn_Organization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        [InverseProperty("Organization")]
        public List<InventoryTransferOutDirectDocType> InventoryTransferOutDirectDocType_Organization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        [InverseProperty("TransInOrganization")]
        public List<InventoryTransferOutDirect> InventoryTransferOutDirect_TransInOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        [InverseProperty("TransOutOrganization")]
        public List<InventoryTransferOutDirect> InventoryTransferOutDirect_TransOutOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        [InverseProperty("Organization")]
        public List<InventoryTransferOutManual> InventoryTransferOutManual_Organization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        [InverseProperty("TransInOrganization")]
        public List<InventoryTransferOutManual> InventoryTransferOutManual_TransInOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        [InverseProperty("TransOutOrganization")]
        public List<InventoryTransferOutManual> InventoryTransferOutManual_TransOutOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        [InverseProperty("TransInOrganization")]
        public List<InventoryTransferIn> InventoryTransferIn_TransInOrganization { get; set; }
        [Display(Name = "_Model._PrintMO._Org")]
        [InverseProperty("Org")]
        public List<PrintMO> PrintMO_Org { get; set; }
        [Display(Name = "_Model._InventoryOtherShipDocType._Organization")]
        [InverseProperty("Organization")]
        public List<InventoryOtherShipDocType> InventoryOtherShipDocType_Organization { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        [InverseProperty("BenefitOrganization")]
        public List<InventoryOtherShip> InventoryOtherShip_BenefitOrganization { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        [InverseProperty("OwnerOrganization")]
        public List<InventoryOtherShip> InventoryOtherShip_OwnerOrganization { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        [InverseProperty("Organization")]
        public List<PurchaseOutsourcingIssue> PurchaseOutsourcingIssue_Organization { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Organization")]
        [InverseProperty("Organization")]
        public List<PurchaseOutsourcingReturn> PurchaseOutsourcingReturn_Organization { get; set; }
        [Display(Name = "_Model._SalesShip._Organization")]
        [InverseProperty("Organization")]
        public List<SalesShip> SalesShip_Organization { get; set; }
        [Display(Name = "_Model._BaseCustomer._Organization")]
        [InverseProperty("Organization")]
        public List<BaseCustomer> BaseCustomer_Organization { get; set; }
        [Display(Name = "_Model._SalesRMA._Organization")]
        [InverseProperty("Organization")]
        public List<SalesRMA> SalesRMA_Organization { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Organization")]
        [InverseProperty("Organization")]
        public List<SalesReturnReceivement> SalesReturnReceivement_Organization { get; set; }
        [Display(Name = "_Model._ProductionIssue._Organization")]
        [InverseProperty("Organization")]
        public List<ProductionIssue> ProductionIssue_Organization { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Organization")]
        [InverseProperty("Organization")]
        public List<ProductionReturnIssue> ProductionReturnIssue_Organization { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        [InverseProperty("Organization")]
        public List<ProductionRcvRpt> ProductionRcvRpt_Organization { get; set; }
    }

}
