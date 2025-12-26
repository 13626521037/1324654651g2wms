using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace WMS.Model.SalesManagement
{
    public class SalesShipReturn
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

        public string Customer { get; set; }

        public string Operators { get; set; }

        public List<SalesShipReturnLine> Lines { get; set; }

        public SalesShipReturn(SalesShip entity)
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
                Customer = entity.Customer?.Name;
                Operators = entity.Operators;
                Lines = new List<SalesShipReturnLine>();
                foreach (var line in entity.SalesShipLine_Ship)
                {
                    var lineModel = new SalesShipReturnLine();
                    lineModel.ID = line.ID;
                    lineModel.DocLineNo = line.DocLineNo;
                    lineModel.ItemID = line.ItemMaster.ID;
                    lineModel.ItemCode = line.ItemMaster.Code;
                    lineModel.ItemName = line.ItemMaster.Name;
                    lineModel.SPECS = line.ItemMaster.SPECS;
                    lineModel.WareHouseID = line.WareHouseId;
                    lineModel.Seiban = line.Seiban;
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

        public SalesShipReturn()
        {

        }
    }

    public class SalesShipReturnLine
    {
        public Guid ID { get; set; }

        public int? DocLineNo { get; set; }

        public Guid? ItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public Guid? WareHouseID { get; set; }

        public string Seiban { get; set; }

        public decimal? Qty { get; set; }

        public decimal? ToBeOffQty { get; set; }

        public decimal? ToBeShipQty { get; set; }

        public decimal? OffQty { get; set; }

        public decimal? ShippedQty { get; set; }

        public string Status { get; set; }

        public int? StatusValue { get; set; }

        public List<SalesShipReturnLineSuggestLoc> SuggestLocs { get; set; }

        public List<SalesShipReturnSubLine> SubLines { get; set; }
    }

    /// <summary>
    /// 推荐库位
    /// </summary>
    public class SalesShipReturnLineSuggestLoc
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
    /// 下架库存明细
    /// </summary>
    public class SalesShipReturnSubLine
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
