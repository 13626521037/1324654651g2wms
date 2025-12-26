using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventorySplitSingle._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.DocNo);
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        public ExcelPropety OriginalInv_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.OriginalInvId);
        [Display(Name = "_Model._InventorySplitSingle._OriginalQty")]
        public ExcelPropety OriginalQty_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.OriginalQty);
        [Display(Name = "_Model._InventorySplitSingle._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.Memo);

	    protected override void InitVM()
        {
            OriginalInv_Excel.DataType = ColumnDataType.ComboBox;
            OriginalInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber);
        }

    }

    public class InventorySplitSingleApiImportVM : BaseImportVM<InventorySplitSingleApiTemplateVM, InventorySplitSingle>
    {

    }

}
