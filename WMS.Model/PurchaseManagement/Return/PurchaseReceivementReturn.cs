using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;

namespace WMS.Model.PurchaseManagement
{
    public class PurchaseReceivementReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public string InspectStatus { get; set; }

        public int? InspectStatusValue { get; set; }

        public string DocType { get; set; }

        public string Organization { get; set; }

        public string BusinessDate { get; set; }

        public string Supplier { get; set; }

        public string Memo { get; set; }

        public string CreatePerson { get; set; }

        public Guid? WareHouseID { get; set; }

        public string WareHouseCode { get; set; }

        public string WareHouseName { get; set; }

        public int? SumQty { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<PurchaseReceivementReturnLine> Lines { get; set; }

        public PurchaseReceivementReturn(PurchaseReceivement receive)
        {
            if (receive != null)
            {
                ID = receive.ID;
                DocNo = receive.DocNo;
                Status = receive.Status.GetEnumDisplayName();
                StatusValue = (int)receive.Status;
                InspectStatus = receive.InspectStatus.GetEnumDisplayName();
                InspectStatusValue = (int)receive.InspectStatus;
                DocType = receive.DocType;
                Organization = receive.Organization.Name;
                BusinessDate = receive.BusinessDate?.ToString("yyyy-MM-dd");
                Supplier = receive.Supplier.Name;
                Memo = receive.Memo;
                CreatePerson = receive.CreatePerson;
                CreateTime = receive.CreateTime;
                Lines = new List<PurchaseReceivementReturnLine>();
                if (receive.PurchaseReceivementLine_PurchaseReceivement != null && receive.PurchaseReceivementLine_PurchaseReceivement.Count > 0)
                {
                    WareHouseID = receive.PurchaseReceivementLine_PurchaseReceivement[0].WareHouse.ID;
                    WareHouseCode = receive.PurchaseReceivementLine_PurchaseReceivement[0].WareHouse.Code;
                    WareHouseName = receive.PurchaseReceivementLine_PurchaseReceivement[0].WareHouse.Name;
                    foreach (var line in receive.PurchaseReceivementLine_PurchaseReceivement)
                    {
                        Lines.Add(new PurchaseReceivementReturnLine
                        {
                            ID = line.ID,
                            DocLineNo = line.DocLineNo,
                            ItemID = line.ItemMaster.ID,
                            ItemCode = line.ItemMaster.Code,
                            ItemName = line.ItemMaster.Name,
                            SPECS = line.ItemMaster.SPECS,
                            InspectStatus = line.InspectStatus.GetEnumDisplayName(),
                            InspectStatusValue = (int)line.InspectStatus,
                            Qty = line.Qty,
                            QualifiedQty = line.QualifiedQty,
                            UnqualifiedRejectQty = line.UnqualifiedRejectQty,
                            UnqualifiedAcceptQty = line.UnqualifiedAcceptQty,
                            ConcessionAcceptQty = line.ConcessionAcceptQty,
                            ToBeReceiveQty = line.ToBeReceiveQty,
                            ToBeInspectQty = line.ToBeInspectQty,
                            ToBeInWhQty = line.ToBeInWhQty,
                            InspectQty = line.InspectQty,
                            ReceiveQty = line.ReceiveQty,
                            InWhQty = line.InWhQty,
                            UnqualifiedRejectDeal = line.UnqualifiedRejectDeal == null ? null :(int)line.UnqualifiedRejectDeal,
                            UnqualifiedAcceptDeal = line.UnqualifiedAcceptDeal == null ? null : (int)line.UnqualifiedAcceptDeal
                        });
                    }
                }
            }
        }

        public PurchaseReceivementReturn()
        {

        }

        /// <summary>
        /// 设置子行数据
        /// </summary>
        /// <param name="relations"></param>
        public void SetSubLines(List<BaseDocInventoryRelation> relations)
        {
            foreach (var line in Lines)
            {
                line.SubLines = new List<PurchaseReceivementReturnSubLine>();
                var matchs = relations.FindAll(x => x.BusinessLineId == line.ID);
                if (matchs != null && matchs.Count > 0)
                {
                    foreach (var match in matchs)
                    {
                        line.SubLines.Add(new PurchaseReceivementReturnSubLine
                        {
                            InvID = match.Inventory.ID,
                            BarCode = $"{(int)match.Inventory.ItemSourceType}@{line.ItemCode}@{decimal.Parse(((decimal)match.Inventory.Qty).ToString("0.#############################"))}@{match.Inventory.SerialNumber}",
                            SN = match.Inventory.SerialNumber,
                            TotalQty = (decimal)match.Inventory.Qty,    // 待收库位的库存信息必定是全收的，所以这里直接用待收库存的数量
                            OccupyQty = (decimal)match.Qty
                        });
                    }
                }
            }
        }
    }

    public class PurchaseReceivementReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public string InspectStatus { get; set; }

        public int? InspectStatusValue { get; set; }

        public decimal? Qty { get; set; }

        public decimal? QualifiedQty { get; set; }

        public decimal? UnqualifiedRejectQty { get; set; }

        public decimal? UnqualifiedAcceptQty { get; set; }

        public decimal? ConcessionAcceptQty { get; set; }

        public decimal? ToBeReceiveQty { get; set; }

        public decimal? ToBeInspectQty { get; set; }

        public decimal? ToBeInWhQty { get; set; }
        
        public decimal? InspectQty { get; set; }

        public decimal? ReceiveQty { get; set; }

        public decimal? InWhQty { get; set; }

        public int? UnqualifiedRejectDeal { get; set; }

        public int? UnqualifiedAcceptDeal { get; set; }

        public List<PurchaseReceivementReturnSubLine> SubLines { get; set; }

        public List<PurchaseReceivementReturnLineSuggestLoc> SuggestLocs { get; set; }
    }

    public class PurchaseReceivementReturnSubLine
    {
        public Guid InvID { get; set; }

        public string BarCode { get; set; }

        public string SN { get; set; }

        public decimal TotalQty { get; set; }

        public decimal OccupyQty { get; set; }
    }

    /// <summary>
    /// 推荐库位
    /// </summary>
    public class PurchaseReceivementReturnLineSuggestLoc
    {
        public string AreaCode { get; set; }

        public string LocCode { get; set; }
    }
}
