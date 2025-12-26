
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseAnalysisTypeVMs
{
    public partial class BaseAnalysisTypeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseAnalysisType._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.Code);
        [Display(Name = "_Model._BaseAnalysisType._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.Name);
        [Display(Name = "_Model._BaseAnalysisType._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseAnalysisType._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseAnalysisType._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseAnalysisType._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseAnalysisType._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.CreateBy);
        [Display(Name = "_Model._BaseAnalysisType._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseAnalysisType._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseAnalysisType>(x => x.IsValid);

	    protected override void InitVM()
        {
            
        }

    }

    public class BaseAnalysisTypeImportVM : BaseImportVM<BaseAnalysisTypeTemplateVM, BaseAnalysisType>
    {
            //import

    }

}