using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class GetKnifeInfoResult
    {
        /// <summary>
        /// 匹配数据
        /// </summary>
        public List<GetKnifeInfoMatchDataResult> MatchDatas { get; set; }

        /// <summary>
        /// 不匹配数据
        /// </summary>
        public List<GetKnifeInfoNoMatchDataResult> NoMatchDatas { get; set; }
    }

    /// <summary>
    /// 获取刀具信息的匹配数据
    /// </summary>
    public class GetKnifeInfoMatchDataResult
    {
        public long ItemID { get; set; }

        public string ItemCode { get; set; }

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
    }

    /// <summary>
    /// 获取刀具信息的不匹配数据
    /// </summary>
    public class GetKnifeInfoNoMatchDataResult
    {
        public string Item { get; set; }
    }
}
