
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.SalesManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.SalesManagement.SalesReturnReceivementVMs
{
    public partial class SalesReturnReceivementTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._SalesReturnReceivement._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.CreatePerson);
        [Display(Name = "_Model._SalesReturnReceivement._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.OrganizationId);
        [Display(Name = "_Model._SalesReturnReceivement._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.BusinessDate, true);
        [Display(Name = "_Model._SalesReturnReceivement._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.SubmitDate, true);
        [Display(Name = "_Model._SalesReturnReceivement._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.DocNo);
        [Display(Name = "_Model._SalesReturnReceivement._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.DocType);
        [Display(Name = "_Model._SalesReturnReceivement._Customer")]
        public ExcelPropety Customer_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.CustomerId);
        [Display(Name = "_Model._SalesReturnReceivement._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.Status);
        [Display(Name = "_Model._SalesReturnReceivement._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.Memo);
        [Display(Name = "_Model._SalesReturnReceivement._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.SourceSystemId);
        [Display(Name = "_Model._SalesReturnReceivement._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._SalesReturnReceivement._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.CreateTime, true);
        [Display(Name = "_Model._SalesReturnReceivement._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.UpdateTime, true);
        [Display(Name = "_Model._SalesReturnReceivement._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.CreateBy);
        [Display(Name = "_Model._SalesReturnReceivement._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<SalesReturnReceivement>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Customer_Excel.DataType = ColumnDataType.ComboBox;
            Customer_Excel.ListItems = DC.Set<BaseCustomer>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class SalesReturnReceivementImportVM : BaseImportVM<SalesReturnReceivementTemplateVM, SalesReturnReceivement>
    {
            //import

    }

}