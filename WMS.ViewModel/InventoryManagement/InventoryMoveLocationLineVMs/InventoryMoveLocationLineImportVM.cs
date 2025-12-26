
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationLineVMs
{
    public partial class InventoryMoveLocationLineTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryMoveLocationLineImportVM : BaseImportVM<InventoryMoveLocationLineTemplateVM, InventoryMoveLocationLine>
    {
            //import

    }

}