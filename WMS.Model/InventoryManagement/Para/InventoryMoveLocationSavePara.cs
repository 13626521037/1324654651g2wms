using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public class InventoryMoveLocationSavePara
    {
        public Guid MoveInLocationId { get; set; }

        public string Memo { get; set; }

        public List<InventoryMoveLocationLineSavePara> Lines { get; set; }
    }

    public class InventoryMoveLocationLineSavePara
    {
        public Guid InventoryId { get; set; }

        public decimal Qty { get; set; }
    }
}
