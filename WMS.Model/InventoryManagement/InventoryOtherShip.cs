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
    /// 其它出库单
    /// </summary>
	[Table("InventoryOtherShip")]

    [Display(Name = "_Model.InventoryOtherShip")]
    public class InventoryOtherShip : BasePoco
    {
        [Display(Name = "_Model._InventoryOtherShip._ErpID")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("ERP单据ID")]
        public string ErpID { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BusinessDate")]
        [Comment("业务日期")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public DateTime? BusinessDate { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocType")]
        [Comment("单据类型")]
        public InventoryOtherShipDocType DocType { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._DocType")]
        [Comment("单据类型")]
        public Guid? DocTypeId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        [Comment("受益组织")]
        public BaseOrganization BenefitOrganization { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitOrganization")]
        [Comment("受益组织")]
        public Guid? BenefitOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        [Comment("受益部门")]
        public BaseDepartment BenefitDepartment { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        [Comment("受益部门")]
        public Guid? BenefitDepartmentId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        [Comment("受益人")]
        public BaseOperator BenefitPerson { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitPerson")]
        [Comment("受益人")]
        public Guid? BenefitPersonId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        [Comment("存储地点")]
        public BaseWareHouse Wh { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Wh")]
        [Comment("存储地点")]
        public Guid? WhId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        [Comment("货主组织")]
        public BaseOrganization OwnerOrganization { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._OwnerOrganization")]
        [Comment("货主组织")]
        public Guid? OwnerOrganizationId { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryOtherShipLine._InventoryOtherShip")]
        [InverseProperty("InventoryOtherShip")]
        public List<InventoryOtherShipLine> InventoryOtherShipLine_InventoryOtherShip { get; set; }

	}

}
