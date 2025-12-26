using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.PurchaseManagement
{
    public class ReceivementSaveInspectionDataPara
    {
        public Guid ID { get; set; }

        public List<ReceivementSaveInspectionDataLinePara> Lines { get; set; }
    }

    public class ReceivementSaveInspectionDataLinePara
    {
        public Guid ID { get; set; }

        public decimal? Qty { get; set; }

        public decimal? QualifiedQty { get; set; }

        public decimal? UnqualifiedRejectQty { get; set; }

        public decimal? UnqualifiedAcceptQty { get; set; }

        public decimal? ConcessionAcceptQty { get; set; }

        public int? UnqualifiedRejectDeal { get; set; }

        public int? UnqualifiedAcceptDeal { get; set; }
    }
}
