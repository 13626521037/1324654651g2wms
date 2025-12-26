using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReturnVMs
{
    public partial class PurchaseReturnApiBatchVM : BaseBatchVM<PurchaseReturn, PurchaseReturnApi_BatchEdit>
    {
        public PurchaseReturnApiBatchVM()
        {
            ListVM = new PurchaseReturnApiListVM();
            LinkedVM = new PurchaseReturnApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReturnApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
