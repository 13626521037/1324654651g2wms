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

namespace WMS.Model.SalesManagement
{
    /// <summary>
    /// 退回处理
    /// </summary>
	[Table("SalesRMA")]

    [Display(Name = "_Model.SalesRMA")]
    public class SalesRMA : BaseDocExternal
    {
        [Display(Name = "_Model._SalesRMA._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统创建人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesRMA._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._SalesRMA._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._SalesRMA._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._SalesRMA._ApproveDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? ApproveDate { get; set; }
        [Display(Name = "_Model._SalesRMA._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._SalesRMA._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesRMA._Operators")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("业务员")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        [Comment("客户")]
        public BaseCustomer Customer { get; set; }
        [Display(Name = "_Model._SalesRMA._Customer")]
        [Comment("客户")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._SalesRMA._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SalesRMAStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesRMA._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._SalesRMALine._RMAId")]
        [InverseProperty("RMAId")]
        public List<SalesRMALine> SalesRMALine_RMAId { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }

        [NotMapped]
        public string SyncCustomer { get; set; }
    }

}
