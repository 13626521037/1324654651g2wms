
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseOperatorVMs
{
    public partial class BaseOperatorTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseOperator._JobID")]
        public ExcelPropety JobID_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.JobID);
        [Display(Name = "_Model._BaseOperator._OAAccount")]
        public ExcelPropety OAAccount_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.OAAccount);
        [Display(Name = "_Model._BaseOperator._IDCard")]
        public ExcelPropety IDCard_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.IDCard);
        [Display(Name = "_Model._BaseOperator._TempAuthCode")]
        public ExcelPropety TempAuthCode_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.TempAuthCode);
        [Display(Name = "_Model._BaseOperator._TACExpired")]
        public ExcelPropety TACExpired_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.TACExpired, true);
        [Display(Name = "_Model._BaseOperator._Department")]
        public ExcelPropety Department_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.DepartmentId);
        [Display(Name = "_Model._BaseOperator._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.IsEffective);
        [Display(Name = "_Model._BaseOperator._Phone")]
        public ExcelPropety Phone_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.Phone);
        [Display(Name = "_Model._BaseOperator._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.Memo);
        [Display(Name = "_Model._BaseOperator._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseOperator._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseOperator._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseOperator._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseOperator._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.CreateBy);
        [Display(Name = "_Model._BaseOperator._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseOperator._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseOperator>(x => x.IsValid);

	    protected override void InitVM()
        {
            
            Department_Excel.DataType = ColumnDataType.ComboBox;
            Department_Excel.ListItems = DC.Set<BaseDepartment>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class BaseOperatorImportVM : BaseImportVM<BaseOperatorTemplateVM, BaseOperator>
    {
            //import

    }

}