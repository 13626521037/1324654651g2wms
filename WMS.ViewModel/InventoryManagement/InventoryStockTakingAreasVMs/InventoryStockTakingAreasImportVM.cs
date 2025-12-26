
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryStockTakingAreasVMs
{
    public partial class InventoryStockTakingAreasTemplateVM : BaseTemplateVM
    {
        
	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryStockTakingAreasImportVM : BaseImportVM<InventoryStockTakingAreasTemplateVM, InventoryStockTakingAreas>
    {
            //import

    }

}