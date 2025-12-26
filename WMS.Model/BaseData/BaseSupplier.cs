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
using WMS.Model.PrintManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 供应商
    /// </summary>
	[Table("BaseSupplier")]

    [Display(Name = "_Model.BaseSupplier")]
    public class BaseSupplier : BaseExternal
    {
        [Display(Name = "_Model._BaseSupplier._Code")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("供应商编码")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Code { get; set; }
        [Display(Name = "_Model._BaseSupplier._Name")]
        [StringLength(100, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("供应商名称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Name { get; set; }
        [Display(Name = "_Model._BaseSupplier._ShortName")]
        [StringLength(100, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("供应商简称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ShortName { get; set; }
        [Display(Name = "_Model._BaseSupplier._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseSupplier._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseSupplier._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseSupplier._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        [InverseProperty("Supplier")]
        public List<PurchaseReceivement> PurchaseReceivement_Supplier { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        [InverseProperty("Supplier")]
        public List<PurchaseReturn> PurchaseReturn_Supplier { get; set; }
        [Display(Name = "_Model._PrintSupplier._Supplier")]
        [InverseProperty("Supplier")]
        public List<PrintSupplier> PrintSupplier_Supplier { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        [InverseProperty("Supplier")]
        public List<PurchaseOutsourcingIssue> PurchaseOutsourcingIssue_Supplier { get; set; }
        [Display(Name = "_Model._PurchaseOutsourcingReturn._Supplier")]
        [InverseProperty("Supplier")]
        public List<PurchaseOutsourcingReturn> PurchaseOutsourcingReturn_Supplier { get; set; }
    }

}
