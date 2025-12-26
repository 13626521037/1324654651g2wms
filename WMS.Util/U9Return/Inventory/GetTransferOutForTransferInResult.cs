using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetTransferOutForTransferInResult
    {
        public string DocNo { get; set; }

        public string TransferInOrg { get; set; }

        public string TransferInWh { get; set; }

        /// <summary>
        /// 是否从G2调出
        /// </summary>
        public bool IsG2TransferOut { get; set; }

        public List<GetTransferOutForTransferInLineResult> Lines { get; set; }
    }

    public class GetTransferOutForTransferInLineResult
    {
        public int DocLineNo { get; set; }

        public string ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal Qty { get; set; }

        public List<GetTransferOutForTransferInSubLineResult> SubLines { get; set; }
    }

    /// <summary>
    /// 对应调出单中的扫码明细
    /// </summary>
    public class GetTransferOutForTransferInSubLineResult
    {
        public string BatchNumber { get; set; }

        public string Seiban { get; set; }

        public string SeibanRandom { get; set; }

        public string BarCode { get; set; }

        public decimal BarcodeOccupyQty { get; set; }
    }
}
