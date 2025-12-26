using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._ProductionRcvRpt._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.ErpID);
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.OrganizationId);
        [Display(Name = "_Model._ProductionRcvRpt._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.BusinessDate);
        [Display(Name = "_Model._ProductionRcvRpt._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.DocNo);
        [Display(Name = "_Model._ProductionRcvRpt._Wh")]
        public ExcelPropety Wh_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.WhId);
        [Display(Name = "_Model._ProductionRcvRpt._OrderWh")]
        public ExcelPropety OrderWh_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.OrderWhId);
        [Display(Name = "_Model._ProductionRcvRpt._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.Status);
        [Display(Name = "_Model._ProductionRcvRpt._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.Memo);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.CodeAndName);
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.CodeAndName);
            OrderWh_Excel.DataType = ColumnDataType.ComboBox;
            OrderWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.CodeAndName);
        }

    }

    public class ProductionRcvRptApiImportVM : BaseImportVM<ProductionRcvRptApiTemplateVM, ProductionRcvRpt>
    {

    }

}
