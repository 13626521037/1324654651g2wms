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
    /// 采购收货单行
    /// </summary>
	[Table("PurchaseReceivementLine")]

    [Display(Name = "_Model.PurchaseReceivementLine")]
    public class PurchaseReceivementLine : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseReceivementLine._PurchaseReceivement")]
        [Comment("采购收货单")]
        public PurchaseReceivement PurchaseReceivement { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._PurchaseReceivement")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("采购收货单")]
        public Guid? PurchaseReceivementId { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ItemMaster")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._QualifiedQty")]
        [Comment("合格数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? QualifiedQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._UnqualifiedRejectQty")]
        [Comment("不合格数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? UnqualifiedRejectQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._UnqualifiedAcceptQty")]
        [Comment("不合格接收数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? UnqualifiedAcceptQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ConcessionAcceptQty")]
        [Comment("让步接收数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ConcessionAcceptQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ToBeInspectQty")]
        [Comment("待检验数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInspectQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._InspectQty")]
        [Comment("已检验数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InspectQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._ReceiveQty")]
        [Comment("已收货数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._InWhQty")]
        [Comment("已入库数量")]
        [Precision(18, 5)]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._UnqualifiedRejectDeal")]
        [Comment("不合格退料处理方式")]
        public QCRejectDealEnum? UnqualifiedRejectDeal { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._UnqualifiedAcceptDeal")]
        [Comment("不合格接收处理方式")]
        public QCRejectDealEnum? UnqualifiedAcceptDeal { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._WareHouse")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReceivementLineStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._InspectStatus")]
        [Comment("检验状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReceivementLineInspectStatusEnum? InspectStatus { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._Inspector")]
        [Comment("检验员")]
        [NotMapped]
        public FrameworkUser Inspector { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._Inspector")]
        [Comment("检验员")]
        public string InspectorId { get; set; }
        [Display(Name = "来源单据号")]
        [Comment("来源单据号")]
        public string SrcDocNo { get; set; }
        [Display(Name = "跳过检验")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? NoInspect { get; set; }
        [Timestamp] // 使用Timestamp特性标记为并发令牌
        public byte[] RowVersion { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }
	}

}
