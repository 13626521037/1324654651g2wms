using WMS.Model.InventoryManagement;

namespace WMS.Util
{
    /// <summary>
    /// 创建调出单接口参数
    /// </summary>
    public class CreateTransferOutPara : U9ApiBasePara
    {
        /// <summary>
        /// 表单头
        /// </summary>
        public CreateTransferOutHeadPara? transOutDTO { get; set; }

        public CreateTransferOutPara(InventoryTransferOutDirect transferOut)
        {
            if (transferOut == null)
            {
                return;
            }

            transOutDTO = new CreateTransferOutHeadPara();
            transOutDTO.TransOutDocType_Code = transferOut.DocType.Code;
            transOutDTO.TransOutOrg_Code = transferOut.TransOutOrganization.Code;
            transOutDTO.TransOutWh_Code = transferOut.TransOutWh.Code;
            transOutDTO.TransInOrg_Code = transferOut.TransInOrganization.Code;
            transOutDTO.TransInWh_Code = transferOut.TransInWh.Code;
            transOutDTO.Memo = transferOut.Memo;
            businessId = transferOut.ID;
            transOutDTO.TransOutLineDTOList = new List<CreateTransferOutLinePara>();
            foreach (var transferOutLine in transferOut.InventoryTransferOutDirectLine_InventoryTransferOutDirect)
            {
                var linePara = new CreateTransferOutLinePara();
                linePara.ItemCode = transferOutLine.ItemMaster.Code;
                linePara.StoreUOMQty = transferOutLine.Qty;
                //linePara.SeibanID
                linePara.PrivateDescSeg1 = "";
                linePara.PrivateDescSeg2 = "";
                linePara.PrivateDescSeg3 = "";
                linePara.PrivateDescSeg6 = "";
                linePara.PrivateDescSeg7 = "";
                linePara.PrivateDescSeg8 = "";
                linePara.Seiban = transferOutLine.Inventory?.Seiban;
                transOutDTO.TransOutLineDTOList.Add(linePara);
            }
        }
    }

    /// <summary>
    /// 创建调出单接口表单头
    /// </summary>
    public class CreateTransferOutHeadPara
    {
        /// <summary>
        /// 调出单单据类型编码
        /// </summary>
        public string? TransOutDocType_Code { get; set; }

        /// <summary>
        /// 调出组织编码
        /// </summary>
        public string? TransOutOrg_Code { get; set; }

        /// <summary>
        /// 调出存储地点编码
        /// </summary>
        public string? TransOutWh_Code { get; set; }

        /// <summary>
        /// 调入组织编码
        /// </summary>
        public string? TransInOrg_Code { get; set; }

        /// <summary>
        /// 调入存储地点编码
        /// </summary>
        public string? TransInWh_Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Memo { get; set; }

        /// <summary>
        /// 行集合
        /// </summary>
        public List<CreateTransferOutLinePara>? TransOutLineDTOList { get; set; }
    }

    /// <summary>
    /// 创建调出单接口表单行
    /// </summary>
    public class CreateTransferOutLinePara
    {
        /// <summary>
        /// 料号
        /// </summary>
        public string? ItemCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? StoreUOMQty { get; set; }

        /// <summary>
        /// 番号ID
        /// </summary>
        public long SeibanID { get; set; }

        public string? PrivateDescSeg1 { get; set; }

        public string? PrivateDescSeg2 { get; set; }

        public string? PrivateDescSeg3 { get; set; }

        public string? PrivateDescSeg6 { get; set; }

        public string? PrivateDescSeg7 { get; set; }

        public string? PrivateDescSeg8 { get; set; }

        /// <summary>
        /// 番号
        /// </summary>
        public string? Seiban { get; set; }
    }
}
