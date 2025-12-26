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
    /// 退回收货单行
    /// </summary>
	[Table("SalesReturnReceivementLine")]

    [Display(Name = "_Model.SalesReturnReceivementLine")]
    public class SalesReturnReceivementLine : BaseDocExternal
    {
        [Display(Name = "_Model._SalesReturnReceivementLine._ReturnReceivement")]
        [Comment("退回收货单")]
        public SalesReturnReceivement ReturnReceivement { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ReturnReceivement")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("退回收货单")]
        public Guid? ReturnReceivementId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._Qty")]
        [Comment("数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._ReceiveQty")]
        [Comment("已收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._InWhQty")]
        [Comment("已入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._SalesReturnReceivementLine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SalesReturnReceivementLineStatusEnum? Status { get; set; }
        [Display(Name = "来源单据号")]
        [Comment("来源单据号")]
        public string SrcDocNo { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }
    }

}
