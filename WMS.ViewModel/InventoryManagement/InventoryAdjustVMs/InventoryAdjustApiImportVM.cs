using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        public ExcelPropety StockTaking_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.StockTakingId);
        [Display(Name = "_Model._InventoryAdjust._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.DocNo);
        [Display(Name = "_Model._InventoryAdjust._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.Memo);

	    protected override void InitVM()
        {
            StockTaking_Excel.DataType = ColumnDataType.ComboBox;
            StockTaking_Excel.ListItems = DC.Set<InventoryStockTaking>().GetSelectListItems(Wtm, y => y.ApproveUser);
        }

    }

    public class InventoryAdjustApiImportVM : BaseImportVM<InventoryAdjustApiTemplateVM, InventoryAdjust>
    {

    }

}
