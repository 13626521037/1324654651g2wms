using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.SalesManagement;
using WMS.Model.BaseData;

namespace WMS.Model.SalesManagement
{
    /// <summary>
    /// 出货单行
    /// </summary>
	[Table("SalesShipLine")]

    [Display(Name = "_Model.SalesShipLine")]
    public class SalesShipLine : BaseDocExternal
    {
        [Display(Name = "_Model._SalesShipLine._Ship")]
        [Comment("出货单")]
        public SalesShip Ship { get; set; }
        [Display(Name = "_Model._SalesShipLine._Ship")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("出货单")]
        public Guid? ShipId { get; set; }
        [Display(Name = "_Model._SalesShipLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._SalesShipLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._SalesShipLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._SalesShipLine._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._SalesShipLine._Qty")]
        [Comment("数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._SalesShipLine._ToBeOffQty")]
        [Comment("待下架数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeOffQty { get; set; }
        [Display(Name = "_Model._SalesShipLine._ToBeShipQty")]
        [Comment("待出货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeShipQty { get; set; }
        [Display(Name = "_Model._SalesShipLine._OffQty")]
        [Comment("已下架数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? OffQty { get; set; }
        [Display(Name = "_Model._SalesShipLine._ShippedQty")]
        [Comment("已出货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ShippedQty { get; set; }
        [Display(Name = "_Model._SalesShipLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._SalesShipLine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._SalesShipLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SalesShipLineStatusEnum? Status { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }
    }

}
