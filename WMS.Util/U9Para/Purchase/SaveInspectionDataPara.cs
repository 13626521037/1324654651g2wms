using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Model.PurchaseManagement;

namespace WMS.Util
{
    public class SaveInspectionDataPara: U9ApiBasePara
    {
        public long iD { get; set; }

        public string inspector { get; set; }

        public List<SaveInspectionDataLinePara> lines { get; set; }

        public SaveInspectionDataPara(PurchaseReceivement data)
        {
            long id = 0;
            long.TryParse(data.SourceSystemId, out id);
            iD = id;
            inspector = "";
            businessId = data.ID;
            lines = new List<SaveInspectionDataLinePara>();
            foreach (var item in data.PurchaseReceivementLine_PurchaseReceivement)
            {
                if (item.NoInspect == true)
                    continue;
                SaveInspectionDataLinePara line = new SaveInspectionDataLinePara();
                line.SubLines = new List<SaveInspectionDataSubLinePara>();
                id = 0;
                long.TryParse(item.SourceSystemId, out id);
                line.ID = id;
                if (item.QualifiedQty > 0)
                {
                    line.SubLines.Add(new SaveInspectionDataSubLinePara()
                    {
                        QCQCConclusion = 0,
                        QCQtyPU = (decimal)item.QualifiedQty,
                        DealCode = "",
                        Reason = ""
                    });
                }
                if (item.UnqualifiedRejectQty > 0)
                {
                    line.SubLines.Add(new SaveInspectionDataSubLinePara()
                    {
                        QCQCConclusion = 2,
                        QCQtyPU = (decimal)item.UnqualifiedRejectQty,
                        DealCode = item.UnqualifiedRejectDeal == null ? null : ((int)item.UnqualifiedRejectDeal).ToString(),
                        Reason = ""
                    });
                }
                if (item.UnqualifiedAcceptQty > 0)
                {
                    line.SubLines.Add(new SaveInspectionDataSubLinePara()
                    {
                        QCQCConclusion = 1,
                        QCQtyPU = (decimal)item.UnqualifiedAcceptQty,
                        DealCode = item.UnqualifiedAcceptDeal == null ? null : ((int)item.UnqualifiedAcceptDeal).ToString(),
                        Reason = ""
                    });
                }
                if (item.ConcessionAcceptQty > 0)
                {
                    line.SubLines.Add(new SaveInspectionDataSubLinePara()
                    {
                        QCQCConclusion = 6,
                        QCQtyPU = (decimal)item.ConcessionAcceptQty,
                        DealCode = "",
                        Reason = ""
                    });
                }
                //line.Qty = item.Qty;
                //line.QualifiedQty = item.QualifiedQty;
                //line.UnqualifiedRejectQty = item.UnqualifiedRejectQty;
                //line.UnqualifiedAcceptQty = item.UnqualifiedAcceptQty;
                //line.ConcessionAcceptQty = item.ConcessionAcceptQty;
                //line.UnqualifiedRejectDeal = item.UnqualifiedRejectDeal == null ? null : (int)item.UnqualifiedRejectDeal;
                //line.UnqualifiedAcceptDeal = item.UnqualifiedAcceptDeal == null ? null : (int)item.UnqualifiedAcceptDeal;
                lines.Add(line);
            }
        }
    }

    public class SaveInspectionDataLinePara
    {
        public long ID { get; set; }

        public int DocLineNo { get; set; }

        public List<SaveInspectionDataSubLinePara>? SubLines { get; set; }

        //public decimal? Qty { get; set; }

        //public decimal? QualifiedQty { get; set; }

        //public decimal? UnqualifiedRejectQty { get; set; }

        //public decimal? UnqualifiedAcceptQty { get; set; }

        //public decimal? ConcessionAcceptQty { get; set; }

        //public int? UnqualifiedRejectDeal { get; set; }

        //public int? UnqualifiedAcceptDeal { get; set; }
    }

    public class SaveInspectionDataSubLinePara
    {
        public int QCQCConclusion { get; set; }

        public decimal QCQtyPU { get; set; }

        public string? DealCode { get; set; }

        public string? Reason { get; set; }
    }
}
