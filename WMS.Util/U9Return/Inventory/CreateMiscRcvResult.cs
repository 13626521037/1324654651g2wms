using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateMiscRcvResult
    {
        public string ErpID { get; set; }

        public string DocNo { get; set; }

        public List<CreateMiscRcvLineResult> Lines { get; set; }
    }

    public class CreateMiscRcvLineResult
    {
        public string ErpID { get; set; }

        public int DocLineNo { get; set; }
    }
}
