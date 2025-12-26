using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    /// <summary>
    /// 定义帮助页面显示的内容类
    /// </summary>
    public class Help
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<HelpContent> Contents { get; set; }
    }

    public class HelpContent
    {
        public string Text { get; set; }

        public List<HelpContent> Contents { get; set; }
    }
}
