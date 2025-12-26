using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 委外退料单行
    /// </summary>
	[Table("PurchaseOutsourcingReturnLine")]

    [Display(Name = "_Model.PurchaseOutsourcingReturnLine")]
    public class PurchaseOutsourcingReturnLine : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._OutsourcingReturn")]
        [Comment("委外退料单")]
        public PurchaseOutsourcingReturn OutsourcingReturn { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._OutsourcingReturn")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("委外退料单")]
        public Guid? OutsourcingReturnId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._Qty")]
        [Comment("数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._ReceiveQty")]
        [Comment("已收货数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._InWhQty")]
        [Comment("已入库数量")]
        [Precision(18,5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturnLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseOutsourcingReturnLineStatusEnum? Status { get; set; }
        //[Display(Name = "来源单据号")]
        //[Comment("来源单据号")]
        //public string SrcDocNo { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }
    }

}
