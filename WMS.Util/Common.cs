using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Support.Json;
using WMS.Model.BaseData;

namespace WMS.Util
{
    /// <summary>
    /// 常用的工具类
    /// </summary>
    public class Common
    {
        private static List<(string, string)> FunctionAndName = new List<(string, string)>()
        {
            { ("/api/purchasereceivement/saveinspectiondata", "采购收货-检验") },
            { ("/api/PurchaseReceivement/Receiving", "采购收货-收货") },
            { ("/api/PurchaseReceivement/ApproveAndPutaway", "采购收货-审核") },
            { ("/api/PurchaseReturn/SaveOffDetails", "采购退货-下架") },
            { ("/api/purchasereturn/approve/{id}", "采购退货-审核") },
            { ("/api/PurchaseOutsourcingIssue/SaveOffDetails", "委外发料-下架") },
            { ("/api/PurchaseOutsourcingIssue/approve/{id}", "委外发料-审核") },
            { ("/api/PurchaseOutsourcingReturn/Receiving", "委外退料-收货") },
            { ("/api/PurchaseOutsourcingReturn/ApproveAndPutaway", "委外退料-审核") },
            { ("/api/InventoryTransferOutDirect/CreateTransferOut", "直接调出单-创建") },
            { ("/api/InventoryTransferOutManual/SaveOffDetails", "手动调出单-下架") },
            { ("/api/inventorytransferoutmanual/approve/{id}", "手动调出单-审核") },
            { ("/api/InventoryTransferIn/CreateTransferIn", "调入单-创建") },
            { ("/api/InventoryTransferIn/ApproveAndPutaway", "调入单-审核") },
            { ("/api/InventoryAdjustDirect/Save", "手动调整单-创建") },
            { ("/api/InventoryMoveLocation/Save", "移库单-创建") },
            { ("/api/InventoryOtherShip/CreateInventoryOtherShip", "其它出库单-创建") },
            { ("/api/InventoryOtherReceivement/CreateDoc", "其它入库单-创建") },
            { ("/api/InventorySplit/Save", "库存拆分单-创建") },
            { ("/api/InventoryUnfreeze/Save", "库存解冻单-创建") },
            { ("/api/InventoryFreeze/Save", "库存冻结单-创建") },
            { ("/api/InventoryFreeze/Save", "组托单-创建") },
            { ("/api/InventoryOtherShip/CreateInventoryOtherShip", "杂发-创建") },
            { ("/api/InventoryOtherReceivement/CreateDoc", "杂收-创建") },
            { ("/api/InventoryStockTaking/StockTakeScan", "盘点单-盘点扫码") },
            { ("/api/Knife/KnifeCheckOut", "刀具领用") },
            { ("/api/Knife/KnifeCheckIn", "刀具归还") },
            { ("/api/Knife/KnifeScrap", "刀具报废") },
            { ("/api/Knife/KnifeReplace", "组合刀具配件替换") },
            { ("/api/Knife/KnifeTransferOut", "刀具调出") },
            { ("/api/Knife/KnifeTransferIn", "刀具调入") },
            { ("/api/Knife/KnifeMove", "刀具移库") },
            { ("/api/Knife/KnifeGrindRequest", "刀具修磨申请") },
            { ("/api/Knife/KnifeGrindOut", "刀具修磨出库") },
            { ("/api/Knife/KnifeGrindIn", "刀具修磨入库") },
            { ("/api/Knife/GetKnifeAndInventory", "查询刀具和库存信息") },

        };

        /// <summary>
        /// 添加详情弹窗
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="dialogTitle"></param>
        /// <param name="content"></param>
        /// <param name="dialogWidth"></param>
        /// <returns></returns>
        public static string AddDetailDialog(string url, string id, string dialogTitle, string content, int dialogWidth = 800)
        {
            return $"<span style='text-decoration: underline;cursor: pointer;' onclick='ff.OpenDialog(&quot;{url}?id={id}&quot;,&quot;f44227f2045345c190156c1c72c2bdd5&quot;,&quot;{dialogTitle}&quot;,{dialogWidth},null,undefined,false)'>{content}</span>";
        }

        /// <summary>
        /// 添加详情的提示窗
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string AddMsgDialog(string url, string dialogTitle, string content, int dialogWidth = 800, int dialogHeight = 400)
        {
            // 有url，内容传url，否则传content
            return $"<span style='text-decoration: underline;cursor: pointer;' onclick='ShowUrlMsg(&quot;{dialogTitle}&quot;,&quot;{url}&quot;,{dialogWidth},{dialogHeight})'>{content}</span>";
        }

        /// <summary>
        /// 添加库存信息详情弹窗
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="dialogTitle"></param>
        /// <param name="content"></param>
        /// <param name="dialogWidth"></param>
        /// <returns></returns>
        public static string AddInventoryDialog(BaseInventory inventory)
        {
            if (inventory == null)
                return "";
            return AddDetailDialog("/BaseData/BaseInventory/Details", inventory.ID.ToString(), "库存信息", inventory.SerialNumber, 1000);
        }

