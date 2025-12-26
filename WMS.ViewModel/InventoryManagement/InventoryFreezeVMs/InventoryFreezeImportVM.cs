
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryFreezeVMs
{
    public partial class InventoryFreezeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryFreeze._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.DocNo);
        [Display(Name = "_Model._InventoryFreeze._Reason")]
        public ExcelPropety Reason_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.Reason);
        [Display(Name = "_Model._InventoryFreeze._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.Memo);
        [Display(Name = "_Model._InventoryFreeze._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryFreeze._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryFreeze._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryFreeze._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryFreeze>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryFreezeImportVM : BaseImportVM<InventoryFreezeTemplateVM, InventoryFreeze>
    {
            //import

    }

}