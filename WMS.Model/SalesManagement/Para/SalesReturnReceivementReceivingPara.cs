using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.SalesManagement
{
    /// <summary>
    /// 退货收货单收货操作参数
    /// </summary>
    public class SalesReturnReceivementReceivingPara
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 行集合
        /// </summary>
        public List<SalesReturnReceivementReceivingLinePara> Lines { get; set; }
    }

    public class SalesReturnReceivementReceivingLinePara
    {
        /// <summary>
        /// 行ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 收货明细子行集合
        /// </summary>
        public List<SalesReturnReceivementReceivingSubLinePara> SubLines { get; set; }
    }

    public class SalesReturnReceivementReceivingSubLinePara
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
