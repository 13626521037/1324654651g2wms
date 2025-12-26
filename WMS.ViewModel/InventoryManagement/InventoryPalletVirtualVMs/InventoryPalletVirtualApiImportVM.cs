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


namespace WMS.ViewModel.InventoryManagement.InventoryPalletVirtualVMs
{
    public partial class InventoryPalletVirtualApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._InventoryPalletVirtual._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<InventoryPalletVirtual>(x => x.Code);
        [Display(Name = "_Model._InventoryPalletVirtual._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<InventoryPalletVirtual>(x => x.Status);
        [Display(Name = "_Model._InventoryPalletVirtual._Location")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<InventoryPalletVirtual>(x => x.LocationId);
        [Display(Name = "_Model._InventoryPalletVirtual._SysVersion")]
        public ExcelPropety SysVersion_Excel = ExcelPropety.CreateProperty<InventoryPalletVirtual>(x => x.SysVersion);
        [Display(Name = "_Model._InventoryPalletVirtual._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryPalletVirtual>(x => x.Memo);

	    protected override void InitVM()
        {
            Location_Excel.DataType = ColumnDataType.ComboBox;
            Location_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class InventoryPalletVirtualApiImportVM : BaseImportVM<InventoryPalletVirtualApiTemplateVM, InventoryPalletVirtual>
    {

    }

}
