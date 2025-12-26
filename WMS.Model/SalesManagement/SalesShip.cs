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
    /// 出货单
    /// </summary>
	[Table("SalesShip")]

    [Display(Name = "_Model.SalesShip")]
    public class SalesShip : BaseDocExternal
    {
        [Display(Name = "_Model._SalesShip._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统提交人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._SalesShip._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._SalesShip._Organization")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._SalesShip._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._SalesShip._SubmitDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._SalesShip._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._SalesShip._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._SalesShip._Operators")]
        [StringLength(100, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("业务员")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Operators { get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        [Comment("客户")]
        public BaseCustomer Customer { get; set; }
        [Display(Name = "_Model._SalesShip._Customer")]
        [Comment("客户")]
        public Guid? CustomerId { get; set; }
        [Display(Name = "_Model._SalesShip._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public SalesShipStatusEnum? Status { get; set; }
        [Display(Name = "_Model._SalesShip._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._SalesShipLine._Ship")]
        [InverseProperty("Ship")]
        public List<SalesShipLine> SalesShipLine_Ship { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }

        [NotMapped]
        public string SyncCustomer { get; set; }
    }

}
