using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;


namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementVMs
{
    public partial class PurchaseReceivementApiBatchVM : BaseBatchVM<PurchaseReceivement, PurchaseReceivementApi_BatchEdit>
    {
        public PurchaseReceivementApiBatchVM()
        {
            ListVM = new PurchaseReceivementApiListVM();
            LinkedVM = new PurchaseReceivementApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseReceivementApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
