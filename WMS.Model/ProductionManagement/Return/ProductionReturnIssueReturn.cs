using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;

namespace WMS.Model.ProductionManagement
{
    public class ProductionReturnIssueReturn
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

        public List<ProductionReturnIssueReturnLine> Lines { get; set; }

        public ProductionReturnIssueReturn(ProductionReturnIssue entity)
        {
            if (entity != null)
            {
                ID = entity.ID;
                DocNo = entity.DocNo;
                Status = entity.Status.GetEnumDisplayName();
                StatusValue = (int)entity.Status;
                DocType = entity.DocType;
                Organization = entity.Organization.Name;
                BusinessDate = entity.BusinessDate?.ToString("yyyy-MM-dd");
                Memo = entity.Memo;
                CreatePerson = entity.CreatePerson;
                CreateTime = entity.CreateTime;
                Lines = new List<ProductionReturnIssueReturnLine>();
                if (entity.ProductionReturnIssueLine_ProductionReturnIssue != null && entity.ProductionReturnIssueLine_ProductionReturnIssue.Count > 0)
                {
                    WareHouseID = entity.ProductionReturnIssueLine_ProductionReturnIssue[0].Wh.ID;
                    WareHouseCode = entity.ProductionReturnIssueLine_ProductionReturnIssue[0].Wh.Code;
                    WareHouseName = entity.ProductionReturnIssueLine_ProductionReturnIssue[0].Wh.Name;
                    foreach (var line in entity.ProductionReturnIssueLine_ProductionReturnIssue)
                    {
                        Lines.Add(new ProductionReturnIssueReturnLine
                        {
                            ID = line.ID,
                            DocLineNo = line.DocLineNo,
                            ItemID = line.ItemMaster.ID,
                            ItemCode = line.ItemMaster.Code,
                            ItemName = line.ItemMaster.Name,
                            SPECS = line.ItemMaster.SPECS,
                            Status = line.Status.GetEnumDisplayName(),
                            StatusValue = (int)line.Status,
                            Qty = line.Qty,
                            ToBeReceiveQty = line.ToBeReceiveQty,
                            ToBeInWhQty = line.ToBeInWhQty,
                            ReceiveQty = line.ReceiveQty,
                            InWhQty = line.InWhQty,
                        });
                    }
                }
            }
        }

        public ProductionReturnIssueReturn()
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
                line.SubLines = new List<ProductionReturnIssueReturnSubLine>();
                var matchs = relations.FindAll(x => x.BusinessLineId == line.ID);
                if (matchs != null && matchs.Count > 0)
                {
                    foreach (var match in matchs)
                    {
                        line.SubLines.Add(new ProductionReturnIssueReturnSubLine
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

    public class ProductionReturnIssueReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal? Qty { get; set; }

        public decimal? ToBeReceiveQty { get; set; }

        public decimal? ToBeInWhQty { get; set; }

        public decimal? ReceiveQty { get; set; }

        public decimal? InWhQty { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public List<ProductionReturnIssueReturnSubLine> SubLines { get; set; }

        public List<ProductionReturnIssueReturnLineSuggestLoc> SuggestLocs { get; set; }
    }

    public class ProductionReturnIssueReturnSubLine
    {
        public Guid InvID { get; set; }

        public string BarCode { get; set; }

        public string SN { get; set; }

        public decimal TotalQty { get; set; }

        public decimal OccupyQty { get; set; }
    }

    public class ProductionReturnIssueReturnLineSuggestLoc
    {
        public string AreaCode { get; set; }

        public string LocCode { get; set; }
    }
}
