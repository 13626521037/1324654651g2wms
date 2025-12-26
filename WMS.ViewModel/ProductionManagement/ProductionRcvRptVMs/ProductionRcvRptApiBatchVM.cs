using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;


namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptApiBatchVM : BaseBatchVM<ProductionRcvRpt, ProductionRcvRptApi_BatchEdit>
    {
        public ProductionRcvRptApiBatchVM()
        {
            ListVM = new ProductionRcvRptApiListVM();
            LinkedVM = new ProductionRcvRptApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class ProductionRcvRptApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
