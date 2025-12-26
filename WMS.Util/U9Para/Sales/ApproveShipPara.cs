using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util.U9Para
{
    public class ApproveShipPara : U9ApiBasePara
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string docNo { get; set; }

        public string orgCode { get; set; }

        public string expressNo { get; set; }

        public List<ApproveShipLinePara> shipLineDTOList { get; set; }
    }

    public class ApproveShipLinePara
    {
        public int DocLineNo { get; set; }

        public decimal SaleQty { get; set; }

        public string SeibanCode { get; set; }
    }
}
