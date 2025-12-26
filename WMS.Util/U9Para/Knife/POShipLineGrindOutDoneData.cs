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
    internal class POShipLineGrindOutDoneData : U9ApiBasePara
    {
        public List<System.Int64> pOShipLineIds;

        public POShipLineGrindOutDoneData(List<System.Int64> Ids)
        {
            pOShipLineIds = Ids;
        }

    }
   
}
