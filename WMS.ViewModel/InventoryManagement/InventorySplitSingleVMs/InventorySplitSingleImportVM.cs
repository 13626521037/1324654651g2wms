
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.InventoryManagement.InventorySplitSingleVMs
{
    public partial class InventorySplitSingleTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventorySplitSingle._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.DocNo);
        [Display(Name = "_Model._InventorySplitSingle._OriginalInv")]
        public ExcelPropety OriginalInv_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.OriginalInvId);
        [Display(Name = "_Model._InventorySplitSingle._OriginalQty")]
        public ExcelPropety OriginalQty_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.OriginalQty);
        [Display(Name = "_Model._InventorySplitSingle._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.Memo);
        [Display(Name = "_Model._InventorySplitSingle._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventorySplitSingle._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventorySplitSingle._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.CreateBy);
        [Display(Name = "_Model._InventorySplitSingle._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventorySplitSingle>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            OriginalInv_Excel.DataType = ColumnDataType.ComboBox;
            OriginalInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());

        }

    }

    public class InventorySplitSingleImportVM : BaseImportVM<InventorySplitSingleTemplateVM, InventorySplitSingle>
    {
            //import

    }

}