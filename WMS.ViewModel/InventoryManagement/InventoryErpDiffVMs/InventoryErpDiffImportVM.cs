
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

namespace WMS.ViewModel.InventoryManagement.InventoryErpDiffVMs
{
    public partial class InventoryErpDiffTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryErpDiff._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.WhId);
        [Display(Name = "_Model._InventoryErpDiff._Item")]
        public ExcelPropety Item_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.ItemId);
        [Display(Name = "_Model._InventoryErpDiff._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.Seiban);
        [Display(Name = "_Model._InventoryErpDiff._WmsQty")]
        public ExcelPropety WmsQty_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.WmsQty);
        [Display(Name = "_Model._InventoryErpDiff._ErpQty")]
        public ExcelPropety ErpQty_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.ErpQty);
        [Display(Name = "_Model._InventoryErpDiff._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryErpDiff._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryErpDiff._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryErpDiff._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryErpDiff>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Item_Excel.DataType = ColumnDataType.ComboBox;
            Item_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class InventoryErpDiffImportVM : BaseImportVM<InventoryErpDiffTemplateVM, InventoryErpDiff>
    {
            //import

    }

}