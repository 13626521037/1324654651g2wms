using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.BaseData;


namespace WMS.ViewModel.BaseData.BaseBarCodeVMs
{
    public partial class BaseBarCodeApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._BaseBarCode._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.DocNo);
        [Display(Name = "_Model._BaseBarCode._Code")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Code);
        [Display(Name = "_Model._BaseBarCode._Item")]
        public ExcelPropety Item_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ItemId);
        [Display(Name = "_Model._BaseBarCode._Qty")]
        public ExcelPropety Qty_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Qty);
        [Display(Name = "_Model._BaseBarCode._CustomerCode")]
        public ExcelPropety CustomerCode_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerCode);
        [Display(Name = "_Model._BaseBarCode._CustomerName")]
        public ExcelPropety CustomerName_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerName);
        [Display(Name = "_Model._BaseBarCode._CustomerNameFirstLetter")]
        public ExcelPropety CustomerNameFirstLetter_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.CustomerNameFirstLetter);
        [Display(Name = "_Model._BaseBarCode._Seiban")]
        public ExcelPropety Seiban_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.Seiban);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields1")]
        public ExcelPropety ExtendedFields1_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields1);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields2")]
        public ExcelPropety ExtendedFields2_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields2);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields3")]
        public ExcelPropety ExtendedFields3_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields3);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields4")]
        public ExcelPropety ExtendedFields4_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields4);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields5")]
        public ExcelPropety ExtendedFields5_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields5);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields6")]
        public ExcelPropety ExtendedFields6_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields6);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields7")]
        public ExcelPropety ExtendedFields7_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields7);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields8")]
        public ExcelPropety ExtendedFields8_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields8);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields9")]
        public ExcelPropety ExtendedFields9_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields9);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields10")]
        public ExcelPropety ExtendedFields10_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields10);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields11")]
        public ExcelPropety ExtendedFields11_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields11);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields12")]
        public ExcelPropety ExtendedFields12_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields12);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields13")]
        public ExcelPropety ExtendedFields13_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields13);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields14")]
        public ExcelPropety ExtendedFields14_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields14);
        [Display(Name = "_Model._BaseBarCode._ExtendedFields15")]
        public ExcelPropety ExtendedFields15_Excel = ExcelPropety.CreateProperty<BaseBarCode>(x => x.ExtendedFields15);

	    protected override void InitVM()
        {
            Item_Excel.DataType = ColumnDataType.ComboBox;
            Item_Excel.ListItems = DC.Set<BaseItemMaster>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BaseBarCodeApiImportVM : BaseImportVM<BaseBarCodeApiTemplateVM, BaseBarCode>
    {

    }

}
