
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;

namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueLineVMs
{
    public partial class ProductionReturnIssueLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class ProductionReturnIssueLineImportVM : BaseImportVM<ProductionReturnIssueLineTemplateVM, ProductionReturnIssueLine>
    {
            //import

    }

}