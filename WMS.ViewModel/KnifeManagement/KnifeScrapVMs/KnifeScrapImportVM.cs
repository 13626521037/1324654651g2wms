
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeScrapVMs
{
    public partial class KnifeScrapTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeScrap._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.DocNo);
        [Display(Name = "_Model._KnifeScrap._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.DocType);
        [Display(Name = "_Model._KnifeScrap._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.Status);
        [Display(Name = "_Model._KnifeScrap._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.HandledById);
        [Display(Name = "_Model._KnifeScrap._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeScrap._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeScrap._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeScrap._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeScrap._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeScrap>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            

        }

    }

    public class KnifeScrapImportVM : BaseImportVM<KnifeScrapTemplateVM, KnifeScrap>
    {
            //import

    }

}