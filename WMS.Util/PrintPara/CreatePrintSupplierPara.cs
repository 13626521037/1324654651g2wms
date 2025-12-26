using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Util
{
    public class CreatePrintSupplierPara
    {
        public string ModuleId { get; set; }

        public string OperatorName { get; set; }

        public List<CreatePrintSupplierLinePara> Records { get; set; }
    }

    public class CreatePrintSupplierLinePara
    {
        public List<CreatePrintSupplierSubLinePara> Fields { get; set; }
    }

    public class CreatePrintSupplierSubLinePara
    {
        public string FieldName { get; set; }

        public string FieldValue { get; set; }
    }
}
