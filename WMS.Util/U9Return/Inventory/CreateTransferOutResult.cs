using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateTransferOutResult
    {
        public string? DocNo { get; set; }

        public long ID { get; set; }

        public List<CreateTransferOutLineResult>? Lines { get; set; }
    }

    public class CreateTransferOutLineResult
    {
        public long ID { get; set; }

        public int DocLineNo { get; set; }
    }
}
