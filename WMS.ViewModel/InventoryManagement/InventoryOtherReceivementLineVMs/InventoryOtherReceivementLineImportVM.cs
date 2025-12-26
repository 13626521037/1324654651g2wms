
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementLineVMs
{
    public partial class InventoryOtherReceivementLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryOtherReceivementLineImportVM : BaseImportVM<InventoryOtherReceivementLineTemplateVM, InventoryOtherReceivementLine>
    {
            //import

    }

}