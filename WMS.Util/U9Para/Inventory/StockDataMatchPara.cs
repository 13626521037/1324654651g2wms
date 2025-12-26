using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Model.InventoryManagement;

namespace WMS.Util
{
    public class StockDataMatchPara: U9ApiBasePara
    {
        public long whId { get; set; }

        public List<InventoryErpDiff>? lines { get; set; }
    }
}
