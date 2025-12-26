using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WMS.Model.BaseData;
using WMS.Model.PurchaseManagement;

namespace WMS.Util.U9Para.Knife
{
    internal class GetKnifesByPODocNoData : U9ApiBasePara
    {
        public string pODocNo { get; set; }
        public GetKnifesByPODocNoData(string DocNo)
        {
            pODocNo = DocNo;

        }
    }
   
}
