
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

namespace WMS.ViewModel.KnifeManagement.KnifeOperationVMs
{
    public partial class KnifeOperationTemplateVM : BaseTemplateVM
    {

        [Display(Name = "_Model._KnifeOperation._Knife")]
        public ExcelPropety Knife_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.KnifeId);
        [Display(Name = "_Model._KnifeOperation._OperationType")]
        public ExcelPropety OperationType_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.OperationType);
        [Display(Name = "_Model._KnifeOperation._OperationTime")]
        public ExcelPropety OperationTime_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.OperationTime, true);
        [Display(Name = "_Model._KnifeOperation._OperationBy")]
        public ExcelPropety OperationBy_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.OperationById);
        [Display(Name = "_Model._KnifeOperation._HandledBy")]
        public ExcelPropety HandledBy_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.HandledById);
        [Display(Name = "_Model._KnifeOperation._UsedDays")]
        public ExcelPropety UsedDays_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.UsedDays);
        [Display(Name = "_Model._KnifeOperation._RemainingDays")]
        public ExcelPropety RemainingDays_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.RemainingDays);
        [Display(Name = "_Model._KnifeOperation._WhLocation")]
        public ExcelPropety WhLocation_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.WhLocationId);
        [Display(Name = "_Model._KnifeOperation._GrindNum")]
        public ExcelPropety GrindNum_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.GrindNum);
        [Display(Name = "_Model._KnifeOperation._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.DocNo);
        [Display(Name = "_Model._KnifeOperation._U9SourceLineID")]
        public ExcelPropety U9SourceLineID_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.U9SourceLineID);
        [Display(Name = "_Model._KnifeOperation._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.CreateTime, true);
        [Display(Name = "_Model._KnifeOperation._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.UpdateTime, true);
        [Display(Name = "_Model._KnifeOperation._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.CreateBy);
        [Display(Name = "_Model._KnifeOperation._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<KnifeOperation>(x => x.UpdateBy);

        protected override void InitVM()
        {

            Knife_Excel.DataType = ColumnDataType.ComboBox;
            Knife_Excel.ListItems = DC.Set<Knife>().GetSelectListItems(Wtm, y => y.SerialNumber.ToString());
            OperationBy_Excel.DataType = ColumnDataType.ComboBox;
            OperationBy_Excel.ListItems = DC.Set<BaseOperator>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            WhLocation_Excel.DataType = ColumnDataType.ComboBox;
            WhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Code.ToString());

        }

    }

    public class KnifeOperationImportVM : BaseImportVM<KnifeOperationTemplateVM, KnifeOperation>
    {
        //import

    }

}