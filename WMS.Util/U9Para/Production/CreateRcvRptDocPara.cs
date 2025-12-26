using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreateRcvRptDocPara : U9ApiBasePara
    {
        public CreateRcvRptDocDTO rcvRptDocDTOList { get; set; }
    }

    public class CreateRcvRptDocDTO
    {
        public string OrgCode { get; set; }

        public string ActualRcvTime {  get; set; }

        public List<CreateRcvRptDocLineDTO> RcvRptDocLineDTOList { get; set; }
    }

    public class CreateRcvRptDocLineDTO
    {
        public string MO_DocNo {  get; set; }

        public decimal RcvQtyByWhUOM { get; set; }

        public string Wh_Code { get; set; }
    }
}
