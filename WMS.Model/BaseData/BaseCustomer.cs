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
using WMS.Model.SalesManagement;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 客户
    /// </summary>
	[Table("BaseCustomer")]

    [Display(Name = "_Model.BaseCustomer")]
    public class BaseCustomer : BaseExternal
    {
        [Display(Name = "_Model._BaseCustomer._ShortName")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("客户简称")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string ShortName { get; set; }
        [Display(Name = "_Model._BaseCustomer._EnglishShortName")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("英文缩写")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string EnglishShortName { get; set; }
        [Display(Name = "_Model._BaseCustomer._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseCustomer._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._BaseCustomer._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseCustomer._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        [InverseProperty("Customer")]
        public List<SalesShip> SalesShip_Customer { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Customer")]
        [InverseProperty("Customer")]
        public List<BaseSeibanCustomerRelation> BaseSeibanCustomerRelation_Customer { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        [InverseProperty("Customer")]
        public List<SalesRMA> SalesRMA_Customer { get; set; }
        [Display(Name = "_Model._SalesReturnReceivement._Customer")]
        [InverseProperty("Customer")]
        public List<SalesReturnReceivement> SalesReturnReceivement_Customer { get; set; }
    }

}
