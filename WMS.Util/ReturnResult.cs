using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 消息型返回值类型
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }

        public ReturnResult()
        {
            Success = true;
            Msg = "";
        }

        /// <summary>
        /// 设置成功
        /// </summary>
        /// <param name="msg">返回消息</param>
        public void SetSuccess(string msg)
        {
            Success = true;
            Msg = msg.StartsWith("U9:") || msg.StartsWith("[U9]") ? msg : "[G2WMS] " + msg;
        }

        /// <summary>
        /// 设置失败
        /// </summary>
        /// <param name="msg"></param>
        public void SetFail(string msg)
        {
            Success = false;
            Msg = msg.StartsWith("U9:") || msg.StartsWith("[U9]") ? msg :"[G2WMS] " + msg;
        }

        /// <summary>
        /// 返回本实例的json格式
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 实体返回值类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnResult<T> : ReturnResult
    {
        /// <summary>
        /// 返回实体
        /// </summary>
        public T? Entity { get; set; }
    }
}
