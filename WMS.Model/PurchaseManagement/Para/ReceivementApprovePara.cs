using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    public class ReceivementApprovePara
    {
        public Guid ID { get; set; }

        public List<ReceivementApproveDetailPara> Details { get; set; }
    }

    public class ReceivementApproveDetailPara
    {
        public Guid InventoryID { get; set; }

        public Guid LocationID { get; set; }
    }
}
