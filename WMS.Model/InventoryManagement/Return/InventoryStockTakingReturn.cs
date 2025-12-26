using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace WMS.Model.InventoryManagement
{
    public class InventoryStockTakingReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public Guid? WhId { get; set; }

        public string WhName { get; set; }

        public string WhCode { get; set; }

        public DateTime? CreateTime { get; set; }

        public InventoryStockTakingReturn(InventoryStockTaking doc)
        {
            if (doc!= null)
            {
                ID = doc.ID;
                DocNo = doc.DocNo;
                Status = doc.Status.GetEnumDisplayName();
                StatusValue = (int)doc.Status;
                WhId = doc.WhId;
                WhName = doc.Wh?.Name;
                WhCode = doc.Wh?.Code;
                CreateTime = doc.CreateTime;
            }
        }

        public InventoryStockTakingReturn()
        {

        }
    }
}
