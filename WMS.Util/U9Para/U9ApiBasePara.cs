using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// U9API访问参数基类
    /// </summary>
    public class U9ApiBasePara
    {
        /// <summary>
        /// U9基础上下文对象
        /// </summary>
        public U9ApiContext context = new U9ApiContext();

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string lastUpdateTime { get; set; }

        public string guid { get; set; }

        public string wms_user { get; set; }

        /// <summary>
        /// 业务主键（作用：为了避免构造guid参数的哈希值时，多张单据可能存在相同的参数，导致生成的哈希值相同。增加业务主键，当前业务场景下，可以保证哈希值唯一性）
        /// </summary>
        public Guid? businessId { get; set; }

        public U9ApiBasePara()
        {
            lastUpdateTime = "";
            guid = "";
            wms_user = "";
            businessId = Guid.Empty;
        }

        public void InitGuid()
        {
            guid = "";
            string str = JsonConvert.SerializeObject(this);
            guid = Common.GetSHA256Hash(str);
        }

        /// <summary>
        /// 作为参数时，必须使用此方法序列化（自动生成guid）
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            InitGuid();
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// U9API标准Context
    /// </summary>
    public class U9ApiContext
    {
        public string CultureName { get; set; }

        public string EntCode { get; set; }

        public string OrgCode { get; set; }

        public string UserCode { get; set; }

        public U9ApiContext()
        {
            CultureName = "zh-CN";
            UserCode = "G2WMS_API";
            EntCode = "";
            OrgCode = "";
        }
    }
}
