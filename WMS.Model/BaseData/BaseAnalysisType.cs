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
    /// 料品分析类别
    /// </summary>
	[Table("BaseAnalysisType")]

    [Display(Name = "_Model.BaseAnalysisType")]
    public class BaseAnalysisType : BaseExternal
    {
        //[Display(Name = "_Model._BaseAnalysisType._Code")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("编码")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Code { get; set; }
        //[Display(Name = "_Model._BaseAnalysisType._Name")]
        //[StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        //[Comment("名称")]
        //[Required(ErrorMessage = "Validate.{0}required")]
        //public string Name { get; set; }
        [Display(Name = "_Model._BaseItemCategory._AnalysisType")]
        [InverseProperty("AnalysisType")]
        public List<BaseItemCategory> BaseItemCategory_AnalysisType { get; set; }
        [Display(Name = "_Model._BaseItemMaster._AnalysisType")]
        [InverseProperty("AnalysisType")]
        public List<BaseItemMaster> BaseItemMaster_AnalysisType { get; set; }

	}

}
