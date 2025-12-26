using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingReturnVMs
{
    public partial class PurchaseOutsourcingReturnApiBatchVM : BaseBatchVM<PurchaseOutsourcingReturn, PurchaseOutsourcingReturnApi_BatchEdit>
    {
        public PurchaseOutsourcingReturnApiBatchVM()
        {
            ListVM = new PurchaseOutsourcingReturnApiListVM();
            LinkedVM = new PurchaseOutsourcingReturnApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingReturnApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
