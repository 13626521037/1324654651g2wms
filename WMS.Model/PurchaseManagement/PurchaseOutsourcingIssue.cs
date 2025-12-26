using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 委外发料单
    /// </summary>
	[Table("PurchaseOutsourcingIssue")]

    [Display(Name = "_Model.PurchaseOutsourcingIssue")]
    public class PurchaseOutsourcingIssue : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统提交人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SubmitDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        [Comment("供应商")]
        public BaseSupplier Supplier { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        [Comment("供应商")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseOutsourcingIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssueLine._OutsourcingIssue")]
        [InverseProperty("OutsourcingIssue")]
        public List<PurchaseOutsourcingIssueLine> PurchaseOutsourcingIssueLine_OutsourcingIssue { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }

        [NotMapped]
        public string SyncSupplier { get; set; }
    }

}
