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
    /// 采购收货单
    /// </summary>
	[Table("PurchaseReceivement")]

    [Display(Name = "_Model.PurchaseReceivement")]
    public class PurchaseReceivement : BaseDocExternal
    {
        [Display(Name = "_Model._PurchaseReceivement._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP提交人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._SubmitDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        [Comment("供应商")]
        public BaseSupplier Supplier { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Supplier")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        [Comment("供应商")]
        public Guid? SupplierId { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._BizType")]
        [Comment("业务类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public BizTypeEnum? BizType { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._InspectStatus")]
        [Comment("检验状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReceivementInspectStatusEnum? InspectStatus { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public PurchaseReceivementStatusEnum? Status { get; set; }
        [Display(Name = "_Model._PurchaseReceivement._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._PurchaseReceivementLine._PurchaseReceivement")]
        [InverseProperty("PurchaseReceivement")]
        public List<PurchaseReceivementLine> PurchaseReceivementLine_PurchaseReceivement { get; set; }

        [NotMapped]
        public string SyncSupplier { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }
	}

}
