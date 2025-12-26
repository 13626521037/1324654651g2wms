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
    /// 采购退料单
    /// </summary>
	[Table("PurchaseReturn")]

    [Display(Name = "_Model.PurchaseReturn")]
    public class PurchaseReturn : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseReturn._LastUpdatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("最后修改人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string LastUpdatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseReturn._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._CreateDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "_Model._PurchaseReturn._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseReturn._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        [Comment("供应商")]
        public BaseSupplier Supplier { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Supplier")]
        [Comment("供应商")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReturnStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReturn._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._PurchaseReturnLine._PurchaseReturn")]
        [InverseProperty("PurchaseReturn")]
        public List<PurchaseReturnLine> PurchaseReturnLine_PurchaseReturn { get; set; }

        [NotMapped]
        public string SyncSupplier { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }
    }

}
