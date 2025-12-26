using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// U9API标准返回类型
    /// </summary>
    public class U9ApiBaseReturn
    {
        public string d { get; set; }
    }

    public class U9Return
    {
        public bool Success { get; set; }

        public string Msg { get; set; }
    }

    public class U9Return<T> : U9Return
    {
        public T? Entity { get; set; }
    }
}
