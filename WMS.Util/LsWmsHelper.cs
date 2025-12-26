using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 链溯WMS帮助类
    /// </summary>
    public class LsWmsHelper
    {
        public static string? ErrorMsg { get; set; }

        private static string g_wmsUrl = "http://192.168.250.70:8010/AndroidService.svc/SaveT_BarcodeADF";

        public static bool WriteBarCode(List<LsWmsBarCodePara> paras)
        {
            // 插入重复条码WMS会报错。此处无需先校验是否存在重复条码
            ErrorMsg = "";
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //构造参数
            string wmsParm = "[";
            int i = 0;
            foreach (LsWmsBarCodePara para in paras)
            {
                wmsParm += "{";
                wmsParm += "\\\"ErpVoucherNo\\\"" + ":" + "\\\"" + para.DocNo + "\\\",";   // 单号
                wmsParm += "\\\"dimension\\\"" + ":" + "\\\"" + para.DocNo + "\\\",";   // 单号
                wmsParm += "\\\"MaterialNo\\\"" + ":" + "\\\"" + para.ItemCode + "\\\",";   // 物料号
                wmsParm += "\\\"MaterialDesc\\\"" + ":" + "\\\"" + RemoveSpecialCharacter(para.ItemName) + "\\\",";   // 物料描述
                wmsParm += "\\\"spec\\\"" + ":" + "\\\"" + RemoveSpecialCharacter(para.SPECS) + "\\\",";   // 规格
                //wmsParm += "\\\"CusCode\\\"" + ":" + "\\\"" + dc.DescFlexSegments.PrivateDescSeg2 + "\\\",";   // 客户编码
                //if (customer != null)
                //{
                //    wmsParm += "\\\"CusName\\\"" + ":" + "\\\"" + customer.ShortName + "\\\",";   // 客户名
                //    wmsParm += "\\\"BarcodeMType\\\"" + ":" + "\\\"" + customer.DescFlexField.PrivateDescSeg5 + "\\\",";   // 客户首字母拼音
                //}
                wmsParm += "\\\"Qty\\\"" + ":" + "\\\"" + para.Qty + "\\\",";   // 数量
                wmsParm += "\\\"BarCode\\\"" + ":" + "\\\"2@" + para.ItemCode + "@" + para.Qty + "@" + para.Sn + "\\\",";   // 二维码
                //rr.errmsg += "2@" + para.ItemCode + "@" + para.Qty + "@" + para.Sn + ",";   // 写入返回结果
                wmsParm += "\\\"SerialNo\\\"" + ":" + "\\\"" + para.Sn + "\\\",";   // 序列号
                wmsParm += "\\\"StrongHoldCode\\\"" + ":" + "\\\"" + para.OrgCode + "\\\",";   // 组织编码
                wmsParm += "\\\"StrongHoldName\\\"" + ":" + "\\\"" + para.OrgName + "\\\",";   // 组织名
                wmsParm += "\\\"ProductBatch\\\"" + ":" + "\\\"" + (para.SeibanRandom == null ? "" : para.SeibanRandom) + "\\\",";   // 转换后番号
                //wmsParm += "\\\"StoreCondition\\\"" + ":" + "\\\"" + RemoveSpecialCharacter(dc.DescFlexSegments.PrivateDescSeg3) + "\\\",";   // 合同型号
                wmsParm += "\\\"BatchNo\\\"" + ":" + "\\\"" + (string.IsNullOrEmpty(para.Seiban) ? DateTime.Now.ToString("yyyyMMdd") : para.Seiban) + "\\\",";   // 番号
                wmsParm += "\\\"ProtectWay\\\"" + ":" + "\\\"" + "" + "\\\",";   // 销售单据类型
                wmsParm += "\\\"Unit\\\"" + ":" + "\\\"" + para.UnitName + "\\\",";   // 单位
                wmsParm += "\\\"LABELMARK\\\"" + ":" + "\\\"" + "" + "\\\",";   // Customer_voucherno（生产订单的这个字段）
                //wmsParm += "\\\"department\\\"" + ":" + "\\\"" + (dc.TransOutDept == null ? "" : dc.TransOutDept.Code) + "\\\",";   // 部门
                //wmsParm += "\\\"departmentname\\\"" + ":" + "\\\"" + (dc.TransOutDept == null ? "" : dc.TransOutDept.Name) + "\\\"";   // 部门名称
                wmsParm += "\\\"erpwarehouseno\\\"" + ":" + "\\\"" + para.WhCode + "\\\",";   // erp仓库编码
                wmsParm += "\\\"erpwarehousename\\\"" + ":" + "\\\"" + para.WhName + "\\\"";   // erp仓库名称
                wmsParm += "},";
                i++;
            }
            if (wmsParm.Equals("["))
            {
                return false;
            }
            wmsParm = wmsParm.Substring(0, wmsParm.Length - 1);
            wmsParm += "]";
            wmsParm = "{\"BarcodeJson\":\"" + wmsParm + "\"}";
            wmsParm = wmsParm.Replace("\r", "").Replace("\n", "");
            try
            {
                ReturnResult<string> rr = NetHelper.Post<string>(g_wmsUrl, wmsParm, null, LogTypeEnum.AllLog);
                if (rr.Success)
                {
                    LsWmsMessage? wmsMsg = jsonSerializer.Deserialize<LsWmsMessage>(rr.Entity);

                    if (wmsMsg == null || wmsMsg.HeaderStatus != "S")
                    {
                        ErrorMsg = wmsMsg?.Message;
                        return false;
                    }
                }
                else
                {
                    ErrorMsg = rr.Msg;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                return false;
            }
            //RestClient client = new RestClient(g_wmsUrl);
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddParameter("application/json", wmsParm, ParameterType.RequestBody);
            ////request.AddParameter("application/json", paraJson, ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode == HttpStatusCode.OK && response.Content.Length > 0)
            //{
            //    string wmsJson = response.Content;
            //    wmsJson = jsonSerializer.Deserialize<string>(wmsJson);
            //    LsWmsMessage wmsMsg = jsonSerializer.Deserialize<LsWmsMessage>(wmsJson);

            //    if (wmsMsg == null || wmsMsg.HeaderStatus != "S")
            //    {
            //        ErrorMsg = wmsMsg.Message;
            //        return false;
            //    }
            //}
            //else
            //{
            //    //wms_errmsg = string.Format("连接WMS系统失败，请与系统管理员联系。请求参数：{0}", paraJson);
            //    ErrorMsg = string.Format("连接WMS系统失败，请与系统管理员联系。请求参数：{0}", wmsParm);
            //    return false;
            //}

            //return true;
        }

        /// <summary>
        /// 去除特殊字符
        /// </summary>
        /// <param name="hexData"></param>
        /// <returns></returns>
        private static string RemoveSpecialCharacter(string hexData)
        {
            // return Regex.Replace(hexData, "[ \\[ \\] \\^ \\-_*×――(^)$%~!@#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;\"‘’“”-]", "").ToUpper();
            hexData = hexData.Replace("\"", "-");
            hexData = hexData.Replace("/", "-");
            hexData = hexData.Replace("\\", "-");
            hexData = hexData.Replace("\n", "");
            hexData = hexData.Replace("\t", " ");
            hexData = hexData.Replace("{", "(");
            hexData = hexData.Replace("}", ")");
            hexData = hexData.Replace("[", "(");
            hexData = hexData.Replace("]", ")");
            return hexData;
        }

        /// <summary>
        /// 获取条码信息
        /// </summary>
        /// <param name="serialNo">序列号</param>
        /// <returns></returns>
        public static GetBarCodeResult? GetBarCode(string serialNo)
        {
            string sql = $"select barcode,strongholdcode,erpvoucherno from T_OUTBARCODE where serialno='{serialNo}'";
            DatabaseHelper dbHelper = new DatabaseHelper("Data Source = 192.168.250.71; Initial Catalog = WMSDB; User Id = sa; Password = GPGsec2020; TrustServerCertificate=true;");
            var table = dbHelper.ExecuteQuery(sql);
            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                return new GetBarCodeResult
                {
                    BarCode = table.Rows[0]["barcode"].ToString(),
                    OrgCode = table.Rows[0]["strongholdcode"].ToString(),
                    DocNo = table.Rows[0]["erpvoucherno"].ToString()
                };
            }

            return null;
        }
    }

    /// <summary>
    /// wms返回消息实体
    /// </summary>
    public class LsWmsMessage
    {
        /// <summary>
        /// 状态 S成功 E 失败
        /// </summary>
        public string HeaderStatus { get; set; }


        /// <summary>
        /// 失败消息
        /// </summary>
        public string Message { get; set; }

        public string MaterialDoc { get; set; }

        public string TaskNo { get; set; }

        public object ModelJson { get; set; }
    }

    /// <summary>
    /// 链溯创建条码接口参数
    /// </summary>
    public class LsWmsBarCodePara
    {
        public string DocNo { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public string SPECS { get; set; }

        public decimal Qty { get; set; }

        public string Sn { get; set; }

        public string OrgCode { get; set; }

        public string OrgName { get; set; }

        public string UnitName { get; set; }

        public string WhCode { get; set; }

        public string WhName { get; set; }

        public string Seiban { get; set; }

        public string SeibanRandom { get; set; }

        public LsWmsBarCodePara(string docNo, string itemCode, string itemName, string specs, decimal qty, string sn, string orgCode, string orgName, string unitName, string whCode, string whName, string seiban, string seibanRandom)
        {
            DocNo = docNo;
            ItemCode = itemCode;
            ItemName = itemName;
            SPECS = specs;
            Qty = qty;
            Sn = sn;
            OrgCode = orgCode;
            OrgName = orgName;
            UnitName = unitName;
            WhCode = whCode;
            WhName = whName;
            Seiban = seiban;
            SeibanRandom = seibanRandom;
        }
    }

    /// <summary>
    /// 获取链溯中的条码返回值
    /// </summary>
    public class GetBarCodeResult
    {
        /// <summary>
        /// 完整条码
        /// </summary>
        public string? BarCode { get; set; }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string? OrgCode { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string? DocNo { get; set; }
    }
}
