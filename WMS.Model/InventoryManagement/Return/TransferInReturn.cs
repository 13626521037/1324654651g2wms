using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;

namespace WMS.Model.InventoryManagement.Return
{
    public class TransferInReturn
    {
        public Guid ID { get; set; }

        public string DocNo { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public string DocType { get; set; }

        public string Organization { get; set; }

        public string BusinessDate { get; set; }

        public string Memo { get; set; }

        public string CreatePerson { get; set; }

        public Guid? WareHouseID { get; set; }

        public string WareHouseCode { get; set; }

        public string WareHouseName { get; set; }

        public int? SumQty { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<TransferInReturnLine> Lines { get; set; }

        public TransferInReturn(InventoryTransferIn entity)
        {
            if (entity != null)
            {
                ID = entity.ID;
                DocNo = entity.DocNo;
                Status = entity.Status.GetEnumDisplayName();
                StatusValue = (int)entity.Status;
                DocType = entity.DocType;
                Organization = entity.TransInOrganization.Name;
                BusinessDate = entity.BusinessDate?.ToString("yyyy-MM-dd");
                Memo = entity.Memo;
                CreatePerson = entity.CreateBy;
                WareHouseID = entity.TransInWh.ID;
                WareHouseCode = entity.TransInWh.Code;
                WareHouseName = entity.TransInWh.Name;
                CreateTime = entity.CreateTime;
                Lines = new List<TransferInReturnLine>();
                if (entity.InventoryTransferInLine_InventoryTransferIn != null && entity.InventoryTransferInLine_InventoryTransferIn.Count > 0)
                {
                    foreach (var line in entity.InventoryTransferInLine_InventoryTransferIn)
                    {
                        Lines.Add(new TransferInReturnLine
                        {
                            ID = line.ID,
                            DocLineNo = line.DocLineNo,
                            ItemID = line.ItemMaster.ID,
                            ItemCode = line.ItemMaster.Code,
                            ItemName = line.ItemMaster.Name,
                            SPECS = line.ItemMaster.SPECS,
                            Qty = line.Qty,
                        });
                    }
                }
            }
        }

        public TransferInReturn()
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
                line.SubLines = new List<TransferInReturnSubLine>();
                var matchs = relations.FindAll(x => x.BusinessLineId == line.ID);
                if (matchs != null && matchs.Count > 0)
                {
                    foreach (var match in matchs)
                    {
                        line.SubLines.Add(new TransferInReturnSubLine
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

    public class TransferInReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal? Qty { get; set; }

        public List<TransferInReturnSubLine> SubLines { get; set; }

        public List<TransferInReturnLineSuggestLoc> SuggestLocs { get; set; }
    }

    public class TransferInReturnSubLine
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
    public class TransferInReturnLineSuggestLoc
    {
        public string AreaCode { get; set; }

        public string LocCode { get; set; }
    }
}
