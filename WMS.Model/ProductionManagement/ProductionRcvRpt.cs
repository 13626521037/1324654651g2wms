using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.ProductionManagement;

namespace WMS.Model.ProductionManagement
{
    /// <summary>
    /// 生产报工单
    /// </summary>
	[Table("ProductionRcvRpt")]

    [Display(Name = "_Model.ProductionRcvRpt")]
    public class ProductionRcvRpt : BasePoco
    {
        [Display(Name = "_Model._ProductionRcvRpt._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._DocNo")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        [Comment("订单存储地点")]
        public BaseWareHouse OrderWh { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        [Comment("订单存储地点")]
        public Guid? OrderWhId { get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ProductionRcvRptStatusEnum? Status { get; set; }
        [Display(Name = "发货类型")]
        [Comment("发货类型")]
        public WhShipTypeEnum? ShipType { get; set; }
        [Display(Name = "是否零件")]
        [Comment("是否零件")]
        public bool IsParts {  get; set; }
        [Display(Name = "_Model._ProductionRcvRpt._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._ProductionRcvRptLine._ProductionRcvRpt")]
        [InverseProperty("ProductionRcvRpt")]
        public List<ProductionRcvRptLine> ProductionRcvRptLine_ProductionRcvRpt { get; set; }

	}

}
