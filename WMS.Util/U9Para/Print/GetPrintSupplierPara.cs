using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetPrintSupplierPara : U9ApiBasePara
    {
        public string? docNo { get; set; }

        public string? itemCode { get; set; }
    }
}
