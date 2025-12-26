
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

namespace WMS.ViewModel.InventoryManagement.InventorySplitVMs
{
    public partial class InventorySplitTemplateVM : BaseTemplateVM
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
        [Display(Name = "_Model._InventorySplit._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventorySplit._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventorySplit._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.CreateBy);
        [Display(Name = "_Model._InventorySplit._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventorySplit>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            OldInv_Excel.DataType = ColumnDataType.ComboBox;
            OldInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());
            NewInv_Excel.DataType = ColumnDataType.ComboBox;
            NewInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());

        }

    }

    public class InventorySplitImportVM : BaseImportVM<InventorySplitTemplateVM, InventorySplit>
    {
            //import

    }

}