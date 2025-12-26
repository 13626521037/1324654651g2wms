using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    /// <summary>
    /// 通用收货审核操作参数
    /// </summary>
    public class ReceivingApprovePara
    {
        public Guid ID { get; set; }

        public List<ReceivingApproveDetailPara> Details { get; set; }
    }

    public class ReceivingApproveDetailPara
    {
        public Guid InventoryID { get; set; }

        public Guid LocationID { get; set; }
    }
}
