using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public class InventorySplitSaveReturn
    {
        public string NewBarCode { get; set; }

        public string OldBarCode { get; set; }

        /// <summary>
        /// 新库存ID（给下架自动拆分功能使用）
        /// </summary>
        public Guid NewInvId { get; set; }

        /// <summary>
        /// 旧库存ID（给下架自动拆分功能使用）
        /// </summary>
        public Guid OldInvId { get; set; }

        public decimal OldQty { get; set; }

        public decimal NewQty { get; set; }
    }
}
