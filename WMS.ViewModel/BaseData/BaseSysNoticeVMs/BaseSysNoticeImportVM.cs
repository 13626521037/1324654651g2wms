
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;
using WMS.Model;

namespace WMS.ViewModel.BaseData.BaseSysNoticeVMs
{
    public partial class BaseSysNoticeTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._BaseSysNotice._Title")]
        public ExcelPropety Title_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.Title);
        [Display(Name = "_Model._BaseSysNotice._Content")]
        public ExcelPropety Content_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.Content);
        [Display(Name = "_Model._BaseSysNotice._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.CreateTime, true);
        [Display(Name = "_Model._BaseSysNotice._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.UpdateTime, true);
        [Display(Name = "_Model._BaseSysNotice._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.CreateBy);
        [Display(Name = "_Model._BaseSysNotice._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<BaseSysNotice>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
        }

    }

    public class BaseSysNoticeImportVM : BaseImportVM<BaseSysNoticeTemplateVM, BaseSysNotice>
    {
            //import

    }

}