using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    public class PurchaseOutsourcingIssueSaveOffPara
    {
        public Guid ID { get; set; }

        public List<PurchaseOutsourcingIssueSaveOffLinePara> Lines { get; set; }
    }

    public class PurchaseOutsourcingIssueSaveOffLinePara
    {
        public Guid ID { get; set; }

        public List<PurchaseOutsourcingIssueSaveOffSubLinePara> SubLines { get; set; }
    }

    public class PurchaseOutsourcingIssueSaveOffSubLinePara
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
