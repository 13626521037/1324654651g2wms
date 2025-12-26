using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    public class InventoryTransferOutManualSaveOffPara
    {
        public Guid ID { get; set; }

        public List<InventoryTransferOutManualSaveOffLinePara> Lines { get; set; }
    }

    public class InventoryTransferOutManualSaveOffLinePara
    {
        public Guid ID { get; set; }

        public List<InventoryTransferOutManualSaveOffSubLinePara> SubLines { get; set; }
    }

    public class InventoryTransferOutManualSaveOffSubLinePara
    {
        /// <summary>
        /// 库存信息ID
        /// </summary>
        public Guid InvID { get; set; }

        /// <summary>
        /// 下架数量
        /// </summary>
        public decimal OffQty { get; set; }
    }
}
