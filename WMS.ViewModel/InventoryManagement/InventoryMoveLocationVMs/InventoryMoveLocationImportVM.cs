
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

namespace WMS.ViewModel.InventoryManagement.InventoryMoveLocationVMs
{
    public partial class InventoryMoveLocationTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._InventoryMoveLocation._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.DocNo);
        [Display(Name = "_Model._InventoryMoveLocation._InWhLocation")]
        public ExcelPropety InWhLocation_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.InWhLocationId);
        [Display(Name = "_Model._InventoryMoveLocation._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.Memo);
        [Display(Name = "_Model._InventoryMoveLocation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.CreateTime, true);
        [Display(Name = "_Model._InventoryMoveLocation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._InventoryMoveLocation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.CreateBy);
        [Display(Name = "_Model._InventoryMoveLocation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<InventoryMoveLocation>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            InWhLocation_Excel.DataType = ColumnDataType.ComboBox;
            InWhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Code.ToString());

        }

    }

    public class InventoryMoveLocationImportVM : BaseImportVM<InventoryMoveLocationTemplateVM, InventoryMoveLocation>
    {
            //import

    }

}