
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSequenceDefineVMs
{
    public partial class BaseSequenceDefineTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseSequenceDefine._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.Code);
        [Display(Name = "_Model._BaseSequenceDefine._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.Name);
        [Display(Name = "_Model._BaseSequenceDefine._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.DocType);
        [Display(Name = "_Model._BaseSequenceDefine._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.IsEffective);
        [Display(Name = "_Model._BaseSequenceDefine._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.Memo);
        [Display(Name = "_Model._BaseSequenceDefine._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseSequenceDefine._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseSequenceDefine._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.CreateBy);
        [Display(Name = "_Model._BaseSequenceDefine._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseSequenceDefine>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class BaseSequenceDefineImportVM : BaseImportVM<BaseSequenceDefineTemplateVM, BaseSequenceDefine>
    {
            //import

    }

}