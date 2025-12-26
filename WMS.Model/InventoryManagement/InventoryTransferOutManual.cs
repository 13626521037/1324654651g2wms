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
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 手动调出单
    /// </summary>
	[Table("InventoryTransferOutManual")]

    [Display(Name = "_Model.InventoryTransferOutManual")]
    public class InventoryTransferOutManual : BaseDocExternal
    {
        [Display(Name = "_Model._InventoryTransferOutManual._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统提交人")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._SubmitDate")]
        [Comment("创建时间")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? SubmitDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        [Comment("调入组织")]
        public BaseOrganization TransInOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInOrganization")]
        [Comment("调入组织")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        [Comment("调入存储地点")]
        public BaseWareHouse TransInWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransInWh")]
        [Comment("调入存储地点")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        [Comment("调出组织")]
        public BaseOrganization TransOutOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutOrganization")]
        [Comment("调出组织")]
        public Guid? TransOutOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        [Comment("调出存储地点")]
        public BaseWareHouse TransOutWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._TransOutWh")]
        [Comment("调出存储地点")]
        public Guid? TransOutWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryTransferOutManualStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManual._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutManualLine._InventoryTransferOutManual")]
        [InverseProperty("InventoryTransferOutManual")]
        public List<InventoryTransferOutManualLine> InventoryTransferOutManualLine_InventoryTransferOutManual { get; set; }

        [NotMapped]
        public string SyncOrganization { get; set; }

        [NotMapped]
        public string SyncTransInOrganization { get; set; }

        [NotMapped]
        public string SyncTransInWh { get; set; }

        [NotMapped]
        public string SyncTransOutOrganization { get; set; }

        [NotMapped]
        public string SyncTransOutWh { get; set; }

    }

}
