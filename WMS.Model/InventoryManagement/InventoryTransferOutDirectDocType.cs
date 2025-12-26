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
    /// 直接调出单单据类型
    /// </summary>
	[Table("InventoryTransferOutDirectDocType")]

    [Display(Name = "_Model.InventoryTransferOutDirectDocType")]
    public class InventoryTransferOutDirectDocType : BaseExternal
    {
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._CreatePerson")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP系统修改人")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string CreatePerson { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "是否生效")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        //[Display(Name = "_Model._InventoryTransferOutDirectDocType._DocNo")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("单据编码")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string DocNo { get; set; }
        //[Display(Name = "_Model._InventoryTransferOutDirectDocType._DocType")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("单据名称")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string DocType { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirectDocType._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryTransferOutDirect._DocType")]
        [InverseProperty("DocType")]
        public List<InventoryTransferOutDirect> InventoryTransferOutDirect_DocType { get; set; }

	}

}
