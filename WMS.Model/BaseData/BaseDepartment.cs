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

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 部门
    /// </summary>
	[Table("BaseDepartment")]

    [Display(Name = "_Model.BaseDepartment")]
    public class BaseDepartment : BaseExternal
    {
        [Display(Name = "_Model._BaseDepartment._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseDepartment._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        [Display(Name = "生产相关")]
        [Comment("生产相关")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public bool? IsMFG { get; set; }
        [Display(Name = "_Model._BaseDepartment._IsEffective")]
        [Comment("是否生效")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public EffectiveEnum? IsEffective { get; set; }
        [Display(Name = "_Model._BaseDepartment._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Department")]
        [InverseProperty("Department")]
        public List<BaseItemCategory> BaseItemCategory_Department { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ProductionDept")]
        [InverseProperty("ProductionDept")]
        public List<BaseItemMaster> BaseItemMaster_ProductionDept { get; set; }
        [Display(Name = "_Model._BaseOperator._Department")]
        [InverseProperty("Department")]
        public List<BaseOperator> BaseOperator_Department { get; set; }
        [Display(Name = "_Model._InventoryOtherShip._BenefitDepartment")]
        [InverseProperty("BenefitDepartment")]
        public List<InventoryOtherShip> InventoryOtherShip_BenefitDepartment { get; set; }

        #region 非数据库字段

        /// <summary>
        /// U9同步时传递的组织ID
        /// </summary>
        [NotMapped]
        public string Org { get; set; }

        #endregion
    }

}
