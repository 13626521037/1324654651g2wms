using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public static class DecimalExtensions
    {
        public static decimal TrimZero(this decimal value)
        {
            return decimal.Parse(value.ToString("0.#############################"));
        }

        public static decimal? TrimZero(this decimal? value)
        {
            if(value == null)
                return null;
            return decimal.Parse(((decimal)value).ToString("0.#############################"));
        }
    }
}
