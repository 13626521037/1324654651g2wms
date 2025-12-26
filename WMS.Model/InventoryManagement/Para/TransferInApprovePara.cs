using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public class TransferInApprovePara
    {
        public Guid ID { get; set; }

        public List<TransferInApproveDetailPara> Details { get; set; }
    }

    public class TransferInApproveDetailPara
    {
        public Guid InventoryID { get; set; }

        public Guid LocationID { get; set; }
    }
}
