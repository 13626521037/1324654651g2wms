using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.BaseData
{
    public class BaseDepartmentReturn: BaseReturn
    {
        public Guid? OrgId { get; set; }
    }
}
