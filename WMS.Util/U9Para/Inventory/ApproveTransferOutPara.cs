using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Model.InventoryManagement;

namespace WMS.Util
{
    public class ApproveTransferOutPara : U9ApiBasePara
    {
        public string? docNo { get; set; }

        public string? orgCode { get; set; }

        public List<ApproveTransferOutLinePara>? transOutLineDTOList { get; set; }

        public ApproveTransferOutPara()
        {

        }
    }

    public class ApproveTransferOutLinePara
    {
        public int? DocLineNo { get; set; }

        public string? SeibanCode { get; set; }

        public decimal? StoreUOMQty { get; set; }

        public string? ItemInfo_ItemCode { get; set; }

        public int? NewDocLineNo { get; set; }
    }
}
