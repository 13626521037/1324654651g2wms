using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.SalesManagement
{
    public class SalesReturnReceivementApprovePara
    {
        public Guid ID { get; set; }

        public List<SalesReturnReceivementApproveDetailPara> Details { get; set; }
    }

    public class SalesReturnReceivementApproveDetailPara
    {
        public Guid InventoryID { get; set; }

        public Guid LocationID { get; set; }
    }
}
