using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.Model.PrintManagement
{
    /// <summary>
    /// 生产打印
    /// </summary>
	[Table("PrintMO")]

    [Display(Name = "_Model.PrintMO")]
    public class PrintMO : BasePoco
    {
        [Display(Name = "_Model._PrintMO._CustomerCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("客户编码")]
        public string CustomerCode { get; set; }
        [Display(Name = "_Model._PrintMO._OrderWhName")]
        [StringLength(100, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("订单存储地点")]
        public string OrderWhName { get; set; }
        [Display(Name = "_Model._PrintMO._CustomerSPECS")]
        [Comment("客户规格型号")]
        public string CustomerSPECS { get; set; }
        [Display(Name = "_Model._PrintMO._Item")]
        [Comment("料品")]
        public BaseItemMaster Item { get; set; }
        [Display(Name = "_Model._PrintMO._Item")]
        [Comment("料品")]
        public Guid? ItemId { get; set; }
        [Display(Name = "_Model._PrintMO._Seiban")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号")]
        public string Seiban { get; set; }
        [Display(Name = "_Model._PrintMO._SeibanRandom")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("番号随机码")]
        public string SeibanRandom { get; set; }
        [Display(Name = "_Model._PrintMO._BatchNumber")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("批号")]
        public string BatchNumber { get; set; }
        [Display(Name = "_Model._PrintMO._Qty")]
        [Comment("数量")]
        [Precision(18, 5)]
        public decimal? Qty { get; set; }
        [Display(Name = "_Model._PrintMO._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PrintMO._DocNoChange")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号转换")]
        public string DocNoChange { get; set; }
        [Display(Name = "_Model._PrintMO._LocationName")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("地点")]
        public string LocationName { get; set; }
        [Display(Name = "_Model._PrintMO._Org")]
        [Comment("组织")]
        public BaseOrganization Org { get; set; }
        [Display(Name = "_Model._PrintMO._Org")]
        [Comment("组织")]
        public Guid? OrgId { get; set; }
        [Display(Name = "_Model._PrintMO._SyncItem")]
        [Comment("物料同步字段")]
        [NotMapped]
        public string SyncItem { get; set; }
        [Display(Name = "_Model._PrintMO._SyncOrg")]
        [Comment("组织同步字段")]
        [NotMapped]
        public string SyncOrg { get; set; }

        [Display(Name = "打印总数")]
        [NotMapped]
        public decimal? PrintQty { get; set; }

        [Display(Name = "装箱数")]
        [NotMapped]
        public decimal? PackingQty { get; set; }
    }

}
