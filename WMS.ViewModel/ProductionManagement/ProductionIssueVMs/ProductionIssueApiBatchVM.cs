using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;


namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueApiBatchVM : BaseBatchVM<ProductionIssue, ProductionIssueApi_BatchEdit>
    {
        public ProductionIssueApiBatchVM()
        {
            ListVM = new ProductionIssueApiListVM();
            LinkedVM = new ProductionIssueApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionIssueApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
