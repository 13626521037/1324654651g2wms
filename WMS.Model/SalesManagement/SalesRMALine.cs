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
    /// SalesRMALine
    /// </summary>
	[Table("SalesRMALine")]

    [Display(Name = "_Model.SalesRMALine")]
    public class SalesRMALine : BaseDocExternal
    {
        [Display(Name = "_Model._SalesRMALine._RMAId")]
        [Comment("退回处理单")]
        public SalesRMA RMAId { get; set; }
        [Display(Name = "_Model._SalesRMALine._RMAId")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("退回处理单")]
        public Guid? RMAIdId { get; set; }
        [Display(Name = "_Model._SalesRMALine._DocLineNo")]
        [Comment("行号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public int? DocLineNo { get; set; }
        [Display(Name = "_Model._SalesRMALine._ItemMaster")]
        [Comment("料品")]
        public BaseItemMaster ItemMaster { get; set; }
        [Display(Name = "_Model._SalesRMALine._ItemMaster")]
        [Comment("料品")]
        public Guid? ItemMasterId { get; set; }
        [Display(Name = "_Model._SalesRMALine._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._SalesRMALine._Qty")]
        [Comment("数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._SalesRMALine._ToBeReceiveQty")]
        [Comment("待收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeReceiveQty { get; set; }
        [Display(Name = "_Model._SalesRMALine._ToBeInWhQty")]
        [Comment("待入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ToBeInWhQty { get; set; }
        [Display(Name = "_Model._SalesRMALine._ReceiveQty")]
        [Comment("已收货数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? ReceiveQty { get; set; }
        [Display(Name = "_Model._SalesRMALine._InWhQty")]
        [Comment("已入库数量")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public decimal? InWhQty { get; set; }
        [Display(Name = "_Model._SalesRMALine._WareHouse")]
        [Comment("存储地点")]
        public BaseWareHouse WareHouse { get; set; }
        [Display(Name = "_Model._SalesRMALine._WareHouse")]
        [Comment("存储地点")]
        public Guid? WareHouseId { get; set; }
        [Display(Name = "_Model._SalesRMALine._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SalesRMALineStatusEnum? Status { get; set; }

        [NotMapped]
        public string SyncItemMaster { get; set; }

        [NotMapped]
        public string SyncWareHouse { get; set; }
    }

}
