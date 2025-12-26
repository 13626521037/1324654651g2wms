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


namespace WMS.ViewModel.ProductionManagement.ProductionReturnIssueVMs
{
    public partial class ProductionReturnIssueApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._ProductionReturnIssue._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.CreatePerson);
        [Display(Name = "_Model._ProductionReturnIssue._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.OrganizationId);
        [Display(Name = "_Model._ProductionReturnIssue._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.BusinessDate);
        [Display(Name = "_Model._ProductionReturnIssue._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.SubmitDate);
        [Display(Name = "_Model._ProductionReturnIssue._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.DocNo);
        [Display(Name = "_Model._ProductionReturnIssue._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.DocType);
        [Display(Name = "_Model._ProductionReturnIssue._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.Status);
        [Display(Name = "_Model._ProductionReturnIssue._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.Memo);
        [Display(Name = "来源系统主键")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.SourceSystemId);
        [Display(Name = "最后修改时间")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<ProductionReturnIssue>(x => x.LastUpdateTime);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.CodeAndName);
        }

    }

    public class ProductionReturnIssueApiImportVM : BaseImportVM<ProductionReturnIssueApiTemplateVM, ProductionReturnIssue>
    {

    }

}
