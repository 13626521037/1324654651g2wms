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


namespace WMS.ViewModel.KnifeManagement.KnifeVMs
{
    public partial class KnifeApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._Knife._CreatedDate")]
        public ExcelPropety CreatedDate_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CreatedDate);
        [Display(Name = "_Model._Knife._KnifeNo")]
        public ExcelPropety KnifeNo_Excel = ExcelPropety.CreateProperty<Knife>(x => x.SerialNumber);
        [Display(Name = "_Model._Knife._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<Knife>(x => x.Status);
        [Display(Name = "_Model._Knife._CurrentCheckOutBy")]
        public ExcelPropety CurrentCheckOutBy_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CurrentCheckOutBy);
        [Display(Name = "_Model._Knife._LastOperationDate")]
        public ExcelPropety LastOperationDate_Excel = ExcelPropety.CreateProperty<Knife>(x => x.LastOperationDate);
        [Display(Name = "_Model._Knife._WhLocation")]
        public ExcelPropety WhLocation_Excel = ExcelPropety.CreateProperty<Knife>(x => x.WhLocationId);
        [Display(Name = "_Model._Knife._GrindCount")]
        public ExcelPropety GrindCount_Excel = ExcelPropety.CreateProperty<Knife>(x => x.GrindCount);
        [Display(Name = "_Model._Knife._InitialLife")]
        public ExcelPropety InitialLife_Excel = ExcelPropety.CreateProperty<Knife>(x => x.InitialLife);
        [Display(Name = "_Model._Knife._CurrentLife")]
        public ExcelPropety CurrentLife_Excel = ExcelPropety.CreateProperty<Knife>(x => x.CurrentLife);
        [Display(Name = "_Model._Knife._TotalUsedDays")]
        public ExcelPropety TotalUsedDays_Excel = ExcelPropety.CreateProperty<Knife>(x => x.TotalUsedDays);
        [Display(Name = "_Model._Knife._RemainingUsedDays")]
        public ExcelPropety RemainingUsedDays_Excel = ExcelPropety.CreateProperty<Knife>(x => x.RemainingUsedDays);

        [Display(Name = "_Model._Knife._ItemMaster")]
        public ExcelPropety ItemMaster_Excel = ExcelPropety.CreateProperty<Knife>(x => x.ItemMasterId);

	    protected override void InitVM()
        {
            WhLocation_Excel.DataType = ColumnDataType.ComboBox;
            WhLocation_Excel.ListItems = DC.Set<BaseWhLocation>().GetSelectListItems(Wtm, y => y.Name);
            ItemMaster_Excel.DataType = ColumnDataType.ComboBox;
            ItemMaster_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class KnifeApiImportVM : BaseImportVM<KnifeApiTemplateVM, Knife>
    {

    }

}
