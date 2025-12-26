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

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 料品分类
    /// </summary>
	[Table("BaseItemCategory")]

    [Display(Name = "_Model.BaseItemCategory")]
    public class BaseItemCategory : BaseExternal
    {
        [Display(Name = "_Model._BaseItemCategory._Organization")]
        [Comment("组织")]
        public BaseOrganization Organization { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Organization")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("组织")]
        public Guid? OrganizationId { get; set; }
        //[Display(Name = "_Model._BaseItemCategory._Code")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("编码")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Code { get; set; }
        //[Display(Name = "_Model._BaseItemCategory._Name")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("分类名称")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Name { get; set; }
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        [Comment("分析类别")]
        public BaseAnalysisType AnalysisType { get; set; }
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        [Comment("分析类别")]
        public Guid? AnalysisTypeId { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Department")]
        [Comment("生产部门")]
        public BaseDepartment Department { get; set; }
        [Display(Name = "_Model._BaseItemCategory._Department")]
        [Comment("生产部门")]
        public Guid? DepartmentId { get; set; }
        [Display(Name = "_Model._BaseItemMaster._ItemCategory")]
        [InverseProperty("ItemCategory")]
        public List<BaseItemMaster> BaseItemMaster_ItemCategory { get; set; }

        #region 非数据库字段

        /// <summary>
        /// U9同步时传递的组织ID
        /// </summary>
        [NotMapped]
        public string Org { get; set; }

        [NotMapped]
        public string DeptCode { get; set;}

        [NotMapped]
        public string AnalysisTypeCode { get; set; }

        #endregion
    }

}
