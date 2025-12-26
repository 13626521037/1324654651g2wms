
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeCombineVMs
{
    public partial class KnifeCombineTemplateVM : BaseTemplateVM
    {

        [Display(Name = "_Model._KnifeCombine._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.DocNo);
        [Display(Name = "_Model._KnifeCombine._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.HandledById);
        [Display(Name = "_Model._KnifeCombine._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.Status);
        [Display(Name = "_Model._KnifeCombine._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeCombine._CloseTime")]
        public ExcelPropety CloseTime_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.CloseTime, true);
        [Display(Name = "_Model._KnifeCombine._CombineKnifeNo")]
        public ExcelPropety CombineKnifeNo_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.CombineKnifeNo);
        [Display(Name = "_Model._KnifeCombine._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeCombine._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeCombine._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeCombine._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeCombine>(x => x.UpdateBy);

        protected override void InitVM()
        {


        }

    }

    public class KnifeCombineImportVM : BaseImportVM<KnifeCombineTemplateVM, KnifeCombine>
    {
        //import

    }

}