using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateMiscRcvPara : U9ApiBasePara
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        public string? org { get; set; }

        /// <summary>
        /// 是否报废
        /// </summary>
        public int scrap { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? memo { get; set; }

        /// <summary>
        /// 行信息
        /// </summary>
        public List<GetMiscRcvLinePara>? lines { get; set; }
    }

    public class GetMiscRcvLinePara
    {
        /// <summary>
        /// U9来源杂发单行ID
        /// </summary>
        public long SrcID { get; set; }

        /// <summary>
        /// 本次收货数量
        /// </summary>
        public decimal Qty { get; set; }

        [JsonIgnore]
        public string? ItemCode { get; set; }

        [JsonIgnore]
        public List<GetMiscRcvSubLinePara>? SubLines { get; set; }
    }

    /// <summary>
    /// 创建调入单扫码明细（字段均为创建“库存信息”的必要字段）
    /// </summary>
    public class GetMiscRcvSubLinePara
    {
        /// <summary>
        /// 条码ID
        /// </summary>
        //public Guid BarcodeID { get; set; }   // 来源也可能改为库存流水，所以直接使用条码具体的内容

        /// <summary>
        /// 条码料品ID
        /// </summary>
        //public Guid ItemMasterId { get; set; }

        /// <summary>
        /// 库位ID
        /// </summary>
        [JsonIgnore]
        public Guid? LocationID { get; set; }

        /// <summary>
        /// 条码批号
        /// </summary>
        public string? BatchNumber { get; set; }

        /// <summary>
        /// 条码序列号
        /// </summary>
        public string? SerialNumber { get; set; }

        public string? Seiban { get; set; }

        public string? SeibanRandom { get; set; }

        /// <summary>
        /// 完整的条码(..@..@..@..)
        /// </summary>
        public string? BarCode { get; set; }

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
