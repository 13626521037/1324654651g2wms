using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventorySplit._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.DocNo);
        [Display(Name = "_Model._InventorySplit._OldInv")]
        public ExcelPropety OldInv_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.OldInvId);
        [Display(Name = "_Model._InventorySplit._NewInv")]
        public ExcelPropety NewInv_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.NewInvId);
        [Display(Name = "_Model._InventorySplit._OrigQty")]
        public ExcelPropety OrigQty_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.OrigQty);
        [Display(Name = "_Model._InventorySplit._SplitQty")]
        public ExcelPropety SplitQty_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.SplitQty);
        [Display(Name = "_Model._InventorySplit._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.Memo);

	    protected override void InitVM()
        {
            OldInv_Excel.DataType = ColumnDataType.ComboBox;
            OldInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber);
            NewInv_Excel.DataType = ColumnDataType.ComboBox;
            NewInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber);
        }

    }

    public class InventorySplitApiImportVM : BaseImportVM<InventorySplitApiTemplateVM, InventorySplit>
    {

    }

}
