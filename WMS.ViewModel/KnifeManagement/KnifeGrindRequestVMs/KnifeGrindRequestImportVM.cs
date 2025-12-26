
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeGrindRequestVMs
{
    public partial class KnifeGrindRequestTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeGrindRequest._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.DocNo);
        [Display(Name = "_Model._KnifeGrindRequest._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.Status);
        [Display(Name = "_Model._KnifeGrindRequest._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.HandledById);
        [Display(Name = "_Model._KnifeGrindRequest._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeGrindRequest._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeGrindRequest._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeGrindRequest._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeGrindRequest._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeGrindRequest>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            

        }

    }

    public class KnifeGrindRequestImportVM : BaseImportVM<KnifeGrindRequestTemplateVM, KnifeGrindRequest>
    {
            //import

    }

}