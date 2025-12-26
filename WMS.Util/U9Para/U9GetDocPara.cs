using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 获取U9单据标准参数
    /// </summary>
    public class U9GetDocPara : U9ApiBasePara
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string docNo { get; set; }
    }
}
