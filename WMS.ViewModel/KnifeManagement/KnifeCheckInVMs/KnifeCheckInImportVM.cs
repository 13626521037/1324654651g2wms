
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

namespace WMS.ViewModel.KnifeManagement.KnifeCheckInVMs
{
    public partial class KnifeCheckInTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeCheckIn._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.DocType);
        [Display(Name = "_Model._KnifeCheckIn._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.DocNo);
        [Display(Name = "_Model._KnifeCheckIn._CheckInBy")]
        public ExcelPropety CheckInBy_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.CheckInById);
        [Display(Name = "_Model._KnifeCheckIn._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.HandledById);
        [Display(Name = "_Model._KnifeCheckIn._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.Status);
        [Display(Name = "_Model._KnifeCheckIn._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeCheckIn._CombineKnifeNo")]
        public ExcelPropety CombineKnifeNo_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.CombineKnifeNo);
        [Display(Name = "_Model._KnifeCheckIn._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeCheckIn._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeCheckIn._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeCheckIn._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeCheckIn>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            CheckInBy_Excel.DataType = ColumnDataType.ComboBox;
            CheckInBy_Excel.ListItems = DC.Set<BaseOperator>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class KnifeCheckInImportVM : BaseImportVM<KnifeCheckInTemplateVM, KnifeCheckIn>
    {
            //import

    }

}