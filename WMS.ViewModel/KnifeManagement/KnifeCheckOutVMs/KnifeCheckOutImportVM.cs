
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

namespace WMS.ViewModel.KnifeManagement.KnifeCheckOutVMs
{
    public partial class KnifeCheckOutTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._KnifeCheckOut._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.DocType);
        [Display(Name = "_Model._KnifeCheckOut._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.DocNo);
        [Display(Name = "_Model._KnifeCheckOut._CheckOutBy")]
        public ExcelPropety CheckOutBy_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.CheckOutById);
        [Display(Name = "_Model._KnifeCheckOut._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.HandledById);
        [Display(Name = "_Model._KnifeCheckOut._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.Status);
        [Display(Name = "_Model._KnifeCheckOut._ApprovedTime")]
        public ExcelPropety ApprovedTime_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.ApprovedTime, true);
        [Display(Name = "_Model._KnifeCheckOut._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeCheckOut._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeCheckOut._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeCheckOut._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeCheckOut>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            CheckOutBy_Excel.DataType = ColumnDataType.ComboBox;
            CheckOutBy_Excel.ListItems = DC.Set<BaseOperator>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class KnifeCheckOutImportVM : BaseImportVM<KnifeCheckOutTemplateVM, KnifeCheckOut>
    {
            //import

    }

}