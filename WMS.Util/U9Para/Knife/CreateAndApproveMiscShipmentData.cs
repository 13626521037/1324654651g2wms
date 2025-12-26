using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Model.KnifeManagement;
using WMS.Model.PurchaseManagement;

namespace WMS.Util.U9Para.Knife
{
    internal class CreateAndApproveMiscShipmentData : U9ApiBasePara
    {
        public string org { get; set; }
        public string miscShipDocType { get; set; }
        public string memo { get; set; }
        public string stronghold_Org { get; set; }
        public List<CreateAndApproveMiscShipmentDataLine> miscShipmentLine { get; set; }

        public CreateAndApproveMiscShipmentData(BaseOperator handledBy_operator, List<BaseInventory> inventorys, BaseOperator checkOutBy,bool isLowValue)
        {
            //刀具引起的杂发领用 固定类型固定组织
            org = "0410";//isLowValue ? checkOutBy.Department.Organization.SourceSystemId : "0410";//低值也是 刀具中心  受益人用领用人
            miscShipDocType = isLowValue? "MiscShip009":"MiscShip008";//009低值
            memo = "刀具台账领用";
            stronghold_Org = "0410"; //isLowValue ? checkOutBy.Department.Organization.SourceSystemId : "0410";//货主组织都是刀具中心
            miscShipmentLine = new List<CreateAndApproveMiscShipmentDataLine>();
            foreach (var inventory in inventorys)
            {
                miscShipmentLine.Add(new CreateAndApproveMiscShipmentDataLine
                {
                    knifeId ="",
                    ItemInfo_ItemCode = inventory.ItemMaster.Code,
                    Wh = inventory.WhLocation.WhArea.WareHouse.SourceSystemId,//刀具当前的存储地点
                    StoreUOMQty = inventory.Qty.ToString(),
                    Seiban = inventory.Seiban,
                    BenefitOrg = isLowValue ? checkOutBy.Department.Organization.SourceSystemId : handledBy_operator.Department.Organization.SourceSystemId,
                    BenefitDept = isLowValue ? checkOutBy.Department.SourceSystemId :handledBy_operator.Department.SourceSystemId,
                    BenefitPsn = isLowValue ? checkOutBy.SourceSystemId : handledBy_operator.SourceSystemId,
                    KnifeNo = inventory.SerialNumber,
                    CheckOutBy = checkOutBy.Name,
                });
            }
        }

        public CreateAndApproveMiscShipmentData(InventoryOtherShip data)
        {
            businessId = data.ID;
            org = data.OwnerOrganization.Code;
            miscShipDocType = data.DocType.Code;
            memo = data.Memo;
            stronghold_Org = data.OwnerOrganization.Code;
            miscShipmentLine = new List<CreateAndApproveMiscShipmentDataLine>();
            foreach (var line in data.InventoryOtherShipLine_InventoryOtherShip)
            {
                miscShipmentLine.Add(new CreateAndApproveMiscShipmentDataLine
                {
                    knifeId = "",
                    ItemInfo_ItemCode = line.Inventory.ItemMaster.Code,
                    Wh = data.Wh.Code,
                    StoreUOMQty = line.Qty.ToString() ?? "0",
                    Seiban = line.Inventory.Seiban,
                    BenefitOrg = data.BenefitOrganization.SourceSystemId,
                    BenefitDept = data.BenefitDepartment.SourceSystemId,
                    BenefitPsn = data.BenefitPerson.SourceSystemId,
                    KnifeNo = "",
                    CheckOutBy = "",
                });
            }
        }
    }
    public class CreateAndApproveMiscShipmentDataLine
    {

        public string knifeId { get; set; }
        public string ItemInfo_ItemCode { get; set; }
        public string Wh { get; set; }
        public string StoreUOMQty { get; set; }
        public string Seiban { get; set; }
        public string BenefitOrg { get; set; }
        public string BenefitDept { get; set; }
        public string BenefitPsn { get; set; }
        public string KnifeNo { get; set; }
        public string CheckOutBy { get; set; }


    }
    public class MiscShipmentData
    {
        public string DocNo { get; set; }
        public string ID { get; set; }
        public List<MiscShipmentDataLine> Lines { get; set; }


    }
    public class MiscShipmentDataLine
    {
        public string ID { get; set; }
        public string ItemCode { get; set; }
        public decimal Qty { get; set; }
        public string KnifeNo { get; set; }
        public int DocLineNo { get; set; }
    }


}
