using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    public class PurchaseReturnDataPara
    {
        public Guid ID { get; set; }

        public List<PurchaseReturnDataLinePara> Lines { get; set; }
    }

    public class PurchaseReturnDataLinePara
    {
        public Guid ID { get; set; }

        public List<PurchaseReturnDataSubLinePara> SubLines { get; set; }
    }

    public class PurchaseReturnDataSubLinePara: BasePickPara
    {
    }
}
