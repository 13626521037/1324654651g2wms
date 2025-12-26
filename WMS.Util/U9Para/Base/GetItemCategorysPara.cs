using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetItemCategorysPara : U9ApiBasePara
    {
        /// <summary>
        /// 料品分类编码
        /// </summary>
        public string code { get; set; }
    }
}
