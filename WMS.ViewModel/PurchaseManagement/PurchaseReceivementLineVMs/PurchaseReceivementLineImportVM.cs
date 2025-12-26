
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;

namespace WMS.ViewModel.PurchaseManagement.PurchaseReceivementLineVMs
{
    public partial class PurchaseReceivementLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class PurchaseReceivementLineImportVM : BaseImportVM<PurchaseReceivementLineTemplateVM, PurchaseReceivementLine>
    {
            //import

    }

}