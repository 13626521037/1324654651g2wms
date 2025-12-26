using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryAdjustDirect._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.DocNo);
        [Display(Name = "_Model._InventoryAdjustDirect._OldInv")]
        public ExcelPropety OldInv_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.OldInvId);
        [Display(Name = "_Model._InventoryAdjustDirect._NewInv")]
        public ExcelPropety NewInv_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.NewInvId);
        [Display(Name = "_Model._InventoryAdjustDirect._DiffQty")]
        public ExcelPropety DiffQty_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.DiffQty);
        [Display(Name = "_Model._InventoryAdjustDirect._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.Memo);

	    protected override void InitVM()
        {
            OldInv_Excel.DataType = ColumnDataType.ComboBox;
            OldInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber);
            NewInv_Excel.DataType = ColumnDataType.ComboBox;
            NewInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber);
        }

    }

    public class InventoryAdjustDirectApiImportVM : BaseImportVM<InventoryAdjustDirectApiTemplateVM, InventoryAdjustDirect>
    {

    }

}
