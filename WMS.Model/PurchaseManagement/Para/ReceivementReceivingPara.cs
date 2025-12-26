using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 收货单收货操作参数
    /// </summary>
    public class ReceivementReceivingPara
    {
        /// <summary>
        /// 收货单ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 收货单收货行集合
        /// </summary>
        public List<ReceivementReceivingLinePara> Lines { get; set; }
    }

    public class ReceivementReceivingLinePara
    {
        /// <summary>
        /// 收货单行ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 收货明细子行集合
        /// </summary>
        public List<ReceivementReceivingSubLinePara> SubLines { get; set; }
    }

    public class ReceivementReceivingSubLinePara
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
