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


namespace WMS.ViewModel.ProductionManagement.ProductionIssueVMs
{
    public partial class ProductionIssueApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "_Model._ProductionIssue._CreatePerson")]
        public ExcelPropety CreatePerson_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.CreatePerson);
        [Display(Name = "_Model._ProductionIssue._Organization")]
        public ExcelPropety Organization_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.OrganizationId);
        [Display(Name = "_Model._ProductionIssue._BusinessDate")]
        public ExcelPropety BusinessDate_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.BusinessDate);
        [Display(Name = "_Model._ProductionIssue._SubmitDate")]
        public ExcelPropety SubmitDate_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.SubmitDate);
        [Display(Name = "_Model._ProductionIssue._DocNo")]
        public ExcelPropety DocNo_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.DocNo);
        [Display(Name = "_Model._ProductionIssue._DocType")]
        public ExcelPropety DocType_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.DocType);
        [Display(Name = "_Model._ProductionIssue._Status")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.Status);
        [Display(Name = "_Model._ProductionIssue._Memo")]
        public ExcelPropety Memo_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.Memo);
        [Display(Name = "来源系统主键")]
        public ExcelPropety SourceSystemId_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.SourceSystemId);
        [Display(Name = "最后修改时间")]
        public ExcelPropety LastUpdateTime_Excel = ExcelPropety.CreateProperty<ProductionIssue>(x => x.LastUpdateTime);

	    protected override void InitVM()
        {
            Organization_Excel.DataType = ColumnDataType.ComboBox;
            Organization_Excel.ListItems = DC.Set<BaseOrganization>().GetSelectListItems(Wtm, y => y.CodeAndName);
        }

    }

    public class ProductionIssueApiImportVM : BaseImportVM<ProductionIssueApiTemplateVM, ProductionIssue>
    {

    }

}
