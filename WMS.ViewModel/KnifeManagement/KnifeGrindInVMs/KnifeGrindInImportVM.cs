
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindInVMs
{
    public partial class KnifeGrindInTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeGrindIn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.DocNo);
        [Display(Name = "_Model._KnifeGrindIn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.Status);
        [Display(Name = "_Model._KnifeGrindIn._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.HandledById);
        [Display(Name = "_Model._KnifeGrindIn._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeGrindIn._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeGrindIn._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeGrindIn._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeGrindIn._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindIn>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            

        }

    }

    public class KnifeGrindInImportVM : BaseImportVM<KnifeGrindInTemplateVM, KnifeGrindIn>
    {
            //import

    }

}