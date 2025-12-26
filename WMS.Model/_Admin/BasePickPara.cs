using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    /// <summary>
    /// 下架操作的参数基类
    /// </summary>
    public class BasePickPara
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
