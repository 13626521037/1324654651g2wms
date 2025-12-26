using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;

namespace WMS.Model.PurchaseManagement
{
    /// <summary>
    /// 采购退货前端读取返回数据
    /// </summary>
    public class PurchaseReturnReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public string DocType { get; set; }

        public string Organization { get; set; }

        public string BusinessDate { get; set; }

        public string Supplier { get; set; }

        public string Memo { get; set; }

        public int? SumQty { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<PurchaseReturnReturnLine> Lines { get; set; }

        public PurchaseReturnReturn(PurchaseReturn purchaseReturn)
        {
            if (purchaseReturn != null)
            {
                ID = purchaseReturn.ID;
                DocNo = purchaseReturn.DocNo;
                Status = purchaseReturn.Status.GetEnumDisplayName();
                StatusValue = (int)purchaseReturn.Status;
                DocType = purchaseReturn.DocType;
                Organization = purchaseReturn.Organization?.Name;
                BusinessDate = purchaseReturn.BusinessDate?.ToString("yyyy-MM-dd");
                Supplier = purchaseReturn.Supplier?.Name;
                Memo = purchaseReturn.Memo;
                Lines = new List<PurchaseReturnReturnLine>();
                CreateTime = purchaseReturn.CreateTime;
                foreach (var line in purchaseReturn.PurchaseReturnLine_PurchaseReturn)
                {
                    PurchaseReturnReturnLine returnLine = new PurchaseReturnReturnLine();
                    returnLine.ID = line.ID;
                    returnLine.DocLineNo = line.DocLineNo;
                    returnLine.ItemID = line.ItemMaster?.ID;
                    returnLine.ItemCode = line.ItemMaster?.Code;
                    returnLine.ItemName = line.ItemMaster?.Name;
                    returnLine.SPECS = line.ItemMaster?.SPECS;
                    returnLine.Qty = line.Qty;
                    returnLine.ToBeOffQty = line.ToBeOffQty;
                    returnLine.ToBeShipQty = line.ToBeShipQty;
                    returnLine.OffQty = line.OffQty;
                    returnLine.ShippedQty = line.ShippedQty;
                    returnLine.WhId = line.WareHouseId;
                    returnLine.Status = line.Status.GetEnumDisplayName();
                    returnLine.StatusValue = (int)line.Status;
                    Lines.Add(returnLine);
                }
            }
        }

        public PurchaseReturnReturn()
        {

        }

    }

    /// <summary>
    /// 采购退货前端读取返回数据行
    /// </summary>
    public class PurchaseReturnReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal? Qty { get; set; }

        public decimal? ToBeOffQty { get; set; }

        public decimal? ToBeShipQty { get; set; }

        public decimal? OffQty { get; set; }

        public decimal? ShippedQty { get; set; }

        public Guid? WhId { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        /// <summary>
        /// 下架明细
        /// </summary>
        public List<PurchaseReturnReturnSubLine> SubLines { get; set; }

        /// <summary>
        /// 推荐库位明细
        /// </summary>
        public List<PurchaseReturnReturnLineSuggestLoc> SuggestLocs { get; set; }
    }

    /// <summary>
    /// 采购退货前端读取返回数据行推荐库位
    /// </summary>
    public class PurchaseReturnReturnLineSuggestLoc
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
    /// 采购退货单下架库存明细
    /// </summary>
    public class PurchaseReturnReturnSubLine
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
