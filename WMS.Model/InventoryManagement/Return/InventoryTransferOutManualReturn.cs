using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace WMS.Model.InventoryManagement
{
    public class InventoryTransferOutManualReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public string DocType { get; set; }

        public string Organization { get; set; }

        public string BusinessDate { get; set; }

        public int? SumQty { get; set; }

        public Guid? TransOutWhId { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<InventoryTransferOutManualReturnLine> Lines { get; set; }

        public InventoryTransferOutManualReturn(InventoryTransferOutManual transferOutManual)
        {
            if (transferOutManual != null)
            {
                ID = transferOutManual.ID;
                DocNo = transferOutManual.DocNo;
                Status = transferOutManual.Status.GetEnumDisplayName();
                StatusValue = (int)transferOutManual.Status;
                DocType = transferOutManual.DocType;
                Organization = transferOutManual.Organization?.Name;
                BusinessDate = transferOutManual.BusinessDate?.ToString("yyyy-MM-dd");
                TransOutWhId = transferOutManual.TransOutWhId;
                CreateTime = transferOutManual.CreateTime;
                Lines = new List<InventoryTransferOutManualReturnLine>();
                foreach (var line in transferOutManual.InventoryTransferOutManualLine_InventoryTransferOutManual)
                {
                    var lineModel = new InventoryTransferOutManualReturnLine();
                    lineModel.ID = line.ID;
                    lineModel.DocLineNo = line.DocLineNo;
                    lineModel.ItemID = line.ItemMaster.ID;
                    lineModel.ItemCode = line.ItemMaster.Code;
                    lineModel.ItemName = line.ItemMaster.Name;
                    lineModel.SPECS = line.ItemMaster.SPECS;
                    lineModel.Seiban = line.Seiban;
                    lineModel.Qty = line.Qty;
                    lineModel.ToBeOffQty = line.ToBeOffQty;
                    lineModel.ToBeShipQty = line.ToBeShipQty;
                    lineModel.OffQty = line.OffQty;
                    lineModel.ShippedQty = line.ShippedQty;
                    lineModel.TransInQty = line.TransInQty;
                    lineModel.Status = line.Status.GetEnumDisplayName();
                    lineModel.StatusValue = (int)line.Status;
                    Lines.Add(lineModel);
                }
            }
        }

        public InventoryTransferOutManualReturn()
        {

        }
    }

    public class InventoryTransferOutManualReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public string Seiban { get; set; }

        public decimal? Qty { get; set; }

        public decimal? ToBeOffQty { get; set; }

        public decimal? ToBeShipQty { get; set; }

        public decimal? OffQty { get; set; }

        public decimal? ShippedQty { get; set; }

        public decimal? TransInQty { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public List<InventoryTransferOutManualReturnLineSuggestLoc> SuggestLocs { get; set; }

        public List<InventoryTransferOutManualReturnSubLine> SubLines { get; set; }
    }

    /// <summary>
    /// 推荐库位
    /// </summary>
    public class InventoryTransferOutManualReturnLineSuggestLoc
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
    /// 调出单下架库存明细
    /// </summary>
    public class InventoryTransferOutManualReturnSubLine
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
