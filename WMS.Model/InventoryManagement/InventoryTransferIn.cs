using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 调入单
    /// </summary>
	[Table("InventoryTransferIn")]

    [Display(Name = "_Model.InventoryTransferIn")]
    public class InventoryTransferIn : BasePoco
    {
        [Display(Name = "_Model._InventoryTransferIn._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocType")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单据类型")]
        public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        [Comment("调入组织")]
        public BaseOrganization TransInOrganization { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInOrganization")]
        [Comment("调入组织")]
        public Guid? TransInOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        [Comment("调入存储地点")]
        public BaseWareHouse TransInWh { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransInWh")]
        [Comment("调入存储地点")]
        public Guid? TransInWhId { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOut")]
        [Comment("调出单")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public Guid? TransferOut { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._TransferOutType")]
        [Comment("调出单类型")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        public InventoryTransferOutTypeEnum? TransferOutType { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._Status")]
        [Comment("状态")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public InventoryTransferInStatusEnum? Status { get; set; }
        [Display(Name = "_Model._InventoryTransferIn._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryTransferInLine._InventoryTransferIn")]
        [InverseProperty("InventoryTransferIn")]
        public List<InventoryTransferInLine> InventoryTransferInLine_InventoryTransferIn { get; set; }

	}

}
