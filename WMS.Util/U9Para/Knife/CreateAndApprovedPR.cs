using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;

namespace WMS.Util.U9Para.Knife
{
    internal class CreateAndApprovedPR : U9ApiBasePara
    {
        public string docType { get; set; }
        public string demander { get; set; }
        public string demandDept { get; set; }
        public string demandOrg { get; set; }
        public string demandDate { get; set; }
        public string businessType { get; set; }
        public string grindRequestDocNo { get; set; }


        public List<CreateAndApprovedPRDataLine> lines { get; set; }

        public CreateAndApprovedPR(BaseOperator handledBy_operator, List<(WMS.Model.KnifeManagement.Knife knife, BaseItemMaster itemMaster)> knifes_actualItem , string docNo,string Memo)
        {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            
            docType = "PR14";//刀具修磨请购  在接口里通过客开参数HighKnifePRCode写死了 这里写啥都没用 
            demander = handledBy_operator.Code;
            demandDept = handledBy_operator.Department.Code;
            demandOrg = handledBy_operator.Department.Organization.Code;
            demandDate = currentTime;
            businessType = "0";
            grindRequestDocNo = docNo;
            lines = new List<CreateAndApprovedPRDataLine>();
            var itemCode = "1800004875";//251212 都是这个正式的修为费用料号了
            /*var itemCode = "1800004577";//20测试维修费
            string ipv4 = Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                ?.ToString() ?? "127.0.0.1";
            if (ipv4.Contains("192.168.250.75"))
            {
                itemCode = "1800004875";//22正式维修费
            }*/
            foreach (var (k,i) in knifes_actualItem)
            {
                lines.Add(new CreateAndApprovedPRDataLine
                {
                    ItemCode= itemCode,
                    SpecifyType = "",
                    Memo=Memo,
                    DeliveryDate= currentTime,
                    PurNum="1",
                    Uom="",
                    ApprovalNum="1",
                    ReqEmployee= handledBy_operator.Code,
                    ReqDept= handledBy_operator.Department.Code,
                    //RegOrg= handledBy_operator.Department.Organization.Code,
                    //RcvOrg= handledBy_operator.Department.Organization.Code,
                    RegOrg= "0410",//需求组织 刀具中心0410 无所谓 sv里写死了
                    RcvOrg= "0410",//收货组织 刀具中心0410 无所谓 sv里写死了
                    KnifeNo =k.SerialNumber,
                    KnifeItemCode = i.Code,
                    /* KnifeName=k.ItemMaster.Code,
                     KnifeSPECS= k.ItemMaster.Code,
                     KnifeDescription= k.ItemMaster.Code,*/
                    KnifeName = i.Name,
                    KnifeSPECS = i.SPECS,
                    KnifeDescription = i.Description,
                    KnifeBarCode = k.BarCode,
                    KnifeGrindKnifeNO = k.GrindKnifeNO,
                    KnifeActualItemCode = k.ActualItemCode,

                });
            }
        }


    }
    public class CreateAndApprovedPRDataLine
    {
        
        public string ItemCode { get; set; }
        public string SpecifyType { get; set; }
        public string Memo { get; set; }
        public string DeliveryDate { get; set; }
        public string PurNum { get; set; }
        public string Uom { get; set; }
        public string ApprovalNum { get; set; }
        public string ReqEmployee { get; set; }
        public string ReqDept { get; set; }
        public string RegOrg { get; set; }//需求组织 0410
        public string RcvOrg { get; set; }
        public string KnifeNo { get; set; }
        public string KnifeItemCode { get; set; }
        public string KnifeName { get; set; }
        public string KnifeSPECS { get; set; }
        public string KnifeDescription { get; set; }
        public string KnifeBarCode { get; set; }
        public string KnifeGrindKnifeNO { get; set; }
        public string KnifeActualItemCode { get; set; }

        
    }
    public class PrlineAndKnifeNOLine
    {
        public long PrlineId { get; set; }
        public string KnifeNO { get; set; }

    }
    public class PRInfoReturn
    {
        public string DocNo { get; set; }
        public List<PrlineAndKnifeNOLine> Lines { get; set; }
        public PRInfoReturn()
        {
            Lines = new List<PrlineAndKnifeNOLine>();
        }
    }

}
