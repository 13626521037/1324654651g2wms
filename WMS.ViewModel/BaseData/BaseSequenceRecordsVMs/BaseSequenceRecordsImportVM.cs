
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceRecordsVMs
{
    public partial class BaseSequenceRecordsTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseSequenceRecords._SequenceDefine")]
        public ExcelPropety SequenceDefine_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.SequenceDefineId);
        [Display(Name = "_Model._BaseSequenceRecords._SegmentFlag")]
        public ExcelPropety SegmentFlag_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.SegmentFlag);
        [Display(Name = "_Model._BaseSequenceRecords._SerialValue")]
        public ExcelPropety SerialValue_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.SerialValue);
        [Display(Name = "_Model._BaseSequenceRecords._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseSequenceRecords._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseSequenceRecords._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.CreateBy);
        [Display(Name = "_Model._BaseSequenceRecords._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseSequenceRecords>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            SequenceDefine_Excel.DataType = ColumnDataType.ComboBox;
            SequenceDefine_Excel.ListItems = DC.Set<BaseSequenceDefine>().GetSelectListItems(Wtm, y => y.Code.ToString());

        }

    }

    public class BaseSequenceRecordsImportVM : BaseImportVM<BaseSequenceRecordsTemplateVM, BaseSequenceRecords>
    {
            //import

    }

}