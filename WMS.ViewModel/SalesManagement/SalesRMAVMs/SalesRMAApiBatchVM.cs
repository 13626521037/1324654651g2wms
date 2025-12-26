using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;


namespace WMS.ViewModel.SalesManagement.SalesRMAVMs
{
    public partial class SalesRMAApiBatchVM : BaseBatchVM<SalesRMA, SalesRMAApi_BatchEdit>
    {
        public SalesRMAApiBatchVM()
        {
            ListVM = new SalesRMAApiListVM();
            LinkedVM = new SalesRMAApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesRMAApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
