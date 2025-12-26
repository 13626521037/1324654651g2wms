using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    /// <summary>
    /// 通用收货操作参数
    /// </summary>
    public class ReceivingPara
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 行集合
        /// </summary>
        public List<ReceivingLinePara> Lines { get; set; }
    }

    public class ReceivingLinePara
    {
        /// <summary>
        /// 行ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 收货明细子行集合
        /// </summary>
        public List<ReceivingSubLinePara> SubLines { get; set; }
    }

    public class ReceivingSubLinePara
    {
        /// <summary>
        /// 条码ID
        /// </summary>
        public Guid BarcodeID { get; set; }

        /// <summary>
        /// 占用条码的数量
        /// </summary>
        public decimal BarcodeOccupyQty { get; set; }
    }
}
