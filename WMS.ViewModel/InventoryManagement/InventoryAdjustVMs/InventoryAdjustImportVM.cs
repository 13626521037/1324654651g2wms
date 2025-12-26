
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustVMs
{
    public partial class InventoryAdjustTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryAdjust._StockTaking")]
        public ExcelPropety StockTaking_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.StockTakingId);
        [Display(Name = "_Model._InventoryAdjust._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.DocNo);
        [Display(Name = "_Model._InventoryAdjust._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.Memo);
        [Display(Name = "_Model._InventoryAdjust._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryAdjust._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryAdjust._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryAdjust._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryAdjust>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            StockTaking_Excel.DataType = ColumnDataType.ComboBox;
            StockTaking_Excel.ListItems = DC.Set<InventoryStockTaking>().GetSelectListItems(Wtm, y => y.ErpID.ToString());

        }

    }

    public class InventoryAdjustImportVM : BaseImportVM<InventoryAdjustTemplateVM, InventoryAdjust>
    {
            //import

    }

}