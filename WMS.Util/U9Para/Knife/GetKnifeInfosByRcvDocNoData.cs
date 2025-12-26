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
    internal class GetKnifeInfosByRcvDocNoData : U9ApiBasePara
    {
        public string rcvDocNo { get; set; }
        public GetKnifeInfosByRcvDocNoData(string DocNo)
        {
            rcvDocNo = DocNo;

        }
    }
   
}
