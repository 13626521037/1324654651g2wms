using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateInventorySheetPara : U9ApiBasePara
    {
        public string? org { get; set; }

        public string? inventorySheetDocType { get; set; }

        public string? businessDate { get; set; }

        public string? wh { get; set; }

        public List<CreateInventorySheetLinePara>? inventSheetLine { get; set; }

        public int allSeinban { get; set; }
    }

    public class CreateInventorySheetLinePara
    {
        public string? ItemInfo_ItemCode { get; set; }

        /// <summary>
        /// 不用传。由U9端自动判定单位
        /// </summary>
        public string? StoreUOM { get; set; }

        /// <summary>
        /// 不用传。无用参数
        /// </summary>
        public decimal AccountQtyCU { get; set; }

        public string? Seiban { get; set; }

        public decimal CheckingQtyCU { get; set; }
    }
}
