using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    /// <summary>
    /// 创建直接调出单API参数
    /// </summary>
    public class InventoryTransferOutDirectCreatePara
    {
        /// <summary>
        /// 调入组织ID
        /// </summary>
        public Guid? TransferInOrgId { get; set; }

        /// <summary>
        /// 调入存储地点ID
        /// </summary>
        public Guid? TransferInWhId { get; set; }

        /// <summary>
        /// 单据类型ID
        /// </summary>
        public Guid? DocTypeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 行信息集合
        /// </summary>
        public List<InventoryTransferOutDirectLineCreatePara> Lines { get; set; }
    }

    public class InventoryTransferOutDirectLineCreatePara
    {
        /// <summary>
        /// 库存信息ID
        /// </summary>
        public Guid? InventoryId { get; set; }

        /// <summary>
        /// 调出数量
        /// </summary>
        public decimal Qty { get; set; }
    }
}
