using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.InventoryManagement;
using WMS.Model.BaseData;


namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryMoveLocation._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.DocNo);
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        public ExcelPropety InWhLocation_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.InWhLocationId);
        [Display(Name = "_Model._InventoryMoveLocation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.Memo);

	    protected override void InitVM()
        {
            InWhLocation_Excel.DataType = ColumnDataType.ComboBox;
            InWhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryMoveLocationApiImportVM : BaseImportVM<InventoryMoveLocationApiTemplateVM, InventoryMoveLocation>
    {

    }

}
