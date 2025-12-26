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

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 番号客户对照关系
    /// </summary>
	[Table("BaseSeibanCustomerRelation")]

    [Display(Name = "_Model.BaseSeibanCustomerRelation")]
    public class BaseSeibanCustomerRelation : BaseExternal
    {
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Customer")]
        [Comment("客户")]
        public BaseCustomer Customer { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Customer")]
        [Comment("客户")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._RandomCode")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("随机码")]
        public string RandomCode { get; set; }
        [Display(Name = "_Model._BaseSeibanCustomerRelation._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }

        [NotMapped]
        public string SyncCustomer { get; set; }
    }

}
