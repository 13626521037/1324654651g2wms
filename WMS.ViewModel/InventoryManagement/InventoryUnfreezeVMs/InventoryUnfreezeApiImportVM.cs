using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryUnfreeze._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.DocNo);
        [Display(Name = "_Model._InventoryUnfreeze._Reason")]
        public ExcelPropety Reason_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.Reason);
        [Display(Name = "_Model._InventoryUnfreeze._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.Memo);

	    protected override void InitVM()
        {
        }

    }

    public class InventoryUnfreezeApiImportVM : BaseImportVM<InventoryUnfreezeApiTemplateVM, InventoryUnfreeze>
    {

    }

}
