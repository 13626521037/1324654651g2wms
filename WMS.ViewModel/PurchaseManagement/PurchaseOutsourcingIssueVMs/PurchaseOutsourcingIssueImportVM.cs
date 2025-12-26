
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WMS.Model.PurchaseManagement;
using WMS.Model;
using WMS.Model.BaseData;

namespace WMS.ViewModel.PurchaseManagement.PurchaseOutsourcingIssueVMs
{
    public partial class PurchaseOutsourcingIssueTemplateVM : BaseTemplateVM
    {
        
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.CreatePerson);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.OrganizationId);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.BusinessDate, true);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.SubmitDate, true);
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
        [Display(Name = "_Model._PurchaseOutsourcingIssue._SourceSystemId")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.SourceSystemId);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._LastUpdateTime")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.LastUpdateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreateTime")]
        public ExcelPropety CreateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.CreateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._UpdateTime")]
        public ExcelPropety UpdateTime_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.UpdateTime, true);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._CreateBy")]
        public ExcelPropety CreateBy_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.CreateBy);
        [Display(Name = "_Model._PurchaseOutsourcingIssue._UpdateBy")]
        public ExcelPropety UpdateBy_Excel = ExcelPropety.CreateProperty<PurchaseOutsourcingIssue>(x => x.UpdateBy);

	    protected override void InitVM()
        {
            
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());
            Supplier_Excel.DataType = ColumnDataType.ComboBox;
            Supplier_Excel.ListItems = DC.Set<BaseSupplier>().GetSelectListItems(Wtm, y => y.SourceSystemId.ToString());

        }

    }

    public class PurchaseOutsourcingIssueImportVM : BaseImportVM<PurchaseOutsourcingIssueTemplateVM, PurchaseOutsourcingIssue>
    {
            //import

    }

}