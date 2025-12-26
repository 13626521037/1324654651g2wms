using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryOtherReceivement._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.ErpID);
        [Display(Name = "_Model._InventoryOtherReceivement._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.BusinessDate);
        [Display(Name = "_Model._InventoryOtherReceivement._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.DocNo);
        [Display(Name = "_Model._InventoryOtherReceivement._IsScrap")]
        public ExcelPropety IsScrap_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.IsScrap);
        [Display(Name = "_Model._InventoryOtherReceivement._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.Memo);

	    protected override void InitVM()
        {
        }

    }

    public class InventoryOtherReceivementApiImportVM : BaseImportVM<InventoryOtherReceivementApiTemplateVM, InventoryOtherReceivement>
    {

    }

}
