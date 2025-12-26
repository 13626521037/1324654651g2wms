using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public class InventoryStockTakingLineReturn
    {
        public Guid? ID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string ItemSPECS { get; set; }

        public string SerialNumber { get; set; }

        public string BatchNumber { get; set; }

        public string Seiban { get; set; }

        public string LocationCode { get; set; }

        public bool? IsNew { get; set; }

        public decimal? Qty { get; set; }

        public decimal? StockTakingQty { get; set; }

        public bool? IsKnifeLedger { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }
    }
}
