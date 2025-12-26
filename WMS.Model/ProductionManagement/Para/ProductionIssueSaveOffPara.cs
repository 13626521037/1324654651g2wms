using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    public class ProductionIssueSaveOffPara
    {
        public Guid ID { get; set; }

        public List<ProductionIssueSaveOffLinePara> Lines { get; set; }
    }

    public class ProductionIssueSaveOffLinePara
    {
        public Guid ID { get; set; }

        public List<ProductionIssueSaveOffSubLinePara> SubLines { get; set; }
    }

    public class ProductionIssueSaveOffSubLinePara
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
