
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferInVMs
{
    public partial class KnifeTransferInTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeTransferIn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.DocNo);
        [Display(Name = "_Model._KnifeTransferIn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.Status);
        [Display(Name = "_Model._KnifeTransferIn._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeTransferIn._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.HandledById);
        [Display(Name = "_Model._KnifeTransferIn._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.Memo);
        [Display(Name = "_Model._KnifeTransferIn._TransferOutDocNo")]
        public ExcelPropety TransferOutDocNo_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.TransferOutDocNo);
        [Display(Name = "_Model._KnifeTransferIn._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeTransferIn._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeTransferIn._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeTransferIn._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeTransferIn>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            

        }

    }

    public class KnifeTransferInImportVM : BaseImportVM<KnifeTransferInTemplateVM, KnifeTransferIn>
    {
            //import

    }

}