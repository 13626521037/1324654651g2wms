using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.PurchaseManagement;

namespace WMS.Model.PurchaseManagement
{
    public class PurchaseOutsourcingIssueReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public string DocType { get; set; }

        public string Organization { get; set; }

        public string BusinessDate { get; set; }

        public int? SumQty { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Supplier { get; set; }

        public List<PurchaseOutsourcingIssueReturnLine> Lines { get; set; }

        public PurchaseOutsourcingIssueReturn(PurchaseOutsourcingIssue entity)
        {
            if (entity != null)
            {
                ID = entity.ID;
                DocNo = entity.DocNo;
                Status = entity.Status.GetEnumDisplayName();
                StatusValue = (int)entity.Status;
                DocType = entity.DocType;
                Organization = entity.Organization?.Name;
                BusinessDate = entity.BusinessDate?.ToString("yyyy-MM-dd");
                CreateTime = entity.CreateTime;
                Supplier = entity.Supplier?.Name;
                Lines = new List<PurchaseOutsourcingIssueReturnLine>();
                foreach (var line in entity.PurchaseOutsourcingIssueLine_OutsourcingIssue)
                {
                    var lineModel = new PurchaseOutsourcingIssueReturnLine();
                    lineModel.ID = line.ID;
                    lineModel.DocLineNo = line.DocLineNo;
                    lineModel.ItemID = line.ItemMaster.ID;
                    lineModel.ItemCode = line.ItemMaster.Code;
                    lineModel.ItemName = line.ItemMaster.Name;
                    lineModel.SPECS = line.ItemMaster.SPECS;
                    lineModel.WareHouseID = line.WareHouseId;
                    //lineModel.Seiban = line.Seiban;
                    lineModel.Qty = line.Qty;
                    lineModel.ToBeOffQty = line.ToBeOffQty;
                    lineModel.ToBeShipQty = line.ToBeShipQty;
                    lineModel.OffQty = line.OffQty;
                    lineModel.ShippedQty = line.ShippedQty;
                    lineModel.Status = line.Status.GetEnumDisplayName();
                    lineModel.StatusValue = (int)line.Status;
                    Lines.Add(lineModel);
                }
            }
        }

        public PurchaseOutsourcingIssueReturn()
        {

        }
    }

    public class PurchaseOutsourcingIssueReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public Guid? WareHouseID { get; set; }

        //public string Seiban { get; set; }

        public decimal? Qty { get; set; }

        public decimal? ToBeOffQty { get; set; }

        public decimal? ToBeShipQty { get; set; }

        public decimal? OffQty { get; set; }

        public decimal? ShippedQty { get; set; }

        //public decimal? TransInQty { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public List<PurchaseOutsourcingIssueReturnLineSuggestLoc> SuggestLocs { get; set; }

        public List<PurchaseOutsourcingIssueReturnSubLine> SubLines { get; set; }
    }

    /// <summary>
    /// 推荐库位
    /// </summary>
    public class PurchaseOutsourcingIssueReturnLineSuggestLoc
    {
        /// <summary>
        /// 库区编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocCode { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
    }

    /// <summary>
    /// 委外发料单下架库存明细
    /// </summary>
    public class PurchaseOutsourcingIssueReturnSubLine
    {
        public Guid InvID { get; set; }

        /// <summary>
        /// 库位编码
        /// </summary>
        public string LocCode { get; set; }

        public string Batch { get; set; }

        public string SN { get; set; }

        public decimal TotalQty { get; set; }

        public decimal OccupyQty { get; set; }
    }
}
