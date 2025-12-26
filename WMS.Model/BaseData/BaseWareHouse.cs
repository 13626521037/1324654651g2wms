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
using WMS.Model.ProductionManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 存储地点
    /// </summary>
	[Table("BaseWareHouse")]

    [Display(Name = "_Model.BaseWareHouse")]
    public class BaseWareHouse : BaseExternal
    {
        [Display(Name = "_Model._BaseWareHouse._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsProduct")]
        [Comment("是否成品仓库")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsProduct { get; set; }
        [Display(Name = "_Model._BaseWareHouse._ShipType")]
        [Comment("发货属性")]
        public WhShipTypeEnum? ShipType { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsStacking")]
        [Comment("是否上架")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsStacking { get; set; }
        [Display(Name = "_Model._BaseWareHouse._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseWareHouse._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseWhArea._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<BaseWhArea> BaseWhArea_WareHouse { get; set; }
        [Display(Name = "_Model._BaseItemMaster._Wh")]
        [InverseProperty("Wh")]
        public List<BaseItemMaster> BaseItemMaster_Wh { get; set; }
        [Display(Name = "_Model._BaseUserWhRelation._Wh")]
        [InverseProperty("Wh")]
        public List<BaseUserWhRelation> BaseUserWhRelation_Wh { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<PurchaseReceivementLine> PurchaseReceivementLine_WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<PurchaseReturnLine> PurchaseReturnLine_WareHouse { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        [InverseProperty("TransInWh")]
        public List<InventoryTransferOutDirect> InventoryTransferOutDirect_TransInWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        [InverseProperty("TransOutWh")]
        public List<InventoryTransferOutDirect> InventoryTransferOutDirect_TransOutWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        [InverseProperty("TransInWh")]
        public List<InventoryTransferOutManual> InventoryTransferOutManual_TransInWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        [InverseProperty("TransOutWh")]
        public List<InventoryTransferOutManual> InventoryTransferOutManual_TransOutWh { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        [InverseProperty("TransInWh")]
        public List<InventoryTransferIn> InventoryTransferIn_TransInWh { get; set; }
        [Display(Name = "_Model._KnifeCheckOut._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeCheckOut> KnifeCheckOut_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeScrap._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeScrap> KnifeScrap_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeCheckIn._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeCheckIn> KnifeCheckIn_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeGrindRequest._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeGrindRequest> KnifeGrindRequest_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeGrindOut._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeGrindOut> KnifeGrindOut_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeGrindIn._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeGrindIn> KnifeGrindIn_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeCombine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeCombine> KnifeCombine_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._FromWH")]
        [InverseProperty("FromWH")]
        public List<KnifeTransferIn> KnifeTransferIn_FromWH { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._ToWH")]
        [InverseProperty("ToWH")]
        public List<KnifeTransferIn> KnifeTransferIn_ToWH { get; set; }
        [Display(Name = "_Model._KnifeTransferIn._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeTransferIn> KnifeTransferIn_WareHouse { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._ToWh")]
        [InverseProperty("ToWh")]
        public List<KnifeTransferOut> KnifeTransferOut_ToWh { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._FromWH")]
        [InverseProperty("FromWH")]
        public List<KnifeTransferOut> KnifeTransferOut_FromWH { get; set; }
        [Display(Name = "_Model._KnifeTransferOut._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<KnifeTransferOut> KnifeTransferOut_WareHouse { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        [InverseProperty("Wh")]
        public List<InventoryOtherShip> InventoryOtherShip_Wh { get; set; }
        [Display(Name = "_Model._InventoryStockTaking._Wh")]
        [InverseProperty("Wh")]
        public List<InventoryStockTaking> InventoryStockTaking_Wh { get; set; }
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        [InverseProperty("Wh")]
        public List<InventoryErpDiff> InventoryErpDiff_Wh { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<PurchaseOutsourcingIssueLine> PurchaseOutsourcingIssueLine_WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<PurchaseOutsourcingReturnLine> PurchaseOutsourcingReturnLine_WareHouse { get; set; }
        [Display(Name = "_Model._SalesShipLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<SalesShipLine> SalesShipLine_WareHouse { get; set; }
        [Display(Name = "_Model._SalesRMALine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<SalesRMALine> SalesRMALine_WareHouse { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._WareHouse")]
        [InverseProperty("WareHouse")]
        public List<SalesReturnReceivementLine> SalesReturnReceivementLine_WareHouse { get; set; }
        [Display(Name = "_Model._ProductionIssueLine._Wh")]
        [InverseProperty("Wh")]
        public List<ProductionIssueLine> ProductionIssueLine_Wh { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._Wh")]
        [InverseProperty("Wh")]
        public List<ProductionReturnIssueLine> ProductionReturnIssueLine_Wh { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        [InverseProperty("Wh")]
        public List<ProductionRcvRpt> ProductionRcvRpt_Wh { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        [InverseProperty("OrderWh")]
        public List<ProductionRcvRpt> ProductionRcvRpt_OrderWh { get; set; }
        /// <summary>
        /// U9同步时传递的组织ID
        /// </summary>
        [NotMapped]
        public string Org { get; set; }
    }

}
