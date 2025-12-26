using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 直接调出单
    /// </summary>
	[Table("InventoryTransferOutDirect")]

    [Display(Name = "_Model.InventoryTransferOutDirect")]
    public class InventoryTransferOutDirect : BasePoco
    {
        [Display(Name = "_Model._InventoryTransferOutDirect._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        [Comment("单据类型")]
        public InventoryTransferOutDirectDocType DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("单据类型")]
        public Guid? DocTypeId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        [Comment("调入组织")]
        public BaseOrganization TransInOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInOrganization")]
        [Comment("调入组织")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        [Comment("调入存储地点")]
        public BaseWareHouse TransInWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransInWh")]
        [Comment("调入存储地点")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        [Comment("调出存储地点")]
        public BaseWareHouse TransOutWh { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutWh")]
        [Comment("调出存储地点")]
        public Guid? TransOutWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        [Comment("调出组织")]
        public BaseOrganization TransOutOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._TransOutOrganization")]
        [Comment("调出组织")]
        public Guid? TransOutOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectLine._InventoryTransferOutDirect")]
        [InverseProperty("InventoryTransferOutDirect")]
        public List<InventoryTransferOutDirectLine> InventoryTransferOutDirectLine_InventoryTransferOutDirect { get; set; }

	}

}
