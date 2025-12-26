using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.InventoryManagement
{
    public class InventoryOtherShipCreatePara
    {
        public Guid? BenefitOrganizationId { get; set; }

        public Guid? BenefitDepartmentId { get; set; }

        public Guid? BenefitPersonId { get; set; }

        public Guid? DocTypeId { get; set; }

        public string Memo { get; set; }

        public List<InventoryOtherShipLineCreatePara> Lines { get; set; }
    }

    public class InventoryOtherShipLineCreatePara
    {
        public Guid? InventoryId { get; set; }

        public decimal Qty { get; set; }
    }
}
