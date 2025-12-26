using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.ProductionManagement
{
    public class ProductionRcvRptMO
    {
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string MoDocNo { get; set; }

        /// <summary>
        /// U9料品ID
        /// </summary>
        public string SyncItem { get; set; }

        /// <summary>
        /// 料品ID
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string ItemSPECS { get; set; }

        /// <summary>
        /// 番号
        /// </summary>
        public string Seiban { get; set; }

        /// <summary>
        /// U9完工存储地点ID
        /// </summary>
        public string SyncWh { get; set; }

        public Guid OrgId { get; set; }

        /// <summary>
        /// 完工存储地点ID
        /// </summary>
        public Guid WhId { get; set; }

        /// <summary>
        /// 完工存储地点编码
        /// </summary>
        public string WhCode { get; set; }

        /// <summary>
        /// 完工存储地点名称
        /// </summary>
        public string WhName { get; set; }

        /// <summary>
        /// U9订单存储地点ID
        /// </summary>
        public string SyncOrderWh { get; set; }

        /// <summary>
        /// 订单存储地点ID
        /// </summary>
        public Guid? OrderWhId { get; set; }

        /// <summary>
        /// 订单存储地点编码
        /// </summary>
        public string OrderWhCode { get; set; }

        /// <summary>
        /// 订单存储地点名称
        /// </summary>
        public string OrderWhName { get; set; }

        /// <summary>
        /// 发货类型
        /// </summary>
        public string ShipType { get; set; }

        /// <summary>
        /// 成品入库单单据类型（根据“推式生单配置”）[弃用字段，原来是根据此字段判断是否允许一起入库。现改为根据生产订单单据类型判断]
        /// </summary>
        public string RcvRptDocType { get; set; }

        /// <summary>
        /// 生产订单单据类型
        /// </summary>
        public string MODocType { get; set; }

        /// <summary>
        /// 生产订单数量
        /// </summary>
        public decimal MOQty { get; set; }

        /// <summary>
        /// 已入库数量
        /// </summary>
        public decimal ReceivedQty { get; set; }

        /// <summary>
        /// 待入库数量
        /// </summary>
        public decimal ToBeRcvQty
        {
            get
            {
                return MOQty - ReceivedQty;
            }
        }
    }
}
