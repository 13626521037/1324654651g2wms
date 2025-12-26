using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetMiscShipForMiscRcvResult
    {
        public string DocNo { get; set; }

        public string BenefitOrgID { get; set; }

        public string BenefitOrgCode { get; set; }

        public string BenefitOrgName { get; set; }

        public string SourceOrgID { get; set; }

        public string SourceOrgCode { get; set; }

        public string SourceOrgName { get; set; }

        public string SourceWhID { get; set; }

        public string SourceWhCode { get; set; }

        public string SourceWhName { get; set; }

        public List<GetMiscShipForMiscRcvLineResult> Lines { get; set; }
    }

    public class GetMiscShipForMiscRcvLineResult
    {
        public string ID { get; set; }

        public decimal Qty { get; set; }

        public string ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }
    }
}
