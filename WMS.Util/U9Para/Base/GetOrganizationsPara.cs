using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetOrganizationsPara : U9ApiBasePara
    {
        /// <summary>
        /// 组织编码
        /// </summary>
        public string code { get; set; }
    }
}
