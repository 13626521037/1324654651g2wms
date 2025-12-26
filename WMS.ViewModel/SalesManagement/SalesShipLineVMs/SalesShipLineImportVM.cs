
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesShipLineVMs
{
    public partial class SalesShipLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class SalesShipLineImportVM : BaseImportVM<SalesShipLineTemplateVM, SalesShipLine>
    {
            //import

    }

}