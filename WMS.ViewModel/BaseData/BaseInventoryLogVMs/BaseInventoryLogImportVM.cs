
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseInventoryLogVMs
{
    public partial class BaseInventoryLogTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseInventoryLog._OperationType")]
        public ExcelPropety OperationType_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.OperationType);
        [Display(Name = "_Model._BaseInventoryLog._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.DocNo);
        [Display(Name = "_Model._BaseInventoryLog._SourceInventory")]
        public ExcelPropety SourceInventory_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.SourceInventoryId);
        [Display(Name = "_Model._BaseInventoryLog._TargetInventory")]
        public ExcelPropety TargetInventory_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.TargetInventoryId);
        [Display(Name = "来源数量变化")]
        public ExcelPropety SourceDiffQty_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.SourceDiffQty);
        [Display(Name = "目标数量变化")]
        public ExcelPropety TargetDiffQty_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.TargetDiffQty);
        [Display(Name = "_Model._BaseInventoryLog._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.Memo);
        [Display(Name = "_Model._BaseInventoryLog._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseInventoryLog._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseInventoryLog._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.CreateBy);
        [Display(Name = "_Model._BaseInventoryLog._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseInventoryLog>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            SourceInventory_Excel.DataType = ColumnDataType.ComboBox;
            SourceInventory_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());
            TargetInventory_Excel.DataType = ColumnDataType.ComboBox;
            TargetInventory_Excel.ListItems = DC.Set<BaseInventory>().GetSelectListItems(Wtm, y => y.BatchNumber.ToString());

        }

    }

    public class BaseInventoryLogImportVM : BaseImportVM<BaseInventoryLogTemplateVM, BaseInventoryLog>
    {
            //import

    }

}