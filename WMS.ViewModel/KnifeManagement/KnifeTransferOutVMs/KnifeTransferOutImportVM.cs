
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.KnifeManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.KnifeManagement.KnifeTransferOutVMs
{
    public partial class KnifeTransferOutTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeTransferOut._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.DocNo);
        [Display(Name = "_Model._KnifeTransferOut._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.Status);
        [Display(Name = "_Model._KnifeTransferOut._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.HandledById);
        [Display(Name = "_Model._KnifeTransferOut._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeTransferOut._ToWH")]
        public ExcelPropety ToWH_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.ToWhId);
        [Display(Name = "_Model._KnifeTransferOut._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeTransferOut._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeTransferOut._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeTransferOut._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeTransferOut>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            ToWH_Excel.DataType = ColumnDataType.ComboBox;
            ToWH_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class KnifeTransferOutImportVM : BaseImportVM<KnifeTransferOutTemplateVM, KnifeTransferOut>
    {
            //import

    }

}