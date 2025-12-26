using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// 不记录日志
        /// </summary>
        NoLog = 0,
        /// <summary>
        /// 均记录日志（无论成功或失败）
        /// </summary>
        AllLog = 1,
        /// <summary>
        /// 只记录失败的
        /// </summary>
        OnlyError = 2
    }

    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
    }
}
