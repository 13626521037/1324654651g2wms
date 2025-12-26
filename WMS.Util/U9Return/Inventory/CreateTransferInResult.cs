using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateTransferInResult
    {
        public string ErpID { get; set; }

        public string DocType { get; set; }

        public string DocNo { get; set; }

        public List<CreateTransferInLineResult> Lines { get; set; }
    }

    public class CreateTransferInLineResult
    {
        public string ErpID { get; set; }

        public int DocLineNo { get; set; }
    }
}
