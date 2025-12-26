using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.BaseData.BaseInventoryVMs
{
    public partial class BaseInventoryApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._BaseInventory._ItemMaster")]
        public ExcelPropety ItemMaster_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.ItemMasterId);
        [Display(Name = "_Model._BaseInventory._WhLocation")]
        public ExcelPropety WhLocation_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.WhLocationId);
        [Display(Name = "_Model._BaseInventory._BatchNumber")]
        public ExcelPropety BatchNumber_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.BatchNumber);
        [Display(Name = "_Model._BaseInventory._SerialNumber")]
        public ExcelPropety SerialNumber_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.SerialNumber);
        [Display(Name = "_Model._BaseInventory._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.Seiban);
        [Display(Name = "_Model._BaseInventory._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.Qty);
        [Display(Name = "_Model._BaseInventory._IsAbandoned")]
        public ExcelPropety IsAbandoned_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.IsAbandoned);
        [Display(Name = "_Model._BaseInventory._ItemSourceType")]
        public ExcelPropety ItemSourceType_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.ItemSourceType);
        [Display(Name = "_Model._BaseInventory._FrozenStatus")]
        public ExcelPropety FrozenStatus_Excel = ExcelPropety.CreateProperty<BaseInventory>(x => x.FrozenStatus);

	    protected override void InitVM()
        {
            ItemMaster_Excel.DataType = ColumnDataType.ComboBox;
            ItemMaster_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.Name);
            WhLocation_Excel.DataType = ColumnDataType.ComboBox;
            WhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BaseInventoryApiImportVM : BaseImportVM<BaseInventoryApiTemplateVM, BaseInventory>
    {

    }

}
