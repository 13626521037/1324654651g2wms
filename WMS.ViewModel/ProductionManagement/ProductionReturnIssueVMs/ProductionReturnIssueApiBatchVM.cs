using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;


namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueApiBatchVM : BaseBatchVM<ProductionReturnIssue, ProductionReturnIssueApi_BatchEdit>
    {
        public ProductionReturnIssueApiBatchVM()
        {
            ListVM = new ProductionReturnIssueApiListVM();
            LinkedVM = new ProductionReturnIssueApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionReturnIssueApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