        /// <summary>
        /// 添加库位信息详情弹窗
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string AddLocationDialog(BaseWhLocation location)
        {
            if (location == null)
                return "";
            return AddDetailDialog("/BaseData/BaseWhLocation/Details", location.ID.ToString(), "库位信息", location.Code, 800);
        }

        /// <summary>
        /// 添加二维码弹窗
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string AddBarCodeDialog(string barcode)
        {
            if (barcode == null)
                return "";
            return AddMsgDialog("/Home/ShowBarCode?code=" + barcode, "二维码", barcode, 400, 300);
        }

        /// <summary>
        /// 获取随机13位字符串
        /// </summary>
        /// <returns></returns>
        public static string GetRandom13()
        {
            // 从2001.09.09 09:46:40 到 2286.11.21 01:46:39 的时间戳为13位。从1000000000000到9999999999999。只要时间在这个范围内，生成的随机数就不会重复。
            Thread.Sleep(1);    // 等待1毫秒，保证时间戳不重复
            string text = ((long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString();
            text += text;   // 加上两次，保证长度超过18位
            text = text.Substring(text.Length - 18, 18);    // 截取最后18位

            string text2 = "";
            for (int num = text.Length - 1; num >= 0; num--)    // 将18位的数字反向重新排列（如果最后一位为0，则自动加1毫秒）
            {
                if (num != text.Length - 1 || text[num] != '0')
                {
                    text2 += text[num];
                }
                else
                {
                    text2 += "1";   // 此处无需等待1毫秒。因为是13位+13位取后18位，只改了最后一位，前面被截断的时间戳毫秒数仍是0。所以即使下一毫秒正好再取数，也是不会重复的。
                }
                // text2 = ((num != text.Length - 1 || text[num] != '0') ? (text2 + text[num]) : (text2 + "5"));
            }

            string text3 = text2.Substring(text2.Length - 1, 1);    // 取最后一位
            string text4 = text2.Substring(text2.Length - 2, 1);    // 取倒数第二位
            string text5 = text2.Substring(2, 1);   // 取第三位
            string text6 = text2.Substring(3, 1);   // 取第四位
            text2 = text2.Substring(0, 2) + text3 + text4 + text2.Substring(4, text2.Length - 6) + text6 + text5;   // 对text2进行顺序打乱，避免用户破解随机码含义
            long seed = long.Parse(text2);
            return 5 + NumberBaseConversionFrom10(seed, 35);    // 转换为36进制（0-9，A-Z）。从1000000000000到9999999999999对应的36进制数范围为118RF2EI4YH5到ACHTAO56ETVE。均在36进制数12位范围内。最后在前面加上3，保证长度为13位。（原因：原来要求取12位，后改为13位，直接加一位固定数比较方便）
        }

        private static string NumberBaseConversionFrom10(long seed, int numberBase)
        {
            string result = "";
            long num = seed % numberBase;
            if (seed > 0)
            {
                seed /= numberBase;
                result = NumberBaseConversionFrom10(seed, numberBase);
                result = ((num < 10) ? (result + num) : (result + (char)(55 + num)));
            }

            return result;
        }

        /// <summary>
        /// 获取SHA256哈希值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 根据条码获取SN
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns></returns>
        public static string GetSN(string barcode)
        {
            string[] arr = barcode.Split('@');
            if (arr.Length != 4)
            {
                return "";
            }
            return arr[3];
        }

        /// <summary>
        /// 根据条码获取SN（批量）。中间如果存在错误条码，则跳过，不影响整体结果
        /// </summary>
        /// <param name="barcodes">条码集合</param>
        /// <returns></returns>
        public static List<string> GetSNs(List<string> barcodes)
        {
            List<string> result = new List<string>();
            foreach (string barcode in barcodes)
            {
                string sn = GetSN(barcode);
                if (!string.IsNullOrEmpty(sn))
                {
                    result.Add(sn);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<string> GetPrivileges(List<SimpleFunctionPri> functionPris, WTMContext Wtm)
        {
            List<string> result = new List<string>();
            var functionPris2 = functionPris.Where(x => x.Allowed == true).Select(x => x.MenuItemId).ToList();

            if (functionPris2 == null || functionPris2.Count == 0)
            {
                return result;
            }
            List<FrameworkMenu> menus = Wtm.DC.Set<FrameworkMenu>().Where(x => functionPris2.Contains(x.ID)).ToList();
            if (menus == null || menus.Count == 0)
            {
                return result;
            }

            foreach (var item in FunctionAndName)
            {
                if (menus.Exists(x => x.Url == item.Item1.ToLower()))
                {
                    result.Add(item.Item2);
                }
            }
            return result;
        }
    }
}
