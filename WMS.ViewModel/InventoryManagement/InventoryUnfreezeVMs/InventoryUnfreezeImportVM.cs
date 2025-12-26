
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryUnfreezeVMs
{
    public partial class InventoryUnfreezeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryUnfreeze._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.DocNo);
        [Display(Name = "_Model._InventoryUnfreeze._Reason")]
        public ExcelPropety Reason_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.Reason);
        [Display(Name = "_Model._InventoryUnfreeze._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.Memo);
        [Display(Name = "_Model._InventoryUnfreeze._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryUnfreeze._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryUnfreeze._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryUnfreeze._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryUnfreeze>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryUnfreezeImportVM : BaseImportVM<InventoryUnfreezeTemplateVM, InventoryUnfreeze>
    {
            //import

    }

}