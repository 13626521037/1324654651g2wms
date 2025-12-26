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
using WMS.Model.ProductionManagement;

namespace WMS.Model.ProductionManagement
{
    /// <summary>
    /// 生产退料单
    /// </summary>
	[Table("ProductionReturnIssue")]

    [Display(Name = "_Model.ProductionReturnIssue")]
    public class ProductionReturnIssue : BaseDocExternal
    {
        [Display(Name = "_Model._ProductionReturnIssue._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统提交人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._SubmitDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public ProductionReturnIssueStatusEnum? Status { get; set; }
        [Display(Name = "_Model._ProductionReturnIssue._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._ProductionReturnIssueLine._ProductionReturnIssue")]
        [InverseProperty("ProductionReturnIssue")]
        public List<ProductionReturnIssueLine> ProductionReturnIssueLine_ProductionReturnIssue { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }
    }

}
