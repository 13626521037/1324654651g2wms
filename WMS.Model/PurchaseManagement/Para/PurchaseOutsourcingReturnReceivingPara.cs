using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 委外退料收货操作参数
    /// </summary>
    public class PurchaseOutsourcingReturnReceivingPara : ReceivingPara
    {
        ///// <summary>
        ///// 单据ID
        ///// </summary>
        //public Guid ID { get; set; }

        ///// <summary>
        ///// 行集合
        ///// </summary>
        //public List<PurchaseOutsourcingReturnReceivingLinePara> Lines { get; set; }
    }

    public class PurchaseOutsourcingReturnReceivingLinePara: ReceivingLinePara
    {
        ///// <summary>
        ///// 行ID
        ///// </summary>
        //public Guid ID { get; set; }

        ///// <summary>
        ///// 收货明细子行集合
        ///// </summary>
        //public List<PurchaseOutsourcingReturnReceivingSubLinePara> SubLines { get; set; }
    }

    public class PurchaseOutsourcingReturnReceivingSubLinePara: ReceivingSubLinePara
    {
        ///// <summary>
        ///// 条码ID
        ///// </summary>
        //public Guid BarcodeID { get; set; }

        ///// <summary>
        ///// 占用条码的数量
        ///// </summary>
        //public decimal BarcodeOccupyQty { get; set; }
    }
}
