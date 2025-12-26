
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.ProductionManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.ProductionManagement.ProductionRcvRptVMs
{
    public partial class ProductionRcvRptTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._ProductionRcvRpt._ErpID")]
        public ExcelPropety ErpID_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.ErpID);
        [Display(Name = "_Model._ProductionRcvRpt._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.OrganizationId);
        [Display(Name = "_Model._ProductionRcvRpt._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.BusinessDate, true);
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
        [Display(Name = "_Model._ProductionRcvRpt._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.CreateTime, true);
        [Display(Name = "_Model._ProductionRcvRpt._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.UpdateTime, true);
        [Display(Name = "_Model._ProductionRcvRpt._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.CreateBy);
        [Display(Name = "_Model._ProductionRcvRpt._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<ProductionRcvRpt>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Wh_Excel.DataType = ColumnDataType.ComboBox;
            Wh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            OrderWh_Excel.DataType = ColumnDataType.ComboBox;
            OrderWh_Excel.ListItems = DC.Set<BaseWareHouse>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class ProductionRcvRptImportVM : BaseImportVM<ProductionRcvRptTemplateVM, ProductionRcvRpt>
    {
            //import

    }

}