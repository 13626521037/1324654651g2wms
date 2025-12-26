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
    /// 库存盘点单（库区表）
    /// </summary>
	[Table("InventoryStockTakingAreas")]

    [Display(Name = "_Model.InventoryStockTakingAreas")]
    public class InventoryStockTakingAreas : BasePoco
    {
        [Display(Name = "_Model._InventoryStockTakingAreas._StockTaking")]
        [Comment("库存盘点单")]
        public InventoryStockTaking StockTaking { get; set; }
        [Display(Name = "_Model._InventoryStockTakingAreas._StockTaking")]
        [Required(ErrorMessage = "Validate.{0}required")]
        [Comment("库存盘点单")]
        public Guid? StockTakingId { get; set; }
        [Display(Name = "_Model._InventoryStockTakingAreas._Area")]
        [Comment("库区")]
        public BaseWhArea Area { get; set; }
        [Display(Name = "_Model._InventoryStockTakingAreas._Area")]
        [Comment("库区")]
        public Guid? AreaId { get; set; }

	}

}
