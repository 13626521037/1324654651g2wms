using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    public class PurchaseOutsourcingReturnApprovePara
    {
        public Guid ID { get; set; }

        public List<PurchaseOutsourcingReturnApproveDetailPara> Details { get; set; }
    }

    public class PurchaseOutsourcingReturnApproveDetailPara
    {
        public Guid InventoryID { get; set; }

        public Guid LocationID { get; set; }
    }
}
