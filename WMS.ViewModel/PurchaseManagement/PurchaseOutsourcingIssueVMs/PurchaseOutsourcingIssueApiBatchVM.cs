using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueApiBatchVM : BaseBatchVM<PurchaseOutsourcingIssue, PurchaseOutsourcingIssueApi_BatchEdit>
    {
        public PurchaseOutsourcingIssueApiBatchVM()
        {
            ListVM = new PurchaseOutsourcingIssueApiListVM();
            LinkedVM = new PurchaseOutsourcingIssueApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PurchaseOutsourcingIssueApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
