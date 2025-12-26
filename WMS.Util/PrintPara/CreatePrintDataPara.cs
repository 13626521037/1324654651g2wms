using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreatePrintDataPara
    {
        public string ModuleId { get; set; }

        public string OperatorName { get; set; }

        public List<CreatePrintDataLinePara> Records { get; set; }
    }

    public class CreatePrintDataLinePara
    {
        public List<CreatePrintDataSubLinePara> Fields { get; set; }
    }

    public class CreatePrintDataSubLinePara
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
