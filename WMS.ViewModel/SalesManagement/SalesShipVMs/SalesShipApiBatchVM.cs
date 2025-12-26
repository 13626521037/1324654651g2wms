using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;


namespace WMS.ViewModel.SalesManagement.SalesShipVMs
{
    public partial class SalesShipApiBatchVM : BaseBatchVM<SalesShip, SalesShipApi_BatchEdit>
    {
        public SalesShipApiBatchVM()
        {
            ListVM = new SalesShipApiListVM();
            LinkedVM = new SalesShipApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesShipApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
