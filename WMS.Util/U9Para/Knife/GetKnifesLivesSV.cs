using Aliyun.OSS;
using Nancy.Json;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;

namespace WMS.Util.U9Para.Knife
{
    public class GetKnifesLivesSV 
    {
        public string pODocNo { get; set; }
        public GetKnifesLivesSV(string DocNo)
        {
            pODocNo = DocNo;

        }
        public static U9KnifeLivesReturn GetKnifesLives_1(List<string> itemCodes)
        {
            var rr = new U9KnifeLivesReturn();
            
            string address = "http://192.168.250.66:20002/api.php/api/U9apitest/GetToolLifeSV";
            var u9KnifeLivesPara = new U9KnifeLivesPara(itemCodes);
            string paras = JsonConvert.SerializeObject(u9KnifeLivesPara);
            //组参数 复制post里的
            RestClient client = new RestClient(address);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.Timeout = TimeSpan.FromMinutes(5);
            request.AddParameter("application/json", paras, ParameterType.RequestBody);
            //执行
            RestResponse response = client.Execute(request);
            //返回值接收
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null && response.Content.Length > 0)
            {
                try
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    string json = response.Content;
                    //json = jsonSerializer.Deserialize<string>(json);
                    rr = jsonSerializer.Deserialize<U9KnifeLivesReturn>(json);
                    /*if (rr.Success && rr.Data != null)
                    {
                        foreach (var itemCode in itemCodes)
                        {
                            if (!rr.Data.Any(x => x.ItemMaster == itemCode))
                            {
                                rr.Success = false;
                                rr.Message = $"{itemCode}料号在U9寿命表未配置 无法获取寿命";
                                break;
                            }
                        }
                    }*///data都返回 原来的3个查到2个返回true   现在3个查2个返回false   就不需要这段报错了
                    if (!rr.Success)
                    {
                        if (rr.Data == null||(rr.Data!=null&& rr.Data.Count == 0))
                        {
                            //rr.Success = false;
                            rr.Message = $"料号[{string.Join(",", itemCodes)}]在U9刀具寿命表未配置 无法获取信息";
                        }
                        else if (rr.Data != null && rr.Data.Count != 0)
                        {
                            var missingItems = itemCodes
                                .Except(rr.Data.Select(x => x.ItemMaster))
                                .ToList();
                            //rr.Success = false;
                            rr.Message = $"料号[{string.Join(",", missingItems)}]在U9刀具寿命表未配置 无法获取信息";
                        }
                    }
                    if (rr.Success && rr.Data==null|| rr.Success && rr.Data!= null&&rr.Data.Count==0)
                    {          
                            rr.Success = false;
                            rr.Message = $"未查询到刀具寿命";
                    }
                }
                catch (Exception e)
                {
                    rr.Success = false;
                    rr.Message = "转换实体类型时失败。错误信息：" + e.Message;
                }
            }
            else
            {
                rr.Success = false;
                rr.Message = string.Format("请求失败。错误信息：{0}。错误代码：{1}。请求地址：{2}。请求参数：{3}", response.Content, response.StatusCode, address, paras);
            }

            return rr;
        }

    }
    //刀具寿命返回值 return
    public class U9KnifeLivesReturn
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<U9KnifeLivesReturn_Line>? Data { get; set; }

        public U9KnifeLivesReturn()
        {
            Success = false;
            Message = "获取U9刀具寿命失败";
            Data = new List<U9KnifeLivesReturn_Line>();
        }
    }
    public class U9KnifeLivesReturn_Line
    {
        public string ItemMaster { get; set; }
        public decimal CurrentLife { get; set; }
        public decimal FirstDepreciationLife { get; set; }
        public decimal SecondDepreciationLife { get; set; }
        public decimal ThirdDepreciationLife { get; set; }
        public decimal FourthDepreciationLife { get; set; }
        public decimal FifthDepreciationLife { get; set; }
        public bool IsAutoSync { get; set; }
        public bool IsActive { get; set; }
        public bool LedgerIncluded { get; set; }
        public string Remark { get; set; }

        public U9KnifeLivesReturn_Line()
        {
            ItemMaster = "";
            /*CurrentLife = 0;
            FirstDepreciationLife = 0;
            SecondDepreciationLife = 0;
            ThirdDepreciationLife = 0;
            FourthDepreciationLife = 0;
            FifthDepreciationLife = 0;
            IsAutoSync = false;
            IsActive = false;
            LedgerIncluded = false;*/
            Remark = "";
        }
    }
    //刀具寿命参数 para
    public class U9KnifeLivesPara
    {
        public List<string> ItemCode { get; set; }
        public U9KnifeLivesPara()
        {
            ItemCode = new List<string>();
        }
        public U9KnifeLivesPara(List<string> utemCode)
        {
            ItemCode = utemCode;
        }
    }
}
