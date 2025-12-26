using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public enum StockTakeScanTypeEnum
    {
        /// <summary>
        /// 库存
        /// </summary>
        Inventory = 0,
        /// <summary>
        /// 刀具台账
        /// </summary>
        Knife = 1,
        /// <summary>
        /// 条码表
        /// </summary>
        BarCode = 2,
    }
    public class StockTakeScanReturn
    {
        /// <summary>
        /// 扫描类型
        /// </summary>
        public StockTakeScanTypeEnum Type { get; set; }

        /// <summary>
        /// 库存ID或者刀具台账ID（不存条码表ID，无更新条码表操作）
        /// </summary>
        public Guid? ID { get; set; }

        public Guid? StockTakeLineID { get; set; }

        public Guid? LocationID { get; set; }

        public string LocationName { get; set; }

        public string LocationCode { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemName { get; set; }

        public string ItemCode { get; set; }

        public string ItemSpec { get; set; }

        public string Batch { get; set; }

        public string Seiban { get; set; }

        public string SerialNumber { get; set; }

        public decimal Qty { get; set; }

        public decimal StockTakingQty { get; set; }

        public bool IsNew { get; set; }

        /// <summary>
        /// 是否再次扫描（已盘过）
        /// </summary>
        public bool IsRescan { get; set; }
    }
}
