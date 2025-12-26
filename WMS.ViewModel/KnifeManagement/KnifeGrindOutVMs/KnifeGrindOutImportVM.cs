
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindOutVMs
{
    public partial class KnifeGrindOutTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeGrindOut._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.DocNo);
        [Display(Name = "_Model._KnifeGrindOut._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.HandledById);
        [Display(Name = "_Model._KnifeGrindOut._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.Status);
        [Display(Name = "_Model._KnifeGrindOut._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeGrindOut._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeGrindOut._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeGrindOut._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeGrindOut._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindOut>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            

        }

    }

    public class KnifeGrindOutImportVM : BaseImportVM<KnifeGrindOutTemplateVM, KnifeGrindOut>
    {
            //import

    }

}