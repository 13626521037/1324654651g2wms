using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;


namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementApiBatchVM : BaseBatchVM<SalesReturnReceivement, SalesReturnReceivementApi_BatchEdit>
    {
        public SalesReturnReceivementApiBatchVM()
        {
            ListVM = new SalesReturnReceivementApiListVM();
            LinkedVM = new SalesReturnReceivementApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class SalesReturnReceivementApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
