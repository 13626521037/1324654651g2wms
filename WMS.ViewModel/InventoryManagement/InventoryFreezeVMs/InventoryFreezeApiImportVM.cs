using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryFreeze._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.DocNo);
        [Display(Name = "_Model._InventoryFreeze._Reason")]
        public ExcelPropety Reason_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.Reason);
        [Display(Name = "_Model._InventoryFreeze._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.Memo);

	    protected override void InitVM()
        {
        }

    }

    public class InventoryFreezeApiImportVM : BaseImportVM<InventoryFreezeApiTemplateVM, InventoryFreeze>
    {

    }

}
