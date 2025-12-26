using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;

namespace WMS.Model.BaseData
{
    /// <summary>
    /// 料品临时表
    /// </summary>
	[Table("BaseItemMasterTemp")]

    [Display(Name = "_Model.BaseItemMasterTemp")]
    public class BaseItemMasterTemp : BasePoco
    {
        [Display(Name = "U9ID")]
        public string U9ID { get; set; }
        [Display(Name = "_Model._BaseItemMasterTemp._ModifiedOn")]
        [Comment("最后修改时间")]
        public DateTime? ModifiedOn { get; set; }

	}

}
