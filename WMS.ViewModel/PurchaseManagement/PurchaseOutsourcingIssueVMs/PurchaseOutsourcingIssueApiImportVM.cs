using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model.BaseData;
using WMS.Model;


namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.CreatePerson);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.OrganizationId);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.BusinessDate);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.SubmitDate);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.DocNo);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.DocType);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Supplier")]
        public ExcelPropety Supplier_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.SupplierId);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.Status);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.Memo);
        [Display(Name = "来源系统主键")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.SourceSystemId);
        [Display(Name = "最后修改时间")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.LastUpdateTime);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.Name);
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class PurchaseOutsourcingIssueApiImportVM : BaseImportVM<PurchaseOutsourcingIssueApiTemplateVM, PurchaseOutsourcingIssue>
    {

    }

}
