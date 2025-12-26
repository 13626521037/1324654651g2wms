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
    /// InventoryStockTakingLocations
    /// </summary>
	[Table("InventoryStockTakingLocationss")]

    [Display(Name = "_Model.InventoryStockTakingLocations")]
    public class InventoryStockTakingLocations : BasePoco
    {
        [Display(Name = "_Model._InventoryStockTakingLocations._StockTaking")]
        [Comment("库存盘点单")]
        public InventoryStockTaking StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLocations._StockTaking")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存盘点单")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "序号")]
        [Comment("序号")]
        public int? LineNum { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLocations._Location")]
        [Comment("库位")]
        public BaseWhLocation Location { get; set; }
        [Display(Name = "_Model._InventoryStockTakingLocations._Location")]
        [Comment("库位")]
        public Guid? LocationId { get; set; }

	}

}
