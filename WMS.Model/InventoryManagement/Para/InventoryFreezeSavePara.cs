using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 冻结、解冻单保存操作参数实体
    /// </summary>
    public class InventoryFreezeSavePara
    {
        public string Reason { get; set; }

        public string Memo { get; set; }

        public List<InventoryFreezeLineSavePara> InventoryFreezeLines { get; set; }
    }

    public class InventoryFreezeLineSavePara
    {
        public Guid InventoryId { get; set; }

        public decimal Qty { get; set; }

        public Guid? PalletId { get; set; }

        public int PalletVersion { get; set; }

        public string Memo { get; set; }
    }
}
