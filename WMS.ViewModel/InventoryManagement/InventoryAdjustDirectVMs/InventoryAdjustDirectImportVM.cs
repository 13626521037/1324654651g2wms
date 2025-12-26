
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

namespace WMS.ViewModel.InventoryManagement.InventoryAdjustDirectVMs
{
    public partial class InventoryAdjustDirectTemplateVM : BaseTemplateVM
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
        [Display(Name = "_Model._InventoryAdjustDirect._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryAdjustDirect._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryAdjustDirect._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryAdjustDirect._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryAdjustDirect>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            OldInv_Excel.DataType = ColumnDataType.ComboBox;
            OldInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());
            NewInv_Excel.DataType = ColumnDataType.ComboBox;
            NewInv_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());

        }

    }

    public class InventoryAdjustDirectImportVM : BaseImportVM<InventoryAdjustDirectTemplateVM, InventoryAdjustDirect>
    {
            //import

    }

}