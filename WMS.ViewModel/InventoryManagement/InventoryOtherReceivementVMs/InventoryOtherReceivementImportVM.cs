
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryOtherReceivementVMs
{
    public partial class InventoryOtherReceivementTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryOtherReceivement._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.ErpID);
        [Display(Name = "_Model._InventoryOtherReceivement._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.BusinessDate, true);
        [Display(Name = "_Model._InventoryOtherReceivement._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.DocNo);
        [Display(Name = "_Model._InventoryOtherReceivement._IsScrap")]
        public ExcelPropety IsScrap_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.IsScrap);
        [Display(Name = "_Model._InventoryOtherReceivement._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.Memo);
        [Display(Name = "_Model._InventoryOtherReceivement._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryOtherReceivement._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryOtherReceivement._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryOtherReceivement._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryOtherReceivement>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class InventoryOtherReceivementImportVM : BaseImportVM<InventoryOtherReceivementTemplateVM, InventoryOtherReceivement>
    {
            //import

    }

}