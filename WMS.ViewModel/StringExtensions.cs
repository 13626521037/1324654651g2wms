using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WalkingTec.Mvvm.Core;
using WMS.Model._Admin;

namespace WMS.ViewModel
{
    public static class StringExtensions
    {
        /// <summary>
        /// 将Code和Name组合。组合后格式如下：[Code] Name
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CodeCombinName(this string code, string name)
        {
            if(string.IsNullOrEmpty(name))
                return code;
            else
                return $"【{code}】 {name}";
        }

        /// <summary>
        /// 将字符串添加HTML格式的12像素的字体大小样式标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SetFontSmallSize(this string str)
        {
            return $"<span style='font-size:12px'>{str}</span>";
        }

        /// <summary>
        /// 将字符串转换为日期时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string str)
        {
            if (DateTime.TryParse(str, out DateTime dt))
            {
                return dt;
            }
            return null;
        }

        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToInt(this string str)
        {
            if (int.TryParse(str, out int i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        /// 将字符串转换为十进制浮点数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string str)
        {
            if (decimal.TryParse(str, out decimal i))
            {
                return i;
            }
            return null;
        }
    }
}
