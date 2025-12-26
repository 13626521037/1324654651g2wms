
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementLineVMs
{
    public partial class SalesReturnReceivementLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class SalesReturnReceivementLineImportVM : BaseImportVM<SalesReturnReceivementLineTemplateVM, SalesReturnReceivementLine>
    {
            //import

    }

}