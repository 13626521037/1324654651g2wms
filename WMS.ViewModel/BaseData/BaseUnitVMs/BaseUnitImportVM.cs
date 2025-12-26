
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseUnitVMs
{
    public partial class BaseUnitTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseUnit._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.Code);
        [Display(Name = "_Model._BaseUnit._Name")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.Name);
        [Display(Name = "_Model._BaseUnit._IsEffective")]
        public ExcelPropety IsEffective_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.IsEffective);
        [Display(Name = "_Model._BaseUnit._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.Memo);
        [Display(Name = "_Model._BaseUnit._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.SourceSystemId);
        [Display(Name = "_Model._BaseUnit._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._BaseUnit._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseUnit._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseUnit._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.CreateBy);
        [Display(Name = "_Model._BaseUnit._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.UpdateBy);
        [Display(Name = "_Model._BaseUnit._IsValid")]
        public ExcelPropety IsValid_Excel = ExcelPropety.CreateProperty<BaseUnit>(x => x.IsValid);

	    protected override void InitVM()
        {
            
        }

    }

    public class BaseUnitImportVM : BaseImportVM<BaseUnitTemplateVM, BaseUnit>
    {
            //import

    }

}