using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.SalesManagement
{
    public class SalesShipDataPara
    {
        public Guid ID { get; set; }

        public List<SalesShipDataLinePara> Lines { get; set; }
    }

    public class SalesShipDataLinePara
    {
        public Guid ID { get; set; }

        public List<SalesShipDataSubLinePara> SubLines { get; set; }
    }

    public class SalesShipDataSubLinePara : BasePickPara
    {
    }
}
