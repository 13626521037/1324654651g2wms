using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateRcvRptDocResult
    {
        public string DocNo { get; set; }

        public string ID { get; set; }

        public List<string> Lines { get; set; }
    }
}
