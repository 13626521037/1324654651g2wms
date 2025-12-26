using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetItemsPara : U9ApiBasePara
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 料品ID集合（逗号分隔）
        /// </summary>
        public string iDS { get; set; }
    }
}
