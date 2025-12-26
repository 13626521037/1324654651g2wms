using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseSysParaVMs
{
    public partial class BaseSysParaTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseSysPara>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BaseSysPara>(x => x.Name);
        [Display(Name = "参数值")]
        public ExcelPropety Value_Excel = ExcelPropety.CreateProperty<BaseSysPara>(x => x.Value);
        [Display(Name = "备注")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<BaseSysPara>(x => x.Memo);

	    protected override void InitVM()
        {
        }

    }

    public class BaseSysParaImportVM : BaseImportVM<BaseSysParaTemplateVM, BaseSysPara>
    {

    }

}
