using Aliyun.OSS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 访问打印接口的帮助类
    /// </summary>
    public class PrintApiHelper
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServerUrl { get; set; }

        public PrintApiHelper(string serverUrl)
        {
            if(serverUrl.EndsWith("/"))
            {
                serverUrl = serverUrl.Substring(0, serverUrl.Length - 1);
            }
            ServerUrl = serverUrl;
        }

        /// <summary>
        /// 获取打印模板
        /// </summary>
        /// <param name="moduleName"></param>
        public ReturnResult<List<GetPrintModuleResult>> GetPrintModule(string moduleName)
        {
            ReturnResult<ReturnResult<List<GetPrintModuleResult>>> rr = NetHelper.Post<ReturnResult<List<GetPrintModuleResult>>>(ServerUrl + "/api/CPrintData/GetPrintModule?type=" + moduleName, "", new List<KeyValuePair<string, string>>());
            if (!rr.Success) // 失败
            {
                if (rr.Entity == null)
                {
                    // 去掉外层的ReturnResult
                    rr.Entity = new ReturnResult<List<GetPrintModuleResult>>();
                    rr.Entity.SetFail(rr.Msg);
                    return rr.Entity;
                }
                else // 不会发生
                {
                    return rr.Entity;
                }
            }
            else
            {
                if (rr.Entity == null)  // 不会发生
                {
                    // 去掉外层的ReturnResult
                    rr.Entity = new ReturnResult<List<GetPrintModuleResult>>();
                    rr.Entity.SetFail("返回数据为空");
                    return rr.Entity;
                }
                else
                {
                    return rr.Entity;
                }
            }
        }

        /// <summary>
        /// 生成打印数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ReturnResult CreatePrintData(CreatePrintDataPara data)
        {
            ReturnResult<ReturnResult> rr = NetHelper.Post<ReturnResult>(ServerUrl + "/api/CPrintData/Create", JsonConvert.SerializeObject(data), new List<KeyValuePair<string, string>>());
            return rr.Entity == null ? rr : rr.Entity;
        }
    }
}
