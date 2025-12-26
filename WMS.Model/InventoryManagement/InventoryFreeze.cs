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
    /// 库存冻结单
    /// </summary>
	[Table("InventoryFreeze")]

    [Display(Name = "_Model.InventoryFreeze")]
    public class InventoryFreeze : BasePoco
    {
        [Display(Name = "_Model._InventoryFreeze._DocNo")]
        [StringLength(50, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("单号")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string DocNo { get; set; }
        [Display(Name = "_Model._InventoryFreeze._Reason")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("冻结原因")]
        [Required(ErrorMessage = "Validate.{0}required")]
        public string Reason { get; set; }
        [Display(Name = "_Model._InventoryFreeze._Memo")]
        [StringLength(500, ErrorMessage = "Validate.{0}stringmax{1}")]
        [Comment("备注")]
        public string Memo { get; set; }
        [Display(Name = "_Model._InventoryFreezeLine._InventoryFreeze")]
        [InverseProperty("InventoryFreeze")]
        public List<InventoryFreezeLine> InventoryFreezeLine_InventoryFreeze { get; set; }

	}

}
