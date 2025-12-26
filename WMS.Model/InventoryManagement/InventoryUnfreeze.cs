using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WalkingTec.Mvvm.Core;
using System.Text.Json.Serialization;
using WMS.Model;
using WMS.Model.InventoryManagement;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 库存解冻单
    /// </summary>
	[Table("InventoryUnfreeze")]

    [Display(Name = "_Model.InventoryUnfreeze")]
    public class InventoryUnfreeze : BasePoco
    {
        [Display(Name = "_Model._InventoryUnfreeze._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryUnfreeze._Reason")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("解冻原因")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Reason { get; set; }
        [Display(Name = "_Model._InventoryUnfreeze._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryUnfreezeLine._InventoryUnfreeze")]
        [InverseProperty("InventoryUnfreeze")]
        public List<InventoryUnfreezeLine> InventoryUnfreezeLine_InventoryUnfreeze { get; set; }

	}

}
