using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateTransferInPara : U9ApiBasePara
    {
        public CreateTransferInHeadPara transferInDTOList { get; set; }
    }

    public class CreateTransferInHeadPara
    {
        public string BusinessDate { get; set; }

        public int SrcDocType { get; set; }

        public string OrgCode { get; set; }

        public List<CreateTransferInLinePara> TransferInLineDTOList { get; set; }

        public string Memo { get; set; }

        [JsonIgnore]
        public Guid? OrgId { get; set; }
    }

    public class CreateTransferInLinePara
    {
        public decimal StoreUOMQty { get; set; }

        public string TransInWh_Code { get; set; }

        public string SrcDocNo { get; set; }

        public int? SrcDocLineNo { get; set; }

        [JsonIgnore]
        public List<CreateTransferInSubLinePara> SubLines { get; set; }

        [JsonIgnore]
        public string ItemCode { get; set; }

        [JsonIgnore]
        public Guid? TransInWhId { get; set; }
    }

    /// <summary>
    /// 创建调入单扫码明细（字段均为创建“库存信息”的必要字段）
    /// </summary>
    public class CreateTransferInSubLinePara
    {
        /// <summary>
        /// 条码ID
        /// </summary>
        //public Guid BarcodeID { get; set; }   // 来源也可能是库存流水，所以直接使用条码具体的内容

        /// <summary>
        /// 条码料品ID
        /// </summary>
        //public Guid ItemMasterId { get; set; }

        /// <summary>
        /// 条码批号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 条码序列号
        /// </summary>
        public string SerialNumber { get; set; }

        public string Seiban { get; set; }

        public string SeibanRandom { get; set; }

        /// <summary>
        /// 完整的条码(..@..@..@..)
        /// </summary>
        public string BarCode { get; set; }

        /// <summary>
        /// 条码数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 占用条码的数量
        /// </summary>
        public decimal BarcodeOccupyQty { get; set; }
    }
}
