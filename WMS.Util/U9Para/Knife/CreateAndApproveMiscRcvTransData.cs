using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.WorkFlow;
using WMS.Model.BaseData;
using WMS.Model.KnifeManagement;
using WMS.Model.PurchaseManagement;
namespace WMS.Util.U9Para.Knife
{
    internal class CreateAndApproveMiscRcvTransData : U9ApiBasePara
    {
        public string org { get; set; }
        public string miscShipDocType { get; set; }
        public string memo { get; set; }
        public string businessDate { get; set; }
        public string BenefitOrg { get; set; }
        public string BenefitDept { get; set; }
        public string BenefitPsn { get; set; }
        public string ownerOrg { get; set; }
        public int scrap { get; set; }
        public List<CreateAndApproveMiscRcvTransDataLine> lines { get; set; }

        public CreateAndApproveMiscRcvTransData(BaseOperator handledBy_operator, List<WMS.Model.KnifeManagement.Knife> knifes)
        {
            //不良退回的杂收单 固定值
            org = "0410";
            miscShipDocType = "MiscRcv009";//不良杂收单
            memo = "不良退回杂收";
            businessDate = DateTime.Now.ToString("yyyy-MM-dd");
            BenefitOrg = handledBy_operator.Department.Organization.Code;
            BenefitDept = handledBy_operator.Department.Code;
            BenefitPsn = handledBy_operator.Code;
            ownerOrg = handledBy_operator.Department.Organization.Code;
            scrap = 0;
            lines = new List<CreateAndApproveMiscRcvTransDataLine>();
            /*var miscLines = knifes.GroupBy(x => new
            {
                x.MiscShipLineID,
                ItemCode = x.ItemMaster.Code,
                Wh = x.WhLocation.WhArea.WareHouse.Code
            })
            .Select(x => new CreateAndApproveMiscRcvTransDataLine()
            {
                SrcID=x.Key.MiscShipLineID,
                ItemInfo_ItemCode=x.Key.ItemCode,
                Wh=x.Key.Wh,
                Qty= x.Count().ToString(), 

            }).ToList();*/
            foreach (var knife in knifes)
            {
                lines.Add(new CreateAndApproveMiscRcvTransDataLine
                {
                    ItemInfo_ItemCode = knife.ItemMaster.Code,
                    Wh = knife.WhLocation.WhArea.WareHouse.Code,
                    Qty =  "1",
                    SrcID = knife.MiscShipLineID,
                    KnifeNo = knife.SerialNumber,
                });
            }
        }


    }
    public class CreateAndApproveMiscRcvTransDataLine
    {
        
        public string ItemInfo_ItemCode { get; set; }
        public string Wh { get; set; }
        public string Qty { get; set; }
        public string SrcID { get; set; }
        public string KnifeNo { get; set; }
        
    }

    public class MiscRcvTransData
    {
        public string DocNo { get; set; }
        public string ID { get; set; }
        public List<MiscRcvTransDataLine> Lines { get; set; }


    }
    public class MiscRcvTransDataLine
    {
        public string ID { get; set; }
    }


}
