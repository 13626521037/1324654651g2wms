using Nancy.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 网络操作
    /// </summary>
    public class NetHelper
    {
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="paras">json格式的参数</param>
        /// <returns></returns>
        public static ReturnResult Post(string address, string paras, List<KeyValuePair<string, string>>? headers, LogTypeEnum logType = LogTypeEnum.OnlyError)
        {
            ReturnResult rr = new ReturnResult() { Success = true, Msg = "" };

            RestClient client = new RestClient(address);
            //client.Timeout = -1;
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.Timeout = TimeSpan.FromMinutes(5);  // 换成这个没有测试过
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            request.AddParameter("application/json", paras, ParameterType.RequestBody);
            //request.AddParameter("application/json", paraJson, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null && response.Content.Length > 0)
            {
                rr.Msg = response.Content;
            }
            else
            {
                rr.Success = false;
                rr.Msg = string.Format("错误代码：{1}<br/><br/>错误信息：{0}<br/><br/>请求地址：{2}<br/><br/>请求参数：{3}", response.Content, response.StatusCode, address, paras);
            }

            // 日志处理
            string log = $"\r\n请求地址：{address}\r\n" +
                        $"请求参数：{paras}\r\n" +
                        $"响应结果编码：{response.StatusCode}\r\n" +
                        $"响应结果内容：{response.Content}\r\n";
            if (logType == LogTypeEnum.AllLog)
            {
                if(rr.Success)
                {
                    Log.Information(log);
                }
                else
                {
                    Log.Error(log);
                }
            }
            else if (logType == LogTypeEnum.OnlyError)
            {
                if (!rr.Success)
                {
                    Log.Error(log);
                }
            }

            return rr;
        }

        /// <summary>
        /// post请求，并将相应的返回值转换为指定类型
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="address">地址</param>
        /// <param name="paras">json格式的参数</param>
        /// <returns></returns>
        public static ReturnResult<T> Post<T>(string address, string paras, List<KeyValuePair<string, string>>? headers, LogTypeEnum logType = LogTypeEnum.OnlyError)
        {
            ReturnResult<T> rr = new ReturnResult<T>() { Success = true, Msg = "", Entity = default(T) };

            RestClient client = new RestClient(address);
            // client.Timeout = -1;
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.Timeout = TimeSpan.FromMinutes(5);  // 换成这个没有测试过
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            request.AddParameter("application/json", paras, ParameterType.RequestBody);
            //request.AddParameter("application/json", paraJson, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null && response.Content.Length > 0)
            {
                try
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    string json = response.Content;
                    //json = jsonSerializer.Deserialize<string>(json);
                    rr.Msg = json;
                    rr.Entity = jsonSerializer.Deserialize<T>(json);
                }
                catch (Exception e)
                {
                    rr.Success = false;
                    rr.Msg = "转换实体类型时失败。错误信息：" + e.Message;
                }
            }
            else
            {
                rr.Success = false;
                rr.Msg = string.Format("请求失败。错误信息：{0}。错误代码：{1}。请求地址：{2}。请求参数：{3}", response.Content, response.StatusCode, address, paras);
            }

            // 日志处理
            string log = $"\r\n请求地址：{address}\r\n" +
                        $"请求参数：{paras}\r\n" +
                        $"响应结果编码：{response.StatusCode}\r\n" +
                        $"响应结果内容：{response.Content}\r\n";
            if (logType == LogTypeEnum.AllLog)
            {
                if (rr.Success)
                {
                    Log.Information(log);
                }
                else
                {
                    Log.Error(log);
                }
            }
            else if (logType == LogTypeEnum.OnlyError)
            {
                if (!rr.Success)
                {
                    Log.Error(log);
                }
            }

            return rr;
        }

        /// <summary>
        /// IP地址转long
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static long IpToLong(string ip)
        {
            long result = 0l;
            string[] arr = ip.Split(".");//将IP地址以“”“”"."分为四个数组元素
            for (int i = 0; i < arr.Length; i++)
            {
                /**
                 * 将每个元素左移（8*i）位，并将结果与前面已经算出的结果做或运算，ip的每
                 * 三位数字都可以用八位二进制表示，将每三位左移（8*i）位后，会将数字向高
                 * 位移动（8*i）位，低位会全部用0补足，与result做或运算后，低位将由result代替，
                 * 最终将四个三位数字组成一个Long类型的整数
                 */
                result |= (long.Parse(arr[i]) << (8 * i));
            }
            return result;
        }

        /// <summary>
        /// long转ip地址
        /// </summary>
        /// <param name="LIP"></param>
        /// <returns></returns>
        public static string LongToIp(long LIP)
        {
            StringBuilder ip = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                /**
                 * 将Long整数右移（8*i）位，每次都会将待转化的数字移动到整个Long整数的低八位，
                 * 然后将这个整数与（0xFF）做与运算，（0xFF）的低八位是1，剩余位全都为0，任何数与其做
                 * 与运算都是低八位不变，高位变成0，这样就将每个三位数从Long整数中取出
                 */
                if (i < 3)
                {
                    ip.Append(((LIP >> (8 * i)) & 0xff) + ".");
                }
                else
                {
                    ip.Append((LIP >> (8 * i)) & 0xff);
                }
            }
            return ip.ToString();
        }
    }
}
